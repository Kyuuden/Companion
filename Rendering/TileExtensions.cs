using KGySoft.Drawing.Imaging;
using System.Drawing;

namespace FF.Rando.Companion.Rendering;

public static class TileExtensions
{
    public static void DrawInto(this byte[,] tile, IWritableBitmapData data, Point point, bool flipHorizontal = false, bool flipVertical = false, int colorOffset = 0)
    {
        tile.DrawInto(data, point.X, point.Y, flipHorizontal, flipVertical);
    }

    public static void DrawInto(this byte[,] tile, IWritableBitmapData data, int destinationX = 0, int destinationY = 0, bool flipHorizontal = false, bool flipVertical = false, int colorOffset = 0)
    {
        if (tile.GetLength(0) != 8 || tile.GetLength(1) != 8)
            return;

        var transparent0 = data.Palette != null && data.Palette[0] == new Color32();
        for (int y = 0; y < 8; y++)
        {
            var sourceY = y;
            if (flipVertical) sourceY = 7 - sourceY;
            var dy = destinationY + y;

            if (dy >= data.Height || dy < 0)
                continue;

            var dRow = data[dy];

            for (int x = 0; x < 8; x++)
            {
                var sourceX = x;
                if (flipHorizontal) sourceX = 7 - sourceX;

                var colorIndex = tile[sourceX, sourceY];

                if (transparent0 && colorIndex == 0)
                    continue;

                var dx = destinationX + x;
                if (dx < 0 || dx >= data.Width) continue;

                dRow.SetColorIndex(dx, colorIndex + colorOffset);
            }
        }
    }

    public static IReadWriteBitmapData DrawTile(this IReadWriteBitmapData data, byte[,] tile, int destinationX = 0, int destinationY = 0, bool flipHorizontal = false, bool flipVertical = false)
    {
        tile.DrawInto(data, destinationX, destinationY, flipHorizontal, flipVertical);
        return data;
    }

    public static bool IsBackground(this byte[,] tile)
    {
        for (int y = 0; y < 8; y++)
            for (int x = 0; x < 8; x++)
                if (tile[x, y] != 0)
                    return false;

        return true;
    }

}