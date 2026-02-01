using FF.Rando.Companion.Extensions;
using Newtonsoft.Json.Linq;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace FF.Rando.Companion.Settings;

public abstract class GameSettings : INotifyPropertyChanged
{
    protected virtual float DefaultBorderScaleFactor => 1.75f;

    [Browsable(false)]
    public abstract string Name { get; }
    [Browsable(false)]
    public abstract string DisplayName { get; }
    protected JToken SettingsData { get; }

    protected GameSettings(JObject parentData)
    {
        var token = parentData[Name];
        if (token == null)
        {
            parentData[Name] = new JObject();
        }

        SettingsData = parentData[Name]!;
        BorderSettings = new BorderSettings(SettingsData);
    }

    [TypeConverter(typeof(ExpandableObjectConverter))]
    [DisplayName("Borders")]
    public virtual BorderSettings BorderSettings { get; }

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
}
