using FF.Rando.Companion.Settings;
using Newtonsoft.Json.Linq;
using System.ComponentModel;

namespace FF.Rando.Companion.Games.JetsOfTime.Settings;
internal class JetsOfTimeSettings : GameSettings
{
    public override string Name => "JetsOfTime";

    public override string DisplayName => "Jets of Time";

    public override string Description => "Chrono Trigger: Jets of Time";

    public JetsOfTimeSettings(JObject parent)
        : base(parent)
    {
        BorderSettings = new JetsOfTimeBorderSettings(SettingsData);
        KeyItems = new KeyItemSettings(SettingsData);
        Characters = new CharacterSettings(SettingsData);
        Bosses = new BossSettings(SettingsData);
        //Checks = new ChecksSettings(SettingsData);
        Maps = new MapSettings(SettingsData);
        Statistics = new StatisticsSettings(SettingsData);
    }

    [TypeConverter(typeof(ExpandableObjectConverter))]
    [DisplayName("Borders and Backgrounds")]
    public override BorderSettings BorderSettings { get; }

    [DisplayName("Key Items")]
    [TypeConverter(typeof(ExpandableObjectConverter))]
    [Description("Tracking of found and used key items.")]
    public KeyItemSettings KeyItems { get; }

    [DisplayName("Characters")]
    [TypeConverter(typeof(ExpandableObjectConverter))]
    [Description("Tracking of found party members.")]
    public CharacterSettings Characters { get; }

    [DisplayName("Bosses")]
    [TypeConverter(typeof(ExpandableObjectConverter))]
    [Description("Tracking of defeated bosses.")]
    public BossSettings Bosses { get; }

    //[DisplayName("Checks")]
    //[TypeConverter(typeof(ExpandableObjectConverter))]
    //[Description("Tracking of available Checks.")]
    //public ChecksSettings Checks { get; }

    [DisplayName("World Maps")]
    [TypeConverter(typeof(ExpandableObjectConverter))]
    [Description("Tracking of available Checks on world maps")]
    public MapSettings Maps { get; }

    [DisplayName("Statistics")]
    [TypeConverter(typeof(ExpandableObjectConverter))]
    [Description("Tracking of general statistics")]
    public StatisticsSettings Statistics { get; }
}
