using FF.Rando.Companion.Extensions;
using FF.Rando.Companion.Games.FreeEnterprise.RomData;
using FF.Rando.Companion.Games.FreeEnterprise.Settings;
using FF.Rando.Companion.Games.FreeEnterprise.Shared;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FF.Rando.Companion.Games.FreeEnterprise._5._0._0;

internal class KeyItems
{
    private readonly IReadOnlyList<KeyItem> _items;
    private readonly Descriptors _descriptors;

    public int NumFound { get; private set; }
    public int NumUsed { get; private set; }

    internal IReadOnlyList<KeyItem> Items => _items;

    public KeyItems(KeyItemSettings settings, Font font, Descriptors descriptors)
    {
        _descriptors = descriptors;
        _items = Enum.GetValues(typeof(KeyItemType)).OfType<KeyItemType>().Select(t => new KeyItem(settings, font, _descriptors, t)).ToList();
    }

    public bool Update(TimeSpan time, ReadOnlySpan<byte> found, ReadOnlySpan<byte> used, ReadOnlySpan<byte> locations)
    {
        var updated = false;
        var numFound = 0;
        var numUsed = 0;
        foreach (var keyitem in  _items)
        {
            var isfound = found.Read<bool>(keyitem.Id);
            var isUsed = used.Read<bool>(keyitem.Id);

            if (isfound != keyitem.IsFound)
            {
                updated = true;
                var slot = (int)locations.Read<uint>(keyitem.Id * 16, 16);
                var slotDescription = _descriptors.GetRewardSlotDescription(slot);
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

            if (keyitem.IsFound) numFound++;
            if (keyitem.IsUsed) numUsed++;
        }

        NumFound = numFound;
        NumUsed = numUsed;

        return updated;
    }
}
