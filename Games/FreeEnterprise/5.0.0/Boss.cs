using FF.Rando.Companion.Games.FreeEnterprise.Shared;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;

namespace FF.Rando.Companion.Games.FreeEnterprise._5._0._0;

internal class Boss : IBoss
{
    private Bitmap? _image;

    private readonly Dictionary<BossLocationType, Encounter> _encounters = [];
    private readonly Seed _seed;

    public Boss(Seed seed, BossType type)
    {
        _seed = seed;
        Id = (int)type;
        AltText = _seed.BossDescriptor.GetName(type);
        SetImage();
    }

    public int Id { get; }

    public string AltText { get; }

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

    public bool AddEncounter(BossLocationType loc)
    {
        if (!_encounters.ContainsKey(loc))
        {
            var description = _seed.BossDescriptor.GetLocationName(loc) ?? "UNKNOWN LOCATION";
            _encounters.Add(loc, new Encounter(description, _seed.Timer.Elapsed));
            NotifyPropertyChanged(nameof(Encounters));
            SetImage();
            return true;
        }

        return false;
    }

    public bool DefeatEncounter(BossLocationType loc)
    {
        if (_encounters.TryGetValue(loc, out var encounter))
        {
            if (!encounter.IsDefeated)
            {
                encounter.IsDefeated = true;
                encounter.WhenDefeated = _seed.Timer.Elapsed;
                NotifyPropertyChanged(nameof(Encounters));
                SetImage();
                return true;
            }
        }

        return false;
    }

    public IEnumerable<IEncounter> Encounters => _encounters.Values;

    private void SetImage()
        => Image = ResourceLookup.GetBossIcon((BossType)Id, _encounters.Values.Any(), _encounters.Values.Any(e => e.IsDefeated));

    public event PropertyChangedEventHandler? PropertyChanged;

    protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
