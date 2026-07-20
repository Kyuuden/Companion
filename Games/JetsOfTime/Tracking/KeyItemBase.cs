using FF.Rando.Companion.Extensions;
using FF.Rando.Companion.View;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;

namespace FF.Rando.Companion.Games.JetsOfTime.Tracking;

internal abstract class KeyItemBase(Container container, KeyItemType type) : INotifyPropertyChanged, IImageTracker
{
    private Bitmap? _image;
    private bool _isFound;
    private bool _hasBeenFound;

    protected Container Container { get; } = container;

    public KeyItemType Type { get; } = type;

    public byte Id { get; } = (byte)type;

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
                Container.Timer.Info($"Found {Type}");
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

    public virtual string AltText => Type.GetDescription();

    protected abstract void SetImage();

    public event PropertyChangedEventHandler? PropertyChanged;

    protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
