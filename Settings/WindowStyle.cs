using System.ComponentModel;

namespace FF.Rando.Companion.Settings;

public enum WindowStyle
{
    [Description("Independent")]
    Custom,
    [Description("Docked")]
    Docked,
    [Description("Docked")]
    [Browsable(false)]
    Docked_Legacy
}
