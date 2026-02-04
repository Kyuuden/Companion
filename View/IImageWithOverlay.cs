using System.ComponentModel;
using System.Drawing;

namespace FF.Rando.Companion.View;

public interface IImageWithOverlay : INotifyPropertyChanged
{
    Bitmap? Image { get; }
    Bitmap? Overlay { get; }

    Size DefaultSize { get; }
}