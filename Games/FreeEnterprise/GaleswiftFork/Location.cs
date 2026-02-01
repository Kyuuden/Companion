using FF.Rando.Companion.Games.FreeEnterprise;
using FF.Rando.Companion.Games.FreeEnterprise.Shared;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace FF.Rando.Companion.Games.FreeEnterprise.GaleswiftFork;
internal class Location(RewardSlot type, World world, string description, bool isKI, bool isChar) : ILocation
{
    private bool _isAvailable;
    private bool _isChecked;

    public int ID { get; } = (int)type;

    public string Description { get; } = description;

    public bool IsCharacter { get; } = isChar;

    public bool IsKeyItem { get; } = isKI;

    public bool IsBoss { get; }

    public bool IsShop { get; }

    public bool IsChecked
    {
        get => _isChecked;
        set
        {
            if (_isChecked == value)
                return;

            _isChecked = value;
            NotifyPropertyChanged();
        }
    }

    public bool IsAvailable
    {
        get => _isAvailable;
        set
        {
            if (_isAvailable == value)
                return;

            _isAvailable = value;
            NotifyPropertyChanged();
        }
    }

    public World World { get; } = world;

    public event PropertyChangedEventHandler? PropertyChanged;

    protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
