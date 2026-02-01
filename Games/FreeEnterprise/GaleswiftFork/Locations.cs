using FF.Rando.Companion.Extensions;
using FF.Rando.Companion.Games.FreeEnterprise.Shared;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace FF.Rando.Companion.Games.FreeEnterprise.GaleswiftFork;
internal class Locations
{
    private readonly Dictionary<RewardSlot, Location> _locations;
    private readonly Descriptors _descriptors;
    private readonly IFlags _flags;

    internal IEnumerable<Location> Items => _locations.Values;

    public Locations(Descriptors descriptors, IFlags flags)
    {
        _descriptors = descriptors;
        _flags = flags;
        _locations = Enum.GetValues(typeof(RewardSlot))
            .OfType<RewardSlot>()
            .Where(IsInSeed)
            .ToDictionary(t => t, t => new Location(t, GetWorld(t), _descriptors.GetRewardSlotName(t), CanHaveKeyItem(t), CanHaveCharcater(t)));
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
            var loc = (RewardSlot)location.ID;
            var isAvailable =
                IsInSeed(loc) &&
                CanGetToWorld(loc, canGetUnderground, canGetToMoon) &&
                HasKeyItemsFor(loc, foundKeyItems);

            if (isAvailable != location.IsAvailable)
            {
                updated = true;
                location.IsAvailable = isAvailable;
            }
        }

        return updated;
    }

    private World GetWorld(RewardSlot slot)
    => slot switch
    {
        RewardSlot.StartingCharacter => World.Main,
        RewardSlot.StartingPartnerCharacter => World.Main,
        RewardSlot.MistCharacter => World.Main,
        RewardSlot.WateryPassCharacter => World.Main,
        RewardSlot.DamcyanCharacter => World.Main,
        RewardSlot.KaipoCharacter => World.Main,
        RewardSlot.HobsCharacter => World.Main,
        RewardSlot.MysidiaCharacter1 => World.Main,
        RewardSlot.MysidiaCharacter2 => World.Main,
        RewardSlot.OrdealsCharacter => World.Main,
        RewardSlot.BaronInnCharacter => World.Main,
        RewardSlot.BaronCastleCharacter => World.Main,
        RewardSlot.ZotCharacter1 => World.Main,
        RewardSlot.ZotCharacter2 => World.Main,
        RewardSlot.DwarfCastleCharacter => World.Underground,
        RewardSlot.CaveEblanCharacter => World.Main,
        RewardSlot.LunarPalaceCharacter => World.Moon,
        RewardSlot.GiantCharacter => World.Main,
        RewardSlot.StartingItem => World.Main,
        RewardSlot.AntlionItem => World.Main,
        RewardSlot.FabulItem => World.Main,
        RewardSlot.OrdealsItem => World.Main,
        RewardSlot.BaronInnItem => World.Main,
        RewardSlot.BaronCastleItem => World.Main,
        RewardSlot.ToroiaHospitalItem when _flags.KNoFreeMode != KNoFreeMode.DwarfCastle => World.Main,
        RewardSlot.ToroiaHospitalItem when _flags.KNoFreeMode == KNoFreeMode.DwarfCastle => World.Underground,
        RewardSlot.MagnesItem => World.Main,
        RewardSlot.ZotItem => World.Main,
        RewardSlot.BabilBossItem => World.Underground,
        RewardSlot.CannonItem => World.Underground,
        RewardSlot.LucaItem => World.Underground,
        RewardSlot.SealedCaveItem => World.Underground,
        RewardSlot.FeymarchItem => World.Underground,
        RewardSlot.RatTradeItem => World.Main,
        RewardSlot.FoundYangItem => World.Underground,
        RewardSlot.PanTradeItem => World.Underground,
        RewardSlot.FeymarchQueenItem => World.Underground,
        RewardSlot.FeymarchKingItem => World.Underground,
        RewardSlot.BaronThroneItem => World.Main,
        RewardSlot.SylphItem => World.Underground,
        RewardSlot.BahamutItem => World.Moon,
        RewardSlot.LunarBoss1Item => World.Moon,
        RewardSlot.LunarBoss2Item => World.Moon,
        RewardSlot.LunarBoss3Item => World.Moon,
        RewardSlot.LunarBoss4Item1 => World.Moon,
        RewardSlot.LunarBoss4Item2 => World.Moon,
        RewardSlot.LunarBoss5Item => World.Moon,
        RewardSlot.ZotChest => World.Main,
        RewardSlot.EblanChest1 => World.Main,
        RewardSlot.EblanChest2 => World.Main,
        RewardSlot.EblanChest3 => World.Main,
        RewardSlot.LowerBabilChest1 => World.Underground,
        RewardSlot.LowerBabilChest2 => World.Underground,
        RewardSlot.LowerBabilChest3 => World.Underground,
        RewardSlot.LowerBabilChest4 => World.Underground,
        RewardSlot.CaveEblanChest => World.Main,
        RewardSlot.UpperBabilChest => World.Main,
        RewardSlot.CaveOfSummonsChest => World.Underground,
        RewardSlot.SylphCaveChest1 => World.Underground,
        RewardSlot.SylphCaveChest2 => World.Underground,
        RewardSlot.SylphCaveChest3 => World.Underground,
        RewardSlot.SylphCaveChest4 => World.Underground,
        RewardSlot.SylphCaveChest5 => World.Underground,
        RewardSlot.SylphCaveChest6 => World.Underground,
        RewardSlot.SylphCaveChest7 => World.Underground,
        RewardSlot.GiantChest => World.Main,
        RewardSlot.LunarPathChest => World.Moon,
        RewardSlot.LunarCoreChest1 => World.Moon,
        RewardSlot.LunarCoreChest2 => World.Moon,
        RewardSlot.LunarCoreChest3 => World.Moon,
        RewardSlot.LunarCoreChest4 => World.Moon,
        RewardSlot.LunarCoreChest5 => World.Moon,
        RewardSlot.LunarCoreChest6 => World.Moon,
        RewardSlot.LunarCoreChest7 => World.Moon,
        RewardSlot.LunarCoreChest8 => World.Moon,
        RewardSlot.LunarCoreChest9 => World.Moon,
        RewardSlot.RydiasMomItem => World.Main,
        RewardSlot.ForgeItem => World.Underground,
        RewardSlot.PinkTradeItem => World.Main,
        _ => World.Unknown
    };

    private bool HasKeyItemsFor(RewardSlot location, ImmutableHashSet<KeyItemType> foundKeyItems)
        => location switch
        {
            RewardSlot.MistCharacter => foundKeyItems.Contains(KeyItemType.Package),
            RewardSlot.KaipoCharacter => foundKeyItems.Contains(KeyItemType.SandRuby),
            RewardSlot.BaronCastleCharacter => foundKeyItems.Contains(KeyItemType.BaronKey),
            RewardSlot.ZotCharacter1 => foundKeyItems.Contains(KeyItemType.EarthCrystal),
            RewardSlot.ZotCharacter2 => foundKeyItems.Contains(KeyItemType.EarthCrystal),
            RewardSlot.CaveEblanCharacter => foundKeyItems.Contains(KeyItemType.Hook),
            RewardSlot.GiantCharacter => foundKeyItems.Contains(KeyItemType.DarknessCrystal),

            RewardSlot.BaronCastleItem => foundKeyItems.Contains(KeyItemType.BaronKey),
            RewardSlot.MagnesItem => foundKeyItems.Contains(KeyItemType.TwinHarp),
            RewardSlot.ZotItem => foundKeyItems.Contains(KeyItemType.EarthCrystal),
            RewardSlot.CannonItem => foundKeyItems.Contains(KeyItemType.TowerKey),
            RewardSlot.SealedCaveItem => foundKeyItems.Contains(KeyItemType.LucaKey),
            RewardSlot.RatTradeItem => foundKeyItems.Contains(KeyItemType.Hook) && foundKeyItems.Contains(KeyItemType.RatTail),
            RewardSlot.PinkTradeItem => foundKeyItems.Contains(KeyItemType.Hook) && foundKeyItems.Contains(KeyItemType.PinkTail),
            RewardSlot.PanTradeItem => foundKeyItems.Contains(KeyItemType.Pan),
            RewardSlot.BaronThroneItem => foundKeyItems.Contains(KeyItemType.BaronKey),
            RewardSlot.SylphItem => foundKeyItems.Contains(KeyItemType.Pan),
            RewardSlot.CaveEblanChest => foundKeyItems.Contains(KeyItemType.Hook),
            RewardSlot.UpperBabilChest => foundKeyItems.Contains(KeyItemType.Hook),
            RewardSlot.GiantChest => foundKeyItems.Contains(KeyItemType.DarknessCrystal),
            RewardSlot.ForgeItem => foundKeyItems.Contains(KeyItemType.Adamant) && foundKeyItems.Contains(KeyItemType.LegendSword),
            RewardSlot.RydiasMomItem when _flags.KNoFreeMode == KNoFreeMode.Package => foundKeyItems.Contains(KeyItemType.Package),
            _ => true
        };

    private bool CanGetToWorld(RewardSlot slot, bool canGetUnderground, bool canGetToMoon)
        => slot switch
        {
            RewardSlot.StartingCharacter => true,
            RewardSlot.StartingPartnerCharacter => true,
            RewardSlot.MistCharacter => true,
            RewardSlot.WateryPassCharacter => true,
            RewardSlot.DamcyanCharacter => true,
            RewardSlot.KaipoCharacter => true,
            RewardSlot.HobsCharacter => true,
            RewardSlot.MysidiaCharacter1 => true,
            RewardSlot.MysidiaCharacter2 => true,
            RewardSlot.OrdealsCharacter => true,
            RewardSlot.BaronInnCharacter => true,
            RewardSlot.BaronCastleCharacter => true,
            RewardSlot.ZotCharacter1 => true,
            RewardSlot.ZotCharacter2 => true,
            RewardSlot.DwarfCastleCharacter => canGetUnderground,
            RewardSlot.CaveEblanCharacter => true,
            RewardSlot.LunarPalaceCharacter => canGetToMoon,
            RewardSlot.GiantCharacter => true,
            RewardSlot.StartingItem => true,
            RewardSlot.AntlionItem => true,
            RewardSlot.FabulItem => true,
            RewardSlot.OrdealsItem => true,
            RewardSlot.BaronInnItem => true,
            RewardSlot.BaronCastleItem => true,
            RewardSlot.ToroiaHospitalItem when _flags.KNoFreeMode != KNoFreeMode.DwarfCastle => true,
            RewardSlot.ToroiaHospitalItem when _flags.KNoFreeMode == KNoFreeMode.DwarfCastle => canGetUnderground,
            RewardSlot.MagnesItem => true,
            RewardSlot.ZotItem => true,
            RewardSlot.BabilBossItem => canGetUnderground,
            RewardSlot.CannonItem => canGetUnderground,
            RewardSlot.LucaItem => canGetUnderground,
            RewardSlot.SealedCaveItem => canGetUnderground,
            RewardSlot.FeymarchItem => canGetUnderground,
            RewardSlot.RatTradeItem => true,
            RewardSlot.FoundYangItem => canGetUnderground,
            RewardSlot.PanTradeItem => canGetUnderground,
            RewardSlot.FeymarchQueenItem => canGetUnderground,
            RewardSlot.FeymarchKingItem => canGetUnderground,
            RewardSlot.BaronThroneItem => true,
            RewardSlot.SylphItem => canGetUnderground,
            RewardSlot.BahamutItem => canGetToMoon,
            RewardSlot.LunarBoss1Item => canGetToMoon,
            RewardSlot.LunarBoss2Item => canGetToMoon,
            RewardSlot.LunarBoss3Item => canGetToMoon,
            RewardSlot.LunarBoss4Item1 => canGetToMoon,
            RewardSlot.LunarBoss4Item2 => canGetToMoon,
            RewardSlot.LunarBoss5Item => canGetToMoon,
            RewardSlot.ZotChest => true,
            RewardSlot.EblanChest1 => true,
            RewardSlot.EblanChest2 => true,
            RewardSlot.EblanChest3 => true,
            RewardSlot.LowerBabilChest1 => canGetUnderground,
            RewardSlot.LowerBabilChest2 => canGetUnderground,
            RewardSlot.LowerBabilChest3 => canGetUnderground,
            RewardSlot.LowerBabilChest4 => canGetUnderground,
            RewardSlot.CaveEblanChest => true,
            RewardSlot.UpperBabilChest => true,
            RewardSlot.CaveOfSummonsChest => canGetUnderground,
            RewardSlot.SylphCaveChest1 => canGetUnderground,
            RewardSlot.SylphCaveChest2 => canGetUnderground,
            RewardSlot.SylphCaveChest3 => canGetUnderground,
            RewardSlot.SylphCaveChest4 => canGetUnderground,
            RewardSlot.SylphCaveChest5 => canGetUnderground,
            RewardSlot.SylphCaveChest6 => canGetUnderground,
            RewardSlot.SylphCaveChest7 => canGetUnderground,
            RewardSlot.GiantChest => true,
            RewardSlot.LunarPathChest => canGetToMoon,
            RewardSlot.LunarCoreChest1 => canGetToMoon,
            RewardSlot.LunarCoreChest2 => canGetToMoon,
            RewardSlot.LunarCoreChest3 => canGetToMoon,
            RewardSlot.LunarCoreChest4 => canGetToMoon,
            RewardSlot.LunarCoreChest5 => canGetToMoon,
            RewardSlot.LunarCoreChest6 => canGetToMoon,
            RewardSlot.LunarCoreChest7 => canGetToMoon,
            RewardSlot.LunarCoreChest8 => canGetToMoon,
            RewardSlot.LunarCoreChest9 => canGetToMoon,
            RewardSlot.RydiasMomItem => true,
            RewardSlot.ForgeItem => canGetUnderground,
            RewardSlot.PinkTradeItem => true,
            _ => false
        };

    public bool CanHaveCharcater(RewardSlot slot)
        => slot switch
        {
            RewardSlot.StartingCharacter => false,
            RewardSlot.StartingPartnerCharacter => !_flags.CNoPartner,
            RewardSlot.MistCharacter => !_flags.CNoEarned,
            RewardSlot.WateryPassCharacter => !_flags.CNoFree,
            RewardSlot.DamcyanCharacter => !_flags.CNoFree,
            RewardSlot.KaipoCharacter => !_flags.CNoEarned,
            RewardSlot.HobsCharacter => !_flags.CNoEarned,
            RewardSlot.MysidiaCharacter1 => !_flags.CNoFree,
            RewardSlot.MysidiaCharacter2 => !_flags.CNoFree,
            RewardSlot.OrdealsCharacter => !_flags.CNoFree,
            RewardSlot.BaronInnCharacter => !_flags.CNoEarned,
            RewardSlot.BaronCastleCharacter => !_flags.CNoEarned,
            RewardSlot.ZotCharacter1 => !_flags.CNoEarned,
            RewardSlot.ZotCharacter2 => !_flags.CNoEarned,
            RewardSlot.DwarfCastleCharacter => !_flags.CNoEarned,
            RewardSlot.CaveEblanCharacter => !_flags.CNoEarned,
            RewardSlot.LunarPalaceCharacter => !_flags.CNoEarned,
            RewardSlot.GiantCharacter => !_flags.CNoEarned && !_flags.CClassicGiant,
            _ => false
        };

    public bool CanHaveKeyItem(RewardSlot slot)
        => slot switch
        {
            RewardSlot.StartingItem => _flags.KMain,
            RewardSlot.AntlionItem => _flags.KMain,
            RewardSlot.FabulItem => _flags.KMain,
            RewardSlot.OrdealsItem => _flags.KMain,
            RewardSlot.BaronInnItem => _flags.KMain,
            RewardSlot.BaronCastleItem => _flags.KMain,
            RewardSlot.ToroiaHospitalItem => _flags.KNoFreeMode == KNoFreeMode.Disabled || _flags.KNoFreeMode == KNoFreeMode.DwarfCastle,
            RewardSlot.MagnesItem => _flags.KMain,
            RewardSlot.ZotItem => _flags.KMain,
            RewardSlot.BabilBossItem => _flags.KMain,
            RewardSlot.CannonItem => _flags.KMain,
            RewardSlot.LucaItem => _flags.KMain,
            RewardSlot.SealedCaveItem => _flags.KMain,
            RewardSlot.FeymarchItem => _flags.KMain,
            RewardSlot.RatTradeItem => _flags.KMain,
            RewardSlot.FoundYangItem => _flags.KMain,
            RewardSlot.PanTradeItem => _flags.KMain,
            RewardSlot.FeymarchQueenItem => _flags.KSummon,
            RewardSlot.FeymarchKingItem => _flags.KSummon,
            RewardSlot.BaronThroneItem => _flags.KSummon,
            RewardSlot.SylphItem => _flags.KSummon,
            RewardSlot.BahamutItem => _flags.KSummon,
            RewardSlot.LunarBoss1Item => _flags.KMoon,
            RewardSlot.LunarBoss2Item => _flags.KMoon,
            RewardSlot.LunarBoss3Item => _flags.KMoon,
            RewardSlot.LunarBoss4Item1 => _flags.KMoon,
            RewardSlot.LunarBoss4Item2 => _flags.KMoon,
            RewardSlot.LunarBoss5Item => _flags.KMoon,
            RewardSlot.ZotChest => _flags.KMaibAll || _flags.KMaibStandard || _flags.KMaibAbove,
            RewardSlot.EblanChest1 => _flags.KMaibAll || _flags.KMaibStandard || _flags.KMaibAbove,
            RewardSlot.EblanChest2 => _flags.KMaibAll || _flags.KMaibStandard || _flags.KMaibAbove,
            RewardSlot.EblanChest3 => _flags.KMaibAll || _flags.KMaibStandard || _flags.KMaibAbove,
            RewardSlot.LowerBabilChest1 => _flags.KMaibAll || _flags.KMaibStandard || _flags.KMaibBelow,
            RewardSlot.LowerBabilChest2 => _flags.KMaibAll || _flags.KMaibStandard || _flags.KMaibBelow,
            RewardSlot.LowerBabilChest3 => _flags.KMaibAll || _flags.KMaibStandard || _flags.KMaibBelow,
            RewardSlot.LowerBabilChest4 => _flags.KMaibAll || _flags.KMaibStandard || _flags.KMaibBelow,
            RewardSlot.CaveEblanChest => _flags.KMaibAll || _flags.KMaibStandard || _flags.KMaibAbove,
            RewardSlot.UpperBabilChest => _flags.KMaibAll || _flags.KMaibStandard || _flags.KMaibAbove,
            RewardSlot.CaveOfSummonsChest => _flags.KMaibAll || _flags.KMaibStandard || _flags.KMaibBelow,
            RewardSlot.SylphCaveChest1 => _flags.KMaibAll || _flags.KMaibStandard || _flags.KMaibBelow,
            RewardSlot.SylphCaveChest2 => _flags.KMaibAll || _flags.KMaibStandard || _flags.KMaibBelow,
            RewardSlot.SylphCaveChest3 => _flags.KMaibAll || _flags.KMaibStandard || _flags.KMaibBelow,
            RewardSlot.SylphCaveChest4 => _flags.KMaibAll || _flags.KMaibStandard || _flags.KMaibBelow,
            RewardSlot.SylphCaveChest5 => _flags.KMaibAll || _flags.KMaibStandard || _flags.KMaibBelow,
            RewardSlot.SylphCaveChest6 => _flags.KMaibAll || _flags.KMaibStandard || _flags.KMaibBelow,
            RewardSlot.SylphCaveChest7 => _flags.KMaibAll || _flags.KMaibStandard || _flags.KMaibBelow,
            RewardSlot.GiantChest => _flags.KMaibAll || _flags.KMaibStandard || _flags.KMaibAbove,
            RewardSlot.LunarPathChest => _flags.KMaibAll || _flags.KMaibStandard || _flags.KMaibAbove,
            RewardSlot.LunarCoreChest1 => _flags.KMaibAll || _flags.KMaibStandard && (_flags.KMoon || _flags.KUnsafe) || _flags.KMaibLST,
            RewardSlot.LunarCoreChest2 => _flags.KMaibAll || _flags.KMaibStandard && (_flags.KMoon || _flags.KUnsafe) || _flags.KMaibLST,
            RewardSlot.LunarCoreChest3 => _flags.KMaibAll || _flags.KMaibStandard && (_flags.KMoon || _flags.KUnsafe) || _flags.KMaibLST,
            RewardSlot.LunarCoreChest4 => _flags.KMaibAll || _flags.KMaibStandard && (_flags.KMoon || _flags.KUnsafe) || _flags.KMaibLST,
            RewardSlot.LunarCoreChest5 => _flags.KMaibAll || _flags.KMaibStandard && (_flags.KMoon || _flags.KUnsafe) || _flags.KMaibLST,
            RewardSlot.LunarCoreChest6 => _flags.KMaibAll || _flags.KMaibStandard && (_flags.KMoon || _flags.KUnsafe) || _flags.KMaibLST,
            RewardSlot.LunarCoreChest7 => _flags.KMaibAll || _flags.KMaibStandard && (_flags.KMoon || _flags.KUnsafe) || _flags.KMaibLST,
            RewardSlot.LunarCoreChest8 => _flags.KMaibAll || _flags.KMaibStandard && (_flags.KMoon || _flags.KUnsafe) || _flags.KMaibLST,
            RewardSlot.LunarCoreChest9 => _flags.KMaibAll || _flags.KMaibStandard && (_flags.KMoon || _flags.KUnsafe) || _flags.KMaibLST,
            RewardSlot.RydiasMomItem => _flags.KNoFreeMode == KNoFreeMode.Standard || _flags.KNoFreeMode == KNoFreeMode.Package,
            RewardSlot.ForgeItem => _flags.KForge,
            RewardSlot.PinkTradeItem => _flags.KPink,
            _ => false
        };

    private bool IsInSeed(RewardSlot slot)
        => CanHaveCharcater(slot) || CanHaveKeyItem(slot);
}
