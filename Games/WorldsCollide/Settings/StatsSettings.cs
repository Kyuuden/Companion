using FF.Rando.Companion.Settings;
using Newtonsoft.Json.Linq;
using System.ComponentModel;

namespace FF.Rando.Companion.Games.WorldsCollide.Settings;

public class StatsSettings(JToken jToken) : PanelSettings(jToken)
{
    protected override float DefaultScaleFactor => 2f;

    public override string Name => "Stats";

    [DefaultValue(2.0f)]
    [Category("Statistics")]
    public override float ScaleFactor { get => base.ScaleFactor; set => base.ScaleFactor = value; }

    [DefaultValue(5)]
    public override int Priority
    {
        get => GetSetting(5);
        set => SaveSetting(value);
    }
}

