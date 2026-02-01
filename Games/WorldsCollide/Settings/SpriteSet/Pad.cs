using FF.Rando.Companion.Rendering;
using System.Drawing;


namespace FF.Rando.Companion.Games.WorldsCollide.Settings.SpriteSet;

internal class Pad : SpriteTransform
{
    public Pad() { }
    public Pad(int width, int height, HorizontalAlignment horizontalalignment = HorizontalAlignment.Center, VerticalAlignment verticalAlignment = VerticalAlignment.Center)
    {
        Width = width; 
        Height = height;
        HorizontalAlignment = horizontalalignment;
        VerticalAlignment = verticalAlignment;
    }

    public int Width { get; set; }
    public int Height { get; set; }

    public HorizontalAlignment HorizontalAlignment { get; set; }
    public VerticalAlignment VerticalAlignment { get; set; }

    public Size GetSize() => new(Width, Height);
}

internal class FlipHorizontal : SpriteTransform;
internal class FlipVertical : SpriteTransform;

internal class AdjustBrightness : SpriteTransform
{
    public float Adjustment { get; set; }
}