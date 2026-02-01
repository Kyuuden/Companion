using FF.Rando.Companion.Games.FreeEnterprise.RomData;
using FF.Rando.Companion.Settings;
using Newtonsoft.Json.Linq;
using System.ComponentModel;

namespace FF.Rando.Companion.Games.FreeEnterprise.Settings;

public class PartySettings(JToken jToken) : PanelSettings(jToken)
{
    [DefaultValue(Pose.Stand)]
    public Pose Pose
    {
        get => GetSetting(Pose.Stand);
        set => SaveSetting(value);
    }

    public override string Name => "Party";

    [Browsable(false)]

    public override int Priority { get; set; } = 0;
}
