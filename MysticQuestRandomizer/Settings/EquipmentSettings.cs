using FF.Rando.Companion.Settings;
using Newtonsoft.Json.Linq;
using System.ComponentModel;

namespace FF.Rando.Companion.MysticQuestRandomizer.Settings;

internal class EquipmentSettings(JToken parentData) : PanelSettings(parentData)
{
    public override string Name => "Equipment";

    [DefaultValue(1)]
    public override int Priority
    {
        get => GetSetting(1);
        set => SaveSetting(value);
    }
}