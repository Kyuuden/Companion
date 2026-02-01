using FF.Rando.Companion.Games.WorldsCollide;
using FF.Rando.Companion.View;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;

namespace FF.Rando.Companion.Games.WorldsCollide.Tracking;

public class Dragon : IDisposable, INotifyPropertyChanged, IImageTracker
{
    private readonly Seed _seed;

    private Bitmap? _image;
    private TimeSpan? _whenDefeated;
    private bool _isDefeated;

    public Dragon(Seed seed, Enums.Dragons dragon)
    {
        _seed = seed;
        DragonType = dragon;
        Description = _seed.Descriptors.GetDescription(dragon);
        _seed.PropertyChanged += Settings_PropertyChanged;
        SetImage();
    }

    private void Settings_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(Seed.SpriteSet))
        {
            SetImage();
        }
    }

    public int Id => (int)DragonType;
    public Enums.Dragons DragonType { get; }
    public string Description { get; }

    public bool IsDefeated
    {
        get => _isDefeated;
        set
        {
            if (_isDefeated == value)
                return;

            _isDefeated = value;
            NotifyPropertyChanged();
            SetImage();
        }
    }

    public TimeSpan? WhenDefeated
    {
        get => _whenDefeated;
        set
        {
            if (_whenDefeated == value || _whenDefeated.HasValue)
                return;

            _whenDefeated = value;
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
        var sprite = _seed.SpriteSet.Get(DragonType);
        if (sprite != null)
            Image = sprite.Render(!IsDefeated);
    }

    public void Dispose()
    {
        _seed.PropertyChanged -= Settings_PropertyChanged;
    }
}
