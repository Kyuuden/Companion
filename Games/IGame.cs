using FF.Rando.Companion.Settings;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace FF.Rando.Companion.Games;

public interface IGame : INotifyPropertyChanged, IDisposable
{
    string Hash { get; }

    Bitmap Icon { get; }

    Color BackgroundColor { get; }

    void OnNewFrame();

    Control CreateControls();

    bool RequiresMemoryEvents { get; }

    IEmulationContainer Container { get; }

    GameSettings Settings { get; }
}
