using FF.Rando.Companion.Extensions;

namespace FF.Rando.Companion.Games.FreeEnterprise.GaleswiftFork;
internal class Flags462 : IFlags
{
    private readonly byte[] _binaryFlags;
    public Flags462(byte[] binaryFlags)
    {
        if (binaryFlags.Length < 70)
        {
            _binaryFlags = new byte[70];
            binaryFlags.CopyTo(_binaryFlags, 0);
        }
        else
        {
            _binaryFlags = binaryFlags;
        }
    }

    public bool KMain => _binaryFlags.Read<bool>(165);
    public bool KSummon => _binaryFlags.Read<bool>(166);
    public bool KMoon => _binaryFlags.Read<bool>(167);
    public bool KMaibStandard => _binaryFlags.Read<bool>(168);
    public bool KMaibAbove => _binaryFlags.Read<bool>(169);
    public bool KMaibBelow => _binaryFlags.Read<bool>(170);
    public bool KMaibLST => _binaryFlags.Read<bool>(171);
    public bool KMaibAll => _binaryFlags.Read<bool>(172);
    public bool KForge => _binaryFlags.Read<bool>(173);
    public bool KPink => _binaryFlags.Read<bool>(174);
    public KNoFreeMode KNoFreeMode => _binaryFlags.Read<KNoFreeMode>(175, 2);
    public bool KUnsafe => _binaryFlags.Read<bool>(177);
    public bool KUnsafer => _binaryFlags.Read<bool>(178);


    public bool CNoFree => _binaryFlags.Read<bool>(194);
    public bool CNoEarned => _binaryFlags.Read<bool>(195);
    public bool CClassicGiant => _binaryFlags.Read<bool>(1);
    public bool CNoPartner => _binaryFlags.Read<bool>(197);
    public bool CHero => _binaryFlags.Read<bool>(280);
    public bool CSuperhero => _binaryFlags.Read<bool>(281);
    public byte MaxPartySize => _binaryFlags.Read<byte>(272, 3) switch
    {
        0 => 5,
        var x => x
    };

    public bool OWinGame => _binaryFlags.Read<bool>(163);
    public bool OWinCrystal => _binaryFlags.Read<bool>(164);

    public int NumRequiredObjectives =>
        _binaryFlags.Read<byte>(136, 4) switch
        {
            1 => -1,
            var x => x - 1
        };

    public bool IsHardRequired(uint objectiveNum) => _binaryFlags.Read<bool>(140 + objectiveNum);

    public int GatedObjectiveNum =>
        _binaryFlags.Read<byte>(158, 5);

    public bool VanillaAgility => _binaryFlags.Read<bool>(441);
    public bool XNoKeyBonus => _binaryFlags.Read<bool>(450);
    public bool XCrystalBonus => _binaryFlags.Read<bool>(451);
    public ObjectiveXpBonus XObjectiveBonus => _binaryFlags.Read<ObjectiveXpBonus>(452, 3);
    public KeyItemCheckXpBonus XKeyItemCheckBonus => _binaryFlags.Read<KeyItemCheckXpBonus>(455, 3);
    public KeyItemZonkXpBonus XKeyItemZonkXpBonus => _binaryFlags.Read<KeyItemZonkXpBonus>(458, 3);
    public MiabXpBonus XMiabXpBonus => _binaryFlags.Read<MiabXpBonus>(460, 2);
    public MoonXpBonus XMoonXpBonus => _binaryFlags.Read<MoonXpBonus>(462, 2);
    public bool XSmallParty => _binaryFlags.Read<bool>(465);
}


