using BizHawk.Client.Common;
using BizHawk.Emulation.Common;
using FF.Rando.Companion.Settings;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace FF.Rando.Companion;

public class GameViewModel : INotifyPropertyChanged
{
    private IGame? _game;
    private ApiContainer? _apiContainer;
    private IMemoryDomains? _memoryDomains;
    private ISettings _settings;

    public GameViewModel(ISettings settings)
    {
        _settings = settings;
    }

    private EmulationContainerBase? _emulationContainer { get; set; }

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

        if (gameInfo.Name.ToUpper().StartsWith("FF4FE"))
        {
            var feContainer = new FreeEnterprise.Container(APIs!, MemoryDomains!, _settings);
            _emulationContainer = feContainer;
            Game = FreeEnterprise.SeedFactory.Create(gameInfo.Hash, feContainer);
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}