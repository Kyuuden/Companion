using BizHawk.Common;

namespace FF.Rando.Companion.Games.FreeEnterprise._5._0._0;

internal static class Addresses
{
    internal static class SRAM
    {
        public readonly static long StartedIndicatorAddress = 0x70F3;

        public readonly static Range<long> SramRegion = 0x7C00L.RangeTo(0x7CFF);
        public readonly static System.Range KeyItemLocationBits = new(0x00, 0x30);
        public readonly static System.Range BossLocationBits = new(0x30, 0x80);
        public readonly static System.Range AxtorBits = new(0x80, 0x100);
    }

    internal static class WRAM
    {
        public readonly static long VictoryIndicatorAddress = 0x151F;

        public readonly static Range<long> PartyRegion = 0x1000L.RangeTo(0x1140);
        public readonly static Range<long> WramRegion = 0x1500L.RangeTo(0x15FF);

        public readonly static System.Range KeyItemFoundBits = new(0x00, 0x03);
        public readonly static System.Range KeyItemUsedBits = new(0x03, 0x06);
        public readonly static System.Range BossDefeatedBits = new(0x06, 0xB);
        public readonly static System.Range BossLocationBits = new(0x0B, 0x10);
        public readonly static System.Range AxtorFoundBits = new(0x10, 0x14);
        public readonly static System.Range ShopCheckedBits = new(0x14, 0x1A);
        public readonly static System.Range VictoryIndicator = new(0x1F, 0x20);
        public readonly static System.Range RewardSlotCheckedBits = new(0x20, 0x30);
        public readonly static System.Range ObjectiveTaskProgress = new(0x30, 0x50);
        public readonly static System.Range ObjectiveGroupProress = new(0x50, 0x60);
        public readonly static System.Index KeyItemCheckCount = new(0x76);
        public readonly static System.Index KeyItemZonkCount = new(0x77);
    }

    internal static class ROM
    {
        public readonly static Range<long> RewardSlots = 0x113000L.RangeTo(0x114FFF);
        public readonly static Range<long> ChestLookup = 0x10DE00L.RangeTo(0x10DFFF);
    }
}
