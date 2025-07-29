using BizHawk.Common;

namespace FF.Rando.Companion.MemoryManagement;

public interface IMemorySpace
{
    string? Name { get; }
    T Read<T>(Range<long> range);
    byte[] ReadBytes(Range<long> range);
    byte ReadByte(long address);
}
