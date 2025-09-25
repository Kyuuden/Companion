using FF.Rando.Companion.Extensions;
using FF.Rando.Companion.FreeEnterprise.RomData;
using FF.Rando.Companion.FreeEnterprise.Settings;
using FF.Rando.Companion.FreeEnterprise.Shared;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FF.Rando.Companion.FreeEnterprise._4._6._0;
internal class KeyItems(KeyItemSettings settings, Font font, Descriptors descriptors)
{
    private readonly IReadOnlyList<KeyItem> _items = 
        Enum.GetValues(typeof(KeyItemType))
        .OfType<KeyItemType>()
        .Select(t => new KeyItem(settings, font, descriptors, t, t != KeyItemType.Pass))
        .ToList();

    public int NumFound { get; private set; }
    public int NumUsed { get; private set; }

    internal IReadOnlyList<KeyItem> Items => _items;

    public bool Update(TimeSpan time, ReadOnlySpan<byte> found, ReadOnlySpan<byte> used, ReadOnlySpan<byte> locations, ReadOnlySpan<byte> inventory)
    {
        var updated = false;
        var numFound = 0;
        var numUsed = 0;
        foreach (var keyitem in _items)
        {
            if (keyitem.Id != (int)KeyItemType.Pass)
            {
                var isfound = found.Read<bool>(keyitem.Id);
                var isUsed = used.Read<bool>(keyitem.Id);

                if (isfound != keyitem.IsFound)
                {
                    updated = true;
                    var slot = (LocationType)locations.Read<uint>(keyitem.Id * 16, 16);
                    var slotDescription = descriptors.GetLocationName(slot);
                    keyitem.WhenFound = time;
                    keyitem.WhereFound = slotDescription;
                    keyitem.IsFound = isfound;
                }

                if (isUsed != keyitem.IsUsed)
                {
                    updated = true;
                    keyitem.IsUsed = isUsed;
                    keyitem.WhenUsed = time;
                }
            }
            else
            {
                bool isFound = false;
                for (var i = 0; i < inventory.Length; i += 2)
                {
                    isFound |= (inventory[i] == 0xEC);
                    if (isFound) break;
                }

                if (isFound != keyitem.IsFound)
                {
                    updated = true;
                    keyitem.WhenFound = time;
                    keyitem.WhereFound = "In your inventory.";
                    keyitem.IsFound = isFound;
                }
            }

            if (keyitem.IsFound) numFound++;
            if (keyitem.IsUsed) numUsed++;
        }

        NumFound = numFound;
        NumUsed = numUsed;

        return updated;
    }

}
