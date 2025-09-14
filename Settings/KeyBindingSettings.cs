using FF.Rando.Companion.Extensions;
using FF.Rando.Companion.Settings.Editor;
using FF.Rando.Companion.Settings.TypeConverters;
using Newtonsoft.Json.Linq;
using System.ComponentModel;
using System.Drawing.Design;
using System.Runtime.CompilerServices;

namespace FF.Rando.Companion.Settings;
public class KeyBindingSettings : INotifyPropertyChanged
{
    protected JToken SettingsData { get; }

    public KeyBindingSettings(JToken parentData)
    {
        var token = parentData[nameof(KeyBindingSettings)];
        if (token == null)
        {
            parentData[nameof(KeyBindingSettings)] = new JObject();
        }

        SettingsData = parentData[nameof(KeyBindingSettings)]!;
    }

    [DisplayName("Next Panel")]
    [DefaultValue("X1 Back")]
    [Category("Buttons")]
    [Editor(typeof(ButtonAssignmentEditor), typeof(UITypeEditor))]
    [TypeConverter(typeof(ButtonAssignmentConverter))]
    public string NextPanelButton
    {
        get => GetStringSetting("X1 Back");
        set => SaveStringSetting(value);
    }

    [DisplayName("Next Page")]
    [DefaultValue("X1 RightTrigger")]
    [Category("Buttons")]
    [Editor(typeof(ButtonAssignmentEditor), typeof(UITypeEditor))]
    [TypeConverter(typeof(ButtonAssignmentConverter))]
    public string NextPageButton
    {
        get => GetStringSetting("X1 RightTrigger");
        set => SaveStringSetting(value);
    }

    [DisplayName("Previous Page")]
    [DefaultValue("X1 LeftTrigger")]
    [Category("Buttons")]
    [Editor(typeof(ButtonAssignmentEditor), typeof(UITypeEditor))]
    [TypeConverter(typeof(ButtonAssignmentConverter))]
    public string PreviousPageButton
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

    protected void SaveStringSetting(string value, [CallerMemberName] string propertyName = "")
    {
        if (value.Equals(SettingsData[propertyName.ToSnakeCase()]?.ToString()))
            return;

        SettingsData[propertyName.ToSnakeCase()] = JToken.FromObject(value!);
        NotifyPropertyChanged(propertyName);
    }

    protected string GetStringSetting(string defaultValue, [CallerMemberName] string propertyName = "")
    {
        return SettingsData[propertyName.ToSnakeCase()]?.ToString() ?? defaultValue;
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public override string ToString()
    {
        return "";
    }
}
