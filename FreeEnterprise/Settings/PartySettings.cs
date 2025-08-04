using FF.Rando.Companion.FreeEnterprise.RomData;
using Newtonsoft.Json.Linq;
using System.ComponentModel;

namespace FF.Rando.Companion.FreeEnterprise.Settings;

public class PartySettings : PanelSettings
{
    public PartySettings(JToken jToken)
        : base(jToken)
    {
        Priority = 0;
        InTopPanel = true;
    }

    [DefaultValue(Pose.Stand)]
    public Pose Pose
    {
        get => GetSetting(Pose.Stand);
        set => SaveSetting(value);
    }

    public override string Name => "Party";

    [Browsable(false)]

    public override int Priority { get; set; }

    [Browsable(false)]
    public override bool InTopPanel { get; set; }
}
