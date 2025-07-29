using FF.Rando.Companion.Extensions;
using FF.Rando.Companion.FreeEnterprise.RomData;
using FF.Rando.Companion.FreeEnterprise.Shared;
using FF.Rando.Companion.MemoryManagement;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FF.Rando.Companion.FreeEnterprise._5._0._0;

internal class Descriptors
{
    private readonly List<string> _rewardSlotDescriptions;
    private readonly byte[] _chestLookup;

    private const int ChestOffset = 0x90;
    private const int MiabOffset = 0xb8;

    internal string GetRewardSlotDescription(int rewardSlot)
    {
        if ((rewardSlot & 0xC000) != 0)
        {
            var chest = rewardSlot & 0x1FF;
            var location = _chestLookup[chest];
            return (rewardSlot & 0x4000) != 0 
                ? _rewardSlotDescriptions[location + MiabOffset]
                :_rewardSlotDescriptions[location + ChestOffset];
        }

        if ((rewardSlot & 0x1000) != 0)
        { 
            //TODO
        }

        if (rewardSlot < 0 || rewardSlot >= _rewardSlotDescriptions.Count)
            return string.Empty;

        return _rewardSlotDescriptions[rewardSlot];
    }

    internal string GetKeyItemDescription(KeyItemType item)
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

    internal string GetTaskDescription(RomData.Task task) 
        => task switch
        {
            BasicTask basicTask => GetDescription(basicTask),
            ThresholdTask thresholdTask => GetDescription(thresholdTask),
            GroupTask groupTask => GetDescription(groupTask),
            _ => string.Empty,
        };

    private string GetDescription(BasicTask basicTask)
        => (basicTask.Description ?? string.Empty).ToLower() switch
        {
            "char_cecil" => "Find Cecil",
            "char_kain" => "Find Kain",
            "char_rydia" => "Find Rydia",
            "char_tellah" => "Find Tellah",
            "char_edward" => "Find Edward",
            "char_rosa" => "Find Rosa",
            "char_yang" => "Find Yang",
            "char_palom" => "Find Palom",
            "char_porom" => "Find Porom",
            "char_cid" => "Find Cid",
            "char_edge" => "Find Edge",
            "char_fusoya" => "Find FuSoYa",
            "boss_dmist" => "Defeat D.Mist",
            "boss_officer" => "Defeat Officer",
            "boss_octomamm" => "Defeat Octomamm",
            "boss_antlion" => "Defeat Antlion",
            "boss_waterhag" => "Defeat Waterhag (boss version)",
            "boss_mombomb" => "Defeat MomBomb",
            "boss_fabulgauntlet" => "Defeat the Fabul Gauntlet",
            "boss_milon" => "Defeat Milon",
            "boss_milonz" => "Defeat Milon Z.",
            "boss_mirrorcecil" => "Defeat D.Knight",
            "boss_guard" => "Defeat the Guards (boss)",
            "boss_karate" => "Defeat Karate",
            "boss_baigan" => "Defeat Baigan",
            "boss_kainazzo" => "Defeat Kainazzo",
            "boss_darkelf" => "Defeat the Dark Elf (dragon form)",
            "boss_magus" => "Defeat the Magus Sisters",
            "boss_valvalis" => "Defeat Valvalis",
            "boss_calbrena" => "Defeat Calbrena",
            "boss_golbez" => "Defeat Golbez",
            "boss_lugae" => "Defeat Dr. Lugae",
            "boss_darkimp" => "Defeat the Dark Imps (boss)",
            "boss_kingqueen" => "Defeat K.Eblan and Q.Eblan",
            "boss_rubicant" => "Defeat Rubicant",
            "boss_evilwall" => "Defeat EvilWall",
            "boss_asura" => "Defeat Asura",
            "boss_leviatan" => "Defeat Leviatan",
            "boss_odin" => "Defeat Odin",
            "boss_bahamut" => "Defeat Bahamut",
            "boss_elements" => "Defeat Elements",
            "boss_cpu" => "Defeat CPU",
            "boss_paledim" => "Defeat Pale Dim",
            "boss_wyvern" => "Defeat Wyvern",
            "boss_plague" => "Defeat Plague",
            "boss_dlunar" => "Defeat the D.Lunars",
            "boss_ogopogo" => "Defeat Ogopogo",
            "quest_mistcave" => "Defeat the boss of the Mist Cave",
            "quest_waterfall" => "Defeat the boss of the Waterfall",
            "quest_antlionnest" => "Complete the Antlion Nest",
            "quest_hobs" => "Rescue the hostage on Mt. Hobs",
            "quest_fabul" => "Defend Fabul",
            "quest_ordeals" => "Complete Mt. Ordeals",
            "quest_baroninn" => "Defeat the bosses of Baron Inn",
            "quest_baroncastle" => "Liberate Baron Castle",
            "quest_magnes" => "Complete Cave Magnes",
            "quest_zot" => "Complete the Tower of Zot",
            "quest_dwarfcastle" => "Defeat the bosses of Dwarf Castle",
            "quest_lowerbabil" => "Defeat the boss of Lower Bab-il",
            "quest_falcon" => "Launch the Falcon",
            "quest_sealedcave" => "Complete the Sealed Cave",
            "quest_monsterqueen" => "Defeat the queen at the Town of Monsters",
            "quest_monsterking" => "Defeat the king at the Town of Monsters",
            "quest_baronbasement" => "Defeat the Baron Castle basement throne",
            "quest_giant" => "Complete the Giant of Bab-il",
            "quest_cavebahamut" => "Complete Cave Bahamut",
            "quest_murasamealtar" => "Conquer the vanilla Murasame altar",
            "quest_crystalaltar" => "Conquer the vanilla Crystal Sword altar",
            "quest_whitealtar" => "Conquer the vanilla White Spear altar",
            "quest_ribbonaltar" => "Conquer the vanilla Ribbon room",
            "quest_masamunealtar" => "Conquer the vanilla Masamune altar",
            "quest_burnmist" => "Burn village Mist with the Package",
            "quest_curefever" => "Cure the fever with the SandRuby",
            "quest_unlocksewer" => "Unlock the sewer with the Baron Key",
            "quest_music" => "Break the Dark Elf's spell with the TwinHarp",
            "quest_toroiatreasury" => "Open the Toroia treasury with the Earth Crystal",
            "quest_magma" => "Drop the Magma Key into the Agart well",
            "quest_supercannon" => "Destroy the Super Cannon",
            "quest_unlocksealedcave" => "Unlock the Sealed Cave",
            "quest_bigwhale" => "Raise the Big Whale",
            "quest_traderat" => "Trade away the Rat Tail",
            "quest_forge" => "Have Kokkol forge Legend Sword with Adamant",
            "quest_wakeyang" => "Wake Yang with the Pan",
            "quest_tradepan" => "Return the Pan to Yang's wife",
            "quest_tradepink" => "Trade away the Pink Tail",
            "quest_pass" => "Unlock the Pass door in Toroia",
            _ => "UNKNOWN TASK"
        };

    private string GetDescription(ThresholdTask thresholdTask)
        => thresholdTask.Objective switch
        {
            "internal_dkmatter" => $"Bring {thresholdTask.Threshold} DkMatter{(thresholdTask.Threshold > 1 ? "s" : string.Empty)} to Kory in Agart",
            "internal_keyitem" => $"Obtain any {thresholdTask.Threshold} key item{(thresholdTask.Threshold > 1 ? "s" : string.Empty)}",
            "internal_bossfight" => $"Defeat any {thresholdTask.Threshold} boss{(thresholdTask.Threshold > 1 ? "es" : string.Empty)}",
            "internal_character" => $"Find any {thresholdTask.Threshold} character{(thresholdTask.Threshold > 1 ? "s" : string.Empty)}",
            _ => "UNKNOWN TASK"
        };

    private string GetDescription(GroupTask groupTask)
        => groupTask.Group switch
        {
            "objectives_a" => $"Complete {groupTask.Req ?? "?"} objective{(groupTask.Req != "1" ? "s" : string.Empty)} in Objective Group A",
            "objectives_b" => $"Complete {groupTask.Req ?? "?"} objective{(groupTask.Req != "1" ? "s" : string.Empty)} in Objective Group B",
            "objectives_c" => $"Complete {groupTask.Req ?? "?"} objective{(groupTask.Req != "1" ? "s" : string.Empty)} in Objective Group C",
            "objectives_d" => $"Complete {groupTask.Req ?? "?"} objective{(groupTask.Req != "1" ? "s" : string.Empty)} in Objective Group D",
            "objectives_e" => $"Complete {groupTask.Req ?? "?"} objective{(groupTask.Req != "1" ? "s" : string.Empty)} in Objective Group E",
            _ => "UNKNOWN TASK"
        };

    internal string GetKeyItemName(KeyItemType item)
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

    internal string? GetBossLocationName(BossLocationType location)
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
            BossLocationType.NotFound => null,
            _ => string.Empty,
        };

    internal string GetBossName(BossType bossType)
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

    public Descriptors(IMemorySpace rom)
    {
        var encoder = new TextEncoding();
        _rewardSlotDescriptions = rom.ReadBytes(Addresses.ROM.RewardSlots)
            .ReadMany<byte[]>(0, 8 * 32, 0x100)
            .Select(encoder.GetString)
            .ToList();

        _chestLookup = rom.ReadBytes(Addresses.ROM.ChestLookup);
    }
}