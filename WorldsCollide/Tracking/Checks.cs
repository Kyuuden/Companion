using FF.Rando.Companion.Extensions;
using FF.Rando.Companion.WorldsCollide.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using FF.Rando.Companion.WorldsCollide.Enums;

namespace FF.Rando.Companion.WorldsCollide.Tracking;

internal class Checks
{
    private readonly IReadOnlyList<Check> _values;

    internal IReadOnlyList<Check> Values => _values;

    public Checks(Seed seed, Descriptors descriptors, WorldsCollideSettings settings, Func<Events, bool> grouping)
    {
        _values = Enum.GetValues(typeof(Events)).OfType<Events>().Where(grouping).Select(c => new Check(c, seed.Font, seed.Sprites, settings, descriptors)).ToList();
    }

    public bool Update(TimeSpan time, ReadOnlySpan<byte> events)
    {
        var updated = false;
        foreach (var check in _values)
        {
            var isComplete = events.Read<bool>((int)check.Id);

            if (isComplete != check.IsCompleted)
            {
                updated = true;
                check.WhenCompleted = time;
                check.IsCompleted = isComplete;
            }

            if (!check.IsCompleted)
            {
                var req = check.Event.GetRequirements();
                bool isAvailable = true;

                foreach (var e in req)
                {
                    isAvailable &= events.Read<bool>((int)e);
                }

                if (isAvailable != check.IsCompleted)
                {
                    updated = true;
                    check.IsAvailable = isAvailable;
                }
            }
        }

        return updated;
    }
}