using FF.Rando.Companion.Settings;
using Newtonsoft.Json.Linq;
using System.ComponentModel;

namespace FF.Rando.Companion.MysticQuestRandomizer.Settings;

internal class ElementsSettings(JToken parentData) : PanelSettings(parentData)
{
    public override string Name => "Elements";

    [Description("How to show key items, either icons (like the game info menu) or text.")]
    [DefaultValue(ElementsStyle.Icons)]
    public ElementsStyle ElementsStyle
    {
        get => GetSetting(ElementsStyle.Icons);
        set => SaveSetting(value);
    }

    [DefaultValue(2)]
    public override int Priority
    {
        get => GetSetting(2);
        set => SaveSetting(value);
    }
}

