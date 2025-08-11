using FF.Rando.Companion.FreeEnterprise.Shared;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace FF.Rando.Companion.FreeEnterprise._5._0._0;

internal class RewardSlotLocation : ILocation
{
    private bool _isAvailable;
    private bool _isChecked;

    public RewardSlotLocation(RewardSlot slot, string description, bool isKI, bool isChar)
    {
        ID = (int)slot;
        Description = description;
        IsCharacter = isChar;
        IsKeyItem = isKI;
        World = slot.World();
    }

    public int ID { get; }

    public string Description { get; }

    public bool IsCharacter { get; }

    public bool IsKeyItem { get; }

    public bool IsBoss => false;

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

    public World World { get; }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
