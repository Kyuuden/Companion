using System;

namespace FF.Rando.Companion.FreeEnterprise.GaleswiftFork;
internal class MysteryFlags : IFlags
{
    public bool CClassicGiant => false;

    public bool CHero => false;

    public bool CNoEarned => false;

    public bool CNoFree => false;

    public bool CNoPartner => false;

    public bool CSuperhero => false;

    public int GatedObjectiveNum => 0;

    public bool KForge => true;

    public bool KMaibAbove => false;

    public bool KMaibAll => true;

    public bool KMaibBelow => false;

    public bool KMaibLST => false;

    public bool KMaibStandard => false;

    public bool KMain => true;

    public bool KMoon => true;

    public KNoFreeMode KNoFreeMode => throw new NotImplementedException();

    public bool KPink => true;

    public bool KSummon => true;

    public bool KUnsafe => false;

    public bool KUnsafer => false;

    public byte MaxPartySize => 5;

    public int NumRequiredObjectives => -1;

    public bool OWinCrystal => true;

    public bool OWinGame => true;

    public bool VanillaAgility => false;

    public bool XCrystalBonus => false;

    public KeyItemCheckXpBonus XKeyItemCheckBonus =>  KeyItemCheckXpBonus.None;

    public KeyItemZonkXpBonus XKeyItemZonkXpBonus => KeyItemZonkXpBonus.None;

    public MiabXpBonus XMiabXpBonus => MiabXpBonus.None;

    public MoonXpBonus XMoonXpBonus => MoonXpBonus.None;

    public bool XNoKeyBonus => false;

    public ObjectiveXpBonus XObjectiveBonus => ObjectiveXpBonus.None;

    public bool XSmallParty =>  false;

    public bool IsHardRequired(uint objectiveNum) => false;
}
