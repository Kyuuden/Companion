using Newtonsoft.Json.Linq;
using System.ComponentModel;

namespace FF.Rando.Companion.FreeEnterprise.Settings;

public class LocationsSettings : PanelSettings
{
    public override string Name => "Locations";

    public LocationsSettings(JToken jToken)
    : base(jToken) { }

    [DisplayName("Combine Location Types")]
    [Description("Display all location types at once (Will probably require scrolling)")]
    [DefaultValue(false)]
    public bool CombineLocationTypes
    {
        get => GetSetting(false);
        set => SaveSetting(value);
    }

    [DisplayName("Include Key Item Loactions")]
    [Description("Show available Key Item locations")]
    [DefaultValue(true)]
    public bool ShowKeyItemChecks
    {
        get => GetSetting(true);
        set => SaveSetting(value);
    }

    [DisplayName("Include character Loactions")]
    [Description("Show available character locations")]
    [DefaultValue(false)]
    public bool ShowCharacterChecks
    {
        get => GetSetting(false);
        set => SaveSetting(value);
    }

    [DisplayName("Include shop Loactions")]
    [Description("Show available shop locations, if supported")]
    [DefaultValue(false)]
    public bool ShowShopChecks
    {
        get => GetSetting(false);
        set => SaveSetting(value);
    }

    [DisplayName("Lines to scroll")]
    [Description("How many lines to scroll for each scroll up or down action.")]
    [DefaultValue(2)]
    public int ScrollLines
    {
        get => GetSetting(2);
        set => SaveSetting(value);
    }

    [DefaultValue(4)]
    public override int Priority
    {
        get => GetSetting(4);
        set => SaveSetting(value);
    }

    [DefaultValue(false)]
    public override bool InTopPanel
    {
        get => GetSetting(false);
        set => SaveSetting(value);
    }
}