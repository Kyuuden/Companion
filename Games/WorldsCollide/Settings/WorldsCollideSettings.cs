using FF.Rando.Companion.Settings;
using FF.Rando.Companion.Settings.TypeConverters;
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


public class WorldsCollideSettings : GameSettings
{
    public override string Name => "WorldsCollide";

    public override string DisplayName => "Worlds Collide";

    public WorldsCollideSettings(JObject parent)
        : base(parent)
    {
        BorderSettings = new WorldsCollideBorderSettings(SettingsData);
        Stats = new StatsSettings(SettingsData);
        Checks = new CheckSettings(SettingsData);
        Characters = new CharacterSettings(SettingsData);
        Dragons = new DragonSettings(SettingsData);
        DragonLocations = new DragonLocationSettings(SettingsData);
    }

    [TypeConverter(typeof(ExpandableObjectConverter))]
    [DisplayName("Borders and Backgrounds")]
    public override BorderSettings BorderSettings { get; }

    [TypeConverter(typeof(ExpandableObjectConverter))]
    [Description("Tracking of general statistics")]
    [Category("Statistics")]
    public StatsSettings Stats { get; }

    [TypeConverter(typeof(ExpandableObjectConverter))]
    [Description("Tracking of checks")]
    public CheckSettings Checks { get; }

    [TypeConverter(typeof(ExpandableObjectConverter))]
    [Description("Tracking of characters")]
    public CharacterSettings Characters { get; }

    [TypeConverter(typeof(ExpandableObjectConverter))]
    [Description("Tracking of dragons")]
    public DragonSettings Dragons { get; }

    [TypeConverter(typeof(ExpandableObjectConverter))]
    [Description("Tracking of dragon locations")]
    [DisplayName("Dragon Locations")]
    public DragonLocationSettings DragonLocations { get; }

    [DefaultValue(SpriteSetType.Locations)]
    [TypeConverter(typeof(EnumDescriptionConverter))]
    [DisplayName("Icons")]
    [Description("Which set of icons should be used for characters, checks, dragons, dragon locations, and statistics.")]
    public SpriteSetType Icons
    {
        get => GetSetting(SpriteSetType.Locations);
        set => SaveSetting(value);
    }
}

public class StatsSettings(JToken jToken) : PanelSettings(jToken)
{
    protected override float DefaultScaleFactor => 2f;

    public override string Name => "Stats";

    [DefaultValue(2.0f)]
    [Category("Statistics")]
    public override float ScaleFactor { get => base.ScaleFactor; set => base.ScaleFactor = value; }

    [DefaultValue(5)]
    public override int Priority
    {
        get => GetSetting(5);
        set => SaveSetting(value);
    }
}

public class DragonLocationSettings(JToken jToken) : PanelSettings(jToken)
{
    protected override float DefaultScaleFactor => 1.75f;

    public override string Name => "DragonLocations";

    [DefaultValue(1.75f)]
    public override float ScaleFactor { get => base.ScaleFactor; set => base.ScaleFactor = value; }

    [DefaultValue(4)]
    public override int Priority
    {
        get => GetSetting(4);
        set => SaveSetting(value);
    }
}

public class DragonSettings(JToken jToken) : PanelSettings(jToken)
{
    protected override float DefaultScaleFactor => 1.75f;

    public override string Name => "Dragons";

    [DefaultValue(1.75f)]
    public override float ScaleFactor { get => base.ScaleFactor; set => base.ScaleFactor = value; }

    [DefaultValue(3)]
    public override int Priority
    {
        get => GetSetting(3);
        set => SaveSetting(value);
    }
}

public class CheckSettings(JToken jToken) : PanelSettings(jToken)
{
    protected override float DefaultScaleFactor => 1.75f;

    public override string Name => "Checks";

    [DefaultValue(1.75f)]
    public override float ScaleFactor { get => base.ScaleFactor; set => base.ScaleFactor = value; }

    [DefaultValue(2)]
    public override int Priority
    {
        get => GetSetting(2);
        set => SaveSetting(value);
    }
}

public class CharacterSettings(JToken jToken) : PanelSettings(jToken)
{
    protected override float DefaultScaleFactor => 2f;

    public override string Name => "Characters";

    [DefaultValue(2.0f)]
    public override float ScaleFactor { get => base.ScaleFactor; set => base.ScaleFactor = value; }

    [DefaultValue(1)]
    public override int Priority
    {
        get => GetSetting(1);
        set => SaveSetting(value);
    }
}

public enum SpriteSetType
{
    [Description("Vanilla Bosses")]
    VanillaBosses,
    [Description("Location Based")]
    Locations,
    //Custom
}

