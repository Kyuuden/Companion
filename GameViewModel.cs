using BizHawk.Client.Common;
using BizHawk.Emulation.Common;
using FF.Rando.Companion.Settings;
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
    private readonly ISettings _settings;
    private readonly List<IGameParser> _gameParsers;

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

    public void OnFrame(IGameInfo gameInfo)
    {
        if (Game == null || Game.Hash != gameInfo.Hash)
        {
            Initialize(gameInfo);
            return;
        }

        Game.OnNewFrame();
    }

    public void Initialize(IGameInfo gameInfo)
    {
        if (Game != null && Game.Hash == gameInfo.Hash)
            return;

        if (gameInfo.IsNullInstance())
            return;

        foreach (var parser in _gameParsers)
        {
            if (parser.TryParseGameInfo(APIs!, MemoryDomains!, _settings, gameInfo, out var game))
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