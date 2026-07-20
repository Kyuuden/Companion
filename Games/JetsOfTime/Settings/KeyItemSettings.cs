using FF.Rando.Companion.Settings;
using Newtonsoft.Json.Linq;
using System.ComponentModel;

namespace FF.Rando.Companion.Games.JetsOfTime.Settings;

public class KeyItemSettings(JToken jToken) : PanelSettings(jToken)
{
    public override string Name => "KeyItems";

    [DefaultValue(2)]
    public override int Priority
    {
        get => GetSetting(2);
        set => SaveSetting(value);
    }
}
