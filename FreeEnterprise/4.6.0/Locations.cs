﻿using FF.Rando.Companion.Extensions;
using FF.Rando.Companion.FreeEnterprise.Shared;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace FF.Rando.Companion.FreeEnterprise._4._6._0;
internal class Locations
{
    private readonly Dictionary<LocationType, Location> _locations;
    private readonly Descriptors _descriptors;
    private readonly Flags _flags;

    internal IEnumerable<Location> Items => _locations.Values;

    public Locations(Descriptors descriptors, Flags flags)
    {
        _descriptors = descriptors;
        _flags = flags;
        _locations = Enum.GetValues(typeof(LocationType))
            .OfType<LocationType>()
            .ToDictionary(t => t, t => new Location(t, _descriptors.GetLocationName(t)));
    }

    public bool Update(TimeSpan time, ReadOnlySpan<byte> checkedLocations, ImmutableHashSet<KeyItemType> foundKeyItems)
    {
        var updated = false;

        foreach (var location in _locations.Values)
        {
            var ischecked = checkedLocations.Read<bool>(location.ID);
            if (ischecked != location.IsChecked)
            {
                updated = true;
                location.IsChecked = ischecked;
            }
        }

        var canGetUnderground = foundKeyItems.Contains(KeyItemType.Hook) || foundKeyItems.Contains(KeyItemType.MagmaKey);
        var canGetToMoon = foundKeyItems.Contains(KeyItemType.DarknessCrystal);

        foreach (var location in _locations.Values)
        {
            var loc = (LocationType)location.ID;
            var isAvailable = IsInSeed(loc) && CanGetTo(loc, canGetUnderground, canGetToMoon) && HasKeyItemsFor(loc, foundKeyItems) && HasDonePreviousChecks(loc);

            if (isAvailable != location.IsAvailable)
            {
                updated = true;
                location.IsAvailable = isAvailable;
            }
        }

        return updated;
    }

    private bool HasDonePreviousChecks(LocationType location)
        => location switch
        {
            LocationType.Shelia2 => _locations[LocationType.FromTheSylphs].IsChecked,
            _ => true
        };

    private bool HasKeyItemsFor(LocationType location, ImmutableHashSet<KeyItemType> foundKeyItems)
        => location switch
        {
            LocationType.KaipoInn => foundKeyItems.Contains(KeyItemType.Package),
            LocationType.KaipoInfirmary => foundKeyItems.Contains(KeyItemType.SandRuby),
            LocationType.BaronCastleCharacter => foundKeyItems.Contains(KeyItemType.BaronKey),
            LocationType.Zot1 => foundKeyItems.Contains(KeyItemType.EarthCrystal),
            LocationType.Zot2 => foundKeyItems.Contains(KeyItemType.EarthCrystal),
            LocationType.EblanCave => foundKeyItems.Contains(KeyItemType.Hook),
            LocationType.Giant => foundKeyItems.Contains(KeyItemType.DarknessCrystal),

            LocationType.BaronCastle => foundKeyItems.Contains(KeyItemType.BaronKey),
            LocationType.CaveMagnes => foundKeyItems.Contains(KeyItemType.TwinHarp),
            LocationType.TowerOfZot => foundKeyItems.Contains(KeyItemType.EarthCrystal),
            LocationType.SuperCannon => foundKeyItems.Contains(KeyItemType.TowerKey),
            LocationType.SealedCave => foundKeyItems.Contains(KeyItemType.LucaKey),
            LocationType.RatTailTrade => foundKeyItems.Contains(KeyItemType.Hook) && foundKeyItems.Contains(KeyItemType.RatTail),
            LocationType.Shelia2 => foundKeyItems.Contains(KeyItemType.Pan),
            LocationType.OdinThrone => foundKeyItems.Contains(KeyItemType.BaronKey),
            LocationType.FromTheSylphs => foundKeyItems.Contains(KeyItemType.Pan),
            LocationType.CaveEblanTrappedChest => foundKeyItems.Contains(KeyItemType.Hook),
            LocationType.UpperBabIlTrappedChest => foundKeyItems.Contains(KeyItemType.Hook),
            LocationType.GiantOfBabIlTrappedChest => foundKeyItems.Contains(KeyItemType.DarknessCrystal),
            _ => true
        };

    private bool CanGetTo(LocationType location, bool canGetUnderground, bool canGetToMoon) 
        => location switch
        {
            LocationType.StartingCharacter or LocationType.StartingPartner or LocationType.KaipoInn or LocationType.WateryPass or LocationType.Damcyan or LocationType.KaipoInfirmary or LocationType.MtHobbs or LocationType.Mysidia1 or LocationType.Mysidia2 or LocationType.MtOrdealsCharacter or LocationType.BaronInnCharacter or LocationType.BaronCastleCharacter or LocationType.Zot1 or LocationType.Zot2 or LocationType.EblanCave or LocationType.Giant or LocationType.RatTailTrade or LocationType.StartingItem or LocationType.AntlionNest or LocationType.DefendingFabul or LocationType.MtOrdeals or LocationType.BaronInn or LocationType.BaronCastle or LocationType.EdwardInToroia or LocationType.CaveMagnes or LocationType.TowerOfZot or LocationType.OdinThrone or LocationType.RydiasMom or LocationType.GiantOfBabIlTrappedChest or LocationType.UpperBabIlTrappedChest or LocationType.CaveEblanTrappedChest or LocationType.TowerOfZotTrappedChest or LocationType.EblanTrappedChest1 or LocationType.EblanTrappedChest2 or LocationType.EblanTrappedChest3 => true,
            LocationType.DwarfCastle or LocationType.LowerBabIlBoss or LocationType.SuperCannon or LocationType.Luca or LocationType.SealedCave or LocationType.FeymarchChest or LocationType.Shelia1 or LocationType.Shelia2 or LocationType.FeymarchQueen or LocationType.FeymarchKing or LocationType.FromTheSylphs or LocationType.LowerBabIlTrappedChest1 or LocationType.LowerBabIlTrappedChest2 or LocationType.LowerBabIlTrappedChest3 or LocationType.LowerBabIlTrappedChest4 or LocationType.CaveOfSummonsTrappedChest or LocationType.SylphCaveTrappedChest1 or LocationType.SylphCaveTrappedChest2 or LocationType.SylphCaveTrappedChest3 or LocationType.SylphCaveTrappedChest4 or LocationType.SylphCaveTrappedChest5 or LocationType.SylphCaveTrappedChest6 or LocationType.SylphCaveTrappedChest7 => canGetUnderground,
            LocationType.Moon or LocationType.CaveBahamut or LocationType.MurasameAltar or LocationType.CrystalSwordAltar or LocationType.WhiteSpearAltar or LocationType.RibbonChest1 or LocationType.RibbonChest2 or LocationType.MasamuneAltar or LocationType.LunarPathTrappedChest or LocationType.LunarCoreTrappedChest1 or LocationType.LunarCoreTrappedChest2 or LocationType.LunarCoreTrappedChest3 or LocationType.LunarCoreTrappedChest4 or LocationType.LunarCoreTrappedChest5 or LocationType.LunarCoreTrappedChest6 or LocationType.LunarCoreTrappedChest7 or LocationType.LunarCoreTrappedChest8 or LocationType.LunarCoreTrappedChest9 => canGetToMoon,
            _ => false,
        };

    private bool IsInSeed(LocationType location)
        => location switch
        {
            LocationType.StartingCharacter => true,
            LocationType.StartingPartner => true,
            LocationType.KaipoInn => !_flags.CNoEarned,
            LocationType.WateryPass => !_flags.CNoFree,
            LocationType.Damcyan => !_flags.CNoFree,
            LocationType.KaipoInfirmary => !_flags.CNoEarned,
            LocationType.MtHobbs => !_flags.CNoEarned,
            LocationType.Mysidia1 => !_flags.CNoFree,
            LocationType.Mysidia2 => !_flags.CNoFree,
            LocationType.MtOrdealsCharacter => !_flags.CNoEarned,
            LocationType.PlaceHolder1 => false,
            LocationType.PlaceHolder2 => false,
            LocationType.BaronInnCharacter => !_flags.CNoEarned,
            LocationType.BaronCastleCharacter => !_flags.CNoEarned,
            LocationType.Zot1 => !_flags.CNoEarned,
            LocationType.Zot2 => !_flags.CNoEarned,
            LocationType.DwarfCastle => !_flags.CNoEarned,
            LocationType.EblanCave => !_flags.CNoEarned,
            LocationType.Moon => !_flags.CNoEarned,
            LocationType.Giant => !_flags.CNoEarned && !_flags.CClassicGiant,

            LocationType.StartingItem => _flags.KMain,
            LocationType.AntlionNest => _flags.KMain,
            LocationType.DefendingFabul => _flags.KMain,
            LocationType.MtOrdeals => _flags.KMain,
            LocationType.BaronInn => _flags.KMain,
            LocationType.BaronCastle => _flags.KMain,
            LocationType.EdwardInToroia => _flags.KMain && !_flags.KNoFree,
            LocationType.CaveMagnes => _flags.KMain,
            LocationType.TowerOfZot => _flags.KMain,
            LocationType.LowerBabIlBoss => _flags.KMain,
            LocationType.SuperCannon => _flags.KMain,
            LocationType.Luca => _flags.KMain,
            LocationType.SealedCave => _flags.KMain,
            LocationType.FeymarchChest => _flags.KMain,
            LocationType.RatTailTrade => _flags.KMain,
            LocationType.Shelia1 => _flags.KMain,
            LocationType.Shelia2 => _flags.KMain,
            LocationType.FeymarchQueen => _flags.KSummon,
            LocationType.FeymarchKing => _flags.KSummon,
            LocationType.OdinThrone => _flags.KSummon,
            LocationType.FromTheSylphs => _flags.KSummon,
            LocationType.CaveBahamut => _flags.KSummon,
            LocationType.MurasameAltar => _flags.KMoon,
            LocationType.CrystalSwordAltar => _flags.KMoon,
            LocationType.WhiteSpearAltar => _flags.KMoon,
            LocationType.RibbonChest1 => _flags.KMoon,
            LocationType.RibbonChest2 => _flags.KMoon,
            LocationType.MasamuneAltar => _flags.KMoon,
            LocationType.TowerOfZotTrappedChest => _flags.KMaib,
            LocationType.EblanTrappedChest1 => _flags.KMaib,
            LocationType.EblanTrappedChest2 => _flags.KMaib,
            LocationType.EblanTrappedChest3 => _flags.KMaib,
            LocationType.LowerBabIlTrappedChest1 => _flags.KMaib,
            LocationType.LowerBabIlTrappedChest2 => _flags.KMaib,
            LocationType.LowerBabIlTrappedChest3 => _flags.KMaib,
            LocationType.LowerBabIlTrappedChest4 => _flags.KMaib,
            LocationType.CaveEblanTrappedChest => _flags.KMaib,
            LocationType.UpperBabIlTrappedChest => _flags.KMaib,
            LocationType.CaveOfSummonsTrappedChest => _flags.KMaib,
            LocationType.SylphCaveTrappedChest1 => _flags.KMaib,
            LocationType.SylphCaveTrappedChest2 => _flags.KMaib,
            LocationType.SylphCaveTrappedChest3 => _flags.KMaib,
            LocationType.SylphCaveTrappedChest4 => _flags.KMaib,
            LocationType.SylphCaveTrappedChest5 => _flags.KMaib,
            LocationType.SylphCaveTrappedChest6 => _flags.KMaib,
            LocationType.SylphCaveTrappedChest7 => _flags.KMaib,
            LocationType.GiantOfBabIlTrappedChest => _flags.KMaib,
            LocationType.LunarPathTrappedChest => _flags.KMaib,
            LocationType.LunarCoreTrappedChest1 => _flags.KMaib && (_flags.KMoon || _flags.KUnsafe),
            LocationType.LunarCoreTrappedChest2 => _flags.KMaib && (_flags.KMoon || _flags.KUnsafe),
            LocationType.LunarCoreTrappedChest3 => _flags.KMaib && (_flags.KMoon || _flags.KUnsafe),
            LocationType.LunarCoreTrappedChest4 => _flags.KMaib && (_flags.KMoon || _flags.KUnsafe),
            LocationType.LunarCoreTrappedChest5 => _flags.KMaib && (_flags.KMoon || _flags.KUnsafe),
            LocationType.LunarCoreTrappedChest6 => _flags.KMaib && (_flags.KMoon || _flags.KUnsafe),
            LocationType.LunarCoreTrappedChest7 => _flags.KMaib && (_flags.KMoon || _flags.KUnsafe),
            LocationType.LunarCoreTrappedChest8 => _flags.KMaib && (_flags.KMoon || _flags.KUnsafe),
            LocationType.LunarCoreTrappedChest9 => _flags.KMaib && (_flags.KMoon || _flags.KUnsafe),
            LocationType.RydiasMom => _flags.KNoFree,
            LocationType.FallenGolbez => false,
            LocationType.ObjectiveCompletion => false,
            _ => false
        };
}
