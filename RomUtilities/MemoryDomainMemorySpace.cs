using BizHawk.Common;
using BizHawk.Emulation.Common;
using BizHawk.FreeEnterprise.Companion.Extensions;

namespace BizHawk.FreeEnterprise.Companion.RomUtilities
{
    public class MemoryDomainMemorySpace : IMemorySpace
    {
        private readonly MemoryDomain? _domain;

        public MemoryDomainMemorySpace(MemoryDomain? domain)
        {
            _domain = domain;
        }

        public string? Name => _domain?.Name;

        public T Read<T>(long startBytes, int numbytes)
            => ReadBytes(startBytes, numbytes).Read<T>(0, (uint)numbytes * 8);

        public byte[] ReadBytes(long startByte, int numbytes)
        {
            var data = new byte[numbytes];
            _domain?.BulkPeekByte(startByte.RangeToExclusive(startByte + numbytes), data);
            return data;
        }
    }

}
