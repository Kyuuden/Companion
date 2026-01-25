using FF.Rando.Companion.WorldsCollide.Enums;
using FF.Rando.Companion.WorldsCollide.RomData;
using FF.Rando.Companion.WorldsCollide.Settings;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;

namespace FF.Rando.Companion.WorldsCollide.Tracking;

internal class Check : IDisposable, INotifyPropertyChanged
{
    private Bitmap? _image;
    private TimeSpan? _whenCompleted;
    private bool _isCompleted;
    private bool _isAvailable;
    private readonly Sprites _sprites;
    private readonly WorldsCollideSettings _settings;
    private ISpriteSet _spriteSet;

    public Check(Events @event, RomData.Font font, Sprites sprites, WorldsCollideSettings settings, Descriptors descriptors)
    {
        Event = @event;
        Description = descriptors.GetDescription(@event);
        _sprites = sprites;
        _settings = settings;
        _spriteSet = settings.SpriteSet;
        _settings.PropertyChanged += Settings_PropertyChanged;
    }

    private void Settings_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(WorldsCollideSettings.Sprites))
        {
            _spriteSet = _settings.SpriteSet;
            SetImage();
        }
    }

    public int Id => (int)Event;
    public Events Event { get; } 
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
        Image = _spriteSet.Get(_sprites, Event).Render(!IsCompleted);
    }

    public void Dispose()
    {
        _settings.PropertyChanged -= Settings_PropertyChanged;
    }
}
