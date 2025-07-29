using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace FF.Rando.Companion;

public interface IGame : INotifyPropertyChanged, IDisposable
{
    string Hash { get; }

    Bitmap Icon { get; }

    void OnNewFrame();

    bool Started { get; }

    TimeSpan Elapsed { get; }

    Control CreateControls();

    void Pause();
    void Unpause();
}
