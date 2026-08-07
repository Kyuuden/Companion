using FF.Rando.Companion.Settings;
using Newtonsoft.Json.Linq;
using System.ComponentModel;

namespace FF.Rando.Companion.Games.JetsOfTime.Settings;

public class BossSettings(JToken jToken) : PanelSettings(jToken)
{
    public override string Name => "Bosses";

    [DefaultValue(5)]
    public override int Priority
    {
        get => GetSetting(5);
        set => SaveSetting(value);
    }

}
