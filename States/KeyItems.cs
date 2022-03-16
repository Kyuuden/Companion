using BizHawk.FreeEnterprise.Companion.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BizHawk.FreeEnterprise.Companion.State
{
    public class KeyItems
    {
        public IReadOnlyDictionary<KeyItemType, KeyItem> Items { get; }

        public KeyItems(KeyItemType foundKeyItems, KeyItemType usedKeyItems, byte[] itemLocations)
        {
            var keyItems = new Dictionary<KeyItemType, KeyItem>();
            foreach (KeyItemType keyitem in Enum.GetValues(typeof(KeyItemType)))
                keyItems.Add(keyitem, new KeyItem(keyitem));

            var parsedFoundItemLocations = itemLocations.ReadMany<KeyItemLocationType>(0, 16, 17).ToArray();
            if (parsedFoundItemLocations.All(l => Enum.IsDefined(typeof(KeyItemLocationType), l) || (uint)l == 0))
            {
                foreach (var found in foundKeyItems.GetFlags())
                {
                    var location = parsedFoundItemLocations[(uint)MathExt.FloorLog2((ulong)found)];
                    keyItems[found].FoundAt(location);
                }

                foreach (var used in usedKeyItems.GetFlags())
                    keyItems[used].Use();
            }

            Items = keyItems;
        }
    

        public KeyItems()
        {
            Items = new Dictionary<KeyItemType, KeyItem>();
        }

        public KeyItem this[KeyItemType itemType] => Items[itemType];

        public override bool Equals(object obj) => obj is KeyItems items && Items.Values.SequenceEqual(items.Items.Values);

        public override int GetHashCode()
        {
            return -604923257 + EqualityComparer<IReadOnlyDictionary<KeyItemType, KeyItem>>.Default.GetHashCode(Items);
        }
    }
}
