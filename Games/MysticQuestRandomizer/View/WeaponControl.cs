using FF.Rando.Companion.View;
using FF.Rando.Companion.Games.MysticQuestRandomizer.Settings;
using FF.Rando.Companion.Games.MysticQuestRandomizer.Tracking;

namespace FF.Rando.Companion.Games.MysticQuestRandomizer.View;

internal class WeaponControl(Seed seed, EquipmentSettings settings, Weapon weapon) : ImageControl<Seed, Weapon>(seed, settings, weapon)
{
}
