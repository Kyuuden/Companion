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
    Control CreateTrackingControl();
    bool RequiresMemoryEventsForTiming { get; }
    GameSettings Settings { get; }

    IEmulationContainer Container { get; }

    void OnNewFrame(bool isOnTrackingInterval);
}
