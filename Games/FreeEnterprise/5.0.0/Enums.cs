namespace FF.Rando.Companion.Games.FreeEnterprise._5._0._0;

public enum RewardSlot : uint
{
    None = 0x00,
    StartingCharacter = 0x01,
    StartingPartnerCharacter = 0x02,
    MistCharacter = 0x03,
    WateryPassCharacter = 0x04,
    DamcyanCharacter = 0x05,
    KaipoCharacter = 0x06,
    HobsCharacter = 0x07,
    MysidiaCharacter1 = 0x08,
    MysidiaCharacter2 = 0x09,
    OrdealsCharacter = 0x0A,
    BaronInnCharacter = 0x0D,
    BaronCastleCharacter = 0x0E,
    ZotCharacter1 = 0x0F,
    ZotCharacter2 = 0x10,
    DwarfCastleCharacter = 0x11,
    CaveEblanCharacter = 0x12,
    LunarPalaceCharacter = 0x13,
    GiantCharacter = 0x14,
    StartingItem = 0x20,
    AntlionItem = 0x21,
    FabulItem = 0x22,
    OrdealsItem = 0x23,
    BaronInnItem = 0x24,
    BaronCastleItem = 0x25,
    ToroiaHospitalItem = 0x26,
    MagnesItem = 0x27,
    ZotItem = 0x28,
    BabilBossItem = 0x29,
    CannonItem = 0x2A,
    LucaItem = 0x2B,
    SealedCaveItem = 0x2C,
    RatTradeItem = 0x2E,
    FoundYangItem = 0x2F,
    PanTradeItem = 0x30,
    FeymarchQueenItem = 0x31,
    FeymarchKingItem = 0x32,
    BaronThroneItem = 0x33,
    SylphItem = 0x34,
    BahamutItem = 0x35,
    LunarBoss1Item = 0x36,
    LunarBoss2Item = 0x37,
    LunarBoss3Item = 0x38,
    LunarBoss5Item = 0x3B,
    RydiasMomItem = 0x3C,
    FallenGolbezItem = 0x3D,
    ForgeItem = 0x3E,
    PinkTradeItem = 0X3F
}

public enum ChestSlot : uint
{
    Feymarch = 0x13D,
    RibbonRoom1 = 0x19F,
    RibbonRoom2 = 0x1A0,

    ZotChest = 0xC4, // 0x1C3
    EblanChest1 = 0x79, // 0x1C0
    EblanChest2 = 0x83, // 0x1C1
    EblanChest3 = 0x87, // 0x1C2
    LowerBabilChest1 = 0x112, // 0x1E0
    LowerBabilChest2 = 0x113, // 0x1E1
    LowerBabilChest3 = 0x114, // 0x1E2
    LowerBabilChest4 = 0x115, // 0x1E3
    CaveEblanChest = 0xEA, // 0x1C6
    UpperBabilChest = 0xCB, // 0x1C4
    CaveOfSummonsChest = 0x135, // 0x1E4
    SylphCaveChest1 = 0x159, // 0x1E5
    SylphCaveChest2 = 0x15E, // 0x1E6
    SylphCaveChest3 = 0x15F, // 0x1E6
    SylphCaveChest4 = 0x160, // 0x1E6
    SylphCaveChest5 = 0x161, // 0x1E7
    SylphCaveChest6 = 0x162, // 0x1E8
    SylphCaveChest7 = 0x163, // 0x1E9
    GiantChest = 0xD6, // 0x1C7
    LunarPathChest = 0x17D, // 0x1EA
    LunarCoreChest1 = 0x180, // 0x1EB
    LunarCoreChest2 = 0x182, // 0x1EC
    LunarCoreChest3 = 0x188, // 0x1F3
    LunarCoreChest4 = 0x189, // 0x1EE
    LunarCoreChest5 = 0x18A, // 0x1F4
    LunarCoreChest6 = 0x18B, // 0x1F0
    LunarCoreChest7 = 0x18C, // 0x1F1
    LunarCoreChest8 = 0x18D, // 0x1F2
    LunarCoreChest9 = 0x19E, // 0x1ED
}

public enum Shops : byte
{
    KaipoWeaponShop = 0x00,
    FabulWeaponArmorShop = 0x01,
    MysidiaWeaponShop = 0x02,
    BaronWeaponShop = 0x03,
    ToroiaWeaponShop = 0x04,
    SilveraWeaponShop = 0x05,
    DwarfCastleWeaponShop = 0x06,
    CaveEblanWeaponShop = 0x07,
    TomraWeapon = 0x08,
    SmithyShop = 0x09,
    KaipoArmorShop = 0x0A,
    MysidiaArmorShop = 0x0B,
    BaronArmorShop = 0x0C,
    ToroiaArmorShop = 0x0D,
    SilveraArmorShop = 0x0E,
    DwarfCastleArmorShop = 0x0F,
    CaveEblanArmorShop = 0x10,
    TomraArmorShop = 0x11,
    MoonItemShop = 0x12,
    BaronItemShop = 0x13,
    MysidiaItemShop = 0x14,
    SilveraItemShop = 0x15,
    MistWeaponShop = 0x16,
    MistArmorShop = 0x17,
    CaveEblanItemShop = 0x18,
    AgartWeaponShop = 0x1A,
    AgartArmorShop = 0x1B,
    FeymarchWeaponShop = 0x1C,
    FeymarchArmorShop = 0x1D,
    TomraItemShop = 0x1E,
    ToroiaCafeItemShop = 0x1F,
    KaipoItemShop = 0x20,
    FabulItemShop = 0x21,
    ToroiaItemShop = 0x22,
    AgartItemShop = 0x23,
    DwarfCastleItemShop = 0x24,
    FeymarchItemShop = 0x25,
}

public enum ObjectiveXpBonus : ushort
{
    None = 0,
    _2Percent = 1,
    _3Percent = 2,
    _5Percent = 4,
    _8Percent = 8,
    _10Percent = 16,
    _12Percent = 32,
    _14Percent = 64,
    _16Percent = 128,
    _20Percent = 256,
    _25Percent = 512,
    _33Percent = 1024,
}


public enum KeyItemCheckXpBonus : byte
{
    None = 0,
    _1Percent = 1,
    _2Percent = 2,
    _3Percent = 4,
    _4Percent = 8,
    _5Percent = 16,
    _8Percent = 32,
    _10Percent = 64,
}

public enum KeyItemZonkXpBonus : byte
{
    None = 0,
    _1Percent = 1,
    _2Percent = 2,
    _3Percent = 4,
    _4Percent = 8,
    _5Percent = 16,
    _8Percent = 32,
    _10Percent = 64,
}

public enum MaxXpRate : ushort
{
    Unlimited = 0,
    _50Percent = 1,
    _75Percent = 2,
    _100Percent = 4,
    _150Percent = 8,
    _200Percent = 16,
    _250Percent = 32,
    _300Percent = 64,
    _400Percent = 128,
    _500Percent = 256,
    _600Percent = 512,
    _700Percent = 1024,
    _800Percent = 2048,
    _1000Percent = 4096,
}

public enum XPBonusMode : byte
{
    Default = 0,
    Additive = 1,
    Multiplicative = 2
}
