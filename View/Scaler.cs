using System.Drawing;

namespace FF.Rando.Companion.View;
public static class Scaler
{
    public static int TileSize(this float scaleFactor) => (int)(8 * scaleFactor);

    public static Size Scale(this Size size, float scaleFactor) => Size.Truncate(((SizeF)size).Scale(scaleFactor));
    public static SizeF Scale(this SizeF size, float scaleFactor) => new(size.Width * scaleFactor, size.Height * scaleFactor);
    public static Point Scale(this Point point, float scaleFactor) => Point.Truncate(((PointF)point).Scale(scaleFactor));
    public static PointF Scale(this PointF point, float scaleFactor) => new(point.X * scaleFactor, point.Y * scaleFactor);

    public static Size Unscale(this Size size, float scaleFactor) => Size.Truncate(((SizeF)size).Unscale(scaleFactor));
    public static SizeF Unscale(this SizeF size, float scaleFactor) => new(size.Width / scaleFactor, size.Height / scaleFactor);
    public static Point Unscale(this Point point, float scaleFactor) => Point.Truncate(((PointF)point).Unscale(scaleFactor));
    public static PointF Unscale(this PointF point, float scaleFactor) => new(point.X / scaleFactor, point.Y / scaleFactor);
}
