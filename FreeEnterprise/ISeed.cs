using FF.Rando.Companion.FreeEnterprise.RomData;
using FF.Rando.Companion.FreeEnterprise.Settings;
using FF.Rando.Companion.Settings;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace FF.Rando.Companion.FreeEnterprise;
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
    bool Victory { get; }
    new FreeEnterpriseSettings Settings { get; }
    ISettings RootSettings { get; }
    RomData.Font Font { get; }
    Sprites Sprites { get; }
    bool CanTackBosses { get; }
    event Action<string>? ButtonPressed;
}