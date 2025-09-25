using FF.Rando.Companion.Settings;
using Newtonsoft.Json.Linq;
using System.ComponentModel;

namespace FF.Rando.Companion.MysticQuestRandomizer.Settings;

internal class EquipmentSettings(JToken parentData) : PanelSettings(parentData)
{
    public override string Name => "Equipment";

    protected override float DefaultScaleFactor => 3f;

    [DefaultValue(3.0f)]
    public override float ScaleFactor { get => base.ScaleFactor; set => base.ScaleFactor = value; }

    [DefaultValue(1)]
    public override int Priority
    {
        get => GetSetting(1);
        set => SaveSetting(value);
    }
}