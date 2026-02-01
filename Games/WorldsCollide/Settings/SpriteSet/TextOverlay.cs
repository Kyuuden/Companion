using System.Drawing;


namespace FF.Rando.Companion.Games.WorldsCollide.Settings.SpriteSet;

internal class TextOverlay : SpriteTransform
{
    public TextOverlay() { }

    public TextOverlay(int x, int y, string text)
    {
        X = x;
        Y = y;
        Text = text;
    }

    public int X { get; set; }
    public int Y { get; set; }
    public string Text { get; set; } = string.Empty;

    public Point GetDestination() => new(X, Y);
}