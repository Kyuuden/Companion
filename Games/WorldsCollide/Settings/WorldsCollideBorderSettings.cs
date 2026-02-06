using FF.Rando.Companion.Settings;
using Newtonsoft.Json.Linq;
using System.ComponentModel;

namespace FF.Rando.Companion.Games.WorldsCollide.Settings;

public class WorldsCollideBorderSettings : BorderSettings
{
    public WorldsCollideBorderSettings(JToken parentData) : base(parentData)
    { }

    [DefaultValue(true)]
    [Description("If not enabled, backgrounds will be not rendered.")]
    [DisplayName("Backgrounds Enabled")]
    public virtual bool BackgroundsEnabled
    {
        get => GetSetting(true);
        set => SaveSetting(value);
    }

    [DefaultValue(true)]
    [Description("If not enabled, borders will be not rendered.")]
    [DisplayName("Borders Enabled")]
    public override bool BordersEnabled { get => base.BordersEnabled; set => base.BordersEnabled = value; }
}

