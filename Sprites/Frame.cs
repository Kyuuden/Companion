using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizHawk.FreeEnterprise.Companion.Sprites
{
    public class Frame
    {
        public Frame(int frameDelay, byte[,] spriteIndexes, sbyte?[,]? sticker = null)
        {
            FrameDelay = frameDelay;
            IndexArray = spriteIndexes;
            StickerArray = sticker;
        }

        public int FrameDelay { get; }

        public byte[,] IndexArray { get; }

        public sbyte?[,]? StickerArray { get; }

        public void Render(Bitmap destination, Func<int, byte[,]> tileLookup, List<Color> tilePalette, Func<int, byte[,]> stickerLookup, List<Color> stickerPalette)
        {
            for (var y = 0; y < destination.Height; y++)
            {
                for (var x = 0; x < destination.Width; x++)
                {
                    var tileIndex = IndexArray[y / 8, x / 8];
                    if (tileIndex != 255)
                        destination.SetPixel(x, y, tilePalette[tileLookup(tileIndex)[x % 8, y % 8]]);

                    var stickerIndex = StickerArray?[y / 8, x / 8];
                    if (stickerIndex.HasValue)
                    {
                        var color = Color.Transparent;
                        if (stickerIndex > 0)
                            color=  stickerPalette[stickerLookup(stickerIndex.Value)[x % 8, y % 8]];
                        if (stickerIndex < 0)
                            color = stickerPalette[stickerLookup(Math.Abs(stickerIndex.Value))[7 - x % 8, y % 8]];

                        if (color != Color.Transparent)
                            destination.SetPixel(x, y, color);
                    }
                }
            }
        }
    }
}
