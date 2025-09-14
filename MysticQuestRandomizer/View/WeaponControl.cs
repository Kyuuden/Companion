using FF.Rando.Companion.View;
using FF.Rando.Companion.MysticQuestRandomizer.Settings;

namespace FF.Rando.Companion.MysticQuestRandomizer.View;

internal class WeaponControl(Seed seed, EquipmentSettings settings, Weapon weapon) : ImageControl<Seed, Weapon>(seed, settings, weapon)
{
}
