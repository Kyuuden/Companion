namespace BizHawk.FreeEnterprise.Companion.RomUtilities
{
    public interface IMemorySpace
    {
        string? Name { get; }
        T Read<T>(long startBytes, int numbytes);
        byte[] ReadBytes(long startByte, int numbytes);
    }
}