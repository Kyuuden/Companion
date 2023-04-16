using BizHawk.Client.Common;
using BizHawk.FreeEnterprise.Companion.Extensions;
using System.Linq;

namespace BizHawk.FreeEnterprise.Companion.RomUtilities
{
    public class MemoryApiSpace : IMemorySpace
    {
        private readonly IMemoryApi _memoryApi;
        public string Name { get; private set; }

        public MemoryApiSpace(IMemoryApi memoryApi, string domain)
        {
            _memoryApi = memoryApi;
            Name = domain;
        }

        public byte[] ReadBytes(long startByte, int numbytes)
            => _memoryApi.ReadByteRange(startByte, numbytes, Name).ToArray();

        public T Read<T>(long startBytes, int numbytes)
            => ReadBytes(startBytes, numbytes).Read<T>(0, (uint)numbytes * 8);
    }

}
