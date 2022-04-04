using BizHawk.FreeEnterprise.Companion.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BizHawk.FreeEnterprise.Companion.State
{
    public class KeyItems
    {
        private Dictionary<KeyItemType, KeyItem> _items;
        public IReadOnlyDictionary<KeyItemType, KeyItem> Items => _items;

        public bool Update(TimeSpan now, KeyItemType foundKeyItems, KeyItemType usedKeyItems, byte[] itemLocations)
        {
            var currentFound = Items.Values.Where(i => i.Found).Select(i => i.Key).Aggregate((KeyItemType)0, (l, r) => l | r);
            var currentUsed = Items.Values.Where(i => i.Used).Select(i => i.Key).Aggregate((KeyItemType)0, (l, r) => l | r);

            if (currentFound != foundKeyItems || currentUsed != usedKeyItems)
            {                
                var parsedFoundItemLocations = itemLocations.ReadMany<KeyItemLocationType>(0, 16, 17).ToArray();

                foreach (var item in Items.Values)
                {
                    if (!currentFound.HasFlag(item.Key) && foundKeyItems.HasFlag(item.Key))
                    {
                        var location = parsedFoundItemLocations[(uint)MathExt.FloorLog2((ulong)item.Key)];
                        item.Find(now, location);
                    }
                    else if (currentFound.HasFlag(item.Key) && !foundKeyItems.HasFlag(item.Key))
                        item.ResetFound();

                    if (!currentUsed.HasFlag(item.Key) && usedKeyItems.HasFlag(item.Key))
                        item.Use(now);
                    else if (currentUsed.HasFlag(item.Key) && !usedKeyItems.HasFlag(item.Key))
                        item.ResetUsed();
                }

                return true;
            }

            return false;
        }
    

        public KeyItems()
        {
            _items = new Dictionary<KeyItemType, KeyItem>();
            foreach (KeyItemType keyitem in Enum.GetValues(typeof(KeyItemType)))
                _items.Add(keyitem, new KeyItem(keyitem));
        }

        public KeyItem this[KeyItemType itemType] => Items[itemType];
    }
}
