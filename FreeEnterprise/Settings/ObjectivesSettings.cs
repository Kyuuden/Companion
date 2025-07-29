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
}
