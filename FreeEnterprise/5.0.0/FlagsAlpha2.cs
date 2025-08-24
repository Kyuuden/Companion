using FF.Rando.Companion.Extensions;

namespace FF.Rando.Companion.FreeEnterprise._5._0._0;
internal class FlagsAlpha2 : IFlags
{
    private readonly byte[] _binaryFlags;

    public FlagsAlpha2(byte[] binaryFlags)
    {
        if (binaryFlags.Length < 131)
        {
            _binaryFlags = new byte[131];
            binaryFlags.CopyTo(_binaryFlags, 0);
        }
        else
        {
            _binaryFlags = binaryFlags;
        }
    }

    public bool KMain => _binaryFlags.Read<bool>(788);
    public bool KSummon => _binaryFlags.Read<bool>(789);
    public bool KMoon => _binaryFlags.Read<bool>(790);
    public bool KChar => _binaryFlags.Read<bool>(796);
    public bool KForge => _binaryFlags.Read<bool>(797);
    public bool KMaib => _binaryFlags.Read<bool>(791);
    public bool KMaibAbove => _binaryFlags.Read<bool>(792);
    public bool KMaibBelow => _binaryFlags.Read<bool>(793);
    public bool KMaibLst => _binaryFlags.Read<bool>(794);
    public bool KMaibAll => _binaryFlags.Read<bool>(795);
    public bool KNoFree => _binaryFlags.Read<bool>(798);
    public bool KRisky => _binaryFlags.Read<bool>(799);

    public bool CNoFree => _binaryFlags.Read<bool>(809);
    public bool CNoEarned => _binaryFlags.Read<bool>(810);
    public bool CNoGiant => _binaryFlags.Read<bool>(811);
    public bool CNoPartner => _binaryFlags.Read<bool>(843);

    public bool CHero => _binaryFlags.Read<bool>(895);
    public bool CWishes => _binaryFlags.Read<bool>(896);

    public bool VanillaAgility => _binaryFlags.Read<bool>(1021);

    public bool XNoKeyBonus => _binaryFlags.Read<bool>(950);

    public ObjectiveXpBonus XObjBonus => _binaryFlags.Read<ObjectiveXpBonus>(951, 11);
    public KeyItemCheckXpBonus XKeyItemCheckBonus  => _binaryFlags.Read<KeyItemCheckXpBonus>(962, 7);
    public KeyItemZonkXpBonus XKeyItemZonkXpBonus => _binaryFlags.Read<KeyItemZonkXpBonus>(969, 7);
    public MaxXpRate XMaxXpRate => _binaryFlags.Read<MaxXpRate>(976, 12);
    public XPBonusMode XPBonusMode => _binaryFlags.Read<XPBonusMode>(988, 2);
}