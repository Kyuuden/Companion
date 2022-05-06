using System.Collections.Generic;

namespace BizHawk.FreeEnterprise.Companion
{
    public static class WRAMAddresses
    {
        public const int PartyAddress = 0x1000;
        public const int PartyBytes = 64 * 5;

        public const int FoundKeyItems = 0x1500;
        public const int FoundKeyItemsBytes = 3;

        public const int UsedKeyItems = 0x1503;
        public const int UsedKeyItemsBytes = 3;

        public const int CheckedLocations = 0x1510;
        public const int CheckedLocationsBytes = 16;

        public const int BackgroundColorAddress = 0x16AA;
        public const int BackgroundColorBytes = 2;

        public const int ObjectiveCompletionAddress = 0x1520;
        public const int ObjectiveCompletionBytes = 32;
    }

    public static class CARTRAMAddresses
    {
        public const int KeyItemLocations = 0x7080;
        public const int KeyItemLocationsBytes = 34;
    }

    public static class CARTROMAddresses
    {
        public const int Font = 0x057000;
        public const int FontBytes = 4096;

        public const int BattleStatusStickers = 0x0679c0;
        public const int BattleStatusStickersBytes = 0x600;

        public const int BattleStatusStickersGreyScalePalette = 0x74b00;
        public const int BattleStatusStickersColorPalette = 0x74b20;

        public static IEnumerable<int> CharacterSpritesAddresses 
        {
            get
            {
                yield return CharacterSpritesAddress;
                yield return CharacterAlternateSprites01Address;
                yield return CharacterAlternateSprites02Address;
                yield return CharacterAlternateSprites03Address;
                yield return CharacterAlternateSprites04Address;
                yield return CharacterAlternateSprites05Address;
                yield return CharacterAlternateSprites06Address;
                yield return CharacterAlternateSprites07Address;
                yield return CharacterAlternateSprites08Address;
                yield return CharacterAlternateSprites09Address;
                yield return CharacterAlternateSprites10Address;
                yield return CharacterAlternateSprites11Address;
                yield return CharacterAlternateSprites12Address;
            } 
        }

        public const int CharacterSpritesAddress = 0x0d0000;
        public const int CharacterAlternateSprites01Address = 0x140000;
        public const int CharacterAlternateSprites02Address = 0x148000;
        public const int CharacterAlternateSprites03Address = 0x150000;
        public const int CharacterAlternateSprites04Address = 0x158000;
        public const int CharacterAlternateSprites05Address = 0x160000;
        public const int CharacterAlternateSprites06Address = 0x168000;
        public const int CharacterAlternateSprites07Address = 0x170000;
        public const int CharacterAlternateSprites08Address = 0x178000;
        public const int CharacterAlternateSprites09Address = 0x180000;
        public const int CharacterAlternateSprites10Address = 0x188000;
        public const int CharacterAlternateSprites11Address = 0x190000;
        public const int CharacterAlternateSprites12Address = 0x198000;

        public const int CharacterSpritesBytes = 0x7000;


        public static IEnumerable<int> CharacterPalettesAddresses
        {
            get
            {
                yield return CharacterPalettesAddress;
                yield return CharacterAlternatePalettes01Address;
                yield return CharacterAlternatePalettes02Address;
                yield return CharacterAlternatePalettes03Address;
                yield return CharacterAlternatePalettes04Address;
                yield return CharacterAlternatePalettes05Address;
                yield return CharacterAlternatePalettes06Address;
                yield return CharacterAlternatePalettes07Address;
                yield return CharacterAlternatePalettes08Address;
                yield return CharacterAlternatePalettes09Address;
                yield return CharacterAlternatePalettes10Address;
                yield return CharacterAlternatePalettes11Address;
                yield return CharacterAlternatePalettes12Address;
            }
        }

        public const int CharacterPalettesAddress            = 0x0e7d00;
        public const int CharacterAlternatePalettes01Address = 0x147d00;
        public const int CharacterAlternatePalettes02Address = 0x14fd00;
        public const int CharacterAlternatePalettes03Address = 0x157d00;
        public const int CharacterAlternatePalettes04Address = 0x15fd00;
        public const int CharacterAlternatePalettes05Address = 0x167d00;
        public const int CharacterAlternatePalettes06Address = 0x16fd00;
        public const int CharacterAlternatePalettes07Address = 0x177d00;
        public const int CharacterAlternatePalettes08Address = 0x17fd00;
        public const int CharacterAlternatePalettes09Address = 0x187d00;
        public const int CharacterAlternatePalettes10Address = 0x18fd00;
        public const int CharacterAlternatePalettes11Address = 0x197d00;
        public const int CharacterAlternatePalettes12Address = 0x19fd00;

        public const int CharacterPalettesBytes = 16 * 16 * 2;

        public const int CharacterPortraitsAddress = 0xed3c0;
        public const int CharacterPortraitsBytes = 0x1500;

        public const int CharacterPortraitsPalettesAddress = 0x686d0;
        public const int CharacterPortraitsPalettesBytes = 16 * 16;

        public const int WeaponSprites = 0xE5900;

        public const int MetadataLengthAddress = 0x1FF000;
        public const int MetadataLengthBytes = 4;
        public const int MetadataAddress = 0x1FF004;
    }

    public static class SystemBusAddresses
    {
        public const int ZeromusDeathAnimation = 0x03F591;
        public const int Congradulations = 0x10963E;
        public const int MenuSaveNewGame = 0x019914;
        public const int MenuLoadSaveGame = 0x0198AD;
    }
}
