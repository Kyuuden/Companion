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
        var tilesetData = memorySpace.ReadBytes(Addresses.ROM.TilesetData);
        var tilesetPaletteData = memorySpace.ReadBytes(Addresses.ROM.TilesetPaletteData);

        Characters = new CharacterSprites(actorTileData, actorPaletteData);
        Items = new ItemSprites(actorTileData, actorPaletteData);
        Backgrounds = new TileSetSprites(tilesetData, tilesetPaletteData);
    }

    public CombatSprites Combat { get; }
    public PortraitSprites Portraits { get; }
    public CharacterSprites Characters { get; }
    public ItemSprites Items { get; }
    public TileSetSprites Backgrounds { get; }

    public void Dispose()
    {
        Combat.Dispose();
        Portraits.Dispose();
        Characters.Dispose();
        Items.Dispose();
        Backgrounds.Dispose();
    }
}
