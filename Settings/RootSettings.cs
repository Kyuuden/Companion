using FF.Rando.Companion.Extensions;
using FF.Rando.Companion.Settings.TypeConverters;
using KGySoft.Drawing.Imaging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;

namespace FF.Rando.Companion.Settings;

public class RootSettings : ISettings
{
    private static string FileName = "FF.Rando.Companion.Settings.json";

    private readonly Dictionary<string, GameSettings> _gameSettings = [];

    private readonly JObject _settingsFile;

    private readonly TypeConverter _fontConverter = TypeDescriptor.GetConverter(typeof(Font));

    public RootSettings()
    {
        _settingsFile = (File.Exists(FileName) ? JObject.Parse(File.ReadAllText(FileName)) : []) ?? [];

        var gameSettingsType = typeof(GameSettings);
        var gameSettings = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(s => s.GetTypes())
            .Where(p => p != gameSettingsType && gameSettingsType.IsAssignableFrom(p));

        foreach (var game in gameSettings)
        {
            var settings = (GameSettings)Activator.CreateInstance(game, _settingsFile);
            _gameSettings.Add(settings.Name, settings);
        }
    }

    [Browsable(false)]
    public JObject Source => _settingsFile;

    [Category("Window")]
    [DisplayName("Window Style")]
    [Description("Should the tracker window be docked to BizHawk's window, or allowed to be independent.")]
    [TypeConverter(typeof(EnumDescriptionConverter))]
    [DefaultValue(WindowStyle.Dock_16x9)]
    public WindowStyle WindowStyle { get => GetSetting(WindowStyle.Dock_16x9); set => SaveSetting(value); }

    [Category("Window")]
    [DisplayName("BizHawk Dock Side")]
    [Description("Which side of the main BizHawk window to dock to.")]
    [DefaultValue(DockSide.Right)]
    public DockSide DockSide { get => GetSetting(DockSide.Right); set => SaveSetting(value); }

    [Category("Window")]
    [DisplayName("BizHawk Dock Offset")]
    [Description("BizHawk doesn't report its window size and position quite right, this offset is used to line up windows exactly.")]
    [DefaultValue(-16)]
    public int DockOffset { get => GetSetting(-16); set => SaveSetting(value); }

    [Category("Tracking")]
    [DisplayName("Tracking interval")]
    [Description("How many frames between reading tracking data, increase this value if you expierence sound stuttering.")]
    [DefaultValue(60)]
    public int TrackingInterval { get => GetSetting(60); set => SaveSetting(value); }

    [Category("Timer")]
    public Font Font
    {
        get
        {
            return (Font)_fontConverter.ConvertFromString(GetStringSetting("Lucida Console, 27.75pt"));
        }
        set
        {
            SaveStringSetting(_fontConverter.ConvertToString(value));
        }
    }

    [Category("Timer")]
    public Color TextColor
    {
        get
        {
            return Color32.FromArgb(GetSetting(SystemColors.HighlightText.ToArgb()));
        }
        set
        {
            SaveSetting(value.ToArgb());
        }
    }

    [Category("Timer")]
    [DisplayName("Auto Pause")]
    [Description("Pause timer when emulation is paused.")]
    [DefaultValue(true)]
    public bool AutoPauseTimer { get => GetSetting(true); set => SaveSetting(value); }

    [Browsable(false)]
    public Dictionary<string, GameSettings> GameSettings => _gameSettings;

    public event PropertyChangedEventHandler? PropertyChanged;

    protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    protected void SaveStringSetting(string value, [CallerMemberName] string propertyName = "") 
    {
        if (value.Equals(_settingsFile[propertyName.ToSnakeCase()]?.ToString()))
            return;

        _settingsFile[propertyName.ToSnakeCase()] = JToken.FromObject(value!);
        NotifyPropertyChanged(propertyName);
    }

    protected string GetStringSetting(string defaultValue, [CallerMemberName] string propertyName = "")
    {
        return _settingsFile[propertyName.ToSnakeCase()]?.ToString() ?? defaultValue;
    }

    protected void SaveSetting<T>(T value, [CallerMemberName] string propertyName = "") where T : struct
    {
        if (value.Equals(_settingsFile[propertyName.ToSnakeCase()]?.ToObject<T>()))
            return;

        _settingsFile[propertyName.ToSnakeCase()] = JToken.FromObject(value!);
        NotifyPropertyChanged(propertyName);
    }

    protected T GetSetting<T>(T defaultValue, [CallerMemberName] string propertyName = "") where T : struct
    {
        return _settingsFile[propertyName.ToSnakeCase()]?.ToObject<T>() ?? defaultValue;
    }

    public void SaveToFile()
    {
        using var file = File.CreateText(FileName);
        using var writer = new JsonTextWriter(file);
        writer.Formatting = Formatting.Indented;
        _settingsFile.WriteTo(writer);
    }
}


