using FF.Rando.Companion.Rendering;
using FF.Rando.Companion.Timing;
using FF.Rando.Companion.View;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;

namespace FF.Rando.Companion.Games.JetsOfTime.Tracking;
internal class Character : INotifyPropertyChanged, IImageTracker
{
    private readonly ITimer _timer;
    private Bitmap? _image;
    private bool _isFound;
    private bool _hasBeenFound;
    private readonly ISprite _sprite;

    public Character(CharacterType characterType, ITimer timer, ISprite? sprite)
    {
        _timer = timer;
        _sprite = sprite ?? throw new ArgumentNullException(nameof(sprite));
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
                _timer.Info($"Found {Type}");
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
        Image = _sprite.Render(!IsFound);
    }
}
