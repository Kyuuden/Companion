using System.ComponentModel;

namespace FF.Rando.Companion.Settings;

public enum WindowStyle
{
    [Description("Custom (Independent of BizHawk)")]
    Custom,
    [Description("Dock (16x9)")]
    Dock_16x9,
    [Description("Dock (16x10)")]
    Dock_16x10
}
