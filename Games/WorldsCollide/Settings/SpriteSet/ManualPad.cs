namespace FF.Rando.Companion.Games.WorldsCollide.Settings.SpriteSet;

internal class ManualPad : SpriteTransform
{
    public ManualPad() { }
    public ManualPad(int left, int top, int width, int height)
    {
        Left = left;
        Top = top;
        Width = width;
        Height = height;
    }

    public int Left { get; set; }
    public int Top { get; set;  }
    public int Width { get; set;  }
    public int Height { get; set; }
}
