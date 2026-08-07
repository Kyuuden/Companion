using FF.Rando.Companion.Extensions;
using FF.Rando.Companion.View;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;

namespace FF.Rando.Companion.Games.MysticQuestRandomizer.Tracking;

public abstract class Equipment<TType> : IImageTracker where TType : struct
{
    private readonly IList<TType> _order;
    private readonly Seed _seed;
    private ImmutableHashSet<TType> _found = [];
    private readonly HashSet<TType> _notified = [];
    private Bitmap? _image;

    public event PropertyChangedEventHandler? PropertyChanged;

    internal Equipment(Seed seed, IList<TType> order, EquipmentType equipmentType)
    {
        _seed = seed;
        _order = order;
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

            var newlyFound = value.Except(_found);

            _found = value;
            NotifyPropertyChanged();
            SetImage();

            foreach (var item in newlyFound)
            {
                if (_notified.Add(item))
                    _seed.Timer.Info($"Found {item.GetDescription()}");
            }
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

    public string AltText => Found.OrderBy(_order.IndexOf).Last().GetDescription();

    protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    private void SetImage()
    {
        if (Found.Count == 0)
            Image = _seed.Sprites.GetDefault(EquipmentType);
        else
            Image = _seed.Sprites.GetEquipment(Found.OrderBy(_order.IndexOf).Last());
    }
}
