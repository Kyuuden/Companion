using FF.Rando.Companion.Extensions;
using Newtonsoft.Json.Linq;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace FF.Rando.Companion.Settings;

public class BorderSettings : INotifyPropertyChanged
{
    [Browsable(false)]
    public string Name => "Borders";
    protected JToken SettingsData { get; }

    public BorderSettings(JToken parentData)
    {
        var token = parentData[Name];
        if (token == null)
        {
            parentData[Name] = new JObject();
        }

        SettingsData = parentData[Name]!;
    }

    [DefaultValue(true)]
    [Description("If not enabled, borders will be not rendered.")]
    [DisplayName("Enabled")]
    public virtual bool BordersEnabled
    {
        get => GetSetting(true);
        set => SaveSetting(value);
    }

    [Description("Scaling for borders.")]
    [DisplayName("Scale Factor")]
    [DefaultValue(1.75f)]
    public virtual float BorderScaleFactor
    {
        get => GetSetting(1.75f);
        set => SaveSetting(value);
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    protected void SaveSetting<T>(T value, [CallerMemberName] string propertyName = "") where T : struct
    {
        if (value.Equals(SettingsData[propertyName.ToSnakeCase()]?.ToObject<T>()))
            return;

        SettingsData[propertyName.ToSnakeCase()] = JToken.FromObject(value!);
        NotifyPropertyChanged(propertyName);
    }

    protected T GetSetting<T>(T defaultValue, [CallerMemberName] string propertyName = "") where T : struct
    {
        return SettingsData[propertyName.ToSnakeCase()]?.ToObject<T>() ?? defaultValue;
    }

    public override string ToString()
    {
        return "";
    }
}
