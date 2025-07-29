using Newtonsoft.Json.Linq;
using System.ComponentModel;

namespace FF.Rando.Companion.FreeEnterprise.Settings;

public class StatsSettings : PanelSettings
{

    protected override float DefaultScaleFactor => 1f;

    public StatsSettings(JToken jToken)
        :base(jToken) { }

    public override string Name => "Stats";

    [DefaultValue(1.0f)]
    public override float ScaleFactor { get => base.ScaleFactor; set => base.ScaleFactor = value; }
}
