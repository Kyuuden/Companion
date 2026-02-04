using FF.Rando.Companion.Games.WorldsCollide;
using FF.Rando.Companion.Games.WorldsCollide.Enums;
using FF.Rando.Companion.View;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;

namespace FF.Rando.Companion.Games.WorldsCollide.Tracking;

public class Character : IDisposable, INotifyPropertyChanged, IImageTracker
{
    private readonly Seed _seed;

    private Bitmap? _image;
    private TimeSpan? _whenFound;
    private bool _isFound;

    public Character(Seed seed, Events @event)
    {
        Event = @event;
        _seed = seed;
        seed.PropertyChanged += Settings_PropertyChanged;
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
        }
    }

    public TimeSpan? WhenFound
    {
        get => _whenFound;
        set
        {
            if (_whenFound == value || _whenFound.HasValue)
                return;

            _whenFound = value;
            NotifyPropertyChanged();
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
