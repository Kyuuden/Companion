namespace BizHawk.FreeEnterprise.Companion
{
    public static class KeyItemOrdering
    {
        public static int ToIconOrder(this KeyItemType item)
            => item switch
            {
                KeyItemType.Crystal => 0,
                KeyItemType.Hook => 1,
                KeyItemType.DarknessCrystal => 2,
                KeyItemType.EarthCrystal => 3,
                KeyItemType.TwinHarp => 4,
                KeyItemType.Package => 5,
                KeyItemType.SandRuby => 6,
                KeyItemType.BaronKey => 7,
                KeyItemType.MagmaKey => 8,
                KeyItemType.TowerKey => 9,
                KeyItemType.LucaKey => 10,
                KeyItemType.Adamant => 11,
                KeyItemType.LegendSword => 12,
                KeyItemType.Pan => 13,
                KeyItemType.Spoon => 14,
                KeyItemType.RatTail => 15,
                KeyItemType.PinkTail => 16,
                KeyItemType.Pass => 17,
                _ => -1
            };

        public static int ToIndex(this KeyItemType item)
            => item switch
            {
                KeyItemType.Package => 0,
                KeyItemType.SandRuby => 1,
                KeyItemType.LegendSword => 2,
                KeyItemType.BaronKey => 3,
                KeyItemType.TwinHarp => 4,
                KeyItemType.EarthCrystal => 5,
                KeyItemType.MagmaKey => 6,
                KeyItemType.TowerKey => 7,
                KeyItemType.Hook => 8,
                KeyItemType.LucaKey => 9,
                KeyItemType.DarknessCrystal => 10,
                KeyItemType.RatTail => 11,
                KeyItemType.Adamant => 12,
                KeyItemType.Pan => 13,
                KeyItemType.Spoon => 14,
                KeyItemType.PinkTail => 15,
                KeyItemType.Crystal => 16,
                KeyItemType.Pass => 17,
                _ => 0
            };
    }
}
