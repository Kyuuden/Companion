using FF.Rando.Companion.MysticQuestRandomizer.RomData;
using System.Collections.Generic;

namespace FF.Rando.Companion.MysticQuestRandomizer;
public class Armor : Equipment<ArmorType>
{
    internal Armor(IList<ArmorType> order, EquipmentType equipmentType, RomData.Sprites sprites)
        : base(order, equipmentType, sprites)
    {
    }
}

internal class Armors(Sprites sprites) : Equipments<Armor, ArmorType>(
    [
            new Armor([ArmorType.SteelHelm, ArmorType.MoonHelm, ArmorType.ApolloHelm], EquipmentType.Helmet, sprites),
            new Armor([ArmorType.SteelArmor, ArmorType.NobleArmor, ArmorType.GaiasArmor], EquipmentType.Armor, sprites),
            new Armor([ArmorType.SteelShield, ArmorType.VenusShield, ArmorType.AegisShield], EquipmentType.Shield, sprites),
            new Armor([ArmorType.Charm, ArmorType.MagicRing, ArmorType.CupidLocket], EquipmentType.Accessory, sprites)
    ])
{
}
