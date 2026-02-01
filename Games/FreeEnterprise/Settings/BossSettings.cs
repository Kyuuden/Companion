using FF.Rando.Companion.Settings;
using Newtonsoft.Json.Linq;
using System.ComponentModel;

namespace FF.Rando.Companion.Games.FreeEnterprise.Settings;

public class BossSettings(JToken jToken) : PanelSettings(jToken)
{
    [Browsable(false)]
    public override string Name => "Bosses";

    [DefaultValue(2)]
    public override int Priority
    {
        get => GetSetting(2);
        set => SaveSetting(value);
    }
}
