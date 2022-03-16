namespace BizHawk.FreeEnterprise.Companion
{
    public static class DescriptionLookup
    {
        private static Names? _names;

        public static void Initialize(Names names)
        {
            _names = names;
        }

        public static string? GetDescription(KeyItemType key)
        {
            int index = (int)key - (int)KeyItemType.Package;
            return _names != null && _names.KeyItemNames != null && index < _names.KeyItemNames.Count ? _names.KeyItemNames[index] : null;
        }

        public static string? GetDescription(KeyItemLocationType key)
        {
            int index = (int)key - (int)KeyItemLocationType.StartingItem;
            return _names != null && _names.LocationNames != null && index < _names.LocationNames.Count ? _names.LocationNames[index] : null;
        }
    }
}
