using FF.Rando.Companion.MysticQuestRandomizer.RomData;
using FF.Rando.Companion.View;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;

namespace FF.Rando.Companion.MysticQuestRandomizer;

public abstract class Equipment<TType> : IImageTracker
{
    private IList<TType> _order;
    private readonly Sprites _sprites;
    private ImmutableHashSet<TType> _found = [];
    private Bitmap? _image;

    public event PropertyChangedEventHandler? PropertyChanged;

    internal Equipment(IList<TType> order, EquipmentType equipmentType, Sprites sprites)
    {
        _order = order;
        _sprites = sprites;
        Desired = [.. order];
        EquipmentType = equipmentType;
        
        SetImage();
    }

    public EquipmentType EquipmentType { get; }

    public ImmutableHashSet<TType> Desired { get; }

    public ImmutableHashSet<TType> Found
    {
        get => _found;
        set
        {
            if (_found.SetEquals(value))
                return;

            _found = value;
            NotifyPropertyChanged();
            SetImage();
        }
    }

    public Bitmap Image
    {
        get => _image!;
        set
        {
            if (_image == value)
                return;

            _image = value;
            NotifyPropertyChanged();
        }
    }

    protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    private void SetImage()
    {
        if (Found.Count == 0)
            Image = _sprites.GetDefault(EquipmentType);
        else
            Image = _sprites.GetEquipment(Found.OrderBy(_order.IndexOf).Last());
    }
}
