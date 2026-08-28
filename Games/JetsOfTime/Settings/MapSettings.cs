using FF.Rando.Companion.Settings;
using Newtonsoft.Json.Linq;
using System.ComponentModel;
using System.Drawing;

namespace FF.Rando.Companion.Games.JetsOfTime.Settings;

public class MapSettings(JToken jToken) : PanelSettings(jToken)
{
    public override string Name => "Maps";

    [DefaultValue(3)]
    public override int Priority
    {
        get => GetSetting(3);
        set => SaveSetting(value);
    }

    [DefaultValue(KnownColor.Fuchsia)]
    [DisplayName("Progression Marker Color")]
    public Color ProgressionMarkerColor
    {
        get => Color.FromArgb((int)GetSetting((uint)Color.Fuchsia.ToArgb()));
        set => SaveSetting((uint)value.ToArgb());
    }

    [DisplayName("Go Mode Marker Color")]
    [DefaultValue(KnownColor.Green)]
    public Color GoModeMarkerColor
    {
        get => Color.FromArgb((int)GetSetting((uint)Color.Green.ToArgb()));
        set => SaveSetting((uint)value.ToArgb());
    }

    [DisplayName("Points of Interest Marker Color")]
    [DefaultValue(KnownColor.DarkRed)]
    public Color PointOfInterestMarkerColor
    {
        get => Color.FromArgb((int)GetSetting((uint)Color.DarkRed.ToArgb()));
        set => SaveSetting((uint)value.ToArgb());
    }

    [DisplayName("Points of Interest Enabled")]
    [Description("Show markers for points of interest, where no progression can be found, but chests or other sources of items exist.")]
    [DefaultValue(false)]
    public bool ShowPointsOfInterest
    {
        get => GetSetting(false);
        set => SaveSetting(value);
    }

    [DefaultValue(false)]
    [Description("If true, shows all checks that exist in the seed, regardless if they are currently accessable")]
    [DisplayName("Show All Checks")]
    public bool ShowAllExistingChecks
    {
        get => GetSetting(false);
        set => SaveSetting(value);
    }

    [DefaultValue(true)]
    [Description("If true, Map changes to current time period automatically.")]
    public bool Follow
    {
        get => GetSetting(true);
        set => SaveSetting(value);
    }

    [Browsable(false)]
    public override float ScaleFactor { get => base.ScaleFactor; set => base.ScaleFactor = value; }
}
