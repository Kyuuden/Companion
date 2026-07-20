using BizHawk.Common.ReflectionExtensions;
using FF.Rando.Companion.Rendering;
using FF.Rando.Companion.View;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;

namespace FF.Rando.Companion.Games.JetsOfTime.Tracking;
internal class Boss : INotifyPropertyChanged, IImageTracker
{
    private readonly Container _container;
    private readonly ISprite? _sprite;
    private Bitmap? _image;
    private bool _isDefeated;
    private bool _hasBeenDefeated;

    public Boss(Container container, BossType boss, ISprite? sprite)
    {
        _container = container;
        Type = boss;
        _sprite = sprite;
        SetImage();
    }

    public byte Id => (byte)Type;

    public BossType Type { get; }

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

            if (_isDefeated && !_hasBeenDefeated)
            {
                _hasBeenDefeated = true;
                _container.Timer.Info($"Defeated {Type}");
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

    public string AltText => Type.GetDescription();

    public event PropertyChangedEventHandler? PropertyChanged;

    protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    protected void SetImage()
    {
        var tmp = _sprite?.Render(!IsDefeated);
        if (tmp != null)
            Image = tmp;
    }
}
