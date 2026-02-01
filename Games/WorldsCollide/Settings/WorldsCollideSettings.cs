using FF.Rando.Companion.Settings;
using FF.Rando.Companion.Settings.TypeConverters;
using Newtonsoft.Json.Linq;
using System.ComponentModel;

namespace FF.Rando.Companion.Games.WorldsCollide.Settings;
public class WorldsCollideSettings : GameSettings
{
    public override string Name => "WorldsCollide";

    public override string DisplayName => "Worlds Collide";

    public WorldsCollideSettings(JObject parent)
        : base(parent)
    {
        Stats = new StatsSettings(SettingsData);
        Checks = new CheckSettings(SettingsData);
        Characters = new CharacterSettings(SettingsData);
    }

    [TypeConverter(typeof(ExpandableObjectConverter))]
    [DisplayName("Borders and Backgrounds")]
    public override BorderSettings BorderSettings => base.BorderSettings;

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

    [DefaultValue(SpriteSetType.Locations)]
    [TypeConverter(typeof(EnumDescriptionConverter))]
    [DisplayName("Check Icons")]
    [Description("Which set of icons should be used for checks.")]
    public SpriteSetType CheckIcons
    {
        get => GetSetting(SpriteSetType.Locations);
        set => SaveSetting(value);
    }
}

public class StatsSettings(JToken jToken) : PanelSettings(jToken)
{
    protected override float DefaultScaleFactor => 1f;

    public override string Name => "Stats";

    [DefaultValue(1.0f)]
    [Category("Statistics")]
    public override float ScaleFactor { get => base.ScaleFactor; set => base.ScaleFactor = value; }

    [DefaultValue(3)]
    [Category("Statistics")]
    public override int Priority
    {
        get => GetSetting(5);
        set => SaveSetting(value);
    }
}

public class CheckSettings(JToken jToken) : PanelSettings(jToken)
{
    protected override float DefaultScaleFactor => 1f;

    public override string Name => "Checks";

    [DefaultValue(1.0f)]
    public override float ScaleFactor { get => base.ScaleFactor; set => base.ScaleFactor = value; }

    [DefaultValue(2)]
    public override int Priority
    {
        get => GetSetting(5);
        set => SaveSetting(value);
    }
}

public class CharacterSettings(JToken jToken) : PanelSettings(jToken)
{
    protected override float DefaultScaleFactor => 1f;

    public override string Name => "Characters";

    [DefaultValue(1.0f)]
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

