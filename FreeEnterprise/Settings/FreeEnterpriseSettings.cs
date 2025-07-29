using FF.Rando.Companion.Settings;
using Newtonsoft.Json.Linq;
using System.ComponentModel;

namespace FF.Rando.Companion.FreeEnterprise.Settings;


public class FreeEnterpriseSettings : GameSettings
{
    public override string Name => "FreeEnterprise";
    public override string DisplayName => "Free Enterprise";

    public FreeEnterpriseSettings(JObject parent)
        : base(parent)
    {
        Bosses = new BossSettings(SettingsData);
        KeyItems = new KeyItemSettings(SettingsData);
        Objectives = new ObjectivesSettings(SettingsData);
        Party = new PartySettings(SettingsData);
        Stats = new StatsSettings(SettingsData);
    }

    [TypeConverter(typeof(ExpandableObjectConverter))]
    [Description("Tracking of found and defeated bosses.")]
    public BossSettings Bosses { get; }

    [DisplayName("Key Items")]
    [TypeConverter(typeof(ExpandableObjectConverter))]
    [Description("Tracking of found and used iey items.")]
    public KeyItemSettings KeyItems { get; }

    [TypeConverter(typeof(ExpandableObjectConverter))]
    [Description("Tracking of completed objectives")]
    public ObjectivesSettings Objectives { get; }

    [TypeConverter(typeof(ExpandableObjectConverter))]
    [Description("Tracking of party members")]
    public PartySettings Party { get; }

    [TypeConverter(typeof(ExpandableObjectConverter))]
    [Description("Tracking of general statistics")]
    public StatsSettings Stats { get; }
}
