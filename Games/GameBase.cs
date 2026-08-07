using BizHawk.Client.Common;
using FF.Rando.Companion.MemoryManagement;
using FF.Rando.Companion.Settings;
using FF.Rando.Companion.Timing;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace FF.Rando.Companion.Games;

public abstract class GameBase<T> : IGame where T : GameSettings
{
    private readonly EmulationContainer<T> _emulationContainer;

    private Color _backgroundColor = Color.Black;
    private bool _started = false;
    private bool _victory = false;

    protected GameBase(string hash, EmulationContainer<T> emulationContainer)
    {
        Hash = hash;
        _emulationContainer = emulationContainer;
        _emulationContainer.ButtonPressed += ButtonPressed;
    }

    private void ButtonPressed(InputAction action)
    {
        if (action != InputAction.ToggleTimer)
            return;

        if (!Started)
            Started = true;
    }

    public string Hash { get; }

    public IMemorySpace Rom => _emulationContainer.Rom;
    public IMemorySpace Wram => _emulationContainer.Wram;
    public IMemorySpace Sram => _emulationContainer.Sram;
    public IInputApi Input => _emulationContainer.Input;
    public IEmulationApi Emulation => _emulationContainer.Emulation;
    public IMemoryEventsApi? MemoryEvents => _emulationContainer.MemoryEvents;
    public ITimer Timer => _emulationContainer.Timer;
    public ISettings GeneralSettings => _emulationContainer.GeneralSettings;
    public T Settings => _emulationContainer.GameSettings;
    public IEmulationContainer Container => _emulationContainer;
    GameSettings IGame.Settings => Settings;

    public abstract Bitmap Icon { get; }

    public abstract Control CreateTrackingControl();
    public virtual bool RequiresMemoryEventsForTiming => false;

    public event PropertyChangedEventHandler? PropertyChanged;
    protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public void OnNewFrame(bool isOnTrackingInterval)
    {
        if (!Started)
        {
            Started = CheckIfStarted();
        }

        if (!Victory)
        {
            Victory = CheckIfVictory();
        }

        if (isOnTrackingInterval)
        {
            ReadTrackingData();
        }
    }

    protected virtual bool CheckIfStarted() => false;

    protected virtual bool CheckIfVictory() => false;

    protected abstract void ReadTrackingData();

    public bool Started
    {
        get => _started;
        protected set
        {
            if (!_started && value)
            {
                _started = true;
                NotifyPropertyChanged();
                if (_started)
                {
                    Timer.Start();
                }
            }
        }
    }

    public bool Victory
    {
        get => _victory;
        protected set
        {
            if (!_victory && value)
            {
                _victory = true;
                NotifyPropertyChanged();
                if (_victory)
                {
                    Timer.Stop();
                }
            }
        }
    }

    public Color BackgroundColor
    {
        get => _backgroundColor;
        protected set
        {
            if (value != _backgroundColor)
            {
                _backgroundColor = value;
                NotifyPropertyChanged();
            }
        }
    }

    public virtual void Dispose()
    {
        _emulationContainer.ButtonPressed -= ButtonPressed;
    }
}
