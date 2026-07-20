using FF.Rando.Companion.View;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;

namespace FF.Rando.Companion.Games.JetsOfTime.Tracking;
internal class Character : INotifyPropertyChanged, IImageTracker
{
    private readonly Seed _seed;
    private Bitmap? _image;
    private bool _isFound;
    private bool _hasBeenFound;

    public Character(Seed seed, CharacterType characterType)
    {
        _seed = seed;
        Type = characterType;
        AltText = Type.ToString();
        SetImage();
    }

    public byte Id => (byte)Type;

    public CharacterType Type { get; }

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
                _seed.Container.Timer.Info($"Found {Type}");
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

    public string AltText { get; }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    private void SetImage()
    {
        var sprite = _seed.SpriteDB.GetPortrait((PortraitType)Type);
        if (sprite != null)
            Image = sprite.Render(!IsFound);
    }
}
