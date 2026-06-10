using BizHawk.Client.Common;
using BizHawk.Emulation.Common;
using FF.Rando.Companion.MemoryManagement;
using FF.Rando.Companion.Settings;
using FF.Rando.Companion.Timing;
using System;

namespace FF.Rando.Companion.Games;
public abstract class EmulationContainerBase : IEmulationContainer
{
    public IMemorySpace Rom { get; private set; }
    public IMemorySpace Wram { get; private set; }
    public IMemorySpace Sram { get; private set; }
    public IEmulationApi Emulation { get; private set; }
    public IInputApi Input { get; private set; }
    public IMemoryEventsApi? MemoryEvents { get; private set; }
    public ITimer Timer { get; private set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    protected EmulationContainerBase(ApiContainer container, IMemoryDomains domains, ITimer timer)
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    {
        Update(container);
        Update(domains);
        Update(timer);
    }

    public event Action<InputAction> ButtonPressed;

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

    public void RaiseButtonPressed(InputAction button)
    {
        if (button == InputAction.ToggleTimer)
        {
            switch (Timer.Status)
            {
                case TimerStatus.Running:
                    Timer.Pause();
                    break;
                case TimerStatus.Paused:
                    Timer.Resume();
                    break;
                case TimerStatus.Ready:
                    Timer.Start();
                    break;
            }

            return;
        }

        ButtonPressed?.Invoke(button);
    }

    public void Update(ITimer timer)
    {
        Timer = timer;
    }
}
