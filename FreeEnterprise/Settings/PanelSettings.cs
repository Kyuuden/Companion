using FF.Rando.Companion.Extensions;
using Newtonsoft.Json.Linq;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;

namespace FF.Rando.Companion.FreeEnterprise.Settings;

public abstract class PanelSettings : INotifyPropertyChanged
{
    protected virtual float DefaultScaleFactor => 1.75f;

    [Browsable(false)]
    public abstract string Name { get; }
    protected JToken SettingsData { get; }

    protected PanelSettings(JToken parentData)
    {
        var token = parentData[Name];
        if (token == null)
        {
            parentData[Name] = new JObject();
        }

        SettingsData = parentData[Name]!;
    }

    [DefaultValue(true)]
    [Description("If not enabled, tracking panel will be hidden.")]
    public bool Enabled
    {
        get => GetSetting(true);
        set => SaveSetting(value);
    }

    [Description("Scaling for all elements in this panel, to optimize visibility vs readability")]
    [DefaultValue(1.75f)]
    public virtual float ScaleFactor
    {
        get => GetSetting(DefaultScaleFactor);
        set => SaveSetting(value);
    }

    [Description("Determines the order of the panels in the tracker.")]
    public abstract int Priority { get; set; }

    [Description("Should this panel be in the top of the tracker, beside the party.")]
    [DisplayName("Top Panel")]
    public abstract bool InTopPanel { get; set; }

    [Browsable(false)]
    public int TileSize => (int)(8 * ScaleFactor);
    public Size Scale(Size size) => Size.Truncate(Scale((SizeF)size));
    public SizeF Scale(SizeF size) => new SizeF(size.Width * ScaleFactor, size.Height * ScaleFactor);
    public Point Scale(Point point) => Point.Truncate(Scale((PointF)point));
    public PointF Scale(PointF point) => new PointF(point.X * ScaleFactor, point.Y * ScaleFactor);

    public Size Unscale(Size size) => Size.Truncate(Unscale((SizeF)size));
    public SizeF Unscale(SizeF size) => new SizeF(size.Width / ScaleFactor, size.Height / ScaleFactor);
    public Point Unscale(Point point) => Point.Truncate(Unscale((PointF)point));
    public PointF Unscale(PointF point) => new PointF(point.X / ScaleFactor, point.Y / ScaleFactor);

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

    protected T GetSetting<T>(T defaultValue, [CallerMemberName] string propertyName = "") where T : struct
    {
        return SettingsData[propertyName.ToSnakeCase()]?.ToObject<T>() ?? defaultValue;
    }

    public override string ToString()
    {
        return "";
    }
}
