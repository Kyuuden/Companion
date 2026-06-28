using FF.Rando.Companion.Extensions;

namespace FF.Rando.Companion.Games.FreeEnterprise._5._0._0;
internal class FlagsAlpha3 : IFlags
{
    private readonly byte[] _binaryFlags;

    public FlagsAlpha3(byte[] binaryFlags)
    {
        if (binaryFlags.Length < 132)
        {
            _binaryFlags = new byte[132];
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

    public bool CNoFree => _binaryFlags.Read<bool>(814);
    public bool CNoEarned => _binaryFlags.Read<bool>(815);
    public bool CNoGiant => _binaryFlags.Read<bool>(816);
    public bool CNoPartner => _binaryFlags.Read<bool>(848);
    public bool CPartnerChar => _binaryFlags.Read<bool>(849);

    public bool CHero => _binaryFlags.Read<bool>(900);
    public bool CWishes => _binaryFlags.Read<bool>(901);

    public bool XNoKeyBonus => _binaryFlags.Read<bool>(1005);

    public ObjectiveXpBonus XObjBonus => _binaryFlags.Read<ObjectiveXpBonus>(1006, 4);
    public KeyItemCheckXpBonus XKeyItemCheckBonus  => _binaryFlags.Read<KeyItemCheckXpBonus>(1010, 3);
    public KeyItemZonkXpBonus XKeyItemZonkXpBonus => _binaryFlags.Read<KeyItemZonkXpBonus>(1013, 3);
    public MaxXpRate XMaxXpRate => _binaryFlags.Read<MaxXpRate>(1016, 4);
    public BaseXpRate XBaseXpRate => BaseXpRate._100Percent;
    public XPBonusMode XPBonusMode => _binaryFlags.Read<XPBonusMode>(1020, 2);

    public bool VanillaAgility => _binaryFlags.Read<bool>(1053);
}