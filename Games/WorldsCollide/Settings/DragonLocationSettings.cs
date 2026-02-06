using FF.Rando.Companion.Settings;
using Newtonsoft.Json.Linq;
using System.ComponentModel;

namespace FF.Rando.Companion.Games.WorldsCollide.Settings;

public class DragonLocationSettings(JToken jToken) : PanelSettings(jToken)
{
    protected override float DefaultScaleFactor => 1.75f;

    public override string Name => "DragonLocations";

    [DefaultValue(1.75f)]
    public override float ScaleFactor { get => base.ScaleFactor; set => base.ScaleFactor = value; }

    [DefaultValue(4)]
    public override int Priority
    {
        get => GetSetting(4);
        set => SaveSetting(value);
    }
}

