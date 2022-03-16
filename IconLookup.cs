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

    public class IconLookup
    {
        private Dictionary<KeyItemType, KeyItemIconSet> _icons = new Dictionary<KeyItemType, KeyItemIconSet>();

        public IconLookup()
        {
            _icons.Add(KeyItemType.Adamant, new KeyItemIconSet(Properties.Resources.FFIVFE_Icons_13Adamant_Gray, Properties.Resources.FFIVFE_Icons_13Adamant_Color, Properties.Resources.FFIVFE_Icons_13Adamant_Check));
            _icons.Add(KeyItemType.BaronKey, new KeyItemIconSet(Properties.Resources.FFIVFE_Icons_9BaronKey_Gray, Properties.Resources.FFIVFE_Icons_9BaronKey_Color, Properties.Resources.FFIVFE_Icons_9BaronKey_Check));
            _icons.Add(KeyItemType.Crystal, new KeyItemIconSet(Properties.Resources.FFIVFE_Icons_1THECrystal_Gray, Properties.Resources.FFIVFE_Icons_1THECrystal_Color, Properties.Resources.FFIVFE_Icons_1THECrystal_Check));
            _icons.Add(KeyItemType.DarknessCrystal, new KeyItemIconSet(Properties.Resources.FFIVFE_Icons_4DarkCrystal_Gray, Properties.Resources.FFIVFE_Icons_4DarkCrystal_Color, Properties.Resources.FFIVFE_Icons_4DarkCrystal_Check));
            _icons.Add(KeyItemType.EarthCrystal, new KeyItemIconSet(Properties.Resources.FFIVFE_Icons_5EarthCrystal_Gray, Properties.Resources.FFIVFE_Icons_5EarthCrystal_Color, Properties.Resources.FFIVFE_Icons_5EarthCrystal_Check));
            _icons.Add(KeyItemType.Hook, new KeyItemIconSet(Properties.Resources.FFIVFE_Icons_3Hook_Gray, Properties.Resources.FFIVFE_Icons_3Hook_Color, Properties.Resources.FFIVFE_Icons_3Hook_Check));
            _icons.Add(KeyItemType.LegendSword, new KeyItemIconSet(Properties.Resources.FFIVFE_Icons_14LegendSword_Gray, Properties.Resources.FFIVFE_Icons_14LegendSword_Color, Properties.Resources.FFIVFE_Icons_14LegendSword_Check));
            _icons.Add(KeyItemType.LucaKey, new KeyItemIconSet(Properties.Resources.FFIVFE_Icons_12LucaKey_Gray, Properties.Resources.FFIVFE_Icons_12LucaKey_Color, Properties.Resources.FFIVFE_Icons_12LucaKey_Check));
            _icons.Add(KeyItemType.MagmaKey, new KeyItemIconSet(Properties.Resources.FFIVFE_Icons_10MagmaKey_Gray, Properties.Resources.FFIVFE_Icons_10MagmaKey_Color, Properties.Resources.FFIVFE_Icons_10MagmaKey_Check));
            _icons.Add(KeyItemType.Package, new KeyItemIconSet(Properties.Resources.FFIVFE_Icons_7Package_Gray, Properties.Resources.FFIVFE_Icons_7Package_Color, Properties.Resources.FFIVFE_Icons_7Package_Check));
            _icons.Add(KeyItemType.Pan, new KeyItemIconSet(Properties.Resources.FFIVFE_Icons_15Pan_Gray, Properties.Resources.FFIVFE_Icons_15Pan_Color, Properties.Resources.FFIVFE_Icons_15Pan_Check));
            _icons.Add(KeyItemType.PinkTail, new KeyItemIconSet(Properties.Resources.FFIVFE_Icons_18PinkTail_Gray, Properties.Resources.FFIVFE_Icons_18PinkTail_Color, Properties.Resources.FFIVFE_Icons_18PinkTail_Check));
            _icons.Add(KeyItemType.RatTail, new KeyItemIconSet(Properties.Resources.FFIVFE_Icons_17RatTail_Gray, Properties.Resources.FFIVFE_Icons_17RatTail_Color, Properties.Resources.FFIVFE_Icons_17RatTail_Check));
            _icons.Add(KeyItemType.SandRuby, new KeyItemIconSet(Properties.Resources.FFIVFE_Icons_8SandRuby_Gray, Properties.Resources.FFIVFE_Icons_8SandRuby_Color, Properties.Resources.FFIVFE_Icons_8SandRuby_Check));
            _icons.Add(KeyItemType.Spoon, new KeyItemIconSet(Properties.Resources.FFIVFE_Icons_16Spoon_Gray, Properties.Resources.FFIVFE_Icons_16Spoon_Color, Properties.Resources.FFIVFE_Icons_16Spoon_Check));
            _icons.Add(KeyItemType.TowerKey, new KeyItemIconSet(Properties.Resources.FFIVFE_Icons_11TowerKey_Gray, Properties.Resources.FFIVFE_Icons_11TowerKey_Color, Properties.Resources.FFIVFE_Icons_11TowerKey_Check));
            _icons.Add(KeyItemType.TwinHarp, new KeyItemIconSet(Properties.Resources.FFIVFE_Icons_6TwinHarp_Gray, Properties.Resources.FFIVFE_Icons_6TwinHarp_Color, Properties.Resources.FFIVFE_Icons_6TwinHarp_Check));
        }

        public KeyItemIconSet GetIcons(KeyItemType keyItemType)
        {
            return _icons[keyItemType];
        }
    }
}
