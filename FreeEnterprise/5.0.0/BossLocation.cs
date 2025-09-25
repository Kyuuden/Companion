using FF.Rando.Companion.FreeEnterprise.Shared;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace FF.Rando.Companion.FreeEnterprise._5._0._0;

internal class BossLocation(BossLocationType slot, string description) : ILocation
{
    private bool _isAvailable;
    private bool _isChecked;

    public int ID { get; } = (int)slot;

    public string Description { get; } = description;

    public bool IsCharacter => false;

    public bool IsKeyItem => false;

    public bool IsBoss => true;

    public bool IsShop => false;

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

    public World World { get; } = slot.World();

    public event PropertyChangedEventHandler? PropertyChanged;

    protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
