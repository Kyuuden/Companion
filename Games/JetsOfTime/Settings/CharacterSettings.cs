using FF.Rando.Companion.Settings;
using Newtonsoft.Json.Linq;
using System.ComponentModel;

namespace FF.Rando.Companion.Games.JetsOfTime.Settings;

public class CharacterSettings(JToken jToken) : PanelSettings(jToken)
{
    public override string Name => "Characters";

    [DefaultValue(1)]
    public override int Priority
    {
        get => GetSetting(1);
        set => SaveSetting(value);
    }
}
