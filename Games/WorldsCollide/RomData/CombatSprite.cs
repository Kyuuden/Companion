using FF.Rando.Companion.Extensions;
using FF.Rando.Companion.Games.WorldsCollide.Enums;
using FF.Rando.Companion.Rendering;
using KGySoft.Drawing.Imaging;
using KGySoft.Drawing.Shapes;
using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;

namespace FF.Rando.Companion.Games.WorldsCollide.RomData;

public class CombatSprite : Sprite
{
    private readonly int _tileCount;
    private readonly byte[] _stencil;
    private readonly List<byte[,]> _tiles;
    private readonly int _id;

    public CombatSprite(
        int id,
        CombatSpriteTemplate template,
        ReadOnlySpan<byte> tileData,
        ReadOnlySpan<byte> paletteData,
        ReadOnlySpan<byte> stencilData)
        : base(paletteData.Slice(template.PaletteOffset, template.PaletteSize).DecodePalette(new Color32()))
    {
        _id = id;
        _stencil = stencilData.Slice(template.StencilOffset, template.StencilSize).ToArray();
        _tileCount = _stencil.CountBits();
        _tiles = tileData
            .Slice(template.TileOffset, _tileCount * template.TileSize)
            .ToArray()
            .ReadMany<byte[]>(0, (uint)template.TileSize * 8, _tileCount)
            .Select(data => data.DecodeTile(template.BitsPerPixel))
            .ToList();
    }

    private bool SmallStencil => _stencil.Length == 8;

    protected override IReadableBitmapData RenderColorData()
    {
        var data = SmallStencil
            ? RenderSmallStencil(_stencil)
            : RenderLargeStencil(Enumerable.Range(0, 16).Select(i => BinaryPrimitives.ReadUInt16BigEndian(new ReadOnlySpan<byte>(_stencil, i * 2, 2))).ToArray());

        switch (_id)
        {
            case (int)Esper.Ifrit:
                var ifrit = BitmapDataFactory.CreateBitmapData(new Size(32, 56), KnownPixelFormat.Format8bppIndexed, Palette);
                data.CopyTo(ifrit, new Rectangle(96, 0, 32, 56), new Point(0, 0));
                data.Dispose();
                return ifrit;
            case (int)Esper.Tritoch:
                var tritoch = BitmapDataFactory.CreateBitmapData(new Size(75, 96), KnownPixelFormat.Format8bppIndexed, Palette);
                data.CopyTo(tritoch, new Rectangle(0, 0, 75, 80), new Point(0, 0));
                data.CopyTo(tritoch, new Rectangle(80, 0, 36, 16), new Point(16, 80));
                data.Dispose();
                return tritoch;
            case (int)Esper.Bahamut:
                var bahamut = BitmapDataFactory.CreateBitmapData(new Size(96, 128), KnownPixelFormat.Format8bppIndexed, Palette);
                data.CopyTo(bahamut, new Rectangle(0, 0, 96, 96), new Point(0, 0));
                data.CopyTo(bahamut, new Rectangle(96, 0, 32, 32), new Point(0, 96));
                data.CopyTo(bahamut, new Rectangle(96, 32, 32, 32), new Point(32, 96));
                data.CopyTo(bahamut, new Rectangle(96, 64, 32, 32), new Point(64, 96));
                data.Dispose();
                return bahamut;
            case (int)Esper.Alexandr:
                var alexandr = BitmapDataFactory.CreateBitmapData(new Size(88, 128), KnownPixelFormat.Format8bppIndexed, Palette);
                data.CopyTo(alexandr, new Rectangle(0, 0, 80, 96), new Point(0, 0));
                data.CopyTo(alexandr, new Rectangle(80, 32, 8, 64), new Point(80, 32));
                data.CopyTo(alexandr, new Rectangle(80, 0, 48, 32), new Point(0, 96));
                data.CopyTo(alexandr, new Rectangle(96, 32, 32, 32), new Point(48, 96));
                data.CopyTo(alexandr, new Rectangle(96, 64, 32, 32), new Point(80, 96));
                data.Dispose();
                return alexandr;
            case (int)Esper.Crusader:
                var crusader = BitmapDataFactory.CreateBitmapData(new Size(48, 56), KnownPixelFormat.Format8bppIndexed, Palette);
                data.CopyTo(crusader, new Rectangle(0, 0, 48, 32), new Point(0, 0));
                data.CopyTo(crusader, new Rectangle(64, 0, 48, 24), new Point(0, 32));
                data.Dispose();
                return crusader;
            case (int)Esper.Starlet:
                var starlet = BitmapDataFactory.CreateBitmapData(new Size(63, 88), KnownPixelFormat.Format8bppIndexed, Palette);
                data.CopyTo(starlet, new Rectangle(0, 0, 63, 80), new Point(0, 0));
                data.CopyTo(starlet, new Rectangle(80, 0, 63, 8), new Point(16, 80));
                data.Dispose();
                return starlet;
        }

        return data;
    }

    private IReadableBitmapData RenderSmallStencil(byte[] stencil)
    {
        using var data = BitmapDataFactory.CreateBitmapData(new Size(64, 64), KnownPixelFormat.Format8bppIndexed, Palette);

        var tileIndex = 0;
        var width = 0;
        var height = 0;

        for (var stencilIndex = 0; stencilIndex < stencil.Length; stencilIndex++)
        {
            var stencilRow = stencil[stencilIndex];
            for (var stencilBit = 7; stencilBit >= 0; stencilBit--)
            {
                if ((stencilRow >> stencilBit & 0x01) == 0x01)
                {
                    var dest = new Point((7 - stencilBit) * 8, stencilIndex * 8);
                    width = Math.Max(width, dest.X + 8);
                    height = Math.Max(height, dest.Y + 8);
                    _tiles[tileIndex++].DrawInto(data, dest);
                }
            }
        }

        return data.Clone(new Rectangle(0, 0, width, height));
    }

    private IReadableBitmapData RenderLargeStencil(ushort[] stencil)
    {
        using var data = BitmapDataFactory.CreateBitmapData(new Size(128, 128), KnownPixelFormat.Format8bppIndexed, Palette);

        var tileIndex = 0;
        var width = 0;
        var height = 0;

        for (var stencilIndex = 0; stencilIndex < stencil.Length; stencilIndex++)
        {
            var stencilRow = stencil[stencilIndex];
            for (var stencilBit = 15; stencilBit >= 0; stencilBit--)
            {
                if ((stencilRow >> stencilBit & 0x01) == 0x01)
                {
                    var dest = new Point((15 - stencilBit) * 8, stencilIndex * 8);
                    width = Math.Max(width, dest.X + 8);
                    height = Math.Max(height, dest.Y + 8);
                    _tiles[tileIndex++].DrawInto(data, dest);
                }
            }
        }

        return data.Clone(new Rectangle(0, 0, width, height));
    }
}
