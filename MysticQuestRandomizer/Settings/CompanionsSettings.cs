
using FF.Rando.Companion.Settings;
using Newtonsoft.Json.Linq;
using System.ComponentModel;

namespace FF.Rando.Companion.MysticQuestRandomizer.Settings;

internal class CompanionsSettings : PanelSettings
{
    public CompanionsSettings(JToken parentData) : base(parentData)
    {
    }

    public override string Name => "Companions";

    [DefaultValue(3)]
    public override int Priority
    {
        get => GetSetting(6);
        set => SaveSetting(value);
    }

    [DisplayName("Combine Companions")]
    [Description("Display all companion data at once. (Will probably require scrolling)")]
    [DefaultValue(true)]
    public bool CombineCompanions
    {
        get => GetSetting(true);
        set => SaveSetting(value);
    }
}
