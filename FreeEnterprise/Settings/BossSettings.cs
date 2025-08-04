using Newtonsoft.Json.Linq;
using System.ComponentModel;

namespace FF.Rando.Companion.FreeEnterprise.Settings;

public class BossSettings : PanelSettings
{
    public BossSettings(JToken jToken)
        : base(jToken) { }

    [Browsable(false)]
    public override string Name => "Bosses";

    [DefaultValue(2)]
    public override int Priority
    {
        get => GetSetting(2);
        set => SaveSetting(value);
    }

    [DefaultValue(true)]
    public override bool InTopPanel
    {
        get => GetSetting(true);
        set => SaveSetting(value);
    }
}
