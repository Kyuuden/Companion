using System.ComponentModel;
using System.Drawing;

namespace FF.Rando.Companion.FreeEnterprise;

public interface IImageTracker : INotifyPropertyChanged
{
    Bitmap Image { get; }

    bool IsGameAsset { get; }
}