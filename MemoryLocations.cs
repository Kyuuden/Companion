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

        public const int zeromus_death_animation = 0x02F591;
        public const int zeromus_flash = 0x02F609;
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

        public const int CharacterSpritesAddress = 0x0d0000;
        public const int CharacterSpritesBytes = 0x7000;

        public const int CharacterPalettesAddress = 0x0e7d00;
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

        public const int MetadataLengthAddress = 0x1FF000;
        public const int MetadataLengthBytes = 4;
        public const int MetadataAddress = 0x1FF004;
    }
}
