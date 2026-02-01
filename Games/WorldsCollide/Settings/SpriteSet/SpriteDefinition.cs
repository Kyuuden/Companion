using System.Collections.Generic;


namespace FF.Rando.Companion.Games.WorldsCollide.Settings.SpriteSet;

internal class SpriteDefinition
{
    public SpriteDefinition()
    {
    }

    public SpriteDefinition(SpriteSource source, int id, int subId, List<SpriteTransform> spriteTransforms)
    {
        Source = source;
        Id = id;
        SubId = subId;
        Transforms = spriteTransforms;
    }

    public SpriteDefinition(SpriteSource source, int id)
        : this(source, id, 0, [])
    { }

    public SpriteDefinition(SpriteSource source, int id, int subId)
    : this(source, id, subId, [])
    { }

    public SpriteDefinition(SpriteSource source, int id, List<SpriteTransform> spriteTransforms)
    : this(source, id, 0, spriteTransforms)
    { }

    public SpriteSource Source { get; set; }
    public int Id { get; set; }
    public int SubId { get; set; }
    public List<SpriteTransform> Transforms { get; set; } = [];
}
