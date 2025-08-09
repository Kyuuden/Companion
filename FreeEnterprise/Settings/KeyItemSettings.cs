using FF.Rando.Companion.Extensions;
using Newtonsoft.Json.Linq;
using System.ComponentModel;

namespace FF.Rando.Companion.FreeEnterprise.Settings;

public class KeyItemSettings : PanelSettings
{
    public KeyItemSettings(JToken jToken)
    : base(jToken)
    {
        KeyItemStyle = jToken?[nameof(KeyItemStyle).ToSnakeCase()]?.ToObject<KeyItemStyle>() ?? KeyItemStyle.Text;
    }

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
