using System;
using System.Drawing;

namespace FF.Rando.Companion.FreeEnterprise.Shared;
internal static class ResourceLookup
{
    public static Bitmap GetKeyItemIcon(KeyItemType type, bool isFound, bool isUsed)
        => type switch
        {
            KeyItemType.Package => isUsed ? FreeEnterprise.FFIVFE_Icons_7Package_Check : isFound ? FreeEnterprise.FFIVFE_Icons_7Package_Color : FreeEnterprise.FFIVFE_Icons_7Package_Gray,
            KeyItemType.SandRuby => isUsed ? FreeEnterprise.FFIVFE_Icons_8SandRuby_Check : isFound ? FreeEnterprise.FFIVFE_Icons_8SandRuby_Color : FreeEnterprise.FFIVFE_Icons_8SandRuby_Gray,
            KeyItemType.LegendSword => isUsed ? FreeEnterprise.FFIVFE_Icons_14LegendSword_Check : isFound ? FreeEnterprise.FFIVFE_Icons_14LegendSword_Color : FreeEnterprise.FFIVFE_Icons_14LegendSword_Gray,
            KeyItemType.BaronKey => isUsed ? FreeEnterprise.FFIVFE_Icons_9BaronKey_Check : isFound ? FreeEnterprise.FFIVFE_Icons_9BaronKey_Color : FreeEnterprise.FFIVFE_Icons_9BaronKey_Gray,
            KeyItemType.TwinHarp => isUsed ? FreeEnterprise.FFIVFE_Icons_6TwinHarp_Check : isFound ? FreeEnterprise.FFIVFE_Icons_6TwinHarp_Color : FreeEnterprise.FFIVFE_Icons_6TwinHarp_Gray,
            KeyItemType.EarthCrystal => isUsed ? FreeEnterprise.FFIVFE_Icons_5EarthCrystal_Check : isFound ? FreeEnterprise.FFIVFE_Icons_5EarthCrystal_Color : FreeEnterprise.FFIVFE_Icons_5EarthCrystal_Gray,
            KeyItemType.MagmaKey => isUsed ? FreeEnterprise.FFIVFE_Icons_10MagmaKey_Check : isFound ? FreeEnterprise.FFIVFE_Icons_10MagmaKey_Color : FreeEnterprise.FFIVFE_Icons_10MagmaKey_Gray,
            KeyItemType.TowerKey => isUsed ? FreeEnterprise.FFIVFE_Icons_11TowerKey_Check : isFound ? FreeEnterprise.FFIVFE_Icons_11TowerKey_Color : FreeEnterprise.FFIVFE_Icons_11TowerKey_Gray,
            KeyItemType.Hook => isUsed ? FreeEnterprise.FFIVFE_Icons_3Hook_Check : isFound ? FreeEnterprise.FFIVFE_Icons_3Hook_Color : FreeEnterprise.FFIVFE_Icons_3Hook_Gray,
            KeyItemType.LucaKey => isUsed ? FreeEnterprise.FFIVFE_Icons_12LucaKey_Check : isFound ? FreeEnterprise.FFIVFE_Icons_12LucaKey_Color : FreeEnterprise.FFIVFE_Icons_12LucaKey_Gray,
            KeyItemType.DarknessCrystal => isUsed ? FreeEnterprise.FFIVFE_Icons_4DarkCrystal_Check : isFound ? FreeEnterprise.FFIVFE_Icons_4DarkCrystal_Color : FreeEnterprise.FFIVFE_Icons_4DarkCrystal_Gray,
            KeyItemType.RatTail => isUsed ? FreeEnterprise.FFIVFE_Icons_17RatTail_Check : isFound ? FreeEnterprise.FFIVFE_Icons_17RatTail_Color : FreeEnterprise.FFIVFE_Icons_17RatTail_Gray,
            KeyItemType.Adamant => isUsed ? FreeEnterprise.FFIVFE_Icons_13Adamant_Check : isFound ? FreeEnterprise.FFIVFE_Icons_13Adamant_Color : FreeEnterprise.FFIVFE_Icons_13Adamant_Gray,
            KeyItemType.Pan => isUsed ? FreeEnterprise.FFIVFE_Icons_15Pan_Check : isFound ? FreeEnterprise.FFIVFE_Icons_15Pan_Color : FreeEnterprise.FFIVFE_Icons_15Pan_Gray,
            KeyItemType.Spoon => isUsed ? FreeEnterprise.FFIVFE_Icons_16Spoon_Check : isFound ? FreeEnterprise.FFIVFE_Icons_16Spoon_Color : FreeEnterprise.FFIVFE_Icons_16Spoon_Gray,
            KeyItemType.PinkTail => isUsed ? FreeEnterprise.FFIVFE_Icons_18PinkTail_Check : isFound ? FreeEnterprise.FFIVFE_Icons_18PinkTail_Color : FreeEnterprise.FFIVFE_Icons_18PinkTail_Gray,
            KeyItemType.Crystal => isUsed ? FreeEnterprise.FFIVFE_Icons_1THECrystal_Check : isFound ? FreeEnterprise.FFIVFE_Icons_1THECrystal_Color : FreeEnterprise.FFIVFE_Icons_1THECrystal_Gray,
            KeyItemType.Pass => isUsed ? FreeEnterprise.FFIVFE_Icons_2Pass_Check : isFound ? FreeEnterprise.FFIVFE_Icons_2Pass_Color : FreeEnterprise.FFIVFE_Icons_2Pass_Gray,
            _ => throw new InvalidOperationException()
        };

    public static Bitmap GetBossIcon(BossType bossType, bool isFound, bool isDefeated)
        => bossType switch
        {
            BossType.Dmist => (isDefeated || isFound) ? FreeEnterprise.FFIVFE_Bosses_1MistD_Color : FreeEnterprise.FFIVFE_Bosses_1MistD_Gray,
            BossType.Officer => (isDefeated || isFound) ? FreeEnterprise.FFIVFE_Bosses_2Soldier_Color : FreeEnterprise.FFIVFE_Bosses_2Soldier_Gray,
            BossType.Octomamm => (isDefeated || isFound) ? FreeEnterprise.FFIVFE_Bosses_3Octo_Color : FreeEnterprise.FFIVFE_Bosses_3Octo_Gray,
            BossType.Antlion => (isDefeated || isFound) ? FreeEnterprise.FFIVFE_Bosses_4Antlion_Color : FreeEnterprise.FFIVFE_Bosses_4Antlion_Gray,
            BossType.Waterhag => (isDefeated || isFound) ? FreeEnterprise.FFIVFE_Bosses_5WHag_Color : FreeEnterprise.FFIVFE_Bosses_5WHag_Gray,
            BossType.Mombomb => (isDefeated || isFound) ? FreeEnterprise.FFIVFE_Bosses_6Mombomb_Color : FreeEnterprise.FFIVFE_Bosses_6Mombomb_Gray,
            BossType.FabulGauntlet => (isDefeated || isFound) ? FreeEnterprise.FFIVFE_Bosses_7Gauntlet_Color : FreeEnterprise.FFIVFE_Bosses_7Gauntlet_Gray,
            BossType.Milon => (isDefeated || isFound) ? FreeEnterprise.FFIVFE_Bosses_8Milon_Color : FreeEnterprise.FFIVFE_Bosses_8Milon_Gray,
            BossType.Milonz => (isDefeated || isFound) ? FreeEnterprise.FFIVFE_Bosses_9MilonZ_Color : FreeEnterprise.FFIVFE_Bosses_9MilonZ_Gray,
            BossType.Mirrorcecil => (isDefeated || isFound) ? FreeEnterprise.FFIVFE_Bosses_10DKCecil_Color : FreeEnterprise.FFIVFE_Bosses_10DKCecil_Gray,
            BossType.Karate => (isDefeated || isFound) ? FreeEnterprise.FFIVFE_Bosses_12Yang_Color : FreeEnterprise.FFIVFE_Bosses_12Yang_Gray,
            BossType.Guard => (isDefeated || isFound) ? FreeEnterprise.FFIVFE_Bosses_11Guards_Color : FreeEnterprise.FFIVFE_Bosses_11Guards_Gray,
            BossType.Baigan => (isDefeated || isFound) ? FreeEnterprise.FFIVFE_Bosses_13Baigan_Color : FreeEnterprise.FFIVFE_Bosses_13Baigan_Gray,
            BossType.Kainazzo => (isDefeated || isFound) ? FreeEnterprise.FFIVFE_Bosses_14Kainazzo_Color : FreeEnterprise.FFIVFE_Bosses_14Kainazzo_Gray,
            BossType.Darkelf => (isDefeated || isFound) ? FreeEnterprise.FFIVFE_Bosses_15DElf_Color : FreeEnterprise.FFIVFE_Bosses_15DElf_Gray,
            BossType.Magus => (isDefeated || isFound) ? FreeEnterprise.FFIVFE_Bosses_16MagusSis_Color : FreeEnterprise.FFIVFE_Bosses_16MagusSis_Gray,
            BossType.Valvalis => (isDefeated || isFound) ? FreeEnterprise.FFIVFE_Bosses_17Valvalis_Color : FreeEnterprise.FFIVFE_Bosses_17Valvalis_Gray,
            BossType.Calbrena => (isDefeated || isFound) ? FreeEnterprise.FFIVFE_Bosses_18Calcabrina_Color : FreeEnterprise.FFIVFE_Bosses_18Calcabrina_Gray,
            BossType.Golbez => (isDefeated || isFound) ? FreeEnterprise.FFIVFE_Bosses_19Golbez_Color : FreeEnterprise.FFIVFE_Bosses_19Golbez_Gray,
            BossType.Lugae => (isDefeated || isFound) ? FreeEnterprise.FFIVFE_Bosses_20Lugae_Color : FreeEnterprise.FFIVFE_Bosses_20Lugae_Gray,
            BossType.Darkimp => (isDefeated || isFound) ? FreeEnterprise.FFIVFE_Bosses_35DarkImps_Color : FreeEnterprise.FFIVFE_Bosses_35DarkImps_Gray,
            BossType.Kingqueen => (isDefeated || isFound) ? FreeEnterprise.FFIVFE_Bosses_21Eblan_Color : FreeEnterprise.FFIVFE_Bosses_21Eblan_Gray,
            BossType.Rubicant => (isDefeated || isFound) ? FreeEnterprise.FFIVFE_Bosses_22Rubicante_Color : FreeEnterprise.FFIVFE_Bosses_22Rubicante_Gray,
            BossType.Evilwall => (isDefeated || isFound) ? FreeEnterprise.FFIVFE_Bosses_23EvilWall_Color : FreeEnterprise.FFIVFE_Bosses_23EvilWall_Gray,
            BossType.Asura => (isDefeated || isFound) ? FreeEnterprise.FFIVFE_Bosses_27Asura_Color : FreeEnterprise.FFIVFE_Bosses_27Asura_Gray,
            BossType.Leviatan => (isDefeated || isFound) ? FreeEnterprise.FFIVFE_Bosses_28Leviath_Color : FreeEnterprise.FFIVFE_Bosses_28Leviath_Gray,
            BossType.Odin => (isDefeated || isFound) ? FreeEnterprise.FFIVFE_Bosses_26Odin_Color : FreeEnterprise.FFIVFE_Bosses_26Odin_Gray,
            BossType.Bahamut => (isDefeated || isFound) ? FreeEnterprise.FFIVFE_Bosses_29Bahamut_Color : FreeEnterprise.FFIVFE_Bosses_29Bahamut_Gray,
            BossType.Elements => (isDefeated || isFound) ? FreeEnterprise.FFIVFE_Bosses_24Fiends_Color : FreeEnterprise.FFIVFE_Bosses_24Fiends_Gray,
            BossType.Cpu => (isDefeated || isFound) ? FreeEnterprise.FFIVFE_Bosses_25CPU_Color : FreeEnterprise.FFIVFE_Bosses_25CPU_Gray,
            BossType.Paledim => (isDefeated || isFound) ? FreeEnterprise.FFIVFE_Bosses_30PaleDim_Color : FreeEnterprise.FFIVFE_Bosses_30PaleDim_Gray,
            BossType.Wyvern => (isDefeated || isFound) ? FreeEnterprise.FFIVFE_Bosses_34Wyvern_Color : FreeEnterprise.FFIVFE_Bosses_34Wyvern_Gray,
            BossType.Plague => (isDefeated || isFound) ? FreeEnterprise.FFIVFE_Bosses_32Plague_Color : FreeEnterprise.FFIVFE_Bosses_32Plague_Gray,
            BossType.Dlunar => (isDefeated || isFound) ? FreeEnterprise.FFIVFE_Bosses_31LunarD_Color : FreeEnterprise.FFIVFE_Bosses_31LunarD_Gray,
            BossType.Ogopogo => (isDefeated || isFound) ? FreeEnterprise.FFIVFE_Bosses_33Ogopogo_Color : FreeEnterprise.FFIVFE_Bosses_33Ogopogo_Gray,
            _ => throw new InvalidOperationException(),
        };
}
