using Newtonsoft.Json.Linq;
using System.ComponentModel;

namespace FF.Rando.Companion.FreeEnterprise.Settings;

public class BossSettings : PanelSettings
{
    public BossSettings(JToken jToken)
        : base(jToken) { }

    [Browsable(false)]
    public override string Name => "Bosses";
}
