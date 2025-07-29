using BizHawk.Common;
using BizHawk.Emulation.Common;
using FF.Rando.Companion.Extensions;
using System;

namespace FF.Rando.Companion.MemoryManagement;

public class MemoryDomainMemorySpace : IMemorySpace
{
    private readonly MemoryDomain _domain;

    public MemoryDomainMemorySpace(MemoryDomain domain)
    {
        if (domain == null) throw new ArgumentNullException(nameof(domain));
        _domain = domain;
    }

    public string? Name => _domain?.Name;

    public T Read<T>(Range<long> range)
        => ReadBytes(range).Read<T>(0, (uint)range.Length() * 8);

    public byte ReadByte(long address)
    {
        return _domain.PeekByte(address);
    }

    public byte[] ReadBytes(Range<long> range)
    {
        var data = new byte[range.Length()];
        _domain.BulkPeekByte(range, data);
        return data;
    }
}
