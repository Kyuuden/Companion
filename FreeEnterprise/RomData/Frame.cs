using KGySoft.Drawing.Imaging;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace FF.Rando.Companion.FreeEnterprise.RomData;

public class Frame(int frameDelay, byte?[,] spriteIndexes, sbyte?[,]? sticker = null)
{
    public int FrameDelay { get; } = frameDelay;

    public byte?[,] IndexArray { get; } = spriteIndexes;

    public sbyte?[,]? StickerArray { get; } = sticker;

    public IReadableBitmapData Render(Func<int, byte[,]> tileLookup, Palette tilePalette, Func<int, byte[,]> stickerLookup, Palette stickerPalette)
    {
        var combined = new List<Color32>();
        for(int i = 0; i < tilePalette.Count; i ++)
            combined.Add(tilePalette[i]);
        for(int i = 0;i < stickerPalette.Count; i ++)
            combined.Add(stickerPalette[i]);
        int width = IndexArray.GetLength(1) * 8;
        int height = IndexArray.GetLength(0) * 8;

        var xOffset = 0;
        int yOffset = 0;

        if (height > width)
        {
            xOffset = (height - width) / 2;
        }
        else if (width > height)
        {
            yOffset = (width - height) / 2;
        }

        var data = BitmapDataFactory.CreateBitmapData(new Size(Math.Max(width, height), Math.Max(width, height)), KnownPixelFormat.Format8bppIndexed, new Palette(combined));

        for (var y = 0; y < height; y++)
        {
            for (var x = 0; x < width; x++)
            {
                var tileIndex = IndexArray[y / 8, x / 8];
                if (tileIndex.HasValue)
                    data.SetColorIndex(x + xOffset, y + yOffset, tileLookup(tileIndex.Value)[x % 8, y % 8]);

                var stickerIndex = StickerArray?[y / 8, x / 8];
                if (stickerIndex.HasValue)
                {
                    byte? color = null;
                    if (stickerIndex > 0)
                        color = stickerLookup(stickerIndex.Value)[x % 8, y % 8];
                    if (stickerIndex < 0)
                        color = stickerLookup(Math.Abs(stickerIndex.Value))[7 - x % 8, y % 8];

                    if (color.HasValue && color.Value != 0)
                        data.SetColorIndex(x + xOffset, y + yOffset, color.Value + tilePalette.Count);
                }
            }
        }

        return data;
    }
}
