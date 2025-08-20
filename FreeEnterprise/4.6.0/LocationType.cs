using FF.Rando.Companion.FreeEnterprise.Shared;

namespace FF.Rando.Companion.FreeEnterprise._4._6._0;

public enum LocationType : uint
{
    StartingCharacter = 1,
    StartingPartner,
    KaipoInn,
    WateryPass,
    Damcyan,
    KaipoInfirmary,
    MtHobbs,
    Mysidia1,
    Mysidia2,
    MtOrdealsCharacter,
    PlaceHolder1,
    PlaceHolder2,
    BaronInnCharacter,
    BaronCastleCharacter,
    Zot1,
    Zot2,
    DwarfCastle,
    EblanCave,
    Moon,
    Giant,
    StartingItem = 0x0020,
    AntlionNest = 0x0021,
    DefendingFabul = 0x0022,
    MtOrdeals = 0x0023,
    BaronInn = 0x0024,
    BaronCastle = 0x0025,
    EdwardInToroia = 0x0026,
    CaveMagnes = 0x0027,
    TowerOfZot = 0x0028,
    LowerBabIlBoss = 0x0029,
    SuperCannon = 0x002A,
    Luca = 0x002B,
    SealedCave = 0x002C,
    FeymarchChest = 0x002D,
    RatTailTrade = 0x002E,
    Shelia1 = 0x002F,
    Shelia2 = 0x0030,
    FeymarchQueen = 0x0031,
    FeymarchKing = 0x0032,
    OdinThrone = 0x0033,
    FromTheSylphs = 0x0034,
    CaveBahamut = 0x0035,
    MurasameAltar = 0x0036,
    CrystalSwordAltar = 0x0037,
    WhiteSpearAltar = 0x0038,
    RibbonChest1 = 0x0039,
    RibbonChest2 = 0x003A,
    MasamuneAltar = 0x003B,
    TowerOfZotTrappedChest = 0x003C,
    EblanTrappedChest1 = 0x003D,
    EblanTrappedChest2 = 0x003E,
    EblanTrappedChest3 = 0x003F,
    LowerBabIlTrappedChest1 = 0x0040,
    LowerBabIlTrappedChest2 = 0x0041,
    LowerBabIlTrappedChest3 = 0x0042,
    LowerBabIlTrappedChest4 = 0x0043,
    CaveEblanTrappedChest = 0x0044,
    UpperBabIlTrappedChest = 0x0045,
    CaveOfSummonsTrappedChest = 0x0046,
    SylphCaveTrappedChest1 = 0x0047,
    SylphCaveTrappedChest2 = 0x0048,
    SylphCaveTrappedChest3 = 0x0049,
    SylphCaveTrappedChest4 = 0x004A,
    SylphCaveTrappedChest5 = 0x004B,
    SylphCaveTrappedChest6 = 0x004C,
    SylphCaveTrappedChest7 = 0x004D,
    GiantOfBabIlTrappedChest = 0x004E,
    LunarPathTrappedChest = 0x004F,
    LunarCoreTrappedChest1 = 0x0050,
    LunarCoreTrappedChest2 = 0x0051,
    LunarCoreTrappedChest3 = 0x0052,
    LunarCoreTrappedChest4 = 0x0053,
    LunarCoreTrappedChest5 = 0x0054,
    LunarCoreTrappedChest6 = 0x0055,
    LunarCoreTrappedChest7 = 0x0056,
    LunarCoreTrappedChest8 = 0x0057,
    LunarCoreTrappedChest9 = 0x0058,
    RydiasMom = 0x0059,
    FallenGolbez = 0x005A,
    ObjectiveCompletion = 0x005D
}

internal static class LocationTypeExtensions
{
    public static World World(this LocationType locationType)
        => locationType switch
        {
            LocationType.StartingCharacter or 
            LocationType.StartingPartner or
            LocationType.KaipoInn or
            LocationType.WateryPass or 
            LocationType.Damcyan or
            LocationType.KaipoInfirmary or 
            LocationType.MtHobbs or
            LocationType.Mysidia1 or
            LocationType.Mysidia2 or 
            LocationType.MtOrdealsCharacter or
            LocationType.BaronInnCharacter or 
            LocationType.BaronCastleCharacter or 
            LocationType.Zot1 or 
            LocationType.Zot2 or 
            LocationType.EblanCave or 
            LocationType.Giant or 
            LocationType.RatTailTrade or 
            LocationType.StartingItem or 
            LocationType.AntlionNest or 
            LocationType.DefendingFabul or 
            LocationType.MtOrdeals or 
            LocationType.BaronInn or 
            LocationType.BaronCastle or 
            LocationType.EdwardInToroia or 
            LocationType.CaveMagnes or 
            LocationType.TowerOfZot or 
            LocationType.OdinThrone or 
            LocationType.RydiasMom or 
            LocationType.GiantOfBabIlTrappedChest or 
            LocationType.UpperBabIlTrappedChest or 
            LocationType.CaveEblanTrappedChest or 
            LocationType.TowerOfZotTrappedChest or 
            LocationType.EblanTrappedChest1 or 
            LocationType.EblanTrappedChest2 or 
            LocationType.EblanTrappedChest3 
                => Shared.World.Main,
            LocationType.DwarfCastle or 
            LocationType.LowerBabIlBoss or 
            LocationType.SuperCannon or 
            LocationType.Luca or 
            LocationType.SealedCave or 
            LocationType.FeymarchChest or 
            LocationType.Shelia1 or 
            LocationType.Shelia2 or 
            LocationType.FeymarchQueen or 
            LocationType.FeymarchKing or 
            LocationType.FromTheSylphs or 
            LocationType.LowerBabIlTrappedChest1 or 
            LocationType.LowerBabIlTrappedChest2 or 
            LocationType.LowerBabIlTrappedChest3 or 
            LocationType.LowerBabIlTrappedChest4 or 
            LocationType.CaveOfSummonsTrappedChest or 
            LocationType.SylphCaveTrappedChest1 or 
            LocationType.SylphCaveTrappedChest2 or 
            LocationType.SylphCaveTrappedChest3 or 
            LocationType.SylphCaveTrappedChest4 or 
            LocationType.SylphCaveTrappedChest5 or 
            LocationType.SylphCaveTrappedChest6 or 
            LocationType.SylphCaveTrappedChest7 
                => Shared.World.Underground,
            LocationType.Moon or 
            LocationType.CaveBahamut or 
            LocationType.MurasameAltar or 
            LocationType.CrystalSwordAltar or 
            LocationType.WhiteSpearAltar or 
            LocationType.RibbonChest1 or 
            LocationType.RibbonChest2 or 
            LocationType.MasamuneAltar or 
            LocationType.LunarPathTrappedChest or 
            LocationType.LunarCoreTrappedChest1 or 
            LocationType.LunarCoreTrappedChest2 or 
            LocationType.LunarCoreTrappedChest3 or 
            LocationType.LunarCoreTrappedChest4 or 
            LocationType.LunarCoreTrappedChest5 or 
            LocationType.LunarCoreTrappedChest6 or 
            LocationType.LunarCoreTrappedChest7 or 
            LocationType.LunarCoreTrappedChest8 or 
            LocationType.LunarCoreTrappedChest9 
                => Shared.World.Moon,
            _ => Shared.World.Unknown,
        };
}