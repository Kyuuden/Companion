using BizHawk.Common;
using FF.Rando.Companion.Extensions;
using System.Collections.Generic;

namespace FF.Rando.Companion.Games.FreeEnterprise.Shared;

internal class Addresses
{
    internal static class ROM
    {
        public readonly static Range<long> Font = 0x057000L.WithLength(4096);
        public readonly static Range<long> BattleStatusStickers = 0x0679c0L.WithLength(0x600);
        public readonly static Range<long> BattleStatusStickersGreyScalePalette = 0x74bf0L.WithLength(16);  //0x74b00L.WithLength(16);
        public readonly static Range<long> BattleStatusStickersPalette = 0x74b20L.WithLength(16);
        public readonly static Range<long> CharacterDefaultSprites = 0x0d0000L.WithLength(0x7000);
        public readonly static Range<long> CharacterFashion01Sprites = 0x140000L.WithLength(0x7000);
        public readonly static Range<long> CharacterFashion02Sprites = 0x148000L.WithLength(0x7000);
        public readonly static Range<long> CharacterFashion03Sprites = 0x150000L.WithLength(0x7000);
        public readonly static Range<long> CharacterFashion04Sprites = 0x158000L.WithLength(0x7000);
        public readonly static Range<long> CharacterFashion05Sprites = 0x160000L.WithLength(0x7000);
        public readonly static Range<long> CharacterFashion06Sprites = 0x168000L.WithLength(0x7000);
        public readonly static Range<long> CharacterFashion07Sprites = 0x170000L.WithLength(0x7000);
        public readonly static Range<long> CharacterFashion08Sprites = 0x178000L.WithLength(0x7000);
        public readonly static Range<long> CharacterFashion09Sprites = 0x180000L.WithLength(0x7000);
        public readonly static Range<long> CharacterFashion10Sprites = 0x188000L.WithLength(0x7000);
        public readonly static Range<long> CharacterFashion11Sprites = 0x190000L.WithLength(0x7000);
        public readonly static Range<long> CharacterFashion12Sprites = 0x198000L.WithLength(0x7000);

        public readonly static IList<Range<long>> CharacterSprites =
        [
            CharacterDefaultSprites,
            CharacterFashion01Sprites,
            CharacterFashion02Sprites,
            CharacterFashion03Sprites,
            CharacterFashion04Sprites,
            CharacterFashion05Sprites,
            CharacterFashion06Sprites,
            CharacterFashion07Sprites,
            CharacterFashion08Sprites,
            CharacterFashion09Sprites,
            CharacterFashion10Sprites,
            CharacterFashion11Sprites,
            CharacterFashion12Sprites,
        ];

        public readonly static Range<long> NpcSprites = 0xDB300L.WithLength(0x4CE0);
        public readonly static Range<long> NpcPalettes = 0x680D0L.WithLength(0x120);

        public readonly static Range<long> ChestSprites = 0xF1418L.WithLength(0x90);
        public readonly static Range<long> ChestPalette = 0xA5F70L.WithLength(0x10);

        public readonly static Range<long> EggSprites = 0x48240L.WithLength(0x180);
        public readonly static Range<long> EggPalette = 0xe6e80L.WithLength(0x10);

        public readonly static Range<long> CharacterDefaultPalettes = 0x0e7d00L.WithLength(16 * 16 * 2);
        public readonly static Range<long> CharacterFashion01Palettes = 0x147d00L.WithLength(16 * 16 * 2);
        public readonly static Range<long> CharacterFashion02Palettes = 0x14fd00L.WithLength(16 * 16 * 2);
        public readonly static Range<long> CharacterFashion03Palettes = 0x157d00L.WithLength(16 * 16 * 2);
        public readonly static Range<long> CharacterFashion04Palettes = 0x15fd00L.WithLength(16 * 16 * 2);
        public readonly static Range<long> CharacterFashion05Palettes = 0x167d00L.WithLength(16 * 16 * 2);
        public readonly static Range<long> CharacterFashion06Palettes = 0x16fd00L.WithLength(16 * 16 * 2);
        public readonly static Range<long> CharacterFashion07Palettes = 0x177d00L.WithLength(16 * 16 * 2);
        public readonly static Range<long> CharacterFashion08Palettes = 0x17fd00L.WithLength(16 * 16 * 2);
        public readonly static Range<long> CharacterFashion09Palettes = 0x187d00L.WithLength(16 * 16 * 2);
        public readonly static Range<long> CharacterFashion10Palettes = 0x18fd00L.WithLength(16 * 16 * 2);
        public readonly static Range<long> CharacterFashion11Palettes = 0x197d00L.WithLength(16 * 16 * 2);
        public readonly static Range<long> CharacterFashion12Palettes = 0x19fd00L.WithLength(16 * 16 * 2);

        public readonly static IList<Range<long>> CharacterPalettes =
        [
            CharacterDefaultPalettes,
            CharacterFashion01Palettes,
            CharacterFashion02Palettes,
            CharacterFashion03Palettes,
            CharacterFashion04Palettes,
            CharacterFashion05Palettes,
            CharacterFashion06Palettes,
            CharacterFashion07Palettes,
            CharacterFashion08Palettes,
            CharacterFashion09Palettes,
            CharacterFashion10Palettes,
            CharacterFashion11Palettes,
            CharacterFashion12Palettes,
        ];

        public readonly static Range<long> CharacterPortraits = 0xed3c0L.WithLength(0x1500);
        public readonly static Range<long> CharacterPortraitsPalettes = 0x686d0L.WithLength(16 * 16);
        public readonly static Range<long> MetadataLength = 0x1FF000L.WithLength(4);
        public const long MetadataAddress = 0x1FF004;
    }

    internal static class WRAM
    {
        public readonly static Range<long> EndGameTime = 0x157dL.WithLength(3);
        public readonly static Range<long> BackgroundColor = 0x16AAL.WithLength(2);
        public readonly static Range<long> TreasureBits = 0x12A0L.RangeTo(0x12DF);
    }
}
