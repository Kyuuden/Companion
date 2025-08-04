using FF.Rando.Companion.FreeEnterprise.Shared;

namespace FF.Rando.Companion.FreeEnterprise;

internal class Descriptors : IKeyItemDescriptor, IBossDescriptor
{
    string IKeyItemDescriptor.GetDescription(KeyItemType item)
        => item switch
        {
            KeyItemType.Package => "Bring to village Mist to burn it down, then visit Kaipo",
            KeyItemType.SandRuby => "Bring to the Kaipo hospital to heal the sick.",
            KeyItemType.LegendSword => "WIL+3. Deals holy damage. Kokkol can reforge this sword using Adamant.",
            KeyItemType.BaronKey => "Opens the locked doors in Baron town.",
            KeyItemType.TwinHarp => "Used to defeat the Dark Elf in Cave Magnes.",
            KeyItemType.EarthCrystal => "Bring to the top of the Tower of Zot.",
            KeyItemType.MagmaKey => "Throw into the well in Agart to open the path to the underworld.",
            KeyItemType.TowerKey => "Opens the Super Cannon room in the Tower of Bab-il.",
            KeyItemType.Hook => "Allows the Enterprise to carry the hovercraft.",
            KeyItemType.LucaKey => "\nUnlocks the Sealed Cave.",
            KeyItemType.DarknessCrystal => "Bring to Mysida to raise the Big Whale.",
            KeyItemType.RatTail => "Bring to the Adamant Grotto to trade for an item.",
            KeyItemType.Adamant => "Bring this and the |Legend to Kokkol the Smith.",
            KeyItemType.Pan => "Use to wake Yang in the Sylph Cave, then return it to Yang's wife in Fabul",
            KeyItemType.Spoon => "\nDart for massive damage.",
            KeyItemType.PinkTail => "Bring to the Adamant Grotto to trade for strong gear.",
            KeyItemType.Crystal => "Combat item. Use on Zeromus to unlock his final form.",
            KeyItemType.Pass => "Opens the Path to Zeromus from the Toroia Cafe.",
            _ => string.Empty
        };

    string IKeyItemDescriptor.GetName(KeyItemType item)
        => item switch
        {
            KeyItemType.Package => "Package  ",
            KeyItemType.SandRuby => "SandRuby ",
            KeyItemType.LegendSword => "[sword]Legend  ",
            KeyItemType.BaronKey => "[key]Baron   ",
            KeyItemType.TwinHarp => "[harp]TwinHarp",
            KeyItemType.EarthCrystal => "[crystal]Earth   ",
            KeyItemType.MagmaKey => "[key]Magma   ",
            KeyItemType.TowerKey => "[key]Tower   ",
            KeyItemType.Hook => "Hook     ",
            KeyItemType.LucaKey => "[key]Luca    ",
            KeyItemType.DarknessCrystal => "[crystal]Darkness",
            KeyItemType.RatTail => "[tail]Rat     ",
            KeyItemType.Adamant => "Adamant  ",
            KeyItemType.Pan => "Pan      ",
            KeyItemType.Spoon => "[knife]Spoon   ",
            KeyItemType.PinkTail => "[tail]Pink    ",
            KeyItemType.Crystal => "[crystal]Crystal ",
            KeyItemType.Pass => "Pass     ",
            _ => string.Empty
        };

    string IBossDescriptor.GetLocationName(BossLocationType location)
        => location switch
        {
            BossLocationType.DmistSlot => "Mist Cave",
            BossLocationType.OfficerSlot => "Kaipo Inn",
            BossLocationType.OctomammSlot => "Watery Pass",
            BossLocationType.AntlionSlot => "Antlion Cave",
            BossLocationType.WaterhagSlot => "",
            BossLocationType.MombombSlot => "Mt. Hobs",
            BossLocationType.FabulGauntletSlot => "Fabul",
            BossLocationType.MilonSlot => "Mt. Ordeals (1)",
            BossLocationType.MilonzSlot => "Mt. Ordeals (2)",
            BossLocationType.MirrorcecilSlot => "Mirror Room",
            BossLocationType.KarateSlot => "Baron Inn (1)",
            BossLocationType.GuardSlot => "Baron Inn (2)",
            BossLocationType.BaiganSlot => "Baron Castle Entrance",
            BossLocationType.KainazzoSlot => "Baron Castle Throne",
            BossLocationType.DarkelfSlot => "Cave Magnes",
            BossLocationType.MagusSlot => "Tower of Zot (1)",
            BossLocationType.ValvalisSlot => "Tower of Zot (2)",
            BossLocationType.CalbrenaSlot => "Dwarf Castle (1)",
            BossLocationType.GolbezSlot => "Dwarf Castle (2)",
            BossLocationType.LugaeSlot => "Lower Bab-il",
            BossLocationType.DarkimpSlot => "Super Cannon Room",
            BossLocationType.KingqueenSlot => "Upper Bab-il (1)",
            BossLocationType.RubicantSlot => "Upper Bab-il (2)",
            BossLocationType.EvilwallSlot => "Sealed Cave",
            BossLocationType.AsuraSlot => "Queen of the Monsters",
            BossLocationType.LeviatanSlot => "King of the Monsters",
            BossLocationType.OdinSlot => "Baron Castle Basement Throne",
            BossLocationType.BahamutSlot => "Cave Bahamut",
            BossLocationType.ElementsSlot => "Giant of Bab-il (1)",
            BossLocationType.CpuSlot => "Giant of Bab-il (2)",
            BossLocationType.PaledimSlot => "Lunar Subterrane B3 altar",
            BossLocationType.WyvernSlot => "Lunar Subterrane B5 altar",
            BossLocationType.PlagueSlot => "Lunar Subterrane B7 altar (1)",
            BossLocationType.DlunarSlot => "Lunar Subterrane B7 altar (2)",
            BossLocationType.OgopogoSlot => "Lunar Subterrane B8 altar",
            _ => string.Empty,
        };

    string IBossDescriptor.GetName(BossType bossType)
        => bossType switch
        {
            BossType.Dmist => "D. Mist",
            BossType.Officer => "Officer & Soldiers",
            BossType.Octomamm => "OctoMamm",
            BossType.Antlion => "Antlion",
            BossType.Waterhag => "Water Hag",
            BossType.Mombomb => "Mom Bomb",
            BossType.FabulGauntlet => "Gauntlet",
            BossType.Milon => "Milon",
            BossType.Milonz => "Milon Z",
            BossType.Mirrorcecil => "Mirror Cecil",
            BossType.Karate => "Karate",
            BossType.Guard => "Guards",
            BossType.Baigan => "Baigan",
            BossType.Kainazzo => "Kainazzo",
            BossType.Darkelf => "Dark Elf",
            BossType.Magus => "Magus Sisters",
            BossType.Valvalis => "Valvalis",
            BossType.Calbrena => "Cal / Brena",
            BossType.Golbez => "Golbez",
            BossType.Lugae => "Dr. Lugae",
            BossType.Darkimp => "Dark Imps",
            BossType.Kingqueen => "King & Queen of Eblan",
            BossType.Rubicant => "Rubicant",
            BossType.Evilwall => "Evil Wall",
            BossType.Asura => "Asura",
            BossType.Leviatan => "Leviatan",
            BossType.Odin => "Odin",
            BossType.Bahamut => "Bahamut",
            BossType.Elements => "Elements",
            BossType.Cpu => "CPU",
            BossType.Paledim => "Paledim",
            BossType.Wyvern => "Wyvern",
            BossType.Plague => "Plague",
            BossType.Dlunar => "D. Lunars",
            BossType.Ogopogo => "Ogopogo",
            _ => string.Empty
        };
}