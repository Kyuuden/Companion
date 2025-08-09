using FF.Rando.Companion.Extensions;
using FF.Rando.Companion.FreeEnterprise.Shared;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace FF.Rando.Companion.FreeEnterprise._4._6._1.Gale;
internal class Locations
{
    private readonly Dictionary<RewardSlot, Location> _locations;
    private readonly Descriptors _descriptors;
    private readonly Flags _flags;

    internal IEnumerable<Location> Items => _locations.Values;

    public Locations(Descriptors descriptors, Flags flags)
    {
        _descriptors = descriptors;
        _flags = flags;
        _locations = Enum.GetValues(typeof(RewardSlot))
            .OfType<RewardSlot>()
            .Where(IsInSeed)
            .ToDictionary(t => t, t => new Location(t, _descriptors.GetRewardSlotName(t), CanHaveKeyItem(t), CanHaveCharcater(t)));
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
            RewardSlot.LunarCoreChest1 => _flags.KMaibAll || (_flags.KMaibStandard && (_flags.KMoon || _flags.KUnsafe)) || _flags.KMaibLST,
            RewardSlot.LunarCoreChest2 => _flags.KMaibAll || (_flags.KMaibStandard && (_flags.KMoon || _flags.KUnsafe)) || _flags.KMaibLST,
            RewardSlot.LunarCoreChest3 => _flags.KMaibAll || (_flags.KMaibStandard && (_flags.KMoon || _flags.KUnsafe)) || _flags.KMaibLST,
            RewardSlot.LunarCoreChest4 => _flags.KMaibAll || (_flags.KMaibStandard && (_flags.KMoon || _flags.KUnsafe)) || _flags.KMaibLST,
            RewardSlot.LunarCoreChest5 => _flags.KMaibAll || (_flags.KMaibStandard && (_flags.KMoon || _flags.KUnsafe)) || _flags.KMaibLST,
            RewardSlot.LunarCoreChest6 => _flags.KMaibAll || (_flags.KMaibStandard && (_flags.KMoon || _flags.KUnsafe)) || _flags.KMaibLST,
            RewardSlot.LunarCoreChest7 => _flags.KMaibAll || (_flags.KMaibStandard && (_flags.KMoon || _flags.KUnsafe)) || _flags.KMaibLST,
            RewardSlot.LunarCoreChest8 => _flags.KMaibAll || (_flags.KMaibStandard && (_flags.KMoon || _flags.KUnsafe)) || _flags.KMaibLST,
            RewardSlot.LunarCoreChest9 => _flags.KMaibAll || (_flags.KMaibStandard && (_flags.KMoon || _flags.KUnsafe)) || _flags.KMaibLST,
            RewardSlot.RydiasMomItem => _flags.KNoFreeMode == KNoFreeMode.Standard || _flags.KNoFreeMode == KNoFreeMode.Package,
            RewardSlot.ForgeItem => _flags.KForge,
            RewardSlot.PinkTradeItem => _flags.KPink,
            _ => false
        };

    private bool IsInSeed(RewardSlot slot)
        => CanHaveCharcater(slot) || CanHaveKeyItem(slot);
}
