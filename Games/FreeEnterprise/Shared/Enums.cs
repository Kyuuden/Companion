namespace FF.Rando.Companion.Games.FreeEnterprise.Shared;

public enum CharacterType : uint
{
    DarkKnightCecil,
    Kain,
    ChildRydia,
    Tellah,
    Edward,
    Rosa,
    Yang,
    Palom,
    Porom,
    Cecil,
    Cid,
    AdultRydia,
    Edge,
    FuSoYa
}

public enum KeyItemType : uint
{
    Package,
    SandRuby,
    LegendSword,
    BaronKey,
    TwinHarp,
    EarthCrystal,
    MagmaKey,
    TowerKey,
    Hook,
    LucaKey,
    DarknessCrystal,
    RatTail,
    Adamant,
    Pan,
    Spoon,
    PinkTail,
    Crystal,
    Pass
}

public enum BossType : uint
{
    Dmist = 0x00,
    Officer = 0x01,
    Octomamm = 0x02,
    Antlion = 0x03,
    Waterhag = 0x04,
    Mombomb = 0x05,
    FabulGauntlet = 0x06,
    Milon = 0x07,
    Milonz = 0x08,
    Mirrorcecil = 0x09,
    Karate = 0x0B,
    Guard = 0x0A,
    Baigan = 0x0C,
    Kainazzo = 0x0D,
    Darkelf = 0x0E,
    Magus = 0x0F,
    Valvalis = 0x10,
    Calbrena = 0x11,
    Golbez = 0x12,
    Lugae = 0x13,
    Darkimp = 0x14,
    Kingqueen = 0x15,
    Rubicant = 0x16,
    Evilwall = 0x17,
    Asura = 0x18,
    Leviatan = 0x19,
    Odin = 0x1A,
    Bahamut = 0x1B,
    Elements = 0x1C,
    Cpu = 0x1D,
    Paledim = 0x1E,
    Wyvern = 0x1F,
    Plague = 0x20,
    Dlunar = 0x21,
    Ogopogo = 0x22,
    Altgauntlet = 0xFE
}

public enum BossLocationType : uint
{
    DmistSlot = 0x00,
    OfficerSlot = 0x01,
    OctomammSlot = 0x02,
    AntlionSlot = 0x03,
    WaterhagSlot = 0x04,
    MombombSlot = 0x05,
    FabulGauntletSlot = 0x06,
    MilonSlot = 0x07,
    MilonzSlot = 0x08,
    MirrorcecilSlot = 0x09,
    KarateSlot = 0x0A,
    GuardSlot = 0x0B,
    BaiganSlot = 0x0C,
    KainazzoSlot = 0x0D,
    DarkelfSlot = 0x0E,
    MagusSlot = 0x0F,
    ValvalisSlot = 0x10,
    CalbrenaSlot = 0x11,
    GolbezSlot = 0x12,
    LugaeSlot = 0x13,
    DarkimpSlot = 0x14,
    KingqueenSlot = 0x15,
    RubicantSlot = 0x16,
    EvilwallSlot = 0x17,
    AsuraSlot = 0x18,
    LeviatanSlot = 0x19,
    OdinSlot = 0x1A,
    BahamutSlot = 0x1B,
    ElementsSlot = 0x1C,
    CpuSlot = 0x1D,
    PaledimSlot = 0x1E,
    WyvernSlot = 0x1F,
    PlagueSlot = 0x20,
    DlunarSlot = 0x21,
    OgopogoSlot = 0x22,
    NotFound = 0xFF
}

public enum World
{
    Unknown,
    Main,
    Underground,
    Moon
}