using FF.Rando.Companion.Extensions;
using FF.Rando.Companion.Rendering;
using KGySoft.Drawing.Imaging;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace FF.Rando.Companion.Games.WorldsCollide.RomData;

public class PortraitSprite : Sprite
{
    private readonly List<byte[,]> _tiles = [];
    private static readonly byte[] _tileOrder = [0, 1, 2, 3, 8, 16, 17, 18, 19, 9, 4, 5, 6, 7, 10, 20, 21, 22, 23, 11, 13, 14, 15, 24, 12];
    private static readonly Size _size = new(40, 40);

    public PortraitSprite(ReadOnlySpan<byte> tileData, Palette palette) : base(palette)
    {
        var start = 0;
        var length = 0x20;

        for (var i = 0; i < 25; i++)
        {
            _tiles.Add(tileData.Slice(start + length * i, length).ToArray().DecodeTile(4));
        }
    }

    public override Size Size => _size;

    protected override IReadableBitmapData RenderColorData()
    {
        IReadWriteBitmapData? data = BitmapDataFactory.CreateBitmapData(Size, KnownPixelFormat.Format8bppIndexed, Palette);

        for (var i = 0; i < _tileOrder.Length; i++)
        {
            _tiles[_tileOrder[i]].DrawInto(data, new Point(i % 5 * 8, i / 5 * 8));
        }

        return data;
    }
}
