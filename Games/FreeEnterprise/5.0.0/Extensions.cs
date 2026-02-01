using FF.Rando.Companion.Games.FreeEnterprise.Shared;

namespace FF.Rando.Companion.Games.FreeEnterprise._5._0._0;
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
            RewardSlot.DwarfCastleCharacter => Games.FreeEnterprise.Shared.World.Underground,
            RewardSlot.LunarPalaceCharacter => Games.FreeEnterprise.Shared.World.Moon,
            RewardSlot.BabilBossItem => Games.FreeEnterprise.Shared.World.Underground,
            RewardSlot.CannonItem => Games.FreeEnterprise.Shared.World.Underground,
            RewardSlot.LucaItem => Games.FreeEnterprise.Shared.World.Underground,
            RewardSlot.SealedCaveItem => Games.FreeEnterprise.Shared.World.Underground,
            RewardSlot.FoundYangItem => Games.FreeEnterprise.Shared.World.Underground,
            RewardSlot.PanTradeItem => Games.FreeEnterprise.Shared.World.Underground,
            RewardSlot.FeymarchQueenItem => Games.FreeEnterprise.Shared.World.Underground,
            RewardSlot.FeymarchKingItem => Games.FreeEnterprise.Shared.World.Underground,
            RewardSlot.SylphItem => Games.FreeEnterprise.Shared.World.Underground,
            RewardSlot.BahamutItem => Games.FreeEnterprise.Shared.World.Moon,
            RewardSlot.LunarBoss1Item => Games.FreeEnterprise.Shared.World.Moon,
            RewardSlot.LunarBoss2Item => Games.FreeEnterprise.Shared.World.Moon,
            RewardSlot.LunarBoss3Item => Games.FreeEnterprise.Shared.World.Moon,
            RewardSlot.LunarBoss5Item => Games.FreeEnterprise.Shared.World.Moon,
            RewardSlot.ForgeItem => Games.FreeEnterprise.Shared.World.Underground,
            _ => Games.FreeEnterprise.Shared.World.Main
        };

    public static World World(this ChestSlot slot)
        => slot switch
        {
            ChestSlot.Feymarch => Games.FreeEnterprise.Shared.World.Underground,
            ChestSlot.RibbonRoom1 => Games.FreeEnterprise.Shared.World.Moon,
            ChestSlot.RibbonRoom2 => Games.FreeEnterprise.Shared.World.Moon,
            ChestSlot.ZotChest => Games.FreeEnterprise.Shared.World.Main,
            ChestSlot.EblanChest1 => Games.FreeEnterprise.Shared.World.Main,
            ChestSlot.EblanChest2 => Games.FreeEnterprise.Shared.World.Main,
            ChestSlot.EblanChest3 => Games.FreeEnterprise.Shared.World.Main,
            ChestSlot.LowerBabilChest1 => Games.FreeEnterprise.Shared.World.Underground,
            ChestSlot.LowerBabilChest2 => Games.FreeEnterprise.Shared.World.Underground,
            ChestSlot.LowerBabilChest3 => Games.FreeEnterprise.Shared.World.Underground,
            ChestSlot.LowerBabilChest4 => Games.FreeEnterprise.Shared.World.Underground,
            ChestSlot.CaveEblanChest => Games.FreeEnterprise.Shared.World.Main,
            ChestSlot.UpperBabilChest => Games.FreeEnterprise.Shared.World.Main,
            ChestSlot.CaveOfSummonsChest => Games.FreeEnterprise.Shared.World.Underground,
            ChestSlot.SylphCaveChest1 => Games.FreeEnterprise.Shared.World.Underground,
            ChestSlot.SylphCaveChest2 => Games.FreeEnterprise.Shared.World.Underground,
            ChestSlot.SylphCaveChest3 => Games.FreeEnterprise.Shared.World.Underground,
            ChestSlot.SylphCaveChest4 => Games.FreeEnterprise.Shared.World.Underground,
            ChestSlot.SylphCaveChest5 => Games.FreeEnterprise.Shared.World.Underground,
            ChestSlot.SylphCaveChest6 => Games.FreeEnterprise.Shared.World.Underground,
            ChestSlot.SylphCaveChest7 => Games.FreeEnterprise.Shared.World.Underground,
            ChestSlot.GiantChest => Games.FreeEnterprise.Shared.World.Main,
            ChestSlot.LunarPathChest => Games.FreeEnterprise.Shared.World.Moon,
            ChestSlot.LunarCoreChest1 => Games.FreeEnterprise.Shared.World.Moon,
            ChestSlot.LunarCoreChest2 => Games.FreeEnterprise.Shared.World.Moon,
            ChestSlot.LunarCoreChest3 => Games.FreeEnterprise.Shared.World.Moon,
            ChestSlot.LunarCoreChest4 => Games.FreeEnterprise.Shared.World.Moon,
            ChestSlot.LunarCoreChest5 => Games.FreeEnterprise.Shared.World.Moon,
            ChestSlot.LunarCoreChest6 => Games.FreeEnterprise.Shared.World.Moon,
            ChestSlot.LunarCoreChest7 => Games.FreeEnterprise.Shared.World.Moon,
            ChestSlot.LunarCoreChest8 => Games.FreeEnterprise.Shared.World.Moon,
            ChestSlot.LunarCoreChest9 => Games.FreeEnterprise.Shared.World.Moon,
            _ => Games.FreeEnterprise.Shared.World.Unknown
        };

    public static World World(this Shops slot)
        => slot switch
        {
            Shops.KaipoWeaponShop => Games.FreeEnterprise.Shared.World.Main,
            Shops.FabulWeaponArmorShop => Games.FreeEnterprise.Shared.World.Main,
            Shops.MysidiaWeaponShop => Games.FreeEnterprise.Shared.World.Main,
            Shops.BaronWeaponShop => Games.FreeEnterprise.Shared.World.Main,
            Shops.ToroiaWeaponShop => Games.FreeEnterprise.Shared.World.Main,
            Shops.SilveraWeaponShop => Games.FreeEnterprise.Shared.World.Main,
            Shops.DwarfCastleWeaponShop => Games.FreeEnterprise.Shared.World.Underground,
            Shops.CaveEblanWeaponShop => Games.FreeEnterprise.Shared.World.Main,
            Shops.TomraWeapon => Games.FreeEnterprise.Shared.World.Underground,
            Shops.SmithyShop => Games.FreeEnterprise.Shared.World.Underground,
            Shops.KaipoArmorShop => Games.FreeEnterprise.Shared.World.Main,
            Shops.MysidiaArmorShop => Games.FreeEnterprise.Shared.World.Main,
            Shops.BaronArmorShop => Games.FreeEnterprise.Shared.World.Main,
            Shops.ToroiaArmorShop => Games.FreeEnterprise.Shared.World.Main,
            Shops.SilveraArmorShop => Games.FreeEnterprise.Shared.World.Main,
            Shops.DwarfCastleArmorShop => Games.FreeEnterprise.Shared.World.Underground,
            Shops.CaveEblanArmorShop => Games.FreeEnterprise.Shared.World.Main,
            Shops.TomraArmorShop => Games.FreeEnterprise.Shared.World.Underground,
            Shops.MoonItemShop => Games.FreeEnterprise.Shared.World.Moon,
            Shops.BaronItemShop => Games.FreeEnterprise.Shared.World.Main,
            Shops.MysidiaItemShop => Games.FreeEnterprise.Shared.World.Main,
            Shops.SilveraItemShop => Games.FreeEnterprise.Shared.World.Main,
            Shops.MistWeaponShop => Games.FreeEnterprise.Shared.World.Main,
            Shops.MistArmorShop => Games.FreeEnterprise.Shared.World.Main,
            Shops.CaveEblanItemShop => Games.FreeEnterprise.Shared.World.Main,
            Shops.AgartWeaponShop => Games.FreeEnterprise.Shared.World.Main,
            Shops.AgartArmorShop => Games.FreeEnterprise.Shared.World.Main,
            Shops.FeymarchWeaponShop => Games.FreeEnterprise.Shared.World.Underground,
            Shops.FeymarchArmorShop => Games.FreeEnterprise.Shared.World.Underground,
            Shops.TomraItemShop => Games.FreeEnterprise.Shared.World.Underground,
            Shops.ToroiaCafeItemShop => Games.FreeEnterprise.Shared.World.Main,
            Shops.KaipoItemShop => Games.FreeEnterprise.Shared.World.Main,
            Shops.FabulItemShop => Games.FreeEnterprise.Shared.World.Main,
            Shops.ToroiaItemShop => Games.FreeEnterprise.Shared.World.Main,
            Shops.AgartItemShop => Games.FreeEnterprise.Shared.World.Main,
            Shops.DwarfCastleItemShop => Games.FreeEnterprise.Shared.World.Underground,
            Shops.FeymarchItemShop => Games.FreeEnterprise.Shared.World.Underground,
            _ => Games.FreeEnterprise.Shared.World.Unknown
        };

    public static World World(this BossLocationType slot)
        => slot switch
        {
            BossLocationType.WaterhagSlot => Games.FreeEnterprise.Shared.World.Unknown,
            BossLocationType.CalbrenaSlot => Games.FreeEnterprise.Shared.World.Underground,
            BossLocationType.GolbezSlot => Games.FreeEnterprise.Shared.World.Underground,
            BossLocationType.LugaeSlot => Games.FreeEnterprise.Shared.World.Underground,
            BossLocationType.DarkimpSlot => Games.FreeEnterprise.Shared.World.Underground,
            BossLocationType.EvilwallSlot => Games.FreeEnterprise.Shared.World.Underground,
            BossLocationType.AsuraSlot => Games.FreeEnterprise.Shared.World.Underground,
            BossLocationType.LeviatanSlot => Games.FreeEnterprise.Shared.World.Underground,
            BossLocationType.BahamutSlot => Games.FreeEnterprise.Shared.World.Moon,
            BossLocationType.PaledimSlot => Games.FreeEnterprise.Shared.World.Moon,
            BossLocationType.WyvernSlot => Games.FreeEnterprise.Shared.World.Moon,
            BossLocationType.PlagueSlot => Games.FreeEnterprise.Shared.World.Moon,
            BossLocationType.DlunarSlot => Games.FreeEnterprise.Shared.World.Moon,
            BossLocationType.OgopogoSlot => Games.FreeEnterprise.Shared.World.Moon,
            BossLocationType.NotFound => Games.FreeEnterprise.Shared.World.Unknown,
            _ => Games.FreeEnterprise.Shared.World.Main
        };
}
