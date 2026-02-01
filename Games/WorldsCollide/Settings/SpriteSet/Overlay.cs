using System.Diagnostics.CodeAnalysis;
using System.Drawing;


namespace FF.Rando.Companion.Games.WorldsCollide.Settings.SpriteSet;

internal class Overlay : SpriteTransform
{
    public Overlay() { }

    [SetsRequiredMembers]
    public Overlay(int x, int y, SpriteDefinition spriteDefinition)
    {
        X = x;
        Y = y;
        OverlayedSprite = spriteDefinition;
    }

    public required SpriteDefinition OverlayedSprite { get; set; }
    public int X { get; set; }
    public int Y { get; set; }

    public Point GetDestination() => new(X, Y);
}
