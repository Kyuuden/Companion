using BizHawk.Common;
using FF.Rando.Companion.Extensions;

namespace FF.Rando.Companion.Games.MysticQuestRandomizer;
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
        public static readonly long ActivePartner = 0x004dL;

        public static readonly long GameStateIndicator = 0x3749;
        public static readonly long GameVictoryIndicator = 0x0F22;
        public static readonly Range<long> Mob1Health = 0x1114L.WithLength(2);
        public static readonly Range<long> Mob2Health = 0x1194L.WithLength(2);
        public static readonly Range<long> Mob3Health = 0x1214L.WithLength(2);

        public static readonly Range<long> WramRegion = 0x0e00L.WithLength(0x280);

        public static readonly System.Range StateFlags = new(0xA8, 0xC8);
        public static readonly System.Range Chests = new(0xC8, 0xE8);
        public static readonly System.Range Battlefields = new(0x1d4, 0x1e8);

        public static readonly System.Range FoundShards = new(0x93, 0x94);
        public static readonly System.Range FoundKeyItemBits = new(0xa6, 0xa8);
        public static readonly System.Range FoundWeaponBits = new(0x232, 0x235);
        public static readonly System.Range FoundArmorBits = new(0x235, 0x238);
        public static readonly System.Range FoundSpellBits = new(0x238, 0x23A);
    }
}
