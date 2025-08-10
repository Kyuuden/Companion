using FF.Rando.Companion.Extensions;
using FF.Rando.Companion.FreeEnterprise.RomData;
using FF.Rando.Companion.FreeEnterprise.Shared;
using FF.Rando.Companion.MemoryManagement;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FF.Rando.Companion.FreeEnterprise._5._0._0;

internal class Descriptors : Companion.FreeEnterprise.Descriptors
{
    private readonly List<string> _rewardSlotDescriptions;
    private readonly byte[] _chestLookup;

    private const int ShopOffset = 0x60;
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
            return _rewardSlotDescriptions[(rewardSlot & 0x1FF) + ShopOffset];
        }

        if (rewardSlot < 0 || rewardSlot >= _rewardSlotDescriptions.Count)
            return string.Empty;

        return _rewardSlotDescriptions[rewardSlot];
    }

    internal string GetShopDescription(Shops shop)
    {
        return _rewardSlotDescriptions[(int)shop + ShopOffset];
    }

    internal string GetChestDescription(ChestSlot chest)
    {
        var location = _chestLookup[(int)chest];

        return chest.IsMiab()
            ? _rewardSlotDescriptions[location + MiabOffset]
            : _rewardSlotDescriptions[location + ChestOffset];
    }

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

    public Descriptors(IMemorySpace rom)
    {
        var encoder = new TextEncoding();
        _rewardSlotDescriptions = rom.ReadBytes(Addresses.ROM.RewardSlots)
            .ReadMany<byte[]>(0, 8 * 32, 0x100)
            .Select(encoder.GetString)
            .Select(s=> s.Trim())
            .ToList();

        _chestLookup = rom.ReadBytes(Addresses.ROM.ChestLookup);
    }
}