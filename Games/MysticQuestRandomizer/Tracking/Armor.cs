using System.Collections.Generic;

namespace FF.Rando.Companion.Games.MysticQuestRandomizer.Tracking;
public class Armor : Equipment<ArmorType>
{
    internal Armor(Seed seed, IList<ArmorType> order, EquipmentType equipmentType)
        : base(seed, order, equipmentType)
    {
    }
}

internal class Armors(Seed seed) : Equipments<Armor, ArmorType>(
    [
        new Armor(seed, [ArmorType.SteelHelm, ArmorType.MoonHelm, ArmorType.ApolloHelm], EquipmentType.Helmet),
        new Armor(seed, [ArmorType.SteelArmor, ArmorType.NobleArmor, ArmorType.GaiasArmor], EquipmentType.Armor),
        new Armor(seed, [ArmorType.SteelShield, ArmorType.VenusShield, ArmorType.AegisShield], EquipmentType.Shield),
        new Armor(seed, [ArmorType.Charm, ArmorType.MagicRing, ArmorType.CupidLocket], EquipmentType.Accessory)
    ])
{
}
