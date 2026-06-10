using BizHawk.Client.Common;
using BizHawk.Emulation.Common;
using FF.Rando.Companion.Games;
using FF.Rando.Companion.Settings;
using FF.Rando.Companion.Timing;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace FF.Rando.Companion;

public class GameViewModel : INotifyPropertyChanged
{
    private IGame? _game;
    private ApiContainer? _apiContainer;
    private IMemoryDomains? _memoryDomains;
    private ITimer? _timer;
    private readonly ISettings _settings;
    private readonly List<IGameParser> _gameParsers;
    private readonly Dictionary<InputAction, int> _activeInputActions = [];

    public GameViewModel(ISettings settings)
    {
        _settings = settings;
        var parserType = typeof(IGameParser);
        var parsers = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(s => s.GetTypes())
            .Where(p => p != parserType && parserType.IsAssignableFrom(p));

        _gameParsers = [];
        foreach (var parser in parsers)
        {
            _gameParsers.Add((IGameParser)Activator.CreateInstance(parser));
        }
    }

    private IEmulationContainer? _emulationContainer;

    public IGame? Game
    {
        get => _game;
        set
        {
            if (_game == value) return;

            _game = value;
            NotifyPropertyChanged();
        }
    }

    public ApiContainer? APIs
    {
        get => _apiContainer; 
        set
        {
            if (value == null || value == _apiContainer) return;

            _apiContainer = value;
            _emulationContainer?.Update(value);
        }
    }
    public IMemoryDomains? MemoryDomains
    {
        get => _memoryDomains; 
        set
        {
            if (value == null || _memoryDomains == value) return;
            _memoryDomains = value;
            _emulationContainer?.Update(value);
        }
    }

    public ITimer? Timer
    {
        get => _timer;
        set
        {
            if (value is null || _timer == value) 
                return;

            _timer = value;
            _emulationContainer?.Update(value);
        }
    }

    public void OnFrame(IGameInfo gameInfo)
    {
        if (Game == null || Game.Hash != gameInfo.Hash)
        {
            Initialize(gameInfo);
            return;
        }

        if (APIs != null)
        {
            var pressedButton = APIs.Input.GetPressedButtons();
            List<InputAction> seenActions = [];

            foreach (var button in pressedButton)
            {
                if (!TryGetAction(button, out var action))
                    continue;

                seenActions.Add(action);
                if (_activeInputActions.ContainsKey(action))
                {
                    _activeInputActions[action]++;
                    if (_activeInputActions[action] >= 30 && _activeInputActions[action] % 10 == 0)
                    {
                        _emulationContainer?.RaiseButtonPressed(action);
                    }
                }
                else
                {
                    _activeInputActions[action] = 0;
                    _emulationContainer?.RaiseButtonPressed(action);
                }
            }

            var toRemove = _activeInputActions.Keys.Except(seenActions).ToList();
            foreach (var button in toRemove) 
                _activeInputActions.Remove(button);
        }

        Game.OnNewFrame();
    }

    private bool TryGetAction(string button, out InputAction action)
    {
        action = InputAction.None;

        switch (button)
        {
            case "":
                return false;

            case string s when s.Equals(_settings.NextPageButton, StringComparison.InvariantCultureIgnoreCase):
                action = InputAction.NextPage;
                return true;

            case string s when s.Equals(_settings.PreviousPageButton, StringComparison.InvariantCultureIgnoreCase):
                action = InputAction.PreviousPage;
                return true;

            case string s when s.Equals(_settings.ScrollDownButton, StringComparison.InvariantCultureIgnoreCase):
                action = InputAction.ScrollDown;
                return true;

            case string s when s.Equals(_settings.ScrollUpButton, StringComparison.InvariantCultureIgnoreCase):
                action = InputAction.ScrollUp;
                return true;

            case string s when s.Equals(_settings.NextPanelButton, StringComparison.InvariantCultureIgnoreCase):
                action = InputAction.NextPanel;
                return true;

            case string s when s.Equals(_settings.ToggleTimerButton, StringComparison.InvariantCultureIgnoreCase):
                action = InputAction.ToggleTimer;
                return true;

            default:
                return false;
        }
    }

    public void Initialize(IGameInfo gameInfo)
    {
        if (Game != null && Game.Hash == gameInfo.Hash)
            return;

        if (gameInfo.IsNullInstance())
            return;

        foreach (var parser in _gameParsers)
        {
            if (parser.TryParseGameInfo(APIs!, MemoryDomains!, _settings, gameInfo, _timer!, out var game))
            {
                Game = game!;
                _emulationContainer = Game.Container;
                return;
            }
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}