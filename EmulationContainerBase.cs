using BizHawk.Client.Common;
using BizHawk.Emulation.Common;
using FF.Rando.Companion.MemoryManagement;
using System;

namespace FF.Rando.Companion;
public abstract class EmulationContainerBase : IEmulationContainer
{
    public IMemorySpace Rom { get; private set; }
    public IMemorySpace Wram { get; private set; }
    public IMemorySpace Sram { get; private set; }
    public IEmulationApi Emulation { get; private set; }
    public IInputApi Input { get; private set; }
    public IMemoryEventsApi? MemoryEvents { get; private set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    protected EmulationContainerBase(ApiContainer container, IMemoryDomains domains)
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    {
        Update(container);
        Update(domains);
    }

    public void Update(ApiContainer container)
    {
        if (container == null) throw new ArgumentNullException(nameof(container));
        Emulation = container.Emulation;
        Input = container.Input;
        MemoryEvents = container.MemoryEvents;
    }

    public void Update(IMemoryDomains domains)
    {
        if (domains == null) throw new ArgumentNullException(nameof(domains));

        Wram = new MemoryDomainMemorySpace(domains["WRAM"] ?? throw new ArgumentNullException("Cannot find WRAM"));
        Rom = new MemoryDomainMemorySpace((domains.Has("CARTROM") ? domains["CARTROM"] : domains["CARTRIDGE_ROM"]) ?? throw new ArgumentNullException("Cannot find Cart ROM"));
        Sram = new MemoryDomainMemorySpace((domains.Has("CARTRAM") ? domains["CARTRAM"] : domains["CARTRIDGE_RAM"]) ?? throw new ArgumentNullException("Cannot find Cart RAM"));
    }

}
