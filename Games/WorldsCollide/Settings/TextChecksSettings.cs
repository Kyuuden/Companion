using FF.Rando.Companion.Settings;
using Newtonsoft.Json.Linq;
using System.ComponentModel;

namespace FF.Rando.Companion.Games.WorldsCollide.Settings;

public enum CompletedChecks
{
    Hidden,
    Disabled
}

public class TextChecksSettings(JToken jToken) : PanelSettings(jToken)
{
    protected override float DefaultScaleFactor => 1f;

    public override string Name => "TextChecks";

    [DefaultValue(1.0f)]
    public override float ScaleFactor { get => base.ScaleFactor; set => base.ScaleFactor = value; }

    [DefaultValue(5)]
    public override int Priority
    {
        get => GetSetting(5);
        set => SaveSetting(value);
    }

    [DefaultValue(false)]
    public override bool Enabled 
    { 
        get => GetSetting(false);
        set => SaveSetting(value);
    }

    [DefaultValue(CompletedChecks.Disabled)]
    [DisplayName("Completed Checks")]
    [Description("How to display completed checks:\n'Hidden': Completed checks are not shown.\n'Disabled': Compelted checks are shown greyed-out.")]
    public CompletedChecks CompletedChecks
    {
        get => GetSetting(CompletedChecks.Disabled);
        set => SaveSetting(value);
    }
}

