using BizHawk.Client.Common;
using BizHawk.Emulation.Common;
using System.Linq;

namespace BizHawk.FreeEnterprise.Companion.RomUtilities
{
    public class MemoryMapping
    {
        public MemoryMapping(IMemoryApi memory)
        {

            var domains = memory.GetMemoryDomainList().ToList();

            var cartRomDomain = domains.Find(d => d == "CARTROM" || d == "CARTRIDGE_ROM");
            var cartRamDomain = domains.Find(d => d == "CARTRAM" || d == "CARTRIDGE_RAM");
            var mainDomain = domains.Find(d => d == "WRAM");
            var busDomain = domains.Find(d => d == "System Bus");

            CartRom = new MemoryApiSpace(memory, cartRomDomain);
            CartSaveRam = new MemoryApiSpace(memory, cartRamDomain);
            Main = new MemoryApiSpace(memory, mainDomain);
            Bus = new MemoryApiSpace(memory, busDomain);
        }

        public MemoryMapping(IMemoryDomains? domains)
        {
            if (domains?.Has("CARTROM") == true)
                CartRom = new MemoryDomainMemorySpace(domains?["CARTROM"]);
            else
                CartRom = new MemoryDomainMemorySpace(domains?["CARTRIDGE_ROM"]);

            if (domains?.Has("CARTRAM") == true)
                CartSaveRam = new MemoryDomainMemorySpace(domains?["CARTRAM"]);
            else
                CartSaveRam = new MemoryDomainMemorySpace(domains?["CARTRIDGE_RAM"]);

            Main = new MemoryDomainMemorySpace(domains?["WRAM"]);
            Bus = new MemoryDomainMemorySpace(domains?["System Bus"]);
        }

        public IMemorySpace CartRom { get; }
        public IMemorySpace CartSaveRam { get; }
        public IMemorySpace Main { get; }
        public IMemorySpace Bus { get; }
    }

}
