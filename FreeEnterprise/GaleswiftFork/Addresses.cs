using BizHawk.Common;

namespace FF.Rando.Companion.FreeEnterprise.GaleswiftFork;

internal static class Addresses
{
    internal static class SRAM
    {
        public readonly static Range<long> KeyItemLocations = 0x7080L.RangeTo(0x70A2);
    }

    internal static class WRAM
    {
        public readonly static long BossesDefeated = 0x157CL;
        public readonly static Range<long> PartyRegion = 0x1000L.RangeTo(0x1140);
        public readonly static Range<long> WramRegion = 0x1400L.RangeTo(0x17FF);

        public readonly static System.Range KeyItemFoundBits = new(0x100, 0x103);
        public readonly static System.Range KeyItemUsedBits = new(0x103, 0x106);
        public readonly static System.Range CheckedLocations = new(0x110, 0x120);
        public readonly static System.Range ObjectiveCompletion = new(0x120, 0x140);
        public readonly static System.Range Inventory = new(0x40, 0xA0);
        public readonly static System.Range Location = new(0x300, 0x303);
    }
}
