using BizHawk.Common;
using FF.Rando.Companion.Extensions;

namespace FF.Rando.Companion.WorldsCollide.RomData;
internal class Addresses
{
    internal static class ROM
    {
        public static readonly Range<long> Indentifier = 0x3F4D7L.WithLength(20);

        public static readonly Range<long> Font = 0x4fc0L.WithLength(0x1000);
        public static readonly Range<long> FontPalettes = 0x18E800L.WithLength(0xA0);

        public static readonly Range<long> Backgrounds = 0x2d0000L.WithLength(0x1c00);
        public static readonly Range<long> BackgroundPalettes = 0x2d1c00L.WithLength(0x100);

        public static readonly Range<long> BattleSpriteDatabase = 0x127005L.WithLength(2080);
        public static readonly Range<long> BattleSpritePaletteData = 0x127820L.WithLength(0x3000);
        public static readonly Range<long> BattleSpriteSmallStencilData = 0x12A824L.WithLength(0x400);
        public static readonly Range<long> BattleSpriteLargeStencilData = 0x12AC24L.WithLength(0x600);
        public static readonly Range<long> BattleSpriteTileData = 0x297000L.WithLength(0x7200 * 8);

        public static readonly Range<long> PortraitSpriteTileData = 0x2d1d00L.WithLength(0x3B60);
        public static readonly Range<long> PortraitSpritePaletteData = 0x2d5860L.WithLength(0x280);

        public static readonly Range<long> FontTileData = 0x47fc0L.WithLength(0x1000);

        public static readonly Range<long> ActorSpriteData = 0x150000L.WithLength(217088);
        public static readonly Range<long> ActorPaletteData = 0x2d300L.WithLength(1024);

        public static readonly Range<long> TilesetData = 0x1fda00L.WithLength(0x61A00);
        public static readonly Range<long> TilesetPaletteData = 0x2dc480L.WithLength(0x3000);
    }

    internal static class WRAM
    {
        public const uint CHARACTER_COUNT = 0x1fc4;
        public const uint ESPER_COUNT = 0x1FC8;
        public const uint CHECK_COUNT = 0x1FCA;
        public const uint DRAGON_COUNT = 0x1FCE;
        public const uint BOSS_COUNT = 0x1FF8;

        public static readonly Range<long> Statistics = 0x1fc2L.WithLength(64);
        public static readonly Range<long> State = 0x1e80L.WithLength(150);
        public static readonly Range<long> Dragons = 0x1dc9L.WithLength(24);
        public static readonly Range<long> Chests = 0x1e40L.WithLength(47);

        public static readonly Range<long> ConfigData = 0x1D4DL.WithLength(0x7c);
    }
}