using FF.Rando.Companion.Settings;
using Newtonsoft.Json.Linq;
using System.ComponentModel;

namespace FF.Rando.Companion.Games.FreeEnterprise.Settings;

public class ObjectivesSettings(JToken jToken) : PanelSettings(jToken)
{
    public override string Name => "Objectives";

    [DisplayName("Combine Objective Groups")]
    [Description("Display all objective groups at once (Will probably require scrolling)")]
    [DefaultValue(false)]
    public bool CombineObjectiveGroups
    {
        get => GetSetting(false);
        set => SaveSetting(value);
    }

    [DisplayName("Lines to scroll")]
    [Description("How many lines to scroll for each scroll up or down action.")]
    [DefaultValue(2)]
    public int ScrollLines
    {
        get => GetSetting(2);
        set => SaveSetting(value);
    }

    [DefaultValue(3)]
    public override int Priority
    {
        get => GetSetting(3);
        set => SaveSetting(value);
    }
}
