using System.Diagnostics.CodeAnalysis;


namespace FF.Rando.Companion.Games.WorldsCollide.Settings.SpriteSet;

internal class AlternateDisabledSprite : SpriteTransform
{
    public AlternateDisabledSprite() { }

    [SetsRequiredMembers]
    public AlternateDisabledSprite(SpriteDefinition spriteDefinition)
    {
        DisabledSprite = spriteDefinition;
    }

    public required SpriteDefinition DisabledSprite { get; set; }
}
