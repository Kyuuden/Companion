using FF.Rando.Companion.Extensions;
using FF.Rando.Companion.Games.WorldsCollide;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FF.Rando.Companion.Games.WorldsCollide.Tracking;

internal class Dragons(Seed seed)
{
    private readonly IReadOnlyList<Dragon> _values = Enum.GetValues(typeof(Enums.Dragons))
        .OfType<Enums.Dragons>()
        .Distinct()
        .Select(c => new Dragon(seed, c))
        .ToList();

    internal IReadOnlyList<Dragon> Values => _values;

    public bool Update(TimeSpan time, ReadOnlySpan<byte> events)
    {
        var updated = false;
        foreach (var check in _values)
        {
            var isDefeated = events.Read<bool>(check.Id);

            if (isDefeated != check.IsDefeated)
            {
                updated = true;
                check.WhenDefeated = time;
                check.IsDefeated = isDefeated;
            }
        }

        return updated;
    }
}