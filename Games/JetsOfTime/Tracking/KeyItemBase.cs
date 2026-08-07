using FF.Rando.Companion.Extensions;
using FF.Rando.Companion.Timing;
using FF.Rando.Companion.View;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;

namespace FF.Rando.Companion.Games.JetsOfTime.Tracking;

internal abstract class KeyItemBase : INotifyPropertyChanged, IImageTracker
{
    private Bitmap? _image;
    private bool _isFound;
    private bool _hasBeenFound;
    private bool _exists;

    public KeyItemBase(ITimer timer, KeyItemType type)
    {
        Timer = timer;
        Type = type;
        Id = (byte)type;
    }

    protected ITimer Timer { get; }

    public KeyItemType Type { get; }

    public byte Id { get; }

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
                Timer.Info($"Found {Type}");
            }
        }
    }

    public bool Exists
    {
        get => _exists;
        set
        {
            if (_exists == value)
                return;

            _exists = value;
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

    public virtual string AltText => Type.GetDescription();

    protected abstract void SetImage();

    public event PropertyChangedEventHandler? PropertyChanged;

    protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
