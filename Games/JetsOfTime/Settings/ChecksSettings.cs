using FF.Rando.Companion.Settings;
using Newtonsoft.Json.Linq;
using System.ComponentModel;

namespace FF.Rando.Companion.Games.JetsOfTime.Settings;

public class ChecksSettings(JToken jToken) : PanelSettings(jToken)
{
    public override string Name => "Checks";

    [DefaultValue(4)]
    public override int Priority
    {
        get => GetSetting(4);
        set => SaveSetting(value);
    }


    [DisplayName("Combine Eras")]
    [Description("Display all eras at once (Will probably require scrolling)")]
    [DefaultValue(true)]
    public bool CombineEras
    {
        get => GetSetting(true);
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
}
