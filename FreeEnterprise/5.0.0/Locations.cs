using BizHawk.Common;
using FF.Rando.Companion.Extensions;
using FF.Rando.Companion.FreeEnterprise.Shared;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace FF.Rando.Companion.FreeEnterprise._5._0._0;

internal class Locations
{
    private readonly Descriptors _descriptors;
    private readonly Flags _flags;

    private readonly Dictionary<RewardSlot, RewardSlotLocation> _rewardSlots;
    private readonly Dictionary<BossLocationType, BossLocation> _bossLocations;
    private readonly Dictionary<Shops, ShopLocation> _shopLocations;
    private readonly Dictionary<ChestSlot, ChestLocation> _chests;

    public IEnumerable<ILocation> All => _rewardSlots.Values.OfType<ILocation>()
        .Concat(_bossLocations.Values)
        .Concat(_shopLocations.Values)
        .Concat(_chests.Values);

    public Locations(Descriptors descriptors, Flags flags)
    {
        _descriptors = descriptors;
        _flags = flags;

        _rewardSlots = Enum.GetValues(typeof(RewardSlot))
            .OfType<RewardSlot>()
            .Where(IsInSeed)
            .ToDictionary(t => t, t => new RewardSlotLocation(t, _descriptors.GetRewardSlotDescription((int)t), CanHaveKeyItem(t), CanHaveCharcater(t)));

        _bossLocations = Enum.GetValues(typeof(BossLocationType))
            .OfType<BossLocationType>()
            .Where(IsInSeed)
            .ToDictionary(t => t, t => new BossLocation(t, (_descriptors as IBossDescriptor).GetLocationName(t)));

        _shopLocations = Enum.GetValues(typeof(Shops))
            .OfType<Shops>()
            .ToDictionary(t => t, t => new ShopLocation(t, _descriptors.GetShopDescription(t)));

        _chests = Enum.GetValues(typeof(ChestSlot))
            .OfType<ChestSlot>()
            .Where(IsInSeed)
            .ToDictionary(t => t, t => new ChestLocation(t, _descriptors.GetChestDescription(t), CanHaveKeyItem(t), CanHaveCharcater(t)));
    }

    public bool Update(
        TimeSpan time,
        ReadOnlySpan<byte> checkedRewardSlots,
        ReadOnlySpan<byte> checkedShops,
        ReadOnlySpan<byte> checkedChests,
        ReadOnlySpan<byte> defeatedBossLocations,
        ImmutableHashSet<KeyItemType> foundKeyItems,
        ImmutableHashSet<BossType> defeatedBosses)
    {
        var updated = false;
        var canGetUnderground = foundKeyItems.Contains(KeyItemType.Hook) || foundKeyItems.Contains(KeyItemType.MagmaKey);
        var canGetToMoon = foundKeyItems.Contains(KeyItemType.DarknessCrystal);

        foreach (var slot in _rewardSlots.Values)
        {
            var ischecked = checkedRewardSlots.Read<bool>(slot.ID);
            if (ischecked != slot.IsChecked)
            {
                updated = true;
                slot.IsChecked = ischecked;
            }
        }

        foreach (var slot in _shopLocations.Values)
        {
            var ischecked = checkedShops.Read<bool>(slot.ID);
            if (ischecked != slot.IsChecked)
            {
                updated = true;
                slot.IsChecked = ischecked;
            }
        }

        foreach (var slot in _bossLocations.Values)
        {
            var ischecked = defeatedBossLocations.Read<bool>(slot.ID);
            if (ischecked != slot.IsChecked)
            {
                updated = true;
                slot.IsChecked = ischecked;
            }
        }

        foreach (var slot in _chests.Values)
        {
            var isOpened = checkedChests.Read<bool>(slot.ID);
            if (isOpened != slot.IsChecked)
            {
                updated = true;
                slot.IsChecked = isOpened;
            }
        }

        foreach (var slot in _rewardSlots.Values)
        {
            var loc = (RewardSlot)slot.ID;
            var isAvailable =
                IsInSeed(loc) &&
                CanGetToWorld(loc, canGetUnderground, canGetToMoon) &&
                HasKeyItemsFor(loc, foundKeyItems) &&
                HasDefeatedBossFor(loc, defeatedBosses);

            if (isAvailable != slot.IsAvailable)
            {
                updated = true;
                slot.IsAvailable = isAvailable;
            }
        }

        foreach (var slot in _shopLocations.Values)
        {
            var shop = (Shops)slot.ID;
            var isAvailable = CanGetToWorld(shop, canGetUnderground, canGetToMoon) && HasKeyItemsFor(shop, foundKeyItems);
            if (isAvailable != slot.IsAvailable)
            {
                updated = true;
                slot.IsAvailable = isAvailable;
            }
        }

        foreach (var slot in _bossLocations.Values)
        {
            var boss = (BossLocationType)slot.ID;
            var isAvailable = CanGetToWorld(boss, canGetUnderground, canGetToMoon) && HasKeyItemsFor(boss, foundKeyItems);
            if (isAvailable != slot.IsAvailable)
            {
                updated = true;
                slot.IsAvailable = isAvailable;
            }
        }

        foreach (var slot in _chests.Values)
        {
            var chest = (ChestSlot)slot.ID;
            var isAvailable =
                IsInSeed(chest) &&
                CanGetToWorld(chest, canGetUnderground, canGetToMoon) &&
                HasKeyItemsFor(chest, foundKeyItems);
            if (isAvailable != slot.IsAvailable)
            {
                updated = true;
                slot.IsAvailable = isAvailable;
            }
        }

        return updated;
    }

    private bool HasDefeatedBossFor(RewardSlot slot, ImmutableHashSet<BossType> defeatedBosses)
        => slot switch
        { 
            RewardSlot.RydiasMomItem => defeatedBosses.Contains(BossType.Dmist),
            _ => true
        };

    private bool HasKeyItemsFor(RewardSlot slot, ImmutableHashSet<KeyItemType> foundKeyItems)
        => slot switch
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
            RewardSlot.ForgeItem => foundKeyItems.Contains(KeyItemType.Adamant) && foundKeyItems.Contains(KeyItemType.LegendSword),
            _ => true
        };

    private bool CanGetToWorld(RewardSlot slot, bool canGetUnderground, bool canGetToMoon)
        => slot.World() switch
        {
            World.Underground => canGetUnderground,
            World.Main => true,
            World.Moon => canGetToMoon,
            _ => false
        };

    public bool CanHaveCharcater(RewardSlot slot)
        => slot switch
        {
            RewardSlot.None => false,
            RewardSlot.StartingCharacter => false,
            RewardSlot.StartingPartnerCharacter => true,
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
            RewardSlot.GiantCharacter => !_flags.CNoEarned && !_flags.CNoGiant,
            RewardSlot.StartingItem => _flags.KMain && _flags.KChar,
            RewardSlot.AntlionItem => _flags.KMain && _flags.KChar,
            RewardSlot.FabulItem => _flags.KMain && _flags.KChar,
            RewardSlot.OrdealsItem => _flags.KMain && _flags.KChar,
            RewardSlot.BaronInnItem => _flags.KMain && _flags.KChar,
            RewardSlot.BaronCastleItem => _flags.KMain && _flags.KChar,
            RewardSlot.ToroiaHospitalItem => !_flags.KNoFree && _flags.KChar,
            RewardSlot.MagnesItem => _flags.KMain && _flags.KChar,
            RewardSlot.ZotItem => _flags.KMain && _flags.KChar,
            RewardSlot.BabilBossItem => _flags.KMain && _flags.KChar,
            RewardSlot.CannonItem => _flags.KMain && _flags.KChar,
            RewardSlot.LucaItem => _flags.KMain && _flags.KChar,
            RewardSlot.SealedCaveItem => _flags.KMain && _flags.KChar,
            RewardSlot.RatTradeItem => _flags.KMain && _flags.KChar,
            RewardSlot.FoundYangItem => _flags.KMain && _flags.KChar,
            RewardSlot.PanTradeItem => _flags.KMain && _flags.KChar,
            RewardSlot.FeymarchQueenItem => _flags.KSummon && _flags.KChar,
            RewardSlot.FeymarchKingItem => _flags.KSummon && _flags.KChar,
            RewardSlot.BaronThroneItem => _flags.KSummon && _flags.KChar,
            RewardSlot.SylphItem => _flags.KSummon && _flags.KChar,
            RewardSlot.BahamutItem => _flags.KSummon && _flags.KChar,
            RewardSlot.LunarBoss1Item => _flags.KMoon && _flags.KChar,
            RewardSlot.LunarBoss2Item => _flags.KMoon && _flags.KChar,
            RewardSlot.LunarBoss3Item => _flags.KMoon && _flags.KChar,
            RewardSlot.LunarBoss5Item => _flags.KMoon && _flags.KChar,
            RewardSlot.RydiasMomItem => _flags.KNoFree && _flags.KChar,
            RewardSlot.FallenGolbezItem => false,
            RewardSlot.ForgeItem => _flags.KForge && _flags.KChar,
            RewardSlot.PinkTradeItem => false,
            _ => false
        };

    public bool CanHaveKeyItem(RewardSlot slot)
        => slot switch
        {
            RewardSlot.None => false,
            RewardSlot.StartingCharacter => false,
            RewardSlot.StartingPartnerCharacter => _flags.KChar && !_flags.CNoEarned,
            RewardSlot.MistCharacter => _flags.KChar && !_flags.CNoEarned,
            RewardSlot.WateryPassCharacter => false,
            RewardSlot.DamcyanCharacter => false,
            RewardSlot.KaipoCharacter => _flags.KChar && !_flags.CNoEarned,
            RewardSlot.HobsCharacter => _flags.KChar && !_flags.CNoEarned,
            RewardSlot.MysidiaCharacter1 => false,
            RewardSlot.MysidiaCharacter2 => false,
            RewardSlot.OrdealsCharacter => false,
            RewardSlot.BaronInnCharacter => _flags.KChar && !_flags.CNoEarned,
            RewardSlot.BaronCastleCharacter => _flags.KChar && !_flags.CNoEarned,
            RewardSlot.ZotCharacter1 => _flags.KChar && !_flags.CNoEarned,
            RewardSlot.ZotCharacter2 => _flags.KChar && !_flags.CNoEarned,
            RewardSlot.DwarfCastleCharacter => _flags.KChar && !_flags.CNoEarned,
            RewardSlot.CaveEblanCharacter => _flags.KChar && !_flags.CNoEarned,
            RewardSlot.LunarPalaceCharacter => _flags.KChar && !_flags.CNoEarned,
            RewardSlot.GiantCharacter => _flags.KChar && !_flags.CNoEarned && !_flags.CNoGiant,
            RewardSlot.StartingItem => _flags.KMain,
            RewardSlot.AntlionItem => _flags.KMain,
            RewardSlot.FabulItem => _flags.KMain,
            RewardSlot.OrdealsItem => _flags.KMain,
            RewardSlot.BaronInnItem => _flags.KMain,
            RewardSlot.BaronCastleItem => _flags.KMain,
            RewardSlot.ToroiaHospitalItem => !_flags.KNoFree,
            RewardSlot.MagnesItem => _flags.KMain,
            RewardSlot.ZotItem => _flags.KMain,
            RewardSlot.BabilBossItem => _flags.KMain,
            RewardSlot.CannonItem => _flags.KMain,
            RewardSlot.LucaItem => _flags.KMain,
            RewardSlot.SealedCaveItem => _flags.KMain,
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
            RewardSlot.LunarBoss5Item => _flags.KMoon,
            RewardSlot.RydiasMomItem => _flags.KNoFree,
            RewardSlot.FallenGolbezItem => !_flags.KMain,
            RewardSlot.ForgeItem => _flags.KForge,
            RewardSlot.PinkTradeItem => false,
            _ => false
        };

    private bool IsInSeed(RewardSlot slot) => CanHaveCharcater(slot) || CanHaveKeyItem(slot);

    private bool HasKeyItemsFor(BossLocationType slot, ImmutableHashSet<KeyItemType> foundKeyItems)
        => slot switch
        {
            BossLocationType.OfficerSlot => foundKeyItems.Contains(KeyItemType.Package),
            BossLocationType.BaiganSlot => foundKeyItems.Contains(KeyItemType.BaronKey),
            BossLocationType.KainazzoSlot => foundKeyItems.Contains(KeyItemType.BaronKey),
            BossLocationType.DarkelfSlot => foundKeyItems.Contains(KeyItemType.TwinHarp),
            BossLocationType.ValvalisSlot => foundKeyItems.Contains(KeyItemType.EarthCrystal),
            BossLocationType.DarkimpSlot => foundKeyItems.Contains(KeyItemType.TowerKey),
            BossLocationType.KingqueenSlot => foundKeyItems.Contains(KeyItemType.Hook),
            BossLocationType.RubicantSlot => foundKeyItems.Contains(KeyItemType.Hook),
            BossLocationType.EvilwallSlot => foundKeyItems.Contains(KeyItemType.LucaKey),
            BossLocationType.OdinSlot => foundKeyItems.Contains(KeyItemType.BaronKey),
            BossLocationType.ElementsSlot => foundKeyItems.Contains(KeyItemType.DarknessCrystal),
            BossLocationType.CpuSlot => foundKeyItems.Contains(KeyItemType.DarknessCrystal),
            _ => true
        };

    private bool CanGetToWorld(BossLocationType slot, bool canGetUnderground, bool canGetToMoon)
        => slot.World() switch
        {
            World.Underground => canGetUnderground,
            World.Main => true,
            World.Moon => canGetToMoon,
            _ => false
        };

    private bool IsInSeed(BossLocationType boss)
        => boss switch
        {
            BossLocationType.NotFound => false,
            BossLocationType.WaterhagSlot => false,
            _ => true
        };

    private bool HasKeyItemsFor(Shops slot, ImmutableHashSet<KeyItemType> foundKeyItems)
        => slot switch
        {
            Shops.BaronWeaponShop => foundKeyItems.Contains(KeyItemType.BaronKey),
            Shops.CaveEblanWeaponShop => foundKeyItems.Contains(KeyItemType.Hook),
            Shops.SmithyShop => foundKeyItems.Contains(KeyItemType.Adamant) && foundKeyItems.Contains(KeyItemType.LegendSword),
            Shops.BaronArmorShop => foundKeyItems.Contains(KeyItemType.BaronKey),
            Shops.CaveEblanArmorShop => foundKeyItems.Contains(KeyItemType.Hook),
            Shops.CaveEblanItemShop => foundKeyItems.Contains(KeyItemType.Hook),
            _ => true
        };

    private bool CanGetToWorld(Shops slot, bool canGetUnderground, bool canGetToMoon)
        => slot.World() switch
        {
            World.Underground => canGetUnderground,
            World.Main => true,
            World.Moon => canGetToMoon,
            _ => false
        };

    private bool HasKeyItemsFor(ChestSlot slot, ImmutableHashSet<KeyItemType> foundKeyItems)
        => slot switch
        {
            ChestSlot.CaveEblanChest => foundKeyItems.Contains(KeyItemType.Hook),
            ChestSlot.UpperBabilChest => foundKeyItems.Contains(KeyItemType.Hook),
            ChestSlot.GiantChest => foundKeyItems.Contains(KeyItemType.DarknessCrystal),
            _ => true
        };

    private bool CanGetToWorld(ChestSlot slot, bool canGetUnderground, bool canGetToMoon)
        => slot.World() switch
        {
            World.Underground => canGetUnderground,
            World.Main => true,
            World.Moon => canGetToMoon,
            _ => false
        };

    public bool CanHaveCharcater(ChestSlot slot)
        => _flags.KChar && slot switch
        {
            ChestSlot.Feymarch => _flags.KMain,
            ChestSlot.RibbonRoom1 => _flags.KMoon,
            ChestSlot.RibbonRoom2 => _flags.KMoon,
            ChestSlot.ZotChest => _flags.KMaibAll || _flags.KMaib || _flags.KMaibAbove,
            ChestSlot.EblanChest1 => _flags.KMaibAll || _flags.KMaib || _flags.KMaibAbove,
            ChestSlot.EblanChest2 => _flags.KMaibAll || _flags.KMaib || _flags.KMaibAbove,
            ChestSlot.EblanChest3 => _flags.KMaibAll || _flags.KMaib || _flags.KMaibAbove,
            ChestSlot.LowerBabilChest1 => _flags.KMaibAll || _flags.KMaib || _flags.KMaibBelow,
            ChestSlot.LowerBabilChest2 => _flags.KMaibAll || _flags.KMaib || _flags.KMaibBelow,
            ChestSlot.LowerBabilChest3 => _flags.KMaibAll || _flags.KMaib || _flags.KMaibBelow,
            ChestSlot.LowerBabilChest4 => _flags.KMaibAll || _flags.KMaib || _flags.KMaibBelow,
            ChestSlot.CaveEblanChest => _flags.KMaibAll || _flags.KMaib || _flags.KMaibAbove,
            ChestSlot.UpperBabilChest => _flags.KMaibAll || _flags.KMaib || _flags.KMaibAbove,
            ChestSlot.CaveOfSummonsChest => _flags.KMaibAll || _flags.KMaib || _flags.KMaibBelow,
            ChestSlot.SylphCaveChest1 => _flags.KMaibAll || _flags.KMaib || _flags.KMaibBelow,
            ChestSlot.SylphCaveChest2 => _flags.KMaibAll || _flags.KMaib || _flags.KMaibBelow,
            ChestSlot.SylphCaveChest3 => _flags.KMaibAll || _flags.KMaib || _flags.KMaibBelow,
            ChestSlot.SylphCaveChest4 => _flags.KMaibAll || _flags.KMaib || _flags.KMaibBelow,
            ChestSlot.SylphCaveChest5 => _flags.KMaibAll || _flags.KMaib || _flags.KMaibBelow,
            ChestSlot.SylphCaveChest6 => _flags.KMaibAll || _flags.KMaib || _flags.KMaibBelow,
            ChestSlot.SylphCaveChest7 => _flags.KMaibAll || _flags.KMaib || _flags.KMaibBelow,
            ChestSlot.GiantChest => _flags.KMaibAll || _flags.KMaib || _flags.KMaibAbove,
            ChestSlot.LunarPathChest => _flags.KMaibAll || _flags.KMaib || _flags.KMaibAbove,
            ChestSlot.LunarCoreChest1 => _flags.KMaibAll || (_flags.KMaib && (_flags.KMoon || _flags.KRisky)) || _flags.KMaibLst,
            ChestSlot.LunarCoreChest2 => _flags.KMaibAll || (_flags.KMaib && (_flags.KMoon || _flags.KRisky)) || _flags.KMaibLst,
            ChestSlot.LunarCoreChest3 => _flags.KMaibAll || (_flags.KMaib && (_flags.KMoon || _flags.KRisky)) || _flags.KMaibLst,
            ChestSlot.LunarCoreChest4 => _flags.KMaibAll || (_flags.KMaib && (_flags.KMoon || _flags.KRisky)) || _flags.KMaibLst,
            ChestSlot.LunarCoreChest5 => _flags.KMaibAll || (_flags.KMaib && (_flags.KMoon || _flags.KRisky)) || _flags.KMaibLst,
            ChestSlot.LunarCoreChest6 => _flags.KMaibAll || (_flags.KMaib && (_flags.KMoon || _flags.KRisky)) || _flags.KMaibLst,
            ChestSlot.LunarCoreChest7 => _flags.KMaibAll || (_flags.KMaib && (_flags.KMoon || _flags.KRisky)) || _flags.KMaibLst,
            ChestSlot.LunarCoreChest8 => _flags.KMaibAll || (_flags.KMaib && (_flags.KMoon || _flags.KRisky)) || _flags.KMaibLst,
            ChestSlot.LunarCoreChest9 => _flags.KMaibAll || (_flags.KMaib && (_flags.KMoon || _flags.KRisky)) || _flags.KMaibLst,
            _ => false
        };

    public bool CanHaveKeyItem(ChestSlot slot)
        => slot switch
        {
            ChestSlot.Feymarch => _flags.KMain,
            ChestSlot.RibbonRoom1 => _flags.KMoon,
            ChestSlot.RibbonRoom2 => _flags.KMoon,
            ChestSlot.ZotChest => _flags.KMaibAll || _flags.KMaib || _flags.KMaibAbove,
            ChestSlot.EblanChest1 => _flags.KMaibAll || _flags.KMaib || _flags.KMaibAbove,
            ChestSlot.EblanChest2 => _flags.KMaibAll || _flags.KMaib || _flags.KMaibAbove,
            ChestSlot.EblanChest3 => _flags.KMaibAll || _flags.KMaib || _flags.KMaibAbove,
            ChestSlot.LowerBabilChest1 => _flags.KMaibAll || _flags.KMaib || _flags.KMaibBelow,
            ChestSlot.LowerBabilChest2 => _flags.KMaibAll || _flags.KMaib || _flags.KMaibBelow,
            ChestSlot.LowerBabilChest3 => _flags.KMaibAll || _flags.KMaib || _flags.KMaibBelow,
            ChestSlot.LowerBabilChest4 => _flags.KMaibAll || _flags.KMaib || _flags.KMaibBelow,
            ChestSlot.CaveEblanChest => _flags.KMaibAll || _flags.KMaib || _flags.KMaibAbove,
            ChestSlot.UpperBabilChest => _flags.KMaibAll || _flags.KMaib || _flags.KMaibAbove,
            ChestSlot.CaveOfSummonsChest => _flags.KMaibAll || _flags.KMaib || _flags.KMaibBelow,
            ChestSlot.SylphCaveChest1 => _flags.KMaibAll || _flags.KMaib || _flags.KMaibBelow,
            ChestSlot.SylphCaveChest2 => _flags.KMaibAll || _flags.KMaib || _flags.KMaibBelow,
            ChestSlot.SylphCaveChest3 => _flags.KMaibAll || _flags.KMaib || _flags.KMaibBelow,
            ChestSlot.SylphCaveChest4 => _flags.KMaibAll || _flags.KMaib || _flags.KMaibBelow,
            ChestSlot.SylphCaveChest5 => _flags.KMaibAll || _flags.KMaib || _flags.KMaibBelow,
            ChestSlot.SylphCaveChest6 => _flags.KMaibAll || _flags.KMaib || _flags.KMaibBelow,
            ChestSlot.SylphCaveChest7 => _flags.KMaibAll || _flags.KMaib || _flags.KMaibBelow,
            ChestSlot.GiantChest => _flags.KMaibAll || _flags.KMaib || _flags.KMaibAbove,
            ChestSlot.LunarPathChest => _flags.KMaibAll || _flags.KMaib || _flags.KMaibAbove,
            ChestSlot.LunarCoreChest1 => _flags.KMaibAll || (_flags.KMaib && (_flags.KMoon || _flags.KRisky)) || _flags.KMaibLst,
            ChestSlot.LunarCoreChest2 => _flags.KMaibAll || (_flags.KMaib && (_flags.KMoon || _flags.KRisky)) || _flags.KMaibLst,
            ChestSlot.LunarCoreChest3 => _flags.KMaibAll || (_flags.KMaib && (_flags.KMoon || _flags.KRisky)) || _flags.KMaibLst,
            ChestSlot.LunarCoreChest4 => _flags.KMaibAll || (_flags.KMaib && (_flags.KMoon || _flags.KRisky)) || _flags.KMaibLst,
            ChestSlot.LunarCoreChest5 => _flags.KMaibAll || (_flags.KMaib && (_flags.KMoon || _flags.KRisky)) || _flags.KMaibLst,
            ChestSlot.LunarCoreChest6 => _flags.KMaibAll || (_flags.KMaib && (_flags.KMoon || _flags.KRisky)) || _flags.KMaibLst,
            ChestSlot.LunarCoreChest7 => _flags.KMaibAll || (_flags.KMaib && (_flags.KMoon || _flags.KRisky)) || _flags.KMaibLst,
            ChestSlot.LunarCoreChest8 => _flags.KMaibAll || (_flags.KMaib && (_flags.KMoon || _flags.KRisky)) || _flags.KMaibLst,
            ChestSlot.LunarCoreChest9 => _flags.KMaibAll || (_flags.KMaib && (_flags.KMoon || _flags.KRisky)) || _flags.KMaibLst,
            _ => false
        };

    private bool IsInSeed(ChestSlot slot) => CanHaveCharcater(slot) || CanHaveKeyItem(slot);
}
