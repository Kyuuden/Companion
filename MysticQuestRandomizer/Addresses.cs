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
        public static readonly Range<long> AlternateResitances = 0x80F00L.WithLength(0x48);
        public static readonly Range<long> Characters = 0x21A20L.WithLength(0x1C80);
        public static readonly Range<long> Palettes = 0x3D7F4L.WithLength(0x140);

        public static readonly Range<long> Indentifier = 0x60EDDL.WithLength(14);
        public static readonly Range<long> Version = 0x60EEBL.WithLength(8);

        public static readonly Range<long> Font = 0x38430L.WithLength(3072);
    }

    internal static class WRAM
    {
        public static readonly Range<long> ActivePartner = 0x004dL.WithLength(1);

        public static readonly Range<long> WramRegion = 0x0e00L.WithLength(0x280);

        public static readonly System.Range CheckedNpcBits = new(0xA8, 0xA8);
        public static readonly System.Range CheckedlocationBits = new(0xc8, 0xA8);
        public static readonly System.Range CheckedBattlefieldBits = new(0x1d4, 0xA8);

        public static readonly System.Range FoundShards = new(0x93, 0x94);
        public static readonly System.Range FoundKeyItemBits = new(0xa6, 0xa8);
        public static readonly System.Range FoundWeaponBits = new(0x232, 0x235);
        public static readonly System.Range FoundArmorBits = new(0x235, 0x238);
        public static readonly System.Range FoundSpellBits = new(0x238, 0x240);
    }
}
