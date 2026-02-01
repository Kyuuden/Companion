namespace FF.Rando.Companion.Games.FreeEnterprise.GaleswiftFork;

internal interface IFlags
{
    bool CClassicGiant { get; }
    bool CHero { get; }
    bool CNoEarned { get; }
    bool CNoFree { get; }
    bool CNoPartner { get; }
    bool CSuperhero { get; }
    int GatedObjectiveNum { get; }
    bool KForge { get; }
    bool KMaibAbove { get; }
    bool KMaibAll { get; }
    bool KMaibBelow { get; }
    bool KMaibLST { get; }
    bool KMaibStandard { get; }
    bool KMain { get; }
    bool KMoon { get; }
    KNoFreeMode KNoFreeMode { get; }
    bool KPink { get; }
    bool KSummon { get; }
    bool KUnsafe { get; }
    bool KUnsafer { get; }
    byte MaxPartySize { get; }
    int NumRequiredObjectives { get; }
    bool OWinCrystal { get; }
    bool OWinGame { get; }
    bool VanillaAgility { get; }
    bool XCrystalBonus { get; }
    KeyItemCheckXpBonus XKeyItemCheckBonus { get; }
    KeyItemZonkXpBonus XKeyItemZonkXpBonus { get; }
    MiabXpBonus XMiabXpBonus { get; }
    MoonXpBonus XMoonXpBonus { get; }
    bool XNoKeyBonus { get; }
    ObjectiveXpBonus XObjectiveBonus { get; }
    bool XSmallParty { get; }

    bool IsHardRequired(uint objectiveNum);
}