using FF.Rando.Companion.Settings;
using FF.Rando.Companion.Settings.TypeConverters;
using Newtonsoft.Json.Linq;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace FF.Rando.Companion.Games.WorldsCollide.Settings;

public enum MapPosition
{
    Top, 
    //Left, 
    //Right, 
    Bottom, 
    None
}

public class WorldsCollideSettings : GameSettings
{
    public override string Name => "WorldsCollide";

    public override string DisplayName => "Worlds Collide";

    public override string Description => "Final Fantasy 6: Worlds Collide";

    public override PropertySort Sort { get; } = PropertySort.Categorized;

    public WorldsCollideSettings(JObject parent)
        : base(parent)
    {
        BorderSettings = new WorldsCollideBorderSettings(SettingsData);
    }

    [DefaultValue(SpriteSetType.Locations)]
    [TypeConverter(typeof(EnumDescriptionConverter))]
    [Category("Display")]
    [DisplayName("Icons")]
    [Description("Which set of icons should be used for characters, checks, dragons, dragon locations, and statistics.")]
    public SpriteSetType Icons
    {
        get => GetSetting(SpriteSetType.Locations);
        set => SaveSetting(value);
    }

    [DefaultValue(MapPosition.Bottom)]
    [Category("Display")]
    [DisplayName("Map Position")]
    [Description("World Map Position in tracker, with markers for available checks")]
    public MapPosition MapPosition
    {
        get => GetSetting(MapPosition.Bottom);
        set => SaveSetting(value);
    }

    [DefaultValue(KnownColor.Fuchsia)]
    [Category("Map")]
    [DisplayName("Possible Character Reward Color")]
    public Color CharacterRewardColor
    {
        get => Color.FromArgb((int)GetSetting((uint)Color.Magenta.ToArgb()));
        set => SaveSetting((uint)value.ToArgb());
    }

    [DefaultValue(KnownColor.Aqua)]
    [Category("Map")]
    [DisplayName("Possible Esper Reward Color")]
    public Color EsperRewardColor
    {
        get => Color.FromArgb((int)GetSetting((uint)Color.Yellow.ToArgb()));
        set => SaveSetting((uint)value.ToArgb());
    }

    [DefaultValue(KnownColor.Cyan)]
    [Category("Map")]
    [DisplayName("Possible Item Reward Color")]
    public Color ItemRewardColor
    {
        get => Color.FromArgb((int)GetSetting((uint)Color.Cyan.ToArgb()));
        set => SaveSetting((uint)value.ToArgb());
    }

    [DefaultValue(true)]
    [Category("Map")]
    [DisplayName("Auto Change World Map")]
    [Description("If true, Map changes to current world automatically.")]
    public bool Follow
    {
        get => GetSetting(true);
        set => SaveSetting(value);
    }

    [DefaultValue(true)]
    [Category("Borders and Backgrounds")]
    [Description("If not enabled, backgrounds will be not rendered.")]
    [DisplayName("Backgrounds Enabled")]
    public bool BackgroundsEnabled
    {
        get => (BorderSettings as WorldsCollideBorderSettings)!.BackgroundsEnabled;
        set => (BorderSettings as WorldsCollideBorderSettings)!.BackgroundsEnabled = value;
    }

    [DefaultValue(true)]
    [Category("Borders and Backgrounds")]
    [Description("If not enabled, borders will be not rendered.")]
    [DisplayName("Borders Enabled")]
    public bool BordersEnabled 
    { 
        get => BorderSettings.BordersEnabled; 
        set => BorderSettings.BordersEnabled = value; 
    }

    [Category("Borders and Backgrounds")]
    [DisplayName("Scaling")]
    [DefaultValue(2.0f)]
    public float BorderScaleFactor
    {
        get => GetSetting(2.0f);
        set => SaveSetting(value);
    }

    [Browsable(false)]
    public override BorderSettings BorderSettings { get; }
}
