using BizHawk.FreeEnterprise.Companion.Database;
using BizHawk.FreeEnterprise.Companion.Extensions;
using System;

namespace BizHawk.FreeEnterprise.Companion.State
{
    public class KeyItem
    {
        public KeyItemType Key { get; }
        public string Name { get; }
        public string ShortName { get; }
        public string Description { get; }
        public bool Found { get; }
        public TimeSpan? WhenFound { get; }
        public bool Used { get; }
        public TimeSpan? WhenUsed { get; }
        public KeyItemLocationType WhereFound { get; }

        public KeyItem(KeyItemType key, bool isFound, bool isUsed, PersistentStorage storage, KeyItemLocationType[] parsedFoundItemLocations)
        {
            Key = key;
            Name = TextLookup.GetName(key) ?? key.ToString();
            ShortName = TextLookup.GetShortName(key) ?? key.ToString();
            Description = TextLookup.GetDescription(key) ?? key.ToString();
            Found = isFound;
            WhenFound = storage.KeyItemFoundTimes[key];
            Used = isUsed;
            WhenUsed = storage.KeyItemUsedTimes[key];
            WhereFound = parsedFoundItemLocations[key.ToIndex()];
        }
    }
}
