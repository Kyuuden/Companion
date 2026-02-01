using FF.Rando.Companion.Games.FreeEnterprise;
using FF.Rando.Companion.Games.FreeEnterprise.Shared;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace FF.Rando.Companion.Games.FreeEnterprise._4._6._0;
internal class Location : ILocation
{
    private bool _isAvailable;
    private bool _isChecked;

    public Location(LocationType type, string description)
    {
        ID = (int)type;
        Description = description;
        if (type < LocationType.StartingItem)
            IsCharacter = true;
        else
            IsKeyItem = true;

        World = type.World();
    }

    public int ID { get; }

    public string Description { get; }

    public bool IsCharacter { get; }

    public bool IsKeyItem { get; }

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

    public World World { get; }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
