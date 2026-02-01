using FF.Rando.Companion.Extensions;

namespace FF.Rando.Companion.Games.FreeEnterprise._5._0._0;
internal class FlagsAlpha1 : IFlags
{
    private readonly byte[] _binaryFlags;
    public FlagsAlpha1(byte[] binaryFlags)
    {
        if (binaryFlags.Length < 128)
        {
            _binaryFlags = new byte[128];
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


    public bool CNoFree => _binaryFlags.Read<bool>(808);
    public bool CNoEarned => _binaryFlags.Read<bool>(809);
    public bool CNoGiant => _binaryFlags.Read<bool>(810);
    public bool CHero => _binaryFlags.Read<bool>(889);
    public bool CWishes => _binaryFlags.Read<bool>(890);

    public bool VanillaAgility => _binaryFlags.Read<bool>(984);

    public bool XNoKeyBonus => _binaryFlags.Read<bool>(942);

    public ObjectiveXpBonus XObjBonus => _binaryFlags.Read<ObjectiveXpBonus>(943, 10);

    public bool CNoPartner => false;

    public KeyItemCheckXpBonus XKeyItemCheckBonus => KeyItemCheckXpBonus.None;

    public KeyItemZonkXpBonus XKeyItemZonkXpBonus => KeyItemZonkXpBonus.None;

    public MaxXpRate XMaxXpRate => MaxXpRate.Unlimited;

    public XPBonusMode XPBonusMode => XPBonusMode.Multiplicative;
}
