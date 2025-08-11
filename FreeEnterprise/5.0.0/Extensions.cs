using FF.Rando.Companion.FreeEnterprise.Shared;
using System;

namespace FF.Rando.Companion.FreeEnterprise._5._0._0;
internal static class Extensions
{
    public static bool IsMiab(this ChestSlot chestSlot)
        => chestSlot switch
        {
            ChestSlot.Feymarch => false,
            ChestSlot.RibbonRoom1 => false,
            ChestSlot.RibbonRoom2 => false,
            _ => true
        };

    public static World World(this RewardSlot slot)
        => slot switch
        {
            RewardSlot.DwarfCastleCharacter => Shared.World.Underground,
            RewardSlot.LunarPalaceCharacter => Shared.World.Moon,
            RewardSlot.BabilBossItem => Shared.World.Underground,
            RewardSlot.CannonItem => Shared.World.Underground,
            RewardSlot.LucaItem => Shared.World.Underground,
            RewardSlot.SealedCaveItem => Shared.World.Underground,
            RewardSlot.FoundYangItem => Shared.World.Underground,
            RewardSlot.PanTradeItem => Shared.World.Underground,
            RewardSlot.FeymarchQueenItem => Shared.World.Underground,
            RewardSlot.FeymarchKingItem => Shared.World.Underground,
            RewardSlot.SylphItem => Shared.World.Underground,
            RewardSlot.BahamutItem => Shared.World.Moon,
            RewardSlot.LunarBoss1Item => Shared.World.Moon,
            RewardSlot.LunarBoss2Item => Shared.World.Moon,
            RewardSlot.LunarBoss3Item => Shared.World.Moon,
            RewardSlot.LunarBoss5Item => Shared.World.Moon,
            RewardSlot.ForgeItem => Shared.World.Underground,
            _ => Shared.World.Main
        };

    public static World World(this ChestSlot slot)
        => slot switch
        {
            ChestSlot.Feymarch => Shared.World.Underground,
            ChestSlot.RibbonRoom1 => Shared.World.Moon,
            ChestSlot.RibbonRoom2 => Shared.World.Moon,
            ChestSlot.ZotChest => Shared.World.Main,
            ChestSlot.EblanChest1 => Shared.World.Main,
            ChestSlot.EblanChest2 => Shared.World.Main,
            ChestSlot.EblanChest3 => Shared.World.Main,
            ChestSlot.LowerBabilChest1 => Shared.World.Underground,
            ChestSlot.LowerBabilChest2 => Shared.World.Underground,
            ChestSlot.LowerBabilChest3 => Shared.World.Underground,
            ChestSlot.LowerBabilChest4 => Shared.World.Underground,
            ChestSlot.CaveEblanChest => Shared.World.Main,
            ChestSlot.UpperBabilChest => Shared.World.Main,
            ChestSlot.CaveOfSummonsChest => Shared.World.Underground,
            ChestSlot.SylphCaveChest1 => Shared.World.Underground,
            ChestSlot.SylphCaveChest2 => Shared.World.Underground,
            ChestSlot.SylphCaveChest3 => Shared.World.Underground,
            ChestSlot.SylphCaveChest4 => Shared.World.Underground,
            ChestSlot.SylphCaveChest5 => Shared.World.Underground,
            ChestSlot.SylphCaveChest6 => Shared.World.Underground,
            ChestSlot.SylphCaveChest7 => Shared.World.Underground,
            ChestSlot.GiantChest => Shared.World.Main,
            ChestSlot.LunarPathChest => Shared.World.Moon,
            ChestSlot.LunarCoreChest1 => Shared.World.Moon,
            ChestSlot.LunarCoreChest2 => Shared.World.Moon,
            ChestSlot.LunarCoreChest3 => Shared.World.Moon,
            ChestSlot.LunarCoreChest4 => Shared.World.Moon,
            ChestSlot.LunarCoreChest5 => Shared.World.Moon,
            ChestSlot.LunarCoreChest6 => Shared.World.Moon,
            ChestSlot.LunarCoreChest7 => Shared.World.Moon,
            ChestSlot.LunarCoreChest8 => Shared.World.Moon,
            ChestSlot.LunarCoreChest9 => Shared.World.Moon,
            _ => Shared.World.Unknown
        };

    public static World World(this Shops slot)
        => slot switch
        {
            Shops.KaipoWeaponShop => Shared.World.Main,
            Shops.FabulWeaponArmorShop => Shared.World.Main,
            Shops.MysidiaWeaponShop => Shared.World.Main,
            Shops.BaronWeaponShop => Shared.World.Main,
            Shops.ToroiaWeaponShop => Shared.World.Main,
            Shops.SilveraWeaponShop => Shared.World.Main,
            Shops.DwarfCastleWeaponShop => Shared.World.Underground,
            Shops.CaveEblanWeaponShop => Shared.World.Main,
            Shops.TomraWeapon => Shared.World.Underground,
            Shops.SmithyShop => Shared.World.Underground,
            Shops.KaipoArmorShop => Shared.World.Main,
            Shops.MysidiaArmorShop => Shared.World.Main,
            Shops.BaronArmorShop => Shared.World.Main,
            Shops.ToroiaArmorShop => Shared.World.Main,
            Shops.SilveraArmorShop => Shared.World.Main,
            Shops.DwarfCastleArmorShop => Shared.World.Underground,
            Shops.CaveEblanArmorShop => Shared.World.Main,
            Shops.TomraArmorShop => Shared.World.Underground,
            Shops.MoonItemShop => Shared.World.Moon,
            Shops.BaronItemShop => Shared.World.Main,
            Shops.MysidiaItemShop => Shared.World.Main,
            Shops.SilveraItemShop => Shared.World.Main,
            Shops.MistWeaponShop => Shared.World.Main,
            Shops.MistArmorShop => Shared.World.Main,
            Shops.CaveEblanItemShop => Shared.World.Main,
            Shops.AgartWeaponShop => Shared.World.Main,
            Shops.AgartArmorShop => Shared.World.Main,
            Shops.FeymarchWeaponShop => Shared.World.Underground,
            Shops.FeymarchArmorShop => Shared.World.Underground,
            Shops.TomraItemShop => Shared.World.Underground,
            Shops.ToroiaCafeItemShop => Shared.World.Main,
            Shops.KaipoItemShop => Shared.World.Main,
            Shops.FabulItemShop => Shared.World.Main,
            Shops.ToroiaItemShop => Shared.World.Main,
            Shops.AgartItemShop => Shared.World.Main,
            Shops.DwarfCastleItemShop => Shared.World.Underground,
            Shops.FeymarchItemShop => Shared.World.Underground,
            _ => Shared.World.Unknown
        };

    public static World World(this BossLocationType slot)
        => slot switch
        {
            BossLocationType.WaterhagSlot => Shared.World.Unknown,
            BossLocationType.CalbrenaSlot => Shared.World.Underground,
            BossLocationType.GolbezSlot => Shared.World.Underground,
            BossLocationType.LugaeSlot => Shared.World.Underground,
            BossLocationType.DarkimpSlot => Shared.World.Underground,
            BossLocationType.EvilwallSlot => Shared.World.Underground,
            BossLocationType.AsuraSlot => Shared.World.Underground,
            BossLocationType.LeviatanSlot => Shared.World.Underground,
            BossLocationType.BahamutSlot => Shared.World.Moon,
            BossLocationType.PaledimSlot => Shared.World.Moon,
            BossLocationType.WyvernSlot => Shared.World.Moon,
            BossLocationType.PlagueSlot => Shared.World.Moon,
            BossLocationType.DlunarSlot => Shared.World.Moon,
            BossLocationType.OgopogoSlot => Shared.World.Moon,
            BossLocationType.NotFound => Shared.World.Unknown,
            _ => Shared.World.Main
        };
}
