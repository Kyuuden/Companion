using FF.Rando.Companion.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FF.Rando.Companion.MysticQuestRandomizer;

internal abstract class Equipments<T, TEnum>(List<T> items) where T : Equipment<TEnum> where TEnum : struct
{
    internal IReadOnlyList<T> Items => items;

    public bool Update(TimeSpan time, ReadOnlySpan<byte> found)
    {
        var updated = false;

        HashSet<TEnum> foundTypes = [];

        foreach (var enumValue in Enum.GetValues(typeof(TEnum)).OfType<TEnum>())
        {
            if (found.Read<bool>(Convert.ToByte(enumValue)))
                foundTypes.Add(enumValue);
        }

        foreach (var item in items)
        {
            var desired = item.Desired.Intersect(foundTypes);

            if (!desired.SetEquals(item.Found))
            {
                item.Found = desired;
                updated = true;
            }
        }

        return updated;
    }
}