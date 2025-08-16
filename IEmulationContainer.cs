using BizHawk.Client.Common;
using BizHawk.Emulation.Common;
using FF.Rando.Companion.MemoryManagement;

namespace FF.Rando.Companion;
public interface IEmulationContainer
{
    IEmulationApi Emulation { get; }
    IInputApi Input { get; }
    IMemoryEventsApi? MemoryEvents { get; }
    IMemorySpace Rom { get; }
    IMemorySpace Sram { get; }
    IMemorySpace Wram { get; }

    void Update(ApiContainer container);
    void Update(IMemoryDomains domains);
}