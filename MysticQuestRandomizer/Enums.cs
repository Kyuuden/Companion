using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    Flare,
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
    CapitansCap
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

    CupidLocket
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

    MegaGrenade,
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
    Earth,
    Water,
    Fire,
    Air,
    Holy,
    Axe,
    Bomb,
    Projectile,
    Doom,
    Stone,
    Silence,
    Blind,
    Poison,
    Paralysis,
    Sleep,
    Confusion
}

public enum ElementsStyle
{
    Text,
    Icons
}

public enum CompanionType
{
    Kaeli,
    Tristam,
    Phoebe,
    Reuben
}