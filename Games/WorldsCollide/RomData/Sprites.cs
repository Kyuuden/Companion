using FF.Rando.Companion.MemoryManagement;
using FF.Rando.Companion.Games.WorldsCollide.Rendering;
using System;

namespace FF.Rando.Companion.Games.WorldsCollide.RomData;

public class Sprites : IDisposable
{
    public Sprites(IMemorySpace memorySpace)
    {
        Combat = new CombatSprites(memorySpace);
        Portraits = new PortraitSprites(memorySpace);

        var actorTileData = memorySpace.ReadBytes(Addresses.ROM.ActorSpriteData);
        var actorPaletteData = memorySpace.ReadBytes(Addresses.ROM.ActorPaletteData);

        Characters = new CharacterSprites(actorTileData, actorPaletteData);
        Items = new ItemSprites(actorTileData, actorPaletteData);
        Backgrounds = new TileSetSprites(memorySpace.ReadBytes(Addresses.ROM.TilesetData), memorySpace.ReadBytes(Addresses.ROM.TilesetPaletteData));
        Effects = new EffectSprites(memorySpace.ReadBytes(Addresses.ROM.EffectsTileData), memorySpace.ReadBytes(Addresses.ROM.EffectPaletteData));
    }

    public CombatSprites Combat { get; }
    public PortraitSprites Portraits { get; }
    public CharacterSprites Characters { get; }
    public ItemSprites Items { get; }
    public TileSetSprites Backgrounds { get; }
    public EffectSprites Effects { get; }

    public void Dispose()
    {
        Combat.Dispose();
        Portraits.Dispose();
        Characters.Dispose();
        Items.Dispose();
        Backgrounds.Dispose();
    }
}
