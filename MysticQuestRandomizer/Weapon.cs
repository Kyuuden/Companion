using FF.Rando.Companion.MysticQuestRandomizer.RomData;
using System.Collections.Generic;

namespace FF.Rando.Companion.MysticQuestRandomizer;
public class Weapon : Equipment<WeaponType>
{
    internal Weapon(IList<WeaponType> order, EquipmentType equipmentType, RomData.Sprites sprites) 
        : base(order, equipmentType, sprites)
    {
    }
}

internal class Weapons(Sprites sprites) : Equipments<Weapon, WeaponType>(
    [
            new Weapon([WeaponType.SteelSword, WeaponType.KnightSword, WeaponType.Excalibur], EquipmentType.Sword, sprites),
            new Weapon([WeaponType.Axe, WeaponType.BattleAxe, WeaponType.GiantsAxe], EquipmentType.Axe, sprites),
            new Weapon([WeaponType.Bomb, WeaponType.JumboBomb, WeaponType.MegaGrenade], EquipmentType.Bomb, sprites),
            new Weapon([WeaponType.CatClaw, WeaponType.CharmClaw, WeaponType.DragonClaw], EquipmentType.Claw, sprites),
    ])
{
}
