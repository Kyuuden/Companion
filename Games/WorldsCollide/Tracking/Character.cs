using FF.Rando.Companion.Games.WorldsCollide.Enums;
using FF.Rando.Companion.View;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;

namespace FF.Rando.Companion.Games.WorldsCollide.Tracking;

public class Character : IDisposable, INotifyPropertyChanged, IImageTracker
{
    private readonly Seed _seed;
    private Bitmap? _image;
    private bool _isFound;
    private bool _hasBeenFound;

    public Character(Seed seed, Events @event)
    {
        Event = @event;
        _seed = seed;
        seed.PropertyChanged += Settings_PropertyChanged;
        AltText = _seed.Descriptors.GetDescription(Event);
        SetImage();
    }

    private void Settings_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(Seed.SpriteSet))
        {
            SetImage();
        }
    }

    public int Id => (int)Event;
    public Events Event { get; }

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
                _seed.Container.Timer.Info($"Found {AltText}");
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
        var sprite = _seed.SpriteSet.Get(Event);
        if (sprite != null)
            Image = sprite.Render(!IsFound);
    }

    public void Dispose()
    {
        _seed.PropertyChanged -= Settings_PropertyChanged;
    }
}
