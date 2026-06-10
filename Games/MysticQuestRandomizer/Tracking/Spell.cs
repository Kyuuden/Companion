using FF.Rando.Companion.Extensions;
using FF.Rando.Companion.View;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;

namespace FF.Rando.Companion.Games.MysticQuestRandomizer.Tracking;
public class Spell : IImageTracker
{
    private readonly Seed _seed;
    private readonly SpellType _spellType;
    private Bitmap? _image;
    private bool _isFound;
    private bool _hasBeenFound;


    internal Spell(Seed seed, SpellType type)
    {
        _spellType = type;
        _seed = seed;
        SetImage();
    }

    public int Id => (int)_spellType;

    public bool IsFound
    {
        get => _isFound;
        set
        {
            if (_isFound == value)
                return;

            _isFound = value;
            NotifyPropertyChanged();
            SetImage();

            if (_isFound && !_hasBeenFound)
            {
                _hasBeenFound = true;
                _seed.Container.Timer.Info($"Learned {_spellType.GetDescription()}");
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

    public event PropertyChangedEventHandler? PropertyChanged;

    private void SetImage()
    {
        Image = _seed.Sprites.GetSpell(_spellType, IsFound);
    }

    protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
