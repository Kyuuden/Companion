using FF.Rando.Companion.Games.MysticQuestRandomizer;
using FF.Rando.Companion.Settings;
using FF.Rando.Companion.Settings.Editor;
using FF.Rando.Companion.Settings.TypeConverters;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Linq;

namespace FF.Rando.Companion.Games.MysticQuestRandomizer.Settings;

internal class CompanionsSettings(JToken parentData) : PanelSettings(parentData)
{
    public override string Name => "Companions";

    protected override float DefaultScaleFactor => 3f;

    [DefaultValue(3.0f)]
    public override float ScaleFactor { get => base.ScaleFactor; set => base.ScaleFactor = value; }

    [DefaultValue(3)]
    public override int Priority
    {
        get => GetSetting(3);
        set => SaveSetting(value);
    }

    [DisplayName("Spells to Show")]
    [Description("Only selected spells will be shown.")]
    [Editor(typeof(EnumListEditor<SpellType>), typeof(UITypeEditor))]
    [TypeConverter(typeof(EnumListConverter<SpellType>))]
    [DefaultValue("Aero, Life, Exit, Flare, Meteor, White")]
    public string ImportantSpells
    {
        get => GetStringSetting("Aero, Life, Exit, Flare, Meteor, White");
        set => SaveStringSetting(value);
    }

    [Browsable(false)]
    public List<SpellType> Spells => Parse(ImportantSpells);

    [DisplayName("Combine Companions")]
    [Description("Display all companion data at once. (Will probably require scrolling)")]
    [DefaultValue(true)]
    public bool CombineCompanions
    {
        get => GetSetting(true);
        set => SaveSetting(value);
    }

    private List<SpellType> Parse(string spells)
    {
        return (spells.Split([',', ' ', ';'], StringSplitOptions.RemoveEmptyEntries) ?? Enumerable.Empty<string>())
            .Select(x => x.Trim())
            .Select(x => Enum.Parse(typeof(SpellType), x))
            .OfType<SpellType>()
            .ToList();
    }
}
