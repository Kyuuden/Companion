using FF.Rando.Companion.Settings;
using Newtonsoft.Json.Linq;
using System.ComponentModel;

namespace FF.Rando.Companion.Games.FreeEnterprise.Settings;

public class KeyItemSettings(JToken jToken) : PanelSettings(jToken)
{
    [Description("How to show key items, either text (like the track in-game menu) or icons.")]
    [DefaultValue(KeyItemStyle.Text)]
    public KeyItemStyle KeyItemStyle
    {
        get => GetSetting(KeyItemStyle.Text);
        set => SaveSetting(value);
    }

    public override string Name => "KeyItems";

    [DefaultValue(1)]
    public override int Priority
    {
        get => GetSetting(1);
        set => SaveSetting(value);
    }
}
