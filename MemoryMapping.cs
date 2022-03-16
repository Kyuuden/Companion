using BizHawk.Client.Common;
using BizHawk.FreeEnterprise.Companion.Extensions;

namespace BizHawk.FreeEnterprise.Companion
{
    public class MemoryMapping
    {
        private readonly IMemoryApi _memory;

        public MemoryMapping(IMemoryApi memory)
        {
            _memory = memory;

            var domains = _memory.GetMemoryDomainList();

            var cartRomDomain = domains.Find(d => d == "CARTROM" || d == "CARTRIDGE_ROM");
            var cartRamDomain = domains.Find(d => d == "CARTRAM" || d == "CARTRIDGE_RAM");
            var mainDomain = domains.Find(d => d == "WRAM");
            var busDomain = domains.Find(d => d == "System Bus");

            CartRom = new MemorySpace(_memory, cartRomDomain);
            CartSaveRam = new MemorySpace(_memory, cartRamDomain);
            Main = new MemorySpace(_memory, mainDomain);
            Bus = new MemorySpace(_memory, busDomain);
        }

        public MemorySpace CartRom { get; }
        public MemorySpace CartSaveRam { get; }
        public MemorySpace Main { get; }
        public MemorySpace Bus { get; }
    }

    public class MemorySpace
    {
        private readonly IMemoryApi _memoryApi;
        public string Domain { get; }

        public MemorySpace(IMemoryApi memoryApi, string domain)
        {
            _memoryApi = memoryApi;
            Domain = domain;
        }

        public byte[] ReadBytes(long startByte, int numbytes)
            => _memoryApi.ReadByteRange(startByte, numbytes, Domain).ToArray();

        public T Read<T>(long startBytes, int numbytes)
            => ReadBytes(startBytes, numbytes).Read<T>(0, (uint)numbytes * 8);
    }
}
