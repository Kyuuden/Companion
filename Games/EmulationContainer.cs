using BizHawk.Client.Common;
using BizHawk.Emulation.Common;
using FF.Rando.Companion.MemoryManagement;
using FF.Rando.Companion.Settings;
using FF.Rando.Companion.Timing;
using System;

namespace FF.Rando.Companion.Games;

public class EmulationContainer<T> : IEmulationContainer where T : GameSettings
{
    public IEmulationApi Emulation { get; private set; }
    public IMemorySpace Rom { get; private set; }
    public IMemorySpace Wram { get; private set; }
    public IMemorySpace Sram { get; private set; }
    public IInputApi Input { get; private set; }
    public IMemoryEventsApi? MemoryEvents { get; private set; }
    public ITimer Timer { get; private set; }

    public ISettings GeneralSettings { get; }
    public T GameSettings { get; }

    public EmulationContainer(
        ApiContainer container,
        IMemoryDomains domains,
        ITimer timer,
        ISettings settings,
        string gameSettingsKey)
    {
        if (container == null) throw new ArgumentNullException(nameof(container));
        Input = container.Input;
        MemoryEvents = container.MemoryEvents;
        Emulation = container.Emulation;

        if (domains == null) throw new ArgumentNullException(nameof(domains));
        Wram = new MemoryDomainMemorySpace(domains["WRAM"] ?? throw new ArgumentNullException("Cannot find WRAM"));
        Rom = new MemoryDomainMemorySpace((domains.Has("CARTROM") ? domains["CARTROM"] : domains["CARTRIDGE_ROM"]) ?? throw new ArgumentNullException("Cannot find Cart ROM"));
        Sram = new MemoryDomainMemorySpace((domains.Has("CARTRAM") ? domains["CARTRAM"] : domains["CARTRIDGE_RAM"]) ?? throw new ArgumentNullException("Cannot find Cart RAM"));

        Timer = timer ?? throw new ArgumentNullException(nameof(timer));
        GeneralSettings = settings ?? throw new ArgumentNullException(nameof(settings));

        if (GeneralSettings.GameSettings.TryGetValue(gameSettingsKey, out var gameSettings) && gameSettings is T expected)
            GameSettings = expected;
        else
            throw new ArgumentException($"Unknown Game {gameSettingsKey}");
    }

    public event Action<InputAction>? ButtonPressed;

    public void Update(ApiContainer container)
    {
        if (container == null) throw new ArgumentNullException(nameof(container));
        Input = container.Input;
        MemoryEvents = container.MemoryEvents;
        Emulation = container.Emulation;
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
                case TimerStatus.AutomaticPaused:
                case TimerStatus.ManualPaused:
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
