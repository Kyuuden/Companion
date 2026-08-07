using BizHawk.Client.Common;
using BizHawk.Emulation.Common;
using FF.Rando.Companion.Settings;
using FF.Rando.Companion.Timing;
using System;

namespace FF.Rando.Companion.Games;
public interface IEmulationContainer
{
    event Action<InputAction>? ButtonPressed;
    void RaiseButtonPressed(InputAction button);
    void Update(ApiContainer container);
    void Update(IMemoryDomains domains);
    void Update(ITimer timer);
}