using FF.Rando.Companion.Games.WorldsCollide;
using FF.Rando.Companion.Games.WorldsCollide.Enums;
using FF.Rando.Companion.View;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;

namespace FF.Rando.Companion.Games.WorldsCollide.Tracking;

public class Check : IDisposable, INotifyPropertyChanged, IImageTracker
{
    private readonly Seed _seed;

    private Bitmap? _image;
    private TimeSpan? _whenCompleted;
    private bool _isCompleted;
    private bool _isAvailable;
    private bool _isVisible = true;

    public Check(Seed seed, Events @event)
    {
        Event = @event;
        Description = seed.Descriptors.GetDescription(@event);

        Description += "\nRequirements:\n";

        var req = Event.GetRequirements();
        if (req.Count == 0)
            Description += "None";
        else
            Description += string.Join("\n", req.Select(seed.Descriptors.GetDescription));

        CharacterGate = Event.GetRequirements().FirstOrDefault(r => r.IsCharacter());

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

    public Events? CharacterGate { get; }

    public string Description { get; }

    public bool IsCompleted
    {
        get => _isCompleted;
        set
        {
            if (_isCompleted == value)
                return;

            _isCompleted = value;
            NotifyPropertyChanged();
            SetImage();
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
            SetImage();
        }
    }

    public bool IsVisible
    {
        get => _isVisible;
        set
        {
            if (_isVisible == value)
                return;

            _isVisible = value;
            NotifyPropertyChanged();
        }
    }

    public TimeSpan? WhenCompleted
    {
        get => _whenCompleted;
        set
        {
            if (_whenCompleted == value || _whenCompleted.HasValue)
                return;

            _whenCompleted = value;
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
            Image = sprite.Render(!IsCompleted);
    }

    public void Dispose()
    {
        _seed.PropertyChanged -= Settings_PropertyChanged;
    }
}
