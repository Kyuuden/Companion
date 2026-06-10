using System.Collections.Generic;

namespace FF.Rando.Companion.Games.MysticQuestRandomizer.Tracking;
public class Weapon : Equipment<WeaponType>
{
    internal Weapon(Seed seed, IList<WeaponType> order, EquipmentType equipmentType)
        : base(seed, order, equipmentType)
    {
    }
}

internal class Weapons(Seed seed) : Equipments<Weapon, WeaponType>(
    [
        new Weapon(seed, [WeaponType.SteelSword, WeaponType.KnightSword, WeaponType.Excalibur], EquipmentType.Sword),
        new Weapon(seed, [WeaponType.Axe, WeaponType.BattleAxe, WeaponType.GiantsAxe], EquipmentType.Axe),
        new Weapon(seed, [WeaponType.CatClaw, WeaponType.CharmClaw, WeaponType.DragonClaw], EquipmentType.Claw),
        new Weapon(seed, [WeaponType.Bomb, WeaponType.JumboBomb, WeaponType.MegaGrenade], EquipmentType.Bomb),
    ])
{
}
