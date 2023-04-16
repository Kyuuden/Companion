using System.Collections.Generic;
using System.Drawing;

namespace BizHawk.FreeEnterprise.Companion
{
    public class KeyItemIconSet
    {
        public Bitmap NotFound { get; }
        public Bitmap Found { get; }
        public Bitmap Used { get; }

        public KeyItemIconSet(Bitmap notfound, Bitmap found, Bitmap used)
        {
            NotFound = notfound;
            Found = found;
            Used = used;
        }
    }

    public class BossIconSet
    {
        public Bitmap NotFound { get; }
        public Bitmap Found { get; }

        public BossIconSet(Bitmap notfound, Bitmap found)
        {
            NotFound = notfound;
            Found = found;
        }
    }

    public static class IconLookup
    {
        private static Dictionary<KeyItemType, KeyItemIconSet> _keyItemIcons = new Dictionary<KeyItemType, KeyItemIconSet>();
        private static Dictionary<BossType, BossIconSet> _bossIcons = new Dictionary<BossType, BossIconSet>();

        static IconLookup()
        {
            _keyItemIcons.Add(KeyItemType.Adamant, new KeyItemIconSet(Properties.Resources.FFIVFE_Icons_13Adamant_Gray, Properties.Resources.FFIVFE_Icons_13Adamant_Color, Properties.Resources.FFIVFE_Icons_13Adamant_Check));
            _keyItemIcons.Add(KeyItemType.BaronKey, new KeyItemIconSet(Properties.Resources.FFIVFE_Icons_9BaronKey_Gray, Properties.Resources.FFIVFE_Icons_9BaronKey_Color, Properties.Resources.FFIVFE_Icons_9BaronKey_Check));
            _keyItemIcons.Add(KeyItemType.Crystal, new KeyItemIconSet(Properties.Resources.FFIVFE_Icons_1THECrystal_Gray, Properties.Resources.FFIVFE_Icons_1THECrystal_Color, Properties.Resources.FFIVFE_Icons_1THECrystal_Check));
            _keyItemIcons.Add(KeyItemType.DarknessCrystal, new KeyItemIconSet(Properties.Resources.FFIVFE_Icons_4DarkCrystal_Gray, Properties.Resources.FFIVFE_Icons_4DarkCrystal_Color, Properties.Resources.FFIVFE_Icons_4DarkCrystal_Check));
            _keyItemIcons.Add(KeyItemType.EarthCrystal, new KeyItemIconSet(Properties.Resources.FFIVFE_Icons_5EarthCrystal_Gray, Properties.Resources.FFIVFE_Icons_5EarthCrystal_Color, Properties.Resources.FFIVFE_Icons_5EarthCrystal_Check));
            _keyItemIcons.Add(KeyItemType.Hook, new KeyItemIconSet(Properties.Resources.FFIVFE_Icons_3Hook_Gray, Properties.Resources.FFIVFE_Icons_3Hook_Color, Properties.Resources.FFIVFE_Icons_3Hook_Check));
            _keyItemIcons.Add(KeyItemType.LegendSword, new KeyItemIconSet(Properties.Resources.FFIVFE_Icons_14LegendSword_Gray, Properties.Resources.FFIVFE_Icons_14LegendSword_Color, Properties.Resources.FFIVFE_Icons_14LegendSword_Check));
            _keyItemIcons.Add(KeyItemType.LucaKey, new KeyItemIconSet(Properties.Resources.FFIVFE_Icons_12LucaKey_Gray, Properties.Resources.FFIVFE_Icons_12LucaKey_Color, Properties.Resources.FFIVFE_Icons_12LucaKey_Check));
            _keyItemIcons.Add(KeyItemType.MagmaKey, new KeyItemIconSet(Properties.Resources.FFIVFE_Icons_10MagmaKey_Gray, Properties.Resources.FFIVFE_Icons_10MagmaKey_Color, Properties.Resources.FFIVFE_Icons_10MagmaKey_Check));
            _keyItemIcons.Add(KeyItemType.Package, new KeyItemIconSet(Properties.Resources.FFIVFE_Icons_7Package_Gray, Properties.Resources.FFIVFE_Icons_7Package_Color, Properties.Resources.FFIVFE_Icons_7Package_Check));
            _keyItemIcons.Add(KeyItemType.Pan, new KeyItemIconSet(Properties.Resources.FFIVFE_Icons_15Pan_Gray, Properties.Resources.FFIVFE_Icons_15Pan_Color, Properties.Resources.FFIVFE_Icons_15Pan_Check));
            _keyItemIcons.Add(KeyItemType.PinkTail, new KeyItemIconSet(Properties.Resources.FFIVFE_Icons_18PinkTail_Gray, Properties.Resources.FFIVFE_Icons_18PinkTail_Color, Properties.Resources.FFIVFE_Icons_18PinkTail_Check));
            _keyItemIcons.Add(KeyItemType.RatTail, new KeyItemIconSet(Properties.Resources.FFIVFE_Icons_17RatTail_Gray, Properties.Resources.FFIVFE_Icons_17RatTail_Color, Properties.Resources.FFIVFE_Icons_17RatTail_Check));
            _keyItemIcons.Add(KeyItemType.SandRuby, new KeyItemIconSet(Properties.Resources.FFIVFE_Icons_8SandRuby_Gray, Properties.Resources.FFIVFE_Icons_8SandRuby_Color, Properties.Resources.FFIVFE_Icons_8SandRuby_Check));
            _keyItemIcons.Add(KeyItemType.Spoon, new KeyItemIconSet(Properties.Resources.FFIVFE_Icons_16Spoon_Gray, Properties.Resources.FFIVFE_Icons_16Spoon_Color, Properties.Resources.FFIVFE_Icons_16Spoon_Check));
            _keyItemIcons.Add(KeyItemType.TowerKey, new KeyItemIconSet(Properties.Resources.FFIVFE_Icons_11TowerKey_Gray, Properties.Resources.FFIVFE_Icons_11TowerKey_Color, Properties.Resources.FFIVFE_Icons_11TowerKey_Check));
            _keyItemIcons.Add(KeyItemType.TwinHarp, new KeyItemIconSet(Properties.Resources.FFIVFE_Icons_6TwinHarp_Gray, Properties.Resources.FFIVFE_Icons_6TwinHarp_Color, Properties.Resources.FFIVFE_Icons_6TwinHarp_Check));
            _keyItemIcons.Add(KeyItemType.Pass, new KeyItemIconSet(Properties.Resources.FFIVFE_Icons_2Pass_Gray, Properties.Resources.FFIVFE_Icons_2Pass_Color, Properties.Resources.FFIVFE_Icons_2Pass_Check));


            _bossIcons.Add(BossType.DMist, new BossIconSet(Properties.Resources.FFIVFE_Bosses_1MistD_Gray, Properties.Resources.FFIVFE_Bosses_1MistD_Color));
            _bossIcons.Add(BossType.TroiaGuards, new BossIconSet(Properties.Resources.FFIVFE_Bosses_2Soldier_Gray, Properties.Resources.FFIVFE_Bosses_2Soldier_Color));
            _bossIcons.Add(BossType.Octomamm, new BossIconSet(Properties.Resources.FFIVFE_Bosses_3Octo_Gray, Properties.Resources.FFIVFE_Bosses_3Octo_Color));
            _bossIcons.Add(BossType.Antlion, new BossIconSet(Properties.Resources.FFIVFE_Bosses_4Antlion_Gray, Properties.Resources.FFIVFE_Bosses_4Antlion_Color));
            _bossIcons.Add(BossType.WaterHag, new BossIconSet(Properties.Resources.FFIVFE_Bosses_5WHag_Gray, Properties.Resources.FFIVFE_Bosses_5WHag_Color));
            _bossIcons.Add(BossType.MomBomb, new BossIconSet(Properties.Resources.FFIVFE_Bosses_6Mombomb_Gray, Properties.Resources.FFIVFE_Bosses_6Mombomb_Color));
            _bossIcons.Add(BossType.Guantlet, new BossIconSet(Properties.Resources.FFIVFE_Bosses_7Gauntlet_Gray, Properties.Resources.FFIVFE_Bosses_7Gauntlet_Color));
            _bossIcons.Add(BossType.Milon, new BossIconSet(Properties.Resources.FFIVFE_Bosses_8Milon_Gray, Properties.Resources.FFIVFE_Bosses_8Milon_Color));
            _bossIcons.Add(BossType.MilonZ, new BossIconSet(Properties.Resources.FFIVFE_Bosses_9MilonZ_Gray, Properties.Resources.FFIVFE_Bosses_9MilonZ_Color));
            _bossIcons.Add(BossType.DKnight, new BossIconSet(Properties.Resources.FFIVFE_Bosses_10DKCecil_Gray, Properties.Resources.FFIVFE_Bosses_10DKCecil_Color));
            _bossIcons.Add(BossType.BaronGuards, new BossIconSet(Properties.Resources.FFIVFE_Bosses_11Guards_Gray, Properties.Resources.FFIVFE_Bosses_11Guards_Color));
            _bossIcons.Add(BossType.Yang, new BossIconSet(Properties.Resources.FFIVFE_Bosses_12Yang_Gray, Properties.Resources.FFIVFE_Bosses_12Yang_Color));
            _bossIcons.Add(BossType.Baigan, new BossIconSet(Properties.Resources.FFIVFE_Bosses_13Baigan_Gray, Properties.Resources.FFIVFE_Bosses_13Baigan_Color));
            _bossIcons.Add(BossType.Kainazzo, new BossIconSet(Properties.Resources.FFIVFE_Bosses_14Kainazzo_Gray, Properties.Resources.FFIVFE_Bosses_14Kainazzo_Color));
            _bossIcons.Add(BossType.DarkElf, new BossIconSet(Properties.Resources.FFIVFE_Bosses_15DElf_Gray, Properties.Resources.FFIVFE_Bosses_15DElf_Color));
            _bossIcons.Add(BossType.MagusSisters, new BossIconSet(Properties.Resources.FFIVFE_Bosses_16MagusSis_Gray, Properties.Resources.FFIVFE_Bosses_16MagusSis_Color));
            _bossIcons.Add(BossType.Valvalis, new BossIconSet(Properties.Resources.FFIVFE_Bosses_17Valvalis_Gray, Properties.Resources.FFIVFE_Bosses_17Valvalis_Color));
            _bossIcons.Add(BossType.Calbrena, new BossIconSet(Properties.Resources.FFIVFE_Bosses_18Calcabrina_Gray, Properties.Resources.FFIVFE_Bosses_18Calcabrina_Color));
            _bossIcons.Add(BossType.Golbez, new BossIconSet(Properties.Resources.FFIVFE_Bosses_19Golbez_Gray, Properties.Resources.FFIVFE_Bosses_19Golbez_Color));
            _bossIcons.Add(BossType.DrLugae, new BossIconSet(Properties.Resources.FFIVFE_Bosses_20Lugae_Gray, Properties.Resources.FFIVFE_Bosses_20Lugae_Color));
            _bossIcons.Add(BossType.KQEblan, new BossIconSet(Properties.Resources.FFIVFE_Bosses_21Eblan_Gray, Properties.Resources.FFIVFE_Bosses_21Eblan_Color));
            _bossIcons.Add(BossType.Rubicant, new BossIconSet(Properties.Resources.FFIVFE_Bosses_22Rubicante_Gray, Properties.Resources.FFIVFE_Bosses_22Rubicante_Color));
            _bossIcons.Add(BossType.EvilWall, new BossIconSet(Properties.Resources.FFIVFE_Bosses_23EvilWall_Gray, Properties.Resources.FFIVFE_Bosses_23EvilWall_Color));
            _bossIcons.Add(BossType.Elements, new BossIconSet(Properties.Resources.FFIVFE_Bosses_24Fiends_Gray, Properties.Resources.FFIVFE_Bosses_24Fiends_Color));
            _bossIcons.Add(BossType.CPU, new BossIconSet(Properties.Resources.FFIVFE_Bosses_25CPU_Gray, Properties.Resources.FFIVFE_Bosses_25CPU_Color));
            _bossIcons.Add(BossType.Odin, new BossIconSet(Properties.Resources.FFIVFE_Bosses_26Odin_Gray, Properties.Resources.FFIVFE_Bosses_26Odin_Color));
            _bossIcons.Add(BossType.Ausra, new BossIconSet(Properties.Resources.FFIVFE_Bosses_27Asura_Gray, Properties.Resources.FFIVFE_Bosses_27Asura_Color));
            _bossIcons.Add(BossType.Leviatan, new BossIconSet(Properties.Resources.FFIVFE_Bosses_28Leviath_Gray, Properties.Resources.FFIVFE_Bosses_28Leviath_Color));
            _bossIcons.Add(BossType.Bahamut, new BossIconSet(Properties.Resources.FFIVFE_Bosses_29Bahamut_Gray, Properties.Resources.FFIVFE_Bosses_29Bahamut_Color));
            _bossIcons.Add(BossType.PaleDim, new BossIconSet(Properties.Resources.FFIVFE_Bosses_30PaleDim_Gray, Properties.Resources.FFIVFE_Bosses_30PaleDim_Color));
            _bossIcons.Add(BossType.DLunar, new BossIconSet(Properties.Resources.FFIVFE_Bosses_31LunarD_Gray, Properties.Resources.FFIVFE_Bosses_31LunarD_Color));
            _bossIcons.Add(BossType.Plague, new BossIconSet(Properties.Resources.FFIVFE_Bosses_32Plague_Gray, Properties.Resources.FFIVFE_Bosses_32Plague_Color));
            _bossIcons.Add(BossType.Ogopogo, new BossIconSet(Properties.Resources.FFIVFE_Bosses_33Ogopogo_Gray, Properties.Resources.FFIVFE_Bosses_33Ogopogo_Color));
            _bossIcons.Add(BossType.Wyvern, new BossIconSet(Properties.Resources.FFIVFE_Bosses_34Wyvern_Gray, Properties.Resources.FFIVFE_Bosses_34Wyvern_Color));
            _bossIcons.Add(BossType.DarkImps, new BossIconSet(Properties.Resources.FFIVFE_Bosses_35DarkImps_Gray, Properties.Resources.FFIVFE_Bosses_35DarkImps_Color));
            _bossIcons.Add(BossType.Zeromus, new BossIconSet(Properties.Resources.FFIVFE_Bosses_36Zeromus_Gray, Properties.Resources.FFIVFE_Bosses_36Zeromus_Color));
        }

        public static KeyItemIconSet GetIcons(KeyItemType keyItemType) => _keyItemIcons[keyItemType];
        public static BossIconSet GetIcons(BossType bossType) => _bossIcons[bossType];
    }
}
