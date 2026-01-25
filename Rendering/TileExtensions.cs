using KGySoft.Drawing.Imaging;
using System.Drawing;

namespace FF.Rando.Companion.Rendering;

public static class TileExtensions
{
    public static void DrawInto(this byte[,] tile, IWritableBitmapData data, Point point, bool flipHorizontal = false, bool flipVertical = false)
    {
        tile.DrawInto(data, point.X, point.Y, flipHorizontal, flipVertical);
    }

    public static void DrawInto(this byte[,] tile, IWritableBitmapData data, int destinationX = 0, int destinationY = 0, bool flipHorizontal = false, bool flipVertical = false)
    {
        if (tile.GetLength(0) != 8 || tile.GetLength(1) != 8)
            return;

        for (int y = 0; y < 8; y++)
            for (int x = 0; x < 8; x++)
            {
                var sourceX = x;
                var sourceY = y;

                if (flipHorizontal) x = 7 - x;
                if (flipVertical) y = 7 - y;

                data.SetColorIndex(destinationX + x, destinationY + y, tile[sourceX, sourceY]);
            }
    }
}