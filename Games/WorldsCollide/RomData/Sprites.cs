using FF.Rando.Companion.MemoryManagement;
using FF.Rando.Companion.Games.WorldsCollide.Rendering;
using System;

namespace FF.Rando.Companion.Games.WorldsCollide.RomData;

public class Sprites : IDisposable
{
    public Sprites(IMemorySpace memorySpace)
    {
        Combat = new CombatSprites(memorySpace);
        Portrait = new PortraitSprites(memorySpace);

        var actorTileData = memorySpace.ReadBytes(Addresses.ROM.ActorSpriteData);
        var actorPaletteData = memorySpace.ReadBytes(Addresses.ROM.ActorPaletteData);
        var tilesetData = memorySpace.ReadBytes(Addresses.ROM.TilesetData);
        var tilesetPaletteData = memorySpace.ReadBytes(Addresses.ROM.TilesetPaletteData);

        Character = new CharacterSprites(actorTileData, actorPaletteData);
        Other = new OtherSprites(actorTileData, actorPaletteData, tilesetData, tilesetPaletteData);
    }

    public CombatSprites Combat { get; }
    public PortraitSprites Portrait { get; }
    public CharacterSprites Character { get; }
    public OtherSprites Other { get; }

    public void Dispose()
    {
        Combat.Dispose();
        Portrait.Dispose();
        Character.Dispose();
        Other.Dispose();
    }
}
