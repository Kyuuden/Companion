using FF.Rando.Companion.FreeEnterprise.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;

namespace FF.Rando.Companion.FreeEnterprise._5._0._0;

internal class Boss : IBoss
{
    private Bitmap? _image;

    private Dictionary<BossLocationType, Encounter> _encounters = [];
    private readonly Descriptors _descriptors;

    public Boss(Descriptors descriptors, BossType type)
    {
        Id = (int)type;
        Name = descriptors.GetBossName(type);
        SetImage();
        _descriptors = descriptors;
    }

    public int Id { get; }

    public string Name { get; }

  
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

    public bool AddEncounter(BossLocationType loc, TimeSpan when)
    {
        if (!_encounters.ContainsKey(loc))
        {
            var description = _descriptors.GetBossLocationName(loc) ?? "UNKNOWN LOCATION";
            _encounters.Add(loc, new Encounter(description, when));
            NotifyPropertyChanged(nameof(Encounters));
            SetImage();
            return true;
        }

        return false;
    }

    public bool DefeatEncounter(BossLocationType loc, TimeSpan when)
    {
        if (_encounters.TryGetValue(loc, out var encounter))
        {
            if (!encounter.IsDefeated)
            {
                encounter.IsDefeated = true;
                encounter.WhenDefeated = when;
                NotifyPropertyChanged(nameof(Encounters));
                SetImage();
                return true;
            }
        }

        return false;
    }

    public bool IsGameAsset => false;

    public IEnumerable<IEncounter> Encounters => _encounters.Values;

    private void SetImage()
        => Image = ResourceLookup.GetBossIcon((BossType)Id, _encounters.Values.Any(), _encounters.Values.Any(e => e.IsDefeated));

    public event PropertyChangedEventHandler? PropertyChanged;

    protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
