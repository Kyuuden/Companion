using BizHawk.Client.Common;
using BizHawk.Emulation.Common;
using FF.Rando.Companion.MemoryManagement;
using FF.Rando.Companion.Settings;
using FF.Rando.Companion.Timing;
using System;

namespace FF.Rando.Companion.Games;
public interface IEmulationContainer
{
    IEmulationApi Emulation { get; }
    IInputApi Input { get; }
    IMemoryEventsApi? MemoryEvents { get; }
    IMemorySpace Rom { get; }
    IMemorySpace Sram { get; }
    IMemorySpace Wram { get; }

    event Action<InputAction> ButtonPressed;

    ITimer Timer { get; }

    void RaiseButtonPressed(InputAction input);
    void Update(ApiContainer container);
    void Update(IMemoryDomains domains);
    void Update(ITimer timer);
}