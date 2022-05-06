using BizHawk.FreeEnterprise.Companion.Database;
using BizHawk.FreeEnterprise.Companion.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BizHawk.FreeEnterprise.Companion.State
{
    public class KeyItems
    {
        private KeyItemType found;
        private KeyItemType used;
        byte[] foundLocations;
        private readonly PersistentStorage storage;

        public bool Update(TimeSpan? now, KeyItemType foundKeyItems, KeyItemType usedKeyItems, byte[] itemLocations)
        {
            if (foundKeyItems == found &&
                usedKeyItems == used &&
                itemLocations.SequenceEqual(foundLocations))
                return false;

            found = foundKeyItems;
            used = usedKeyItems;
            itemLocations.CopyTo(foundLocations, 0);

            foreach (var item in found.GetFlags())
                storage.KeyItemFoundTimes[item] = now;

            foreach (var item in used.GetFlags())
                storage.KeyItemUsedTimes[item] = now;

            return true;
        }    

        public KeyItems(PersistentStorage storage)
        {
            this.storage = storage;
            foundLocations = new byte[CARTRAMAddresses.KeyItemLocationsBytes];
        }

        public Dictionary<KeyItemType, KeyItem> Items
        {
            get
            {
                var parsedFoundItemLocations = foundLocations.ReadMany<KeyItemLocationType>(0, 16, 17).ToArray();
                return Enum.GetValues(typeof(KeyItemType))
                    .OfType<KeyItemType>()
                    .ToDictionary(
                        key => key,
                        key => new KeyItem(key, found.HasFlag(key), used.HasFlag(key), storage, parsedFoundItemLocations));
            }
        }

        public bool IsFound(KeyItemType keyItem)
            => found.HasFlag(keyItem);

        public KeyItemType? ItemFoundAt(KeyItemLocationType location)
            => Items.Values.FirstOrDefault(item => item.Found && item.WhereFound == location)?.Key;

        public KeyItems Clone()
        {
            var clone = new KeyItems(storage);
            clone.Update(null, found, used, foundLocations);
            return clone;
        }
    }
}
