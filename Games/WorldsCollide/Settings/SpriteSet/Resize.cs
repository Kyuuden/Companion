using System.Drawing;


namespace FF.Rando.Companion.Games.WorldsCollide.Settings.SpriteSet;

internal class Resize : SpriteTransform
{
    public Resize() { }
    public Resize(int width, int height)
    {
        Width = width;
        Height = height;
    }

    public int Width { get; set; }
    public int Height { get; set; }

    public Size GetSize() => new(Width, Height);
}
