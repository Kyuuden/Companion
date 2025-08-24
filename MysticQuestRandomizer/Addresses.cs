using BizHawk.Common;
using FF.Rando.Companion.Extensions;
using System.Collections.Generic;

namespace FF.Rando.Companion.MysticQuestRandomizer;
internal class Addresses
{
    internal static class ROM
    {
        public static readonly Range<long> Items = 0x20000L.WithLength(0x1800);
        public static readonly Range<long> Resitstances = 0x21840L.WithLength(0x180);
        public static readonly Range<long> Characters = 0x21A20L.WithLength(0x1C80);
        public static readonly Range<long> Palettes = 0x3D7F4L.WithLength(0x140);
    }

    internal static class WRAM
    {
    }
}
