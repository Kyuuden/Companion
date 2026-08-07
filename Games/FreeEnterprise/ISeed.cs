using FF.Rando.Companion.Games.FreeEnterprise.RomData;
using System.Collections.Generic;

namespace FF.Rando.Companion.Games.FreeEnterprise;
public interface ISeed : IGame
{
    IEnumerable<ILocation> AvailableLocations { get; }
    decimal? XpRate { get; }
    int TreasureCount { get; }
    IEnumerable<IBoss> Bosses { get; }
    int DefeatedEncounters { get; }
    Flags Flags { get; }
    IEnumerable<IKeyItem> KeyItems { get; }
    Metadata Metadata { get; }
    IEnumerable<IObjectiveGroup> Objectives { get; }
    IEnumerable<ICharacter> Party { get; }
    Font Font { get; }
    Sprites Sprites { get; }
    bool CanTackBosses { get; }
}