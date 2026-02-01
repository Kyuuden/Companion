using System.Drawing;


namespace FF.Rando.Companion.Games.WorldsCollide.Settings.SpriteSet;

internal class Crop : SpriteTransform
{
    public Crop() { }
    public Crop(int x, int y, int width, int height)
    {
        X = x;
        Y = y;
        Width = width;
        Height = height;
    }

    public int X { get; set; }
    public int Y { get; set; }
    public int Width { get; set; }
    public int Height { get; set; }

    public Rectangle GetRectangle() => new(X, Y, Width, Height);
}
