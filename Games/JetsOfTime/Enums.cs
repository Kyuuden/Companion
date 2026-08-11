using System.ComponentModel;

namespace FF.Rando.Companion.Games.JetsOfTime;

public enum TimePeriodType : byte
{
    [Description("65,000,000 BC")]
    Prehistory,
    [Description("12,000 BC")]
    DarkAges,
    [Description("12,000 BC - Kingdom of Zeal")]
    KingdomOfZeal,
    [Description("600 AD")]
    MiddleAges,
    [Description("1000 AD")]
    Present,
    [Description("2300 AD")]
    Future,
    [Description("End of Time")]
    EndOfTime
}

public enum KeyItemType : byte
{
    Magic,

    [Description("Melichor")]
    Masamune,

    Pendant = 0xD6,

    [Description("Gate Key")]
    GateKey = 0xD7,

    [Description("Bent Sword")]
    BentSword = 0x50,

    [Description("Bent Hilt")]
    BentHilt = 0x51,

    [Description("Hero's Medal")]
    HerosMedal = 0xB3,

    [Description("Dream Stone")]
    Dreamstone = 0xDC,

    [Description("Robo's Ribbon")]
    RobosRibbon = 0xB8,

    [Description("Prisim Shard")]
    PrismShard = 0xD8,

    Jerky = 0xDB,

    [Description("Sun Stone")]
    SunStone = 0xDF,

    [Description("Moon Stone")]
    MoonStone = 0xDE,

    Clone = 0xE2,

    [Description("Chrono Trigger")]
    ChronoTrigger = 0xD9,

    [Description("Toma's Pop")]
    TomasPop = 0xE3,

    [Description("Masamune")]
    GrandLeon = 0x42,
    RubyKnife = 0xE0,

    [Description("Jets of Time")]
    JetsOfTime = 0xE9,

    Tools
}

public enum CharacterType
{
    Chrono, Marle, Lucca, Robo, Frog, Ayla, Magus
}

public enum PortraitType
{
    Chrono, Marle, Lucca, Robo, Frog, Ayla, Magus, Epoch
}

public enum BossType
{
    Yakra,
    [Description("Yakra XIII")]
    YakraXIII,
    [Description("Dragon Tank")]
    DragonTank, 
    Guardian,
    [Description("R-Series")]
    RSeries, 
    Heckran,
    Zombor, 
    Retinite,
    [Description("Masa & Mune")]
    MasaAndMune, 
    Nizbel,
    Magus,
    [Description("Black Tyrano")]
    BlackTyrano,
    [Description("Rust Tyrano")]
    RustTyrano,
    [Description("Giga Gaia")]
    GigaGaia,
    [Description("Mother Brain")]
    MotherBrain,
    [Description("Son of Sun")]
    SunOfTheSun, 
    Zeal,
    Golem
}

public enum NPCType
{
    Melchior = 0x00,
    King_Guardia_XXXIII_1000AD = 0x01,
    Johnny = 0x02,
    Queen_Leene = 0x03,
    Tata = 0x04,
    Toma = 0x05,
    Kino = 0x06,
    Chancellor_Green_1000AD = 0x07,
    Dactyl = 0x08,
    Schala = 0x09,
    Janus = 0x0A,
    Chancellor_Brown_600AD = 0x0B,
    Belthasar = 0x0C,
    Middle_Ages_or_Present_Age_villager_Woman = 0x0D,
    Middle_Ages_or_Present_Age_villager_Young_man = 0x0E,
    Middle_Ages_or_Present_Age_villager_Young_woman = 0x0F,
    Middle_Ages_or_Present_Age_villager_Soldier = 0x10,
    Middle_Ages_or_Present_Age_villager_Old_man = 0x11,
    Middle_Ages_or_Present_Age_villager_Old_woman = 0x12,
    Middle_Ages_or_Present_Age_villager_Little_boy = 0x13,
    Middle_Ages_or_Present_Age_villager_Little_girl = 0x14,
    Middle_Ages_or_Present_Age_villager_Waitress = 0x15,
    Middle_Ages_or_Present_Age_villager_Shopkeeper = 0x16,
    Nun = 0x17,
    Knight_Captain_600AD = 0x18,
    Middle_Ages_or_Present_Age_villager_man = 0x19,
    Dome_survivor_man = 0x1A,
    Dome_survivor_woman = 0x1B,
    Doan = 0x1C,
    Dome_survivor_little_girl = 0x1D,
    Prehistoric_villager_man_with_club = 0x1E,
    Prehistoric_villager_woman_in_green_dress = 0x1F,
    Prehistoric_villager_little_girl = 0x20,
    Prehistoric_villager_old_man = 0x21,
    Zeal_citizen_man = 0x22,
    Zeal_citizen_woman = 0x23,
    Zeal_citizen_researcher_with_glasses = 0x24,
    Cronos_mom = 0x25,
    Middle_Ages_or_Present_Age_villager_little_girl_with_purple_hair = 0x26,
    //Middle_Ages_or_Present_Age_villager_man = 0x27,
    Middle_Ages_or_Present_Age_villager_woman_with_purple_hair = 0x28,
    Middle_Ages_or_Present_Age_villager_young_man = 0x29,
    Middle_Ages_or_Present_Age_villager_young_woman = 0x2A,
    Middle_Ages_or_Present_Age_villager_soldier = 0x2B,
    Middle_Ages_or_Present_Age_villager_old_man = 0x2C,
    Middle_Ages_or_Present_Age_villager_old_woman = 0x2D,
    Middle_Ages_or_Present_Age_villager_little_boy = 0x2E,
    Middle_Ages_or_Present_Age_villager_little_girl = 0x2F,
    Middle_Ages_or_Present_Age_villager_waitress_with_purple_hair = 0x30,
    Middle_Ages_or_Present_Age_villager_shopkeeper = 0x31,
    //Nun = 0x32,
    Guardia_knight_600AD = 0x33,
    //Middle_Ages_or_Present_Age_villager_man = 0x34,
    Cyrus = 0x35,
    Young_Glenn = 0x36,
    King_Guardia_XXI_600AD = 0x37,
    Strength_Test_Machine_part_Millennial_Fair = 0x38,
    Middle_Ages_or_Present_Age_villager_old_man_2C_dupe_UNUSED = 0x39,
    //Zeal_citizen_researcher_with_glasses = 0x3A,
    Cat = 0x3B,
    False_prophet_Magus = 0x3C,
    Melchior_in_gray_robe_UNUSED = 0x3D,
    Prehistoric_villager_man_carrying_club_with_purple_hair = 0x3E,
    Prehistoric_villager_woman_with_purple_hair = 0x3F,
    Prehistoric_villager_little_girl_with_purple_hair = 0x40,
    Algetty_earthbound_one_man = 0x41,
    Algetty_earthbound_one_woman = 0x42,
    Algetty_earthbound_one_old_man = 0x43,
    Algetty_earthbound_one_child = 0x44,
    Princess_Nadia = 0x45,
    Guardia_Castle_chef = 0x46,
    Trial_judge = 0x47,
    Gaspar = 0x48,
    Fiona = 0x49,
    Queen_Zeal = 0x4A,
    Guard_enemy = 0x4B,
    Reptite = 0x4C,
    Kilwala = 0x4D,
    Blue_imp = 0x4E,
    //Middle_Ages_or_Present_Age_villager_man = 0x4F,
    Middle_Ages_or_Present_Age_villager_woman = 0x50,
    GI_Jogger = 0x51,
    Millennial_Fair_visitor_old_man = 0x52,
    Millennial_Fair_visitor_woman = 0x53,
    Millennial_Fair_visitor_little_boy = 0x54,
    Millennial_Fair_visitor_little_girl = 0x55,
    Lightning_bolt = 0x56,
    Opened_time_portal_upper_half = 0x57,
    Opened_time_portal_lower_half = 0x58,
    Millennial_Fair_shopkeeper = 0x59,
    Guillotine_blade = 0x5A,
    Guillotine_chain = 0x5B,
    Conveyor_machine = 0x5C,
    Tombstone = 0x5D,
    Giant_soup_bowl = 0x5E,
    Magus_statue = 0x5F,
    Dreamstone = 0x60,
    Gate_Key = 0x61,
    Soda_can = 0x62,
    Pendant = 0x63,
    Poyozo_doll = 0x64,
    Pink_lunch_bag = 0x65,
    UNUSED = 0x66,
    Red_knife = 0x67,
    Broken_Masamune_blade = 0x68,
    Slice_of_cake = 0x69,
    Trash_can_on_its_side = 0x6A,
    Piece_of_cheese = 0x6B,
    Barrel = 0x6C,
    //UNUSED = 0x6D,
    Dead_sunstone = 0x6E,
    Metal_mug = 0x6F,
    Blue_star = 0x70,
    Giant_blue_star = 0x71,
    Red_flame = 0x72,
    Giant_red_flame = 0x73,
    Explosion_ball = 0x74,
    Giant_explosion_ball = 0x75,
    Smoke_trail = 0x76,
    Heros_medal = 0x77,
    Balcony_shadow = 0x78,
    Save_point = 0x79,
    Prehistoric_villager_drummer = 0x7A,
    Prehistoric_villager_log_drummer = 0x7B,
    White_explosion_outline = 0x7C,
    Leenes_bell = 0x7D,
    Bat_hanging_upside_down = 0x7E,
    Computer_screen = 0x7F,
    Water_splash = 0x80,
    Explosion = 0x81,
    Robo_power_up_sparks = 0x82,
    Leaves_falling = 0x83,
    coins_spinning = 0x84,
    Hole_in_the_ground = 0x85,
    Cooking_smoke = 0x86,
    Small_explosion_clouds = 0x87,
    Wind_element_spinning = 0x88,
    Water_element = 0x89,
    Dirt_mound = 0x8A,
    Masamune_Spinning = 0x8B,
    Music_note = 0x8C,
    Small_fish = 0x8D,
    //Water_splash = 0x8E,
    //Lightning_bolt = 0x8F,
    //UNUSED = 0x90,
    //UNUSED = 0x91,
    Small_rock = 0x92,
    Rainbow_shell = 0x95,
    Shadow_beds = 0x96,
    Closed_portal = 0x97,
    Balloon = 0x98,
    Light_green_bush = 0x99,
    Shadow_on_the_ground = 0x9A,
    Brown_dreamstone = 0x9B,
    Crane_machine = 0x9C,
    //UNUSED = 0x9D,
    Dripping_water = 0x9E,
    Cupboard_doors = 0x9F,
    Brown_stones = 0xA0,
    Dark_green_bush = 0xA1,
    Journal = 0xA2,
    Norstein_Bekkler = 0xA3,
    Rat = 0xA4,
    Sparks_from_guillotine_blade = 0xA5,
    Zeal_teleporter = 0xA6,
    Ocean_palace_teleporter = 0xA7,
    Truce_Dome_director = 0xA8,
    Epoch_seats = 0xA9,
    Robot = 0xAA,
    Red_star = 0xAB,
    Sealed_portal = 0xAC,
    Animated_Zz_sleeping_icon = 0xAD,
    Flying_map_Epoch = 0xAE,
    Gray_cat = 0xAF,
    Yellow_cat = 0xB0,
    Alfador = 0xB1,
    Time_egg = 0xB2,
    Zeal_citizen_man_cast_ending = 0xB3,
    //Zeal_citizen_woman = 0xB4,
    Potted_plant = 0xB5,
    Kid_with_purple_hair_Glenn_Cyrus_cutscene = 0xB6,
    Sealed_chest = 0xB7,
    Squirrel_Programmers_Ending = 0xB8,
    Blue_poyozo = 0xB9,
    Stone_rubble_pile = 0xBA,
    Rusted_Robo = 0xBB,
    Gaspar_Gurus_cutscene = 0xBC,
    //UNUSED = 0xBD,
    Orange_cat = 0xBE,
    //Middle_Ages_or_Present_Age_villager_little_boy = 0xBF,
    //Middle_Ages_or_Present_Age_villager_little_girl = 0xC0,
    Spinning_water_element = 0xC1,
    Blue_shining_star_small = 0xC2,
    Blue_shining_star_large = 0xC3,
    Multiple_balloons = 0xC4,
    Dancing_woman_Millennial_Fair_ending = 0xC5,
    //Millennial_Fair_visitor_little_girl = 0xC6,
    Silver_Leenes_bell = 0xC7,
    Figure_atop_Magus_Castle = 0xC8,
    Serving_tray_with_drinks = 0xC9,
    THE_END_text = 0xCA,
    Human_Glenn = 0xCB,
    Queen_Zeal_Death_Peak = 0xCC,
    Schala_Death_Peak = 0xCD,
    Lavos_Death_Peak = 0xCE,
    Crono_Death_Peak = 0xCF,
    Hironobu_Sakaguchi = 0xD0,
    Yuji_Horii = 0xD1,
    Akira_Toriyama = 0xD2,
    Kazuhiko_Aoki = 0xD3,
    Lightning_flash = 0xD4,
    Lara = 0xD5,
    Purple_explosion = 0xD6,
    Cronos_mom_Millennial_Fair = 0xD7,
    //UNUSED = 0xD8,
    //UNUSED = 0xD9,
    //UNUSED = 0xDA,
    //UNUSED = 0xDB,
    //UNUSED = 0xDC,
    //UNUSED = 0xDD,
    //UNUSED = 0xDE,
    //UNUSED = 0xDF,
    Green_balloon = 0xE0,
    Yellow_balloon = 0xE1,
    Blue_balloon = 0xE2,
    Pink_balloon = 0xE3,
    Brown_glowing_light = 0xE4,
    Yellow_glowing_light = 0xE5,
    Purple_glowing_light = 0xE6,
    Blue_glowing_light = 0xE7,
    //UNUSED = 0xE8,
    //UNUSED = 0xE9,
    //UNUSED = 0xEA,
    //UNUSED = 0xEB,
    //UNUSED = 0xEC,
    //UNUSED = 0xED,
    //UNUSED = 0xEE,
    //UNUSED = 0xEF,
    //UNUSED = 0xF0,
    //UNUSED = 0xF1,
    //UNUSED = 0xF2,
    //UNUSED = 0xF3,
    //UNUSED = 0xF4,
    //UNUSED = 0xF5,
    //UNUSED = 0xF6,
    //UNUSED = 0xF7,
    //UNUSED = 0xF8,
    //UNUSED = 0xF9,
    //UNUSED = 0xFA,
    //UNUSED = 0xFB,
    //UNUSED = 0xFC,
    //UNUSED = 0xFD,
    //UNUSED = 0xFE,
    //UNUSED = 0xFF,
}

public enum MonsterType
{
    Nu = 000,
    Terrasaur = 002,
    Krawlie = 004,
    Hench = 005,
    Omicrone = 006,
    Martello = 007,
    BellBird = 008,
    Panel = 009,
    MammonMachine = 010,
    NONAME = 011,
    GreenImp = 013,
    StoneImp = 014,
    MudImp = 015,
    Roly = 016,
    Poly = 017,
    Rolypoly = 018,
    RolyRider = 019,
    LavosSupportMonster20 = 020,
    BlueEaglet = 021,
    GoldEaglet = 022,
    RedEaglet = 023,
    LavosSupportMonster24 = 024,
    AvianChaos = 025,
    ImpAce = 026,
    BantamImp = 027,
    Gnasher = 028,
    Gnawer = 029,
    NagaEtte = 030,
    LavosSupportMonster31 = 031,
    Ruminator = 032,
    LavosSupportMonster33 = 033,
    Octopod = 034,
    Octoblush = 035,
    Octobino = 036,
    QueenZeal = 037,
    FlyTrap = 038,
    MeatEater = 039,
    ManEater = 040,
    Krakker = 041,
    Egder = 042,
    Defunct = 043,
    Departed = 044,
    Deceased = 045,
    Decedent = 046,
    Macabre = 047,
    Reaper = 048,
    Guard1000AD = 049,
    Sentry = 050,
    FreeLancer = 051,
    Outlaw = 052,
    GigaMutant = 053,
    GigaMutant_BottomHalf = 054,
    TerraMutant = 055,
    TerraMutant_BottomHalf = 056,
    Juggler = 057,
    Retinite = 058,
    Mage = 059,
    LavosSupportMonster60 = 060,
    Reptite_Purple = 061,
    BlueShield = 062,
    YoduDe = 063,
    Incognito = 064,
    PeepingDoom = 065,
    Sidekick = 065,
    BossOrb = 066,
    JinnBottle = 069,
    EvilWeevil = 070,
    Tempurite = 071,
    Diablos = 072,
    Gargoyle = 073,
    Grimalkin = 074,
    //Hench = 075,
    Tpole = 076,
    Croaker = 077,
    Amphibite = 078,
    Bullfrog = 078,
    Rainfrog = 078,
    MadBat = 080,
    Vamp = 081,
    Scouter = 082,
    Flyclops = 083,
    Bugger = 084,
    Debugger = 085,
    Debuggest = 085,
    Sorceror = 086,
    Jinn = 087,
    Barghest = 088,
    LavosSupportMonster90 = 090,
    Crater = 091,
    Volcano = 092,
    Shitake = 093,
    Hetake = 094,
    Rubble = 095,
    LavosSupportMonster96 = 096,
    Shist = 097,
    Pahoehoe = 098,
    Nereid = 099,
    FakeSavePoint = 100,
    Mohaver = 101,
    Shadow = 102,
    LavosSupportMonster103 = 103,
    Base = 104,
    Acid = 105,
    Alkaline = 106,
    Ion = 107,
    Anion = 108,
    Thrasher = 109,
    LavosSpawn_Eye = 110,
    LavosSpawn_Shell = 111,
    Lasher = 112,
    Goblin = 113,
    Ogre = 114,
    CaveBat = 115,
    Ogan = 116,
    Flunky = 117,
    Groupie = 118,
    LavosSupportMonster119 = 119,
    LavosSupportMonster120 = 120,
    WingedApe = 121,
    CaveApe = 122,
    Megasaur = 123,
    Omnicrone = 124,
    Beast = 125,
    BlueBeast = 126,
    RedBeast = 126,
    Turret = 128,
    Lizardactyl = 129,
    FakeNu = 130,
    AvianRex = 131,
    Blob = 132,
    Gremlin = 134,
    Rat = 134,
    Runner = 136,
    Proto2 = 137,
    Proto3 = 138,
    Proto4 = 139,
    Bug = 140,
    Beetle = 141,
    Goon = 142,
    Cyrus = 143,
    Yakra = 144,
    Gato = 146,
    DragonTank = 147,
    DragonTankWheel = 148,
    Golem = 149,
    Synchrite = 150,
    Masa = 151,
    Mune = 152,
    Masamune = 153,
    Azala = 154,
    Nizbel = 155,
    NizbelII = 156,
    Slash_unarmed = 157,
    Slash_armed = 158,
    Flea = 159,
    FleaPlus = 160,
    Dalton = 161,
    DaltonPlus = 162,
    Mutant = 163,
    MetalMute = 164,
    SuperSlash = 165,
    Ozzie = 166,
    Ozzie2 = 167,
    GreatOzzie = 168,
    Heckran = 169,
    Gigasaur = 170,
    Leaper = 171,
    FossilApe = 172,
    DragonTankHead = 173,
    FrogKing = 174,
    Octorider = 175,
    Zeal = 176,
    ZealHand_Left = 177,
    ZealHand_Right = 178,
    Zombor_LowerHalf = 179,
    Zombor_UpperHalf = 180,
    Zombor_SandMonster_LowerHalf = 181,
    Zombor_SandMonster_UpperHalf = 182,
    Display = 183,
    MegaMutant = 184,
    MegaMutant_BottomHalf = 185,
    //SuperSlash = 186,
    //FleaPlus = 187,
    BlackTyrano = 188,
    RustTyrano = 189,
    Motherbrain = 190,
    Motherbrain_Body = 191,
    AtroposXR = 192,
    Cybot = 193,
    Lavos1 = 194,
    Lavos2 = 195,
    Lavos3 = 196,
    Lavos4 = 197,
    Lavos5 = 198,
    YakraXIII = 199,
    Tubster = 200,
    Lavos6 = 201,
    Lavos7 = 202,
    Lavos_SecondForm_MainBody = 203,
    Lavos_SecondForm_Arm1 = 204,
    Lavos_SecondForm_Arm2 = 205,
    LavosCore_MainBody = 206,
    Bit = 207,
    Byte = 208,
    GigaGaia = 209,
    GigaGaia_Arm1 = 210,
    GigaGaia_Arm2 = 211,
    Guardian = 212,
    Red_Scout = 213,
    Blue_Scout = 214,
    //LavosSpawn_Shell = 215,
    //LavosSpawn_Eye = 216,
    LaserGuard = 217,
    Lavos_Support_Monster218 = 218,
    Lavos_Support_Monster219 = 219,
    Lavos_Support_Monster221 = 221,
    Lavos_Support_Monster222 = 222,
    Lavos_Support_Monster223 = 223,
    Spekkio_Frog = 224,
    Spekkio_Kilwala = 225,
    Spekkio_Blue_Goblin = 226,
    Spekkio_Omnicrone = 227,
    Spekkio_Blue_Masamune = 228,
    Spekkio_Pink_Nu = 229,
    Lavos = 230,
    //Lavos = 231,
    //Lavos = 232,
    //Lavos = 233,
    //Lavos = 234,
    //Alien = 235,
    //Lavos = 235,
    //Lavos = 236,
    Lavos_Bit1 = 237,
    Hexapod = 238,
    Lavos_Bit2 = 239,
    //Flea? = 240,
    Unknown = 241,
    RolyBomber = 242,
    GolemBoss = 243,
    Johnny = 244,
    Basher = 245,
    BossOrb2 = 246,
    SonofSunFlame = 247,
    RSeries = 248,
    Magus = 249,
    Magus2 = 250,
    Magus3 = 251,
    Crono = 252,
    //Crono (Glitched) = 253,
    //Crono (Glitched) = 254,
    //Crono = 255,
}

public enum LocationType : ushort
{
    [Description("Load Screen")]
    LoadScreen = 0x000,
    [Description("Crono's Kitchen")]
    CronosKitchen = 0x001,
    [Description("Crono's Room")]
    CronosRoom = 0x002,
    [Description("Lucca's Kitchen")]
    LuccasKitchen = 0x003,
    [Description("Lucca's Workshop")]
    LuccasWorkshop = 0x004,
    [Description("Millenial Fair")]
    MillenialFair = 0x005,
    [Description("Gato's Exhibit")]
    GatosExhibit = 0x006,
    [Description("Prehistoric Exhibit")]
    PrehistoricExhibit = 0x007,
    [Description("Telepod Exhibit")]
    TelepodExhibit = 0x008,
    [Description("Lara's Room")]
    LarasRoom = 0x009,
    [Description("Lucca's Room")]
    LuccasRoom = 0x00A,
    [Description("Ending Selector")]
    EndingSelector = 0x00B,
    [Description("Truce Inn (Present)")]
    TruceInn_Present = 0x00C,
    [Description("Truce Mayor's Manor 1F")]
    TruceMayorsManor1F = 0x00D,
    [Description("Truce Mayor's Manor 2F")]
    TruceMayorsManor2F = 0x00E,
    [Description("Truce Single Woman Residence")]
    TruceSingleWomanResidence = 0x00F,
    [Description("Truce Happy Screaming Couple Residence")]
    TruceHappyScreamingCoupleResidence = 0x010,
    [Description("Truce Market (Present)")]
    TruceMarket_Present = 0x011,
    [Description("Truce Ticket Office")]
    TruceTicketOffice = 0x012,
    [Description("Guardia Forest (Present)")]
    GuardiaForest_Present = 0x013,
    [Description("Guardia Forest Dead End")]
    GuardiaForestDeadEnd = 0x014,
    [Description("Guardia Throneroom (Present)")]
    GuardiaThroneroom_Present = 0x015,
    [Description("King's Chamber (Present)")]
    KingsChamber_Present = 0x016,
    [Description("Queen's Chamber (Present)")]
    QueensChamber_Present = 0x017,
    [Description("Guardia Kitchen (Present)")]
    GuardiaKitchen_Present = 0x018,
    [Description("Guardia Barracks (Present)")]
    GuardiaBarracks_Present = 0x019,
    [Description("Guardia Basement")]
    GuardiaBasement = 0x01A,
    [Description("Courtroom")]
    Courtroom = 0x01B,
    [Description("Prison Catwalks")]
    PrisonCatwalks = 0x01C,
    [Description("Prison Supervisor's Office")]
    PrisonSupervisorsOffice = 0x01D,
    [Description("Prison Torture Storage Room")]
    PrisonTortureStorageRoom = 0x01E,
    [Description("Medina Square")]
    MedinaSquare = 0x01F,
    [Description("Zenan Bridge (Present)")]
    ZenanBridge_Present = 0x020,
    [Description("Medina Elder's House 1F")]
    MedinaEldersHouse1F = 0x021,
    [Description("Medina Elder's House 2F")]
    MedinaEldersHouse2F = 0x022,
    [Description("Medina Inn")]
    MedinaInn = 0x023,
    [Description("Medina Portal")]
    MedinaPortal = 0x024,
    [Description("Medina Market")]
    MedinaMarket = 0x027,
    [Description("Melchior's Kitchen")]
    MelchiorsKitchen = 0x028,
    [Description("Melchior's Workshop")]
    MelchiorsWorkshop = 0x029,
    [Description("Forest Ruins")]
    ForestRuins = 0x02A,
    [Description("Leene Square (Future)")]
    LeeneSquare_Future = 0x02C,
    [Description("Heckran Cave Passageways")]
    HeckranCavePassageways = 0x02F,
    [Description("Heckran Cave Entrance")]
    HeckranCaveEntrance = 0x030,
    [Description("Heckran Cave Underground River")]
    HeckranCaveUndergroundRiver = 0x031,
    [Description("Porre Mayor's Manor 1F (Present)")]
    PorreMayorsManor1F_Present = 0x032,
    [Description("Porre Mayor's Manor 2F (Present)")]
    PorreMayorsManor2F_Present = 0x033,
    [Description("Porre Residence (Present)")]
    PorreResidence_Present = 0x034,
    [Description("Snail Stop")]
    SnailStop = 0x035,
    [Description("Porre Market (Present)")]
    PorreMarket_Present = 0x036,
    [Description("Porre Inn (Present)")]
    PorreInn_Present = 0x037,
    [Description("Porre Ticket Office")]
    PorreTicketOffice = 0x038,
    [Description("Fiona's Shrine")]
    FionasShrine = 0x039,
    [Description("Choras Mayor's Manor 1F")]
    ChorasMayorsManor1F = 0x03A,
    [Description("Choras Mayor's Manor 2F")]
    ChorasMayorsManor2F = 0x03B,
    [Description("Choras Carpenter's Residence (Present)")]
    ChorasCarpentersResidence_Present = 0x03D,
    [Description("Choras Inn (Present)")]
    ChorasInn_Present = 0x03E,
    [Description("West Cape")]
    WestCape = 0x03F,
    [Description("Sun Keep (Present)")]
    SunKeep_Present = 0x040,
    [Description("Northern Ruins Entrance (Present)")]
    NorthernRuinsEntrance_Present = 0x041,
    [Description("Northern Ruins Basement Corridor (Present)")]
    NorthernRuinsBasementCorridor_Present = 0x042,
    [Description("Northern Ruins Landing (Present)")]
    NorthernRuinsLanding_Present = 0x043,
    [Description("Northern Ruins Antechamber (Present)")]
    NorthernRuinsAntechamber_Present = 0x044,
    [Description("Northern Ruins Vestibule (Present)")]
    NorthernRuinsVestibule_Present = 0x045,
    [Description("Northern Ruins Back Room (Present)")]
    NorthernRuinsBackRoom_Present = 0x046,
    [Description("Prison Cells")]
    PrisonCells = 0x047,
    [Description("Prison Stairwells")]
    PrisonStairwells = 0x048,
    [Description("Northern Ruins Hero's Grave (Present)")]
    NorthernRuinsHerosGrave_Present = 0x049,
    [Description("Unknown")]
    Unknown = 0x04A,
    [Description("Prison Exterior")]
    PrisonExterior = 0x04B,
    [Description("Fiona's Forest Recriminations")]
    FionasForestRecriminations = 0x053,
    [Description("The End")]
    TheEnd = 0x058,
    [Description("Black Omen 98F Astral Progeny")]
    BlackOmenLavosSpawn = 0x060,
    [Description("Black Omen 3F Teleporter (no exits)")]
    BlackOmen3FTeleporter_noexits = 0x061,
    [Description("Black Omen 45F Teleporter")]
    BlackOmen45FTeleporter = 0x062,
    [Description("Black Omen Platform (no exit)")]
    BlackOmenPlatform_noexit = 0x063,
    [Description("Black Omen Platform Shaft (Downward)")]
    BlackOmenPlatformShaft_Downward = 0x064,
    [Description("Black Omen Platform Shaft (Upward)")]
    BlackOmenPlatformShaft_Upward = 0x065,
    [Description("Black Omen Celestial Gate")]
    BlackOmenCelestialGate = 0x06B,
    [Description("Lucca Explains Paradoxes")]
    LuccaExplainsParadoxes = 0x06C,
    [Description("Ancient Tyrano Lair")]
    AncientTyranoLair = 0x06D,
    [Description("Ancient Tyrano Lair Traps")]
    AncientTyranoLairTraps = 0x06E,
    [Description("Ancient Tyrano Lair Nizbel's Room")]
    AncientTyranoLairNizbelsRoom = 0x06F,
    [Description("Truce Canyon")]
    TruceCanyon = 0x070,
    [Description("Truce Canyon Portal")]
    TruceCanyonPortal = 0x071,
    [Description("Truce Couple's Residence (Middle Ages)")]
    TruceCouplesResidence_MiddleAges = 0x072,
    [Description("Truce Smithy's Residence")]
    TruceSmithysResidence = 0x073,
    [Description("Truce Inn 1F (Middle Ages)")]
    TruceInn1F_MiddleAges = 0x074,
    [Description("Truce Inn 2F (Middle Ages)")]
    TruceInn2F_MiddleAges = 0x075,
    [Description("Truce Market (Middle Ages)")]
    TruceMarket_MiddleAges = 0x076,
    [Description("Guardia Forest (Middle Ages)")]
    GuardiaForest_MiddleAges = 0x077,
    [Description("Guardia Throneroom (Middle Ages)")]
    GuardiaThroneroom_MiddleAges = 0x078,
    [Description("Guardia King's Chamber (Middle Ages)")]
    GuardiaKingsChamber_MiddleAges = 0x079,
    [Description("Guardia Queen's Chamber (Middle Ages)")]
    GuardiaQueensChamber_MiddleAges = 0x07A,
    [Description("Guardia Kitchen (Middle Ages)")]
    GuardiaKitchen_MiddleAges = 0x07B,
    [Description("Guardia Barracks (Middle Ages)")]
    GuardiaBarracks_MiddleAges = 0x07C,
    [Description("Castle Magus Doppleganger Corridor")]
    CastleMagusDopplegangerCorridor = 0x07D,
    [Description("Geno Dome Main Conveyor")]
    GenoDomeMainConveyor = 0x07E,
    [Description("Geno Dome Elevator")]
    GenoDomeElevator = 0x07F,
    [Description("Geno Dome Long Corridor")]
    GenoDomeLongCorridor = 0x080,
    [Description("Manoria Sanctuary")]
    ManoriaSanctuary = 0x081,
    [Description("Manoria Main Hall")]
    ManoriaMainHall = 0x082,
    [Description("Manoria Headquarters")]
    ManoriaHeadquarters = 0x083,
    [Description("Manoria Royal Guard Hall")]
    ManoriaRoyalGuardHall = 0x084,
    [Description("Zenan Bridge (Wrecked)")]
    ZenanBridge_Wrecked = 0x085,
    [Description("Zenan Bridge (Middle Ages)")]
    ZenanBridge_MiddleAges = 0x087,
    [Description("Sandorino Pervert Residence")]
    SandorinoPervertResidence = 0x088,
    [Description("Sandorino Elder's House")]
    SandorinoEldersHouse = 0x089,
    [Description("Sandorino Inn")]
    SandorinoInn = 0x08A,
    [Description("Sandorino Market")]
    SandorinoMarket = 0x08B,
    [Description("Cursed Woods")]
    CursedWoods = 0x08C,
    [Description("Frog's Burrow")]
    FrogsBurrow = 0x08D,
    [Description("Denadoro South Face")]
    DenadoroSouthFace = 0x08E,
    [Description("Denadoro Cave of the Masamune Exterior")]
    DenadoroCaveoftheMasamuneExterior = 0x08F,
    [Description("Denadoro North Face")]
    DenadoroNorthFace = 0x090,
    [Description("Denadoro Entrance")]
    DenadoroEntrance = 0x091,
    [Description("Denadoro Lower East Face")]
    DenadoroLowerEastFace = 0x092,
    [Description("Denadoro Upper East Face")]
    DenadoroUpperEastFace = 0x093,
    [Description("Denadoro Mountain Vista")]
    DenadoroMountainVista = 0x094,
    [Description("Denadoro West Face")]
    DenadoroWestFace = 0x095,
    [Description("Denadoro Gauntlet")]
    DenadoroGauntlet = 0x096,
    [Description("Denadoro Cave of the Masamune")]
    DenadoroCaveoftheMasamune = 0x097,
    [Description("Tata's House 1F")]
    TatasHouse1F = 0x098,
    [Description("Tata's House 2F")]
    TatasHouse2F = 0x099,
    [Description("Porre Elder's House (Middle Ages)")]
    PorreEldersHouse_MiddleAges = 0x09A,
    [Description("Porre Cafe (Middle Ages)")]
    PorreCafe_MiddleAges = 0x09B,
    [Description("Porre Inn (Middle Ages)")]
    PorreInn_MiddleAges = 0x09C,
    [Description("Porre Market (Middle Ages)")]
    PorreMarket_MiddleAges = 0x09D,
    [Description("Fiona's Villa")]
    FionasVilla = 0x09E,
    [Description("Sunken Desert Entrance")]
    SunkenDesertEntrance = 0x09F,
    [Description("Sunken Desert Parasytes")]
    SunkenDesertParasytes = 0x0A0,
    [Description("Sunken Desert Devourer")]
    SunkenDesertDevourer = 0x0A1,
    [Description("Ozzie's Fort Entrance (no map)")]
    OzziesFortEntrance_nomap = 0x0A2,
    [Description("Magic Cave Exterior")]
    MagicCaveExterior = 0x0A3,
    [Description("Magic Cave Interior")]
    MagicCaveInterior = 0x0A4,
    [Description("Castle Magus Exterior")]
    CastleMagusExterior = 0x0A5,
    [Description("Castle Magus Entrance")]
    CastleMagusEntrance = 0x0A6,
    [Description("Castle Magus Chamber of Guillotines")]
    CastleMagusChamberofGuillotines = 0x0A7,
    [Description("Castle Magus Chamber of Pits")]
    CastleMagusChamberofPits = 0x0A8,
    [Description("Castle Magus Throne of Strength")]
    CastleMagusThroneofStrength = 0x0A9,
    [Description("Castle Magus Hall of Aggression")]
    CastleMagusHallofAggression = 0x0AA,
    [Description("Castle Magus Hall of Deceit")]
    CastleMagusHallofDeceit = 0x0AB,
    [Description("Castle Magus Inner Sanctum")]
    CastleMagusInnerSanctum = 0x0AC,
    [Description("Castle Magus Throne of Magic")]
    CastleMagusThroneofMagic = 0x0AD,
    [Description("Castle Magus Throne of Defense")]
    CastleMagusThroneofDefense = 0x0AE,
    [Description("Castle Magus Hall of Apprehension")]
    CastleMagusHallofApprehension = 0x0AF,
    [Description("Castle Magus Lower Battlements")]
    CastleMagusLowerBattlements = 0x0B0,
    [Description("Ozzie's Fort Entrance")]
    OzziesFortEntrance = 0x0B1,
    [Description("Ozzie's Fort Hall of Disregard")]
    OzziesFortHallofDisregard = 0x0B2,
    [Description("Ozzie's Fort Chamber of Kitchen Knives")]
    OzziesFortChamberofKitchenKnives = 0x0B3,
    [Description("Ozzie's Fort Last Stand")]
    OzziesFortLastStand = 0x0B4,
    [Description("Ozzie's Fort Throne of Incompetence")]
    OzziesFortThroneofIncompetence = 0x0B5,
    [Description("Ozzie's Fort Throne of Impertinence (wrong map)")]
    OzziesFortThroneofImpertinence_wrongmap = 0x0B6,
    [Description("Ozzie's Fort Throne of Impertinence")]
    OzziesFortThroneofImpertinence = 0x0B7,
    [Description("Ozzie's Fort Throne of Ineptitude")]
    OzziesFortThroneofIneptitude = 0x0B8,
    [Description("Choras Old Couple Residence (Middle Ages)")]
    ChorasOldCoupleResidence_MiddleAges = 0x0B9,
    [Description("Choras Carpenter's Residence 1F (Middle Ages)")]
    ChorasCarpentersResidence1F_MiddleAges = 0x0BA,
    [Description("Choras Carpenter's Residence 2F (Middle Ages)")]
    ChorasCarpentersResidence2F_MiddleAges = 0x0BB,
    [Description("Choras Cafe")]
    ChorasCafe = 0x0BC,
    [Description("Choras Inn (Middle Ages)")]
    ChorasInn_MiddleAges = 0x0BD,
    [Description("Choras Market (Middle Ages)")]
    ChorasMarket_MiddleAges = 0x0BE,
    [Description("Sun Keep (Middle Ages)")]
    SunKeep_MiddleAges = 0x0BF,
    [Description("Giant's Claw Entrance")]
    GiantsClawEntrance = 0x0C3,
    [Description("Giant's Claw Caverns")]
    GiantsClawCaverns = 0x0C4,
    [Description("Giant's Claw Last Tyranno")]
    GiantsClawLastTyranno = 0x0C5,
    [Description("Manoria Command")]
    ManoriaCommand = 0x0C6,
    [Description("Manoria Confinement")]
    ManoriaConfinement = 0x0C7,
    [Description("Manoria Shrine Antechamber")]
    ManoriaShrineAntechamber = 0x0C8,
    [Description("Manoria Storage")]
    ManoriaStorage = 0x0C9,
    [Description("Manoria Kitchen")]
    ManoriaKitchen = 0x0CA,
    [Description("Manoria Shrine")]
    ManoriaShrine = 0x0CB,
    [Description("Guardia Forest Frog King Battle")]
    GuardiaForestFrogKingBattle = 0x0CC,
    [Description("Denadoro Cyrus's Last Battle")]
    DenadoroCyrussLastBattle = 0x0CD,
    [Description("Guardia Throneroom Cyrus's Final Mission")]
    GuardiaThroneroomCyrussFinalMission = 0x0CE,
    [Description("Schala's Room (no map)")]
    SchalasRoom_nomap = 0x0CF,
    [Description("Bangor Dome")]
    BangorDome = 0x0D0,
    [Description("Bangor Dome Sealed Room")]
    BangorDomeSealedRoom = 0x0D1,
    [Description("Trann Dome")]
    TrannDome = 0x0D2,
    [Description("Trann Dome Sealed Room")]
    TrannDomeSealedRoom = 0x0D3,
    [Description("Lab 16 West")]
    Lab16West = 0x0D4,
    [Description("Lab 16 East")]
    Lab16East = 0x0D5,
    [Description("Arris Dome")]
    ArrisDome = 0x0D6,
    [Description("Arris Dome Infestation")]
    ArrisDomeInfestation = 0x0D7,
    [Description("Arris Dome Auxiliary Console")]
    ArrisDomeAuxiliaryConsole = 0x0D8,
    [Description("Arris Dome Lower Commons")]
    ArrisDomeLowerCommons = 0x0D9,
    [Description("Arris Dome Command Central")]
    ArrisDomeCommandCentral = 0x0DA,
    [Description("Arris Dome Guardian Chamber")]
    ArrisDomeGuardianChamber = 0x0DB,
    [Description("Arris Dome Sealed Room")]
    ArrisDomeSealedRoom = 0x0DC,
    [Description("Arris Dome Rafters")]
    ArrisDomeRafters = 0x0DD,
    [Description("Reptite Lair 2F")]
    ReptiteLair2F = 0x0DE,
    [Description("Lab 32 West Entrance")]
    Lab32WestEntrance = 0x0DF,
    [Description("Lab 32")]
    Lab32 = 0x0E0,
    [Description("Lab 32 East Entrance")]
    Lab32EastEntrance = 0x0E1,
    [Description("Proto Dome")]
    ProtoDome = 0x0E2,
    [Description("Proto Dome Portal")]
    ProtoDomePortal = 0x0E3,
    [Description("Factory Ruins Entrance")]
    FactoryRuinsEntrance = 0x0E4,
    [Description("Factory Ruins Auxiliary Console")]
    FactoryRuinsAuxiliaryConsole = 0x0E5,
    [Description("Factory Ruins Security Center")]
    FactoryRuinsSecurityCenter = 0x0E6,
    [Description("Factory Ruins Crane Room")]
    FactoryRuinsCraneRoom = 0x0E7,
    [Description("Factory Ruins Infestation")]
    FactoryRuinsInfestation = 0x0E8,
    [Description("Factory Ruins Crane Control Room")]
    FactoryRuinsCraneControlRoom = 0x0E9,
    [Description("Factory Ruins Information Archive")]
    FactoryRuinsInformationArchive = 0x0EA,
    [Description("Factory Ruins Power Core")]
    FactoryRuinsPowerCore = 0x0EB,
    [Description("Sewer Access B1")]
    SewerAccessB1 = 0x0EC,
    [Description("Sewer Access B2")]
    SewerAccessB2 = 0x0ED,
    [Description("Keeper's Dome")]
    KeepersDome = 0x0F1,
    [Description("Keeper's Dome Corridor")]
    KeepersDomeCorridor = 0x0F2,
    [Description("Keeper's Dome Hanger")]
    KeepersDomeHanger = 0x0F3,
    [Description("Death Peak Entrance")]
    DeathPeakEntrance = 0x0F4,
    [Description("Death Peak South Face")]
    DeathPeakSouthFace = 0x0F5,
    [Description("Death Peak Southeast Face")]
    DeathPeakSoutheastFace = 0x0F6,
    [Description("Death Peak Northeast Face")]
    DeathPeakNortheastFace = 0x0F7,
    [Description("Geno Dome Entrance")]
    GenoDomeEntrance = 0x0F8,
    [Description("Geno Dome Conveyor Entrance")]
    GenoDomeConveyorEntrance = 0x0F9,
    [Description("Geno Dome Conveyor Exit")]
    GenoDomeConveyorExit = 0x0FA,
    [Description("Sun Palace")]
    SunPalace = 0x0FB,
    [Description("Sun Keep (Last Village)")]
    SunKeep_LastVillage = 0x0FD,
    [Description("Skill Tutorial")]
    SkillTutorial = 0x0FE,
    [Description("Sun Keep (Future)")]
    SunKeep_Future = 0x0FF,
    [Description("Geno Dome Labs")]
    GenoDomeLabs = 0x100,
    [Description("Geno Dome Storage")]
    GenoDomeStorage = 0x101,
    [Description("Geno Dome Robot Hub")]
    GenoDomeRobotHub = 0x102,
    [Description("Factory Ruins Data Core")]
    FactoryRuinsDataCore = 0x103,
    [Description("Death Peak Northwest Face")]
    DeathPeakNorthwestFace = 0x104,
    [Description("Prehistoric Canyon")]
    PrehistoricCanyon = 0x105,
    [Description("Death Peak Upper North Face")]
    DeathPeakUpperNorthFace = 0x106,
    [Description("Death Peak Lower North Face")]
    DeathPeakLowerNorthFace = 0x107,
    [Description("Death Peak Cave")]
    DeathPeakCave = 0x108,
    [Description("Death Peak Summit")]
    DeathPeakSummit = 0x109,
    [Description("Geno Dome Robot Elevator Access")]
    GenoDomeRobotElevatorAccess = 0x10B,
    [Description("Geno Dome Mainframe")]
    GenoDomeMainframe = 0x10C,
    [Description("Geno Dome Waste Disposal")]
    GenoDomeWasteDisposal = 0x10D,
    [Description("Special Purpose Area")]
    SpecialPurposeArea = 0x10F,
    [Description("Mystic Mtn Portal")]
    MysticMtnPortal = 0x110,
    [Description("Mystic Mtn Base")]
    MysticMtnBase = 0x111,
    [Description("Mystic Mtn Gulch")]
    MysticMtnGulch = 0x112,
    [Description("Chief's Hut")]
    ChiefsHut = 0x113,
    [Description("Ioka Southwestern Hut")]
    IokaSouthwesternHut = 0x114,
    [Description("Ioka Trading Post")]
    IokaTradingPost = 0x115,
    [Description("Ioka Sweet Water Hut")]
    IokaSweetWaterHut = 0x116,
    [Description("Ioka Meeting Site")]
    IokaMeetingSite = 0x117,
    [Description("Ioka Meeting Site (Party)")]
    IokaMeetingSite_Party = 0x118,
    [Description("Forest Maze Entrance")]
    ForestMazeEntrance = 0x119,
    [Description("Forest Maze")]
    ForestMaze = 0x11A,
    [Description("Reptite Lair Entrance")]
    ReptiteLairEntrance = 0x11B,
    [Description("Reptite Lair 1F")]
    ReptiteLair1F = 0x11C,
    [Description("Reptite Lair Weevil Burrows B1")]
    ReptiteLairWeevilBurrowsB1 = 0x11D,
    [Description("Reptite Lair Weevil Burrows B2")]
    ReptiteLairWeevilBurrowsB2 = 0x11E,
    [Description("Reptite Lair Commons")]
    ReptiteLairCommons = 0x11F,
    [Description("Reptite Lair Tunnel")]
    ReptiteLairTunnel = 0x120,
    [Description("Reptite Lair Azala's Room")]
    ReptiteLairAzalasRoom = 0x121,
    [Description("Reptite Lair Access Shaft")]
    ReptiteLairAccessShaft = 0x122,
    [Description("Hunting Range")]
    HuntingRange = 0x123,
    [Description("Laruba Ruins")]
    LarubaRuins = 0x124,
    [Description("Dactyl Nest, Lower")]
    DactylNest_Lower = 0x125,
    [Description("Dactyl Nest, Upper")]
    DactylNest_Upper = 0x126,
    [Description("Dactyl Nest Summit")]
    DactylNestSummit = 0x127,
    [Description("Giant's Claw Lair Entrance")]
    GiantsClawLairEntrance = 0x128,
    [Description("Giant's Claw Lair Throneroom")]
    GiantsClawLairThroneroom = 0x129,
    [Description("Tyrano Lair Exterior")]
    TyranoLairExterior = 0x12A,
    [Description("Tyrano Lair Entrance")]
    TyranoLairEntrance = 0x12B,
    [Description("Tyrano Lair Throneroom")]
    TyranoLairThroneroom = 0x12C,
    [Description("Tyrano Lair Keep")]
    TyranoLairKeep = 0x12D,
    [Description("Tyrano Lair Antechambers")]
    TyranoLairAntechambers = 0x12E,
    [Description("Tyrano Lair Storage")]
    TyranoLairStorage = 0x12F,
    [Description("Tyrano Lair Nizbel's Room")]
    TyranoLairNizbelsRoom = 0x130,
    [Description("Tyrano Lair Room of Vertigo")]
    TyranoLairRoomofVertigo = 0x131,
    [Description("Lair Ruins Portal")]
    LairRuinsPortal = 0x133,
    [Description("Black Omen 1F Entrance")]
    BlackOmen1FEntrance = 0x134,
    [Description("Black Omen 1F Walkway")]
    BlackOmen1FWalkway = 0x135,
    [Description("Black Omen 1F Defense Corridor")]
    BlackOmen1FDefenseCorridor = 0x136,
    [Description("Black Omen 1F Stairway")]
    BlackOmen1FStairway = 0x137,
    [Description("Black Omen 3F Walkway")]
    BlackOmen3FWalkway = 0x138,
    [Description("Black Omen 47F Auxilary Command")]
    BlackOmen47FAuxilaryCommand = 0x139,
    [Description("Black Omen 47F Grand Hall")]
    BlackOmen47FGrandHall = 0x13A,
    [Description("Black Omen 47F Emporium")]
    BlackOmen47FEmporium = 0x13B,
    [Description("Black Omen 47F Royal Path")]
    BlackOmen47FRoyalPath = 0x13C,
    [Description("Black Omen 47F Royal Bathroom")]
    BlackOmen47FRoyalBathroom = 0x13D,
    [Description("Black Omen 47F Royal Assembly")]
    BlackOmen47FRoyalAssembly = 0x13E,
    [Description("Black Omen 47F Royal Promenade")]
    BlackOmen47FRoyalPromenade = 0x13F,
    [Description("Black Omen Royal Teleporter (Lower)")]
    BlackOmenRoyalTeleporter_Lower = 0x140,
    [Description("Black Omen Royal Teleporter (Upper)")]
    BlackOmenRoyalTeleporter_Upper = 0x141,
    [Description("Black Omen 63F Divine Esplenade")]
    BlackOmen63FDivineEsplenade = 0x142,
    [Description("Black Omen 63F Divine Guardian")]
    BlackOmen63FDivineGuardian = 0x143,
    [Description("Black Omen 97F Astral Walkway")]
    BlackOmen97FAstralWalkway = 0x144,
    [Description("Black Omen 98F Astral Guardian")]
    BlackOmen98FAstralGuardian = 0x145,
    [Description("Black Omen 98F Astral Walkway")]
    BlackOmen98FAstralWalkway = 0x146,
    [Description("Sunkeep (Prehistoric)")]
    Sunkeep_Prehistoric = 0x147,
    [Description("Zeal Palace Schala's Room")]
    ZealPalaceSchalasRoom = 0x148,
    [Description("Zeal Palace Regal Hall")]
    ZealPalaceRegalHall = 0x149,
    [Description("Zeal Palace Corridor to the Mammon Machine")]
    ZealPalaceCorridortotheMammonMachine = 0x14A,
    [Description("Zeal Palace Hall of the Mammon Machine")]
    ZealPalaceHalloftheMammonMachine = 0x14B,
    [Description("Zeal Palace Zeal Throneroom")]
    ZealPalaceZealThroneroom = 0x14C,
    [Description("Zeal Palace Hall of the Mammon Machine (Night)")]
    ZealPalaceHalloftheMammonMachine_Night = 0x14D,
    [Description("Zeal Palace Zeal Throneroom (Night)")]
    ZealPalaceZealThroneroom_Night = 0x14E,
    [Description("Arris Dome Food Locker")]
    ArrisDomeFoodLocker = 0x158,
    [Description("Arris Dome Guardian Chamber (Battle with Lavos)")]
    ArrisDomeGuardianChamber_BattlewithLavos = 0x15A,
    [Description("Prison Catwalks (Battle with Lavos)")]
    PrisonCatwalks_BattlewithLavos = 0x15B,
    [Description("Heckran Cave (Battle with Lavos)")]
    HeckranCave_BattlewithLavos = 0x15C,
    [Description("Zenan Bridge (Battle with Lavos)")]
    ZenanBridge_BattlewithLavos = 0x15D,
    [Description("Cave of the Masamune (Battle with Lavos)")]
    CaveoftheMasamune_BattlewithLavos = 0x15E,
    [Description("Dark Ages Portal")]
    DarkAgesPortal = 0x15F,
    [Description("Enhasa")]
    Enhasa = 0x163,
    [Description("Kajar")]
    Kajar = 0x165,
    [Description("Kajar Study")]
    KajarStudy = 0x166,
    [Description("Kajar Belthasar's Private Room")]
    KajarBelthasarsPrivateRoom = 0x167,
    [Description("Kajar Magic Lab")]
    KajarMagicLab = 0x168,
    [Description("Zeal Palace Belthasar's Private Room")]
    ZealPalaceBelthasarsPrivateRoom = 0x169,
    [Description("Blackbird Scaffolding")]
    BlackbirdScaffolding = 0x16A,
    [Description("Blackbird Left Wing")]
    BlackbirdLeftWing = 0x16B,
    [Description("Blackbird Right Port")]
    BlackbirdRightPort = 0x16C,
    [Description("Blackbird Left Port")]
    BlackbirdLeftPort = 0x16D,
    [Description("Blackbird Overhead")]
    BlackbirdOverhead = 0x16E,
    [Description("Blackbird Hanger")]
    BlackbirdHanger = 0x16F,
    [Description("Blackbird Rear Halls")]
    BlackbirdRearHalls = 0x170,
    [Description("Blackbird Forward Halls")]
    BlackbirdForwardHalls = 0x171,
    [Description("Blackbird Treasury")]
    BlackbirdTreasury = 0x172,
    [Description("Blackbird Cell")]
    BlackbirdCell = 0x173,
    [Description("Blackbird Barracks")]
    BlackbirdBarracks = 0x174,
    [Description("Blackbird Armory 3")]
    BlackbirdArmory3 = 0x175,
    [Description("Blackbird Inventory")]
    BlackbirdInventory = 0x176,
    [Description("Blackbird Lounge")]
    BlackbirdLounge = 0x177,
    [Description("Blackbird Ducts")]
    BlackbirdDucts = 0x178,
    [Description("Reborn Epoch")]
    RebornEpoch = 0x179,
    [Description("Algetty")]
    Algetty = 0x17C,
    [Description("Algetty Inn")]
    AlgettyInn = 0x17D,
    [Description("Algetty Elder's Grotto")]
    AlgettyEldersGrotto = 0x17E,
    [Description("Algetty Commoner Grotto")]
    AlgettyCommonerGrotto = 0x17F,
    [Description("Algetty Shop")]
    AlgettyShop = 0x180,
    [Description("Algetty Tsunami (wrong map)")]
    AlgettyTsunami_wrongmap = 0x181,
    [Description("Algetty Entrance")]
    AlgettyEntrance = 0x182,
    [Description("The Beast's Nest (wrong map)")]
    TheBeastsNest_wrongmap = 0x183,
    [Description("The Beast's Nest")]
    TheBeastsNest = 0x184,
    [Description("Zeal Teleporters")]
    ZealTeleporters = 0x185,
    [Description("Mt. Woe Western Face")]
    MtWoeWesternFace = 0x188,
    [Description("Mt. Woe Lower Eastern Face")]
    MtWoeLowerEasternFace = 0x189,
    [Description("Mt. Woe Middle Eastern Face")]
    MtWoeMiddleEasternFace = 0x18A,
    [Description("Mt. Woe Upper Eastern Face")]
    MtWoeUpperEasternFace = 0x18B,
    [Description("Mt. Woe Summit (wrong map)")]
    MtWoeSummit_wrongmap = 0x18C,
    [Description("Mt. Woe Summit")]
    MtWoeSummit = 0x18D,
    [Description("Zeal Palace")]
    ZealPalace = 0x191,
    [Description("Zeal Palace Hallway")]
    ZealPalaceHallway = 0x192,
    [Description("Zeal Palace Study")]
    ZealPalaceStudy = 0x193,
    [Description("Ocean Palace Entrance")]
    OceanPalaceEntrance = 0x194,
    [Description("Ocean Palace Piazza")]
    OceanPalacePiazza = 0x195,
    [Description("Ocean Palace Side Rooms")]
    OceanPalaceSideRooms = 0x196,
    [Description("Ocean Palace Forward Area")]
    OceanPalaceForwardArea = 0x197,
    [Description("Ocean Palace B3 Landing")]
    OceanPalaceB3Landing = 0x198,
    [Description("Ocean Palace Grand Stairwell")]
    OceanPalaceGrandStairwell = 0x199,
    [Description("Ocean Palace B20 Landing")]
    OceanPalaceB20Landing = 0x19A,
    [Description("Ocean Palace Southern Access Lift")]
    OceanPalaceSouthernAccessLift = 0x19B,
    [Description("Ocean Palace Security Pool")]
    OceanPalaceSecurityPool = 0x19C,
    [Description("Ocean Palace Security Esplanade")]
    OceanPalaceSecurityEsplanade = 0x19D,
    [Description("Ocean Palace Regal Antechamber")]
    OceanPalaceRegalAntechamber = 0x19E,
    [Description("Ocean Palace Throneroom")]
    OceanPalaceThroneroom = 0x19F,
    [Description("Ocean Palace (TBD)")]
    OceanPalace_TBD = 0x1A0,
    [Description("Ocean Palace Eastern Access Lift")]
    OceanPalaceEasternAccessLift = 0x1A1,
    [Description("Ocean Palace Western Access Lift")]
    OceanPalaceWesternAccessLift = 0x1A2,
    [Description("Time Distortion Mammon Machine")]
    TimeDistortionMammonMachine = 0x1A6,
    [Description("Ocean Palace Time Freeze")]
    OceanPalaceTimeFreeze = 0x1A7,
    [Description("Last Village Commons")]
    LastVillageCommons = 0x1A8,
    [Description("Last Village Empty Hut")]
    LastVillageEmptyHut = 0x1A9,
    [Description("Last Village Shop")]
    LastVillageShop = 0x1AA,
    [Description("Last Village Residence")]
    LastVillageResidence = 0x1AB,
    [Description("North Cape")]
    NorthCape = 0x1AC,
    //[Description("Death Peak Summit")]
    //DeathPeakSummit = 0x1AD,
    [Description("Tyrano Lair Main Cell")]
    TyranoLairMainCell = 0x1AE,
    [Description("Title Screen (wrong map)")]
    TitleScreen_wrongmap = 0x1AF,
    [Description("Flying Epoch")]
    FlyingEpoch = 0x1B0,
    [Description("Title Screen")]
    TitleScreen = 0x1B1,
    [Description("Bekkler's Lab")]
    BekklersLab = 0x1B2,
    [Description("Magic Cave Exterior (after cutscene)")]
    MagicCaveExterior_aftercutscene = 0x1B3,
    [Description("Fiona's Forest Campfire")]
    FionasForestCampfire = 0x1B4,
    [Description("Factory Ruins (TBD)")]
    FactoryRuins_TBD = 0x1B5,
    [Description("Courtroom King's Trial")]
    CourtroomKingsTrial = 0x1B6,
    [Description("Leene Square")]
    LeeneSquare = 0x1B7,
    [Description("Guardia Rear Storage")]
    GuardiaRearStorage = 0x1B8,
    [Description("Courtroom Lobby")]
    CourtroomLobby = 0x1B9,
    [Description("Blackbird Access Shaft")]
    BlackbirdAccessShaft = 0x1BA,
    [Description("Blackbird Armory 2")]
    BlackbirdArmory2 = 0x1BB,
    [Description("Blackbird Armory 1")]
    BlackbirdArmory1 = 0x1BC,
    [Description("Blackbird Storage")]
    BlackbirdStorage = 0x1BD,
    [Description("Castle Magus Upper Battlements")]
    CastleMagusUpperBattlements = 0x1BE,
    [Description("Castle Magus Grand Stairway")]
    CastleMagusGrandStairway = 0x1BF,
    [Description("(Bad Event Data Packet)")]
    _BadEventDataPacket = 0x1C0,
    [Description("Black Omen Entrance")]
    BlackOmenEntrance = 0x1C1,
    [Description("Black Omen Omega Defense")]
    BlackOmenOmegaDefense = 0x1C2,
    [Description("Black Omen Seat of Agelessness")]
    BlackOmenSeatofAgelessness = 0x1C3,
    [Description("Reptite Lair (Battle with Lavos)")]
    ReptiteLair_BattlewithLavos = 0x1C8,
    [Description("Castle Magus Inner Sanctum (Battle with Lavos)")]
    CastleMagusInnerSanctum_BattlewithLavos = 0x1C9,
    [Description("Tyrano Lair Keep (Battle with Lavos)")]
    TyranoLairKeep_BattlewithLavos = 0x1CA,
    [Description("Mt. Woe Summit (Battle with Lavos)")]
    MtWoeSummit_BattlewithLavos = 0x1CB,
    [Description("Credits (TBD)")]
    Credits_TBD = 0x1CC,
    [Description("End of Time")]
    EndofTime = 0x1D0,
    [Description("Spekkio")]
    Spekkio = 0x1D1,
    [Description("Apocalypse Lavos")]
    ApocalypseLavos = 0x1D2,
    [Description("Lavos")]
    Lavos = 0x1D3,
    [Description("Guardia Queen's Tower (Middle Ages)")]
    GuardiaQueensTower_MiddleAges = 0x1D4,
    [Description("Castle Magus Corridor of Combat")]
    CastleMagusCorridorofCombat = 0x1D5,
    [Description("Castle Magus Hall of Ambush")]
    CastleMagusHallofAmbush = 0x1D6,
    [Description("Castle Magus Dungeon")]
    CastleMagusDungeon = 0x1D7,
    [Description("Apocalypse Epoch")]
    ApocalypseEpoch = 0x1D8,
    [Description("End of Time Epoch")]
    EndofTimeEpoch = 0x1D9,
    [Description("Lavos Tunnel")]
    LavosTunnel = 0x1DA,
    [Description("Lavos Core")]
    LavosCore = 0x1DB,
    [Description("Truce Dome")]
    TruceDome = 0x1DC,
    [Description("Emergence of the Black Omen")]
    EmergenceoftheBlackOmen = 0x1DD,
    [Description("Blackbird Wing Access")]
    BlackbirdWingAccess = 0x1DE,
    [Description("Tesseract")]
    Tesseract = 0x1DF,
    [Description("Guardia King's Tower (Middle Ages)")]
    GuardiaKingsTower_MiddleAges = 0x1E0,
    [Description("Death of the Blackbird")]
    DeathoftheBlackbird = 0x1E1,
    [Description("Guardia King's Tower (Present)")]
    GuardiaKingsTower_Present = 0x1E6,
    [Description("Guardia Queen's Tower (Present)")]
    GuardiaQueensTower_Present = 0x1E7,
    [Description("Guardia Lawgiver's Tower")]
    GuardiaLawgiversTower = 0x1E8,
    [Description("Guardia Prison Tower")]
    GuardiaPrisonTower = 0x1E9,
    [Description("Ancient Tyrano Lair Room of Vertigo")]
    AncientTyranoLairRoomofVertigo = 0x1EA,
    [Description("Algetty Tsunami")]
    AlgettyTsunami = 0x1ED,
    [Description("Paradise Lost")]
    ParadiseLost = 0x1EE,
    [Description("Death Peak Guardian Spawn")]
    DeathPeakGuardianSpawn = 0x1EF,
    [Description("Present")]
    Present = 0x1F0,
    [Description("Middle Ages")]
    MiddleAges = 0x1F1,
    [Description("Future")]
    Future = 0x1F2,
    [Description("Prehistoric")]
    Prehistoric = 0x1F3,
    [Description("Dark Ages")]
    DarkAges = 0x1F4,
    [Description("Kingdom of Zeal")]
    KingdomofZeal = 0x1F5,
    [Description("Last Village")]
    LastVillage = 0x1F6,
    [Description("Apocalypse")]
    Apocalypse = 0x1F7,
}

public enum MapType
{
    Present,
    MiddleAges,
    Future,
    Prehistoric,
    DarkAges,
    KingdomOfZeal
}

public enum CheckLocationType
{
    [Description("Mystic Mountains")]
    MysticMountains,
    [Description("Tyrano Lair")]
    TyranoLair,
    [Description("Forest Maze")]
    ForestMaze,
    [Description("Reptite Lair")]
    ReptiteLair,
    [Description("Dactyl Nest")]
    DactylNest,
    [Description("Sun Keep")]
    SunKeep,

    [Description("Mt Woe")]
    MtWoe,
    [Description("Ocean Palace")]
    OceanPalace,

    [Description("Manoria Cathedral")]
    ManoriaCathedral,
    [Description("Truce Canyon")]
    TruceCanyon,
    [Description("Cursed Woods")]
    CursedWoods,
    [Description("Zenan Bridge")]
    ZenanBridge,
    [Description("Sunken Desert")]
    SunkenDesert,
    [Description("Fiona's Villa")]
    FionasVilla,
    [Description("Denadoro Mts")]
    DenadoroMts,
    [Description("Guardia Castle")]
    GuardiaCastlePast,
    [Description("Giant's Claw")]
    GiantsClaw,
    [Description("Magic Cave")]
    MagicCave,
    [Description("Magus's Castle")]
    MagusCastle,
    [Description("Ozzie's Fort")]
    OzziesFort,
    [Description("Porre Elder's House")]
    PorreEldersHouse,
    [Description("Truce Inn Past")]
    TruceInnPast,
    [Description("Northern Ruins")]
    NorthernRuinsPast,
    [Description("Guardia Forest")]
    GuardiaForestPast,

    [Description("Guardia Castle")]
    GuardiaCastlePresent,
    [Description("Snail Stop")]
    SnailStop,
    [Description("Heckran Cave")]
    HeckranCave,
    [Description("Choras Inn")]
    ChorasInn,
    [Description("Choras Carpenter")]
    ChorasCarpenter,
    [Description("Melchior's Hut")]
    MelchiorsHut,
    [Description("Lucca's House")]
    LuccasHouse,
    [Description("Forest Ruins")]
    ForestRuins,
    [Description("Truce Mayor's House")]
    TruceMayorsHouse,
    [Description("Truce Inn")]
    TruceInnPresent,
    [Description("Porre Mayor's House")]
    PorreMayorsHouse,
    [Description("Northern Ruins")]
    NorthernRuinsPresent,
    [Description("Guardia Forest")]
    GuardiaForestPresent,
    [Description("Fiona's Shrine")]
    FionasShrine,
    [Description("West Cape")]
    WestCape,

    [Description("Arris Dome")]
    ArrisDome,
    [Description("Proto Dome")]
    ProtoDome,
    [Description("Factory")]
    Factory,
    [Description("Geno Dome")]
    GenoDome,
    [Description("Sun Keep")]
    SunKeepFuture,
    [Description("Sun Palace")]
    SunPalace,
    [Description("Death Peak")]
    DeathPeak,
    [Description("Trann Dome")]
    TrannDome,
    [Description("Bangor Dome")]
    BangorDome,
    [Description("Sewers")]
    Sewers,
    [Description("Lab16")]
    Lab16,
    [Description("Lab32")]
    Lab32,
    [Description("Keepers Dome")]
    KeepersDome,
}

public enum EventType
{
    ZomborSpotBossDefeated,
    NizbelSpotBossDefeated,
    BlackTyranoSpotBossDefeated,
    GigaGaiaSpotBossDefeated,
    GolemSpotBossDefeated,
    YakaraSpotBossDefeated,
    MasamumeSpotBossDefeated,
    RetiniteSpotBossDefeated,
    RustTryanoSpotBossDefeated,
    MagusSpotBossDefeated,
    HeckranSpotBossDefeated,
    DragonTankSpotBossDefeated,
    YakaraIIISpotBossDefeated,
    GuardianSpotBossDefeated,
    RSeriesSpotBossDefeated,
    SonOfSunSpotBossDefeated,
    MotherBrainSpotBossDefeated,
    ZealSpotBossDefeated,
    OzzieSpotBossDefeated,
    CyrusGraveSpotBossDefeated,
    FriendToTheDactyls,
    SavedByFrog,
    RescueMarle,
    FixRobo,
    ReturnTheMasamune,
    CooksRations,
    MelchiorsRefinements,
    ReplantTheForest,
    BurrowHeroMedalChest,
    RainbowShell,
    SnailStopPurchase,
    TalkToCarpenter,
    BorrowCarpentersTools,
    TabansGift,
    ReforgeTheMasamune,
    KingsGuardiasTrial,
    CloneGame,
    ActivateComputer,
    LearnMagic,
    AttachEpochWings,
    SeedValidated,
    MoonstoneDroppedOff,
    WokeRoboUp,
    TalkedToToma,

    TalkedToKnightCaptain,
    TalkedToCook,

}