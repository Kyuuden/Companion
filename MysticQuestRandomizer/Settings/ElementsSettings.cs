using FF.Rando.Companion.Settings;
using Newtonsoft.Json.Linq;
using System.ComponentModel;

namespace FF.Rando.Companion.MysticQuestRandomizer.Settings;

internal class ElementsSettings(JToken parentData) : PanelSettings(parentData)
{
    public override string Name => "Elements";

    protected override float DefaultScaleFactor => 3f;

    [DefaultValue(3.0f)]
    public override float ScaleFactor { get => base.ScaleFactor; set => base.ScaleFactor = value; }

    [Description("How to show elements, either icons (like the game info menu), text, or abbreviations.")]
    [DefaultValue(ElementsStyle.Icons)]
    public ElementsStyle ElementsStyle
    {
        get => GetSetting(ElementsStyle.Icons);
        set => SaveSetting(value);
    }

    [DisplayName("Hide Unchanged")]
    [Description("Hide elements that were randomized to themselves.")]
    [DefaultValue(false)]
    public bool HideUnchanged
    {
        get => GetSetting(false);
        set => SaveSetting(value);
    }

    [DefaultValue(2)]
    public override int Priority
    {
        get => GetSetting(2);
        set => SaveSetting(value);
    }
}

