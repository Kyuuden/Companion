using KGySoft.Drawing.Imaging;
using System.Drawing;

namespace FF.Rando.Companion.Rendering;

public static class TileExtensions
{
    public static void DrawInto(this byte[,] tile, IWritableBitmapData data, Point point, bool flipHorizontal = false, bool flipVertical = false)
    {
        tile.DrawInto(data, point.X, point.Y, flipHorizontal, flipVertical);
    }

    public static void DrawInto(this byte[,] tile, IWritableBitmapData data, int destinationX = 0, int destinationY = 0, bool flipHorizontal = false, bool flipVertical = false, int colorOffset = 0)
    {
        if (tile.GetLength(0) != 8 || tile.GetLength(1) != 8)
            return;

        var transparent0 = data.Palette != null && data.Palette[0] == new Color32();

        for (int y = 0; y < 8; y++)
            for (int x = 0; x < 8; x++)
            {
                var sourceX = x;
                var sourceY = y;

                if (flipHorizontal) sourceX = 7 - sourceX;
                if (flipVertical) sourceY = 7 - sourceY;

                if (transparent0 && tile[sourceX, sourceY] == 0)
                    continue;

                var dx = destinationX + x;
                var dy = destinationY + y;
                
                if (dx < 0 || dy < 0) continue;
                if (dx >= data.Width || dy >= data.Height) continue;

                data.SetColorIndex(dx, dy, tile[sourceX, sourceY] + colorOffset);
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