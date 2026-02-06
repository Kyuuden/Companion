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
        BorderSettings = new WorldsCollideBorderSettings(SettingsData);
        Stats = new StatsSettings(SettingsData);
        Checks = new CheckSettings(SettingsData);
        Characters = new CharacterSettings(SettingsData);
        Dragons = new DragonSettings(SettingsData);
        DragonLocations = new DragonLocationSettings(SettingsData);
        TextChecks = new TextChecksSettings(SettingsData);
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

    [TypeConverter(typeof(ExpandableObjectConverter))]
    [Description("Tracking of all checks as text")]
    [DisplayName("Text-based Checks")]
    public TextChecksSettings TextChecks { get; }
}
