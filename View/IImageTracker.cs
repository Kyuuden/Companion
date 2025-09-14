using System.ComponentModel;
using System.Drawing;

namespace FF.Rando.Companion.View;

public interface IImageTracker : INotifyPropertyChanged
{
    Bitmap Image { get; }
}