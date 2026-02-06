using FF.Rando.Companion.Settings;
using Newtonsoft.Json.Linq;
using System.ComponentModel;

namespace FF.Rando.Companion.Games.WorldsCollide.Settings;

public class TextChecksSettings(JToken jToken) : PanelSettings(jToken)
{
    protected override float DefaultScaleFactor => 1f;

    public override string Name => "TextChecks";

    [DefaultValue(1.0f)]
    public override float ScaleFactor { get => base.ScaleFactor; set => base.ScaleFactor = value; }

    [DefaultValue(2)]
    public override int Priority
    {
        get => GetSetting(2);
        set => SaveSetting(value);
    }

    [DefaultValue(false)]
    public override bool Enabled 
    { 
        get => GetSetting(false);
        set => SaveSetting(value);
    }
}

