using FF.Rando.Companion.Extensions;

namespace FF.Rando.Companion.FreeEnterprise.GaleswiftFork;
internal class Flags461 : IFlags
{
    private readonly byte[] _binaryFlags;
    public Flags461(byte[] binaryFlags)
    {
        if (binaryFlags.Length < 63)
        {
            _binaryFlags = new byte[63];
            binaryFlags.CopyTo(_binaryFlags, 0);
        }
        else
        {
            _binaryFlags = binaryFlags;
        }
    }

    public bool KMain => _binaryFlags.Read<bool>(164);
    public bool KSummon => _binaryFlags.Read<bool>(165);
    public bool KMoon => _binaryFlags.Read<bool>(166);
    public bool KMaibStandard => _binaryFlags.Read<bool>(167);
    public bool KMaibAbove => _binaryFlags.Read<bool>(168);
    public bool KMaibBelow => _binaryFlags.Read<bool>(169);
    public bool KMaibLST => _binaryFlags.Read<bool>(170);
    public bool KMaibAll => _binaryFlags.Read<bool>(171);
    public bool KForge => _binaryFlags.Read<bool>(172);
    public bool KPink => _binaryFlags.Read<bool>(173);
    public KNoFreeMode KNoFreeMode => _binaryFlags.Read<KNoFreeMode>(174,2);
    public bool KUnsafe => _binaryFlags.Read<bool>(176);
    public bool KUnsafer => _binaryFlags.Read<bool>(177);


    public bool CNoFree => _binaryFlags.Read<bool>(192);
    public bool CNoEarned => _binaryFlags.Read<bool>(193);
    public bool CClassicGiant => _binaryFlags.Read<bool>(1);
    public bool CNoPartner => _binaryFlags.Read<bool>(195);
    public bool CHero => _binaryFlags.Read<bool>(276);
    public bool CSuperhero => _binaryFlags.Read<bool>(277);

    public bool OWinGame => _binaryFlags.Read<bool>(162);
    public bool OWinCrystal => _binaryFlags.Read<bool>(163);

    public int NumRequiredObjectives =>
        _binaryFlags.Read<byte>(135, 4) switch
        {
            1 => -1,
            var x => x - 1
        };

    public bool IsHardRequired(uint objectiveNum) => _binaryFlags.Read<bool>(139 + objectiveNum);

    public int GatedObjectiveNum =>
        _binaryFlags.Read<byte>(157, 5);

    public bool VanillaAgility => _binaryFlags.Read<bool>(430);
    public bool XNoKeyBonus => _binaryFlags.Read<bool>(411);
    public bool XCrystalBonus => _binaryFlags.Read<bool>(412);
    public ObjectiveXpBonus XObjectiveBonus => _binaryFlags.Read<ObjectiveXpBonus>(413,3);
    public KeyItemCheckXpBonus XKeyItemCheckBonus => _binaryFlags.Read<KeyItemCheckXpBonus>(416, 3);
    public KeyItemZonkXpBonus XKeyItemZonkXpBonus => _binaryFlags.Read<KeyItemZonkXpBonus>(419, 3);
    public MiabXpBonus XMiabXpBonus => _binaryFlags.Read<MiabXpBonus>(421, 2);
    public MoonXpBonus XMoonXpBonus => _binaryFlags.Read<MoonXpBonus>(423, 2);

    public byte MaxPartySize => 5;

    public bool XSmallParty => false;
}



