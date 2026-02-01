using FF.Rando.Companion.Extensions;

namespace FF.Rando.Companion.Games.FreeEnterprise._4._6._0;
internal class Flags : IFlags
{
    private readonly byte[] _binaryFlags;
    public Flags(byte[] binaryFlags)
    {
        if (binaryFlags.Length < 33)
        {
            _binaryFlags = new byte[33];
            binaryFlags.CopyTo(_binaryFlags, 0);
        }
        else
        {
            _binaryFlags = binaryFlags;
        }
    }

    public bool KMain => _binaryFlags.Read<bool>(74);
    public bool KSummon => _binaryFlags.Read<bool>(75);
    public bool KMoon => _binaryFlags.Read<bool>(76);
    public bool KMaib => _binaryFlags.Read<bool>(77);
    public bool KNoFree => _binaryFlags.Read<bool>(78);
    public bool KUnsafe => _binaryFlags.Read<bool>(79);


    public bool CNoFree => _binaryFlags.Read<bool>(87);
    public bool CNoEarned => _binaryFlags.Read<bool>(88);
    public bool CClassicGiant => _binaryFlags.Read<bool>(1);
    public bool CHero => _binaryFlags.Read<bool>(166);

    public bool OWinGame => _binaryFlags.Read<bool>(72);
    public bool OWinCrystal => _binaryFlags.Read<bool>(73);

    public int NumRequiredObjectives =>
        _binaryFlags.Read<byte>(68, 4) switch
        {
            1 => -1,
            var x => x - 1
        };

    public bool VanillaAgility => _binaryFlags.Read<bool>(240);

    public bool XNoKeyBonus => _binaryFlags.Read<bool>(238);
}
