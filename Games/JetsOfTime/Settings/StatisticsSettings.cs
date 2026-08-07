using FF.Rando.Companion.Settings;
using Newtonsoft.Json.Linq;
using System.ComponentModel;

namespace FF.Rando.Companion.Games.JetsOfTime.Settings;

public class StatisticsSettings(JToken jToken) : PanelSettings(jToken)
{
    public override string Name => "Statistics";

    [DefaultValue(4)]
    public override int Priority
    {
        get => GetSetting(4);
        set => SaveSetting(value);
    }
}