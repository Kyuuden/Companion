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
    public Color MarkerColor
    {
        get => Color.FromArgb((int)GetSetting(0xFFFF00FF));
        set => SaveSetting((uint)value.ToArgb());
    }

    [DefaultValue(false)]
    [Description("If true, shows all checks that exist in the seed, regardless if they are currently accessable")]
    public bool ShowAllExistingChecks
    {
        get => GetSetting(false);
        set => SaveSetting(value);
    }

    [Browsable(false)]
    public override float ScaleFactor { get => base.ScaleFactor; set => base.ScaleFactor = value; }
}
