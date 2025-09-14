using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;

namespace FF.Rando.Companion.Settings;

public interface ISettings : INotifyPropertyChanged
{
    WindowStyle WindowStyle { get; }
    DockSide DockSide { get; }
    int DockOffset { get; }
    Size WindowSize { get; }
    Point WindowPosition { get; }
    bool IsWindowMaximized { get; }
    bool IsWindowMinimized { get; }
    int TrackingInterval { get; }
    System.Drawing.Font Font { get; }
    System.Drawing.Color TextColor { get; }
    public bool AutoPauseTimer { get; }
    JObject Source { get; }
    Dictionary<string, GameSettings> GameSettings { get; }

    string NextPanelButton { get; }
    string NextPageButton { get; }
    string PreviousPageButton { get; }
    string ScrollDownButton { get; }
    string ScrollUpButton { get; }

    void SaveToFile();
}
