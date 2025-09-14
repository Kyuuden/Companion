using FF.Rando.Companion.Settings;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace FF.Rando.Companion;

public interface IGame: INotifyPropertyChanged, IDisposable
{
    string Hash { get; }

    Bitmap Icon { get; }

    Color BackgroundColor { get; }

    void OnNewFrame();

    bool Started { get; }

    TimeSpan Elapsed { get; }

    Control CreateControls();

    bool RequiresMemoryEvents { get; }

    void Pause();
    void Unpause();

    IEmulationContainer Container { get; }

    GameSettings Settings { get; }
}
