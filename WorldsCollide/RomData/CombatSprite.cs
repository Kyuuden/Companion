using FF.Rando.Companion.Extensions;
using FF.Rando.Companion.Rendering;
using KGySoft.Drawing.Imaging;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace FF.Rando.Companion.WorldsCollide.RomData;

public class CombatSprite : Sprite
{
    private readonly int _tileCount;
    private readonly byte[] _stencil;
    private readonly List<byte[,]> _tiles;

    public CombatSprite(
        CombatSpriteTemplate template,
        ReadOnlySpan<byte> tileData,
        ReadOnlySpan<byte> paletteData,
        ReadOnlySpan<byte> stencilData)
        : base(paletteData.Slice(template.PaletteOffset, template.PaletteSize).DecodePalette(new Color32()))
    {
        _stencil = stencilData.Slice(template.StencilOffset, template.StencilSize).ToArray();
        _tileCount = _stencil.CountBits();
        _tiles = tileData
            .Slice(template.TileOffset, _tileCount * template.TileSize)
            .ToArray()
            .ReadMany<byte[]>(0, (uint)template.TileSize * 8, _tileCount)
            .Select(data => data.DecodeTile(template.BitsPerPixel))
            .ToList();
    }

    protected override IReadableBitmapData RenderColorData()
    {
        IReadWriteBitmapData? data = null;

        if (_stencil.Length == 8)
        {
            var width = _stencil.Select(HoritonzalTileCount).Max();
            var height = 8;
            foreach (byte b in _stencil.Reverse())
            {
                if (b != 0x00)
                    break;
                height--; ;
            }

            data = BitmapDataFactory.CreateBitmapData(new Size(width * 8, height * 8), KnownPixelFormat.Format8bppIndexed, Palette);

            var tileIndex = 0;
            
            for(var stencilIndex = 0; stencilIndex < _stencil.Length; stencilIndex++)
            {
                var stencilRow = _stencil[stencilIndex];
                for (var stencilBit = 7; stencilBit >= 0; stencilBit--)
                {
                    if (((stencilRow >> stencilBit) & 0x01) == 0x01)
                        _tiles[tileIndex++].DrawInto(data, new Point((7 - stencilBit) * 8, stencilIndex * 8));
                }
            }
        }
        //        else if (_stencil.Length == 32) 
        //        {
        // TODO
        //        }
        else
        {
            throw new NotSupportedException();
        }

        return data;
    }

    private int HoritonzalTileCount(byte stencilByte)
    {
        return stencilByte & 0x0F switch
        {
            0x0 => stencilByte >> 4 switch
            {
                0x0 => 0,
                0x2 => 3,
                0x4 => 2,
                0x6 => 3,
                0x8 => 1,
                0xA => 3,
                0xC => 2,
                0xE => 3,
                _ => 4
            },
            0x2 => 7,
            0x4 => 6,
            0x6 => 7,
            0x8 => 5,
            0xA => 7,
            0xC => 6,
            0xE => 7,
            _ => 8
        };
    }
}
