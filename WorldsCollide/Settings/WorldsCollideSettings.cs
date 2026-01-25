using FF.Rando.Companion.Settings;
using Newtonsoft.Json.Linq;
using System.ComponentModel;

namespace FF.Rando.Companion.WorldsCollide.Settings;
internal class WorldsCollideSettings : GameSettings
{
    public override string Name => "WorldsCollide";

    public override string DisplayName => "Worlds Collide";

    public WorldsCollideSettings(JObject parent)
        : base(parent)
    {
        Stats = new StatsSettings(SettingsData);
        Checks = new CheckSettings(SettingsData);
        Characters = new CharacterSettings(SettingsData);
    }

    [TypeConverter(typeof(ExpandableObjectConverter))]
    [Description("Tracking of general statistics")]
    public StatsSettings Stats { get; }

    [TypeConverter(typeof(ExpandableObjectConverter))]
    [Description("Tracking of checks")]
    public CheckSettings Checks { get; }

    [TypeConverter(typeof(ExpandableObjectConverter))]
    [Description("Tracking of characters")]
    public CharacterSettings Characters { get; }

    public ISpriteSet SpriteSet => new VanillaBossSpriteSet();

    [DefaultValue(Settings.SpriteSet.Locations)]
    public SpriteSet Sprites
    {
        get => GetSetting(Settings.SpriteSet.Locations);
        set => SaveSetting(value);
    }
}

public class StatsSettings(JToken jToken) : PanelSettings(jToken)
{
    protected override float DefaultScaleFactor => 1f;

    public override string Name => "Stats";

    [DefaultValue(1.0f)]
    public override float ScaleFactor { get => base.ScaleFactor; set => base.ScaleFactor = value; }

    [DefaultValue(3)]
    public override int Priority
    {
        get => GetSetting(5);
        set => SaveSetting(value);
    }
}

public class CheckSettings(JToken jToken) : PanelSettings(jToken)
{
    protected override float DefaultScaleFactor => 1f;

    public override string Name => "Checks";

    [DefaultValue(1.0f)]
    public override float ScaleFactor { get => base.ScaleFactor; set => base.ScaleFactor = value; }

    [DefaultValue(2)]
    public override int Priority
    {
        get => GetSetting(5);
        set => SaveSetting(value);
    }
}

public class CharacterSettings(JToken jToken) : PanelSettings(jToken)
{
    protected override float DefaultScaleFactor => 1f;

    public override string Name => "Characters";

    [DefaultValue(1.0f)]
    public override float ScaleFactor { get => base.ScaleFactor; set => base.ScaleFactor = value; }

    [DefaultValue(1)]
    public override int Priority
    {
        get => GetSetting(1);
        set => SaveSetting(value);
    }
}

public enum SpriteSet
{
    VanillaBosses,
    Locations,
    Custom
}

