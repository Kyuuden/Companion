using FF.Rando.Companion.Settings.Editor;
using FF.Rando.Companion.Settings.TypeConverters;
using Newtonsoft.Json.Linq;
using System.ComponentModel;
using System.Drawing.Design;

namespace FF.Rando.Companion.FreeEnterprise.Settings;

public class ObjectivesSettings : PanelSettings
{
    public ObjectivesSettings(JToken jToken)
        : base(jToken) { }

    public override string Name => "Objectives";

    [DisplayName("Combine Objective Groups")]
    [Description("Display all objective groups at once (Will probably require scrolling)")]
    [DefaultValue(false)]
    public bool CombineObjectiveGroups
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

    [DisplayName("Next Group")]
    [DefaultValue("X1 RightTrigger")]
    [Category("Buttons")]
    [Editor(typeof(ButtonAssignmentEditor), typeof(UITypeEditor))]
    [TypeConverter(typeof(ButtonAssignmentConverter))]
    public string NextGroupButton
    {
        get => GetStringSetting("X1 RightTrigger");
        set => SaveStringSetting(value);
    }

    [DisplayName("Previous Group")]
    [DefaultValue("X1 LeftTrigger")]
    [Category("Buttons")]
    [Editor(typeof(ButtonAssignmentEditor), typeof(UITypeEditor))]
    [TypeConverter(typeof(ButtonAssignmentConverter))]
    public string PreviousGroupButton
    {
        get => GetStringSetting("X1 LeftTrigger");
        set => SaveStringSetting(value);
    }

    [DisplayName("Scroll Down")]
    [DefaultValue("X1 RStickDown")]
    [Category("Buttons")]
    [Editor(typeof(ButtonAssignmentEditor), typeof(UITypeEditor))]
    [TypeConverter(typeof(ButtonAssignmentConverter))]
    public string ScrollDownButton
    {
        get => GetStringSetting("X1 RStickDown");
        set => SaveStringSetting(value);
    }

    [DisplayName("Scroll Up")]
    [DefaultValue("X1 RStickUp")]
    [Category("Buttons")]
    [Editor(typeof(ButtonAssignmentEditor), typeof(UITypeEditor))]
    [TypeConverter(typeof(ButtonAssignmentConverter))]
    public string ScrollUpButton
    {
        get => GetStringSetting("X1 RStickUp");
        set => SaveStringSetting(value);
    }

    [DefaultValue(3)]
    public override int Priority
    {
        get => GetSetting(3);
        set => SaveSetting(value);
    }

    [DefaultValue(false)]
    public override bool InTopPanel
    {
        get => GetSetting(false);
        set => SaveSetting(value);
    }
}
