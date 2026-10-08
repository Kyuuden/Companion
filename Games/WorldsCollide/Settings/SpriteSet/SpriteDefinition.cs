using FF.Rando.Companion.Games.WorldsCollide.Enums;
using System.Collections.Generic;


namespace FF.Rando.Companion.Games.WorldsCollide.Settings.SpriteSet;

internal class SpriteDefinition
{
    public SpriteDefinition()
    {
    }

    public SpriteDefinition(Item item) : this(item, []) { }
    public SpriteDefinition(Item item, List<SpriteTransform> spriteTransforms) : this(SpriteSource.Item, (int)item, spriteTransforms) { }

    public SpriteDefinition(Boss boss) : this(boss, []) { }
    public SpriteDefinition(Boss boss, List<SpriteTransform> spriteTransforms) : this(SpriteSource.Boss, (int)boss, spriteTransforms) { }

    public SpriteDefinition(Monster monster) : this(monster, []) { }
    public SpriteDefinition(Monster monster, List<SpriteTransform> spriteTransforms) : this(SpriteSource.Monster, (int)monster, spriteTransforms) { }

    public SpriteDefinition(Esper esper) : this(esper, []) { }
    public SpriteDefinition(Esper esper, List<SpriteTransform> spriteTransforms) : this(SpriteSource.Esper, (int)esper, spriteTransforms) { }

    public SpriteDefinition(CharacterEx character, Pose pose = Pose.Stand) : this(SpriteSource.Character, (int)character, (int)pose, []) { }
    public SpriteDefinition(CharacterEx character, Pose pose, List<SpriteTransform> spriteTransforms) : this(SpriteSource.Character, (int)character, (int)pose, spriteTransforms) { }

    public SpriteDefinition(TileSet tileset) : this(tileset, []) { }
    public SpriteDefinition(TileSet tileset, List<SpriteTransform> spriteTransforms) : this(SpriteSource.Background, (int)tileset, spriteTransforms) { }

    public SpriteDefinition(MapLocation location) : this(location, []) { }
    public SpriteDefinition(MapLocation location, List<SpriteTransform> spriteTransforms) : this(SpriteSource.Map, (int)location, spriteTransforms) { }

    public SpriteDefinition(SpriteSource source, int id, int subId, List<SpriteTransform> spriteTransforms)
    {
        Source = source;
        Id = id;
        SubId = subId;
        Transforms = spriteTransforms;
    }

    private SpriteDefinition(SpriteSource source, int id)
        : this(source, id, 0, [])
    { }

    private SpriteDefinition(SpriteSource source, int id, int subId)
    : this(source, id, subId, [])
    { }

    private SpriteDefinition(SpriteSource source, int id, List<SpriteTransform> spriteTransforms)
    : this(source, id, 0, spriteTransforms)
    { }

    public SpriteSource Source { get; set; }
    public int Id { get; set; }
    public int SubId { get; set; }
    public List<SpriteTransform> Transforms { get; set; } = [];
}
