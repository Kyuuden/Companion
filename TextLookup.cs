using BizHawk.FreeEnterprise.Companion.Extensions;

namespace BizHawk.FreeEnterprise.Companion
{
    public static class TextLookup
    {
        private static Names? _names;

        public static void Initialize(Names names)
        {
            _names = names;
        }

        public static string? GetName(KeyItemType key)
        {
            var index = key.ToIndex();
            return _names != null && _names.KeyItemNames != null && index < _names.KeyItemNames.Count ? _names.KeyItemNames[index] : null;
        }

        public static string? GetShortName(KeyItemType key)
        {
            var index = key.ToIndex();
            return _names != null && _names.KeyItemShortNames != null && index < _names.KeyItemShortNames.Count ? _names.KeyItemShortNames[index] : null;
        }

        public static string? GetDescription(KeyItemType key)
        {
            var index = key.ToIndex();
            return _names != null && _names.KeyItemDescriptions != null && index < _names.KeyItemDescriptions.Count ? _names.KeyItemDescriptions[index] : null;
        }

        public static string? GetName(KeyItemLocationType key)
        {
            int index = (int)key - (int)KeyItemLocationType.StartingItem;
            return _names != null && _names.LocationNames != null && index >= 0 && index < _names.LocationNames.Count ? _names.LocationNames[index] : null;
        }

        public static string? GetName(CharacterLocationType key)
        {
            int index = (int)key - (int)CharacterLocationType.StartingCharacter;
            return _names != null && _names.CharacterLocationNames != null && index >= 0 && index < _names.CharacterLocationNames.Count ? _names.CharacterLocationNames[index] : null;
        }
    }
}
