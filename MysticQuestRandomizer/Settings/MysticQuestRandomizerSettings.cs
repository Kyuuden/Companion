using FF.Rando.Companion.Settings;
using Newtonsoft.Json.Linq;
using System.ComponentModel;

namespace FF.Rando.Companion.MysticQuestRandomizer.Settings;
internal class MysticQuestRandomizerSettings : GameSettings
{
    public override string Name => "MysticQuestRandomizer";

    public override string DisplayName => "Mystic Quest Randomizer";

    public MysticQuestRandomizerSettings(JObject parent)
        : base(parent)
    {
        Equipment = new EquipmentSettings(SettingsData);
        Elements = new ElementsSettings(SettingsData);
        Companions = new CompanionsSettings(SettingsData);
        //Stats = new StatsSettings(SettingsData);
    }

    [DisplayName("Equipment")]
    [TypeConverter(typeof(ExpandableObjectConverter))]
    [Description("Tracking of found Weapons, Armors, Spells, and Key Items.")]
    public EquipmentSettings Equipment { get; }

    [DisplayName("Elements")]
    [TypeConverter(typeof(ExpandableObjectConverter))]
    [Description("Tracking of Randomized Elemental Weaknesses.")]
    public ElementsSettings Elements{ get; }

    [DisplayName("Companions")]
    [TypeConverter(typeof(ExpandableObjectConverter))]
    [Description("Tracking of Companions' Spells and Quests.")]
    public CompanionsSettings Companions { get; }

    //[TypeConverter(typeof(ExpandableObjectConverter))]
    //[Description("Tracking of general statistics")]
    //public StatsSettings Stats { get; }
}

