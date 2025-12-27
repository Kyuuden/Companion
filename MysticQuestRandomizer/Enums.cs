using System.ComponentModel;

namespace FF.Rando.Companion.MysticQuestRandomizer;

public enum SpellType : byte
{
    Aero,
    Fire,
    Blizzard,
    Quake,
    Life,
    Heal,
    Cure,
    Exit,

    Flare = 12,
    Meteor,
    White,
    Thunder
}

public enum KeyItemType : byte
{
    ThunderRock,
    MagicMirror,
    GasMask,
    MultiKey,
    VenusKey,
    WakeWater,
    TreeWither,
    Elixer,

    SkyCoin,
    SunCoin,
    RiverCoin,
    SandCoin,
    MobiusCrest,
    GeminiCrest,
    LibraCrest,
    CapitansCap,

    CompleteSkyCoin
}

public enum ArmorType : byte
{
    MysticRobe,
    RelicArmor,
    GaiasArmor,
    NobleArmor,
    SteelArmor,
    ApolloHelm,
    MoonHelm,
    SteelHelm,

    MagicRing,
    Charm,
    EtherShield,
    AegisShield,
    VenusShield,
    SteelShield,
    BlackRobe,
    FlameArmor,

    CupidLocket = 23
}

public enum WeaponType : byte
{
    CharmClaw,
    CatClaw,
    GiantsAxe,
    BattleAxe,
    Axe,
    Excalibur,
    KnightSword,
    SteelSword,

    MegaGrenade = 12,
    JumboBomb,
    Bomb,
    DragonClaw
}

public enum EquipmentType : byte
{
    Sword, Axe, Bomb, Claw, Armor, Helmet, Shield, Accessory
}

public enum ElementsType
{
    [Description("RTH")]
    Earth,
    [Description("WTR")]
    Water,
    [Description("FIR")]
    Fire,
    [Description("AIR")]
    Air,
    [Description("HLY")]
    Holy,
    [Description("AXE")]
    Axe,
    [Description("BMB")]
    Bomb,
    [Description("PRJ")]
    Projectile,
    [Description("DM ")]
    Doom,
    [Description("STN")]
    Stone,
    [Description("SIL")]
    Silence,
    [Description("BLD")]
    Blind,
    [Description("PSN")]
    Poison,
    [Description("PAR")]
    Paralysis,
    [Description("SLP")]
    Sleep,
    [Description("CON")]
    Confusion
}

public enum ElementsStyle
{
    Text,
    Icons,
    Abbreviations
}

public enum CompanionType
{
    Kaeli,
    Tristam,
    Phoebe,
    Reuben
}