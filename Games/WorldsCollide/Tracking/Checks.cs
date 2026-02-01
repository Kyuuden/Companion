using FF.Rando.Companion.Extensions;
using FF.Rando.Companion.Games.WorldsCollide;
using FF.Rando.Companion.Games.WorldsCollide.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FF.Rando.Companion.Games.WorldsCollide.Tracking;

internal class Checks
{
    private readonly IReadOnlyList<Check> _values;
    private readonly IDictionary<Events, Check> _checkLookup;
    private readonly Seed _seed;

    public Checks(Seed seed, Func<Events, bool> grouping)
    {
        _seed = seed;
        _values = Enum.GetValues(typeof(Events))
        .OfType<Events>()
        .Where(grouping)
        .Distinct()
        .OrderBy(c => (uint)c).Select(c => new Check(seed, c))
        .ToList();

        _checkLookup = _values.ToDictionary(c => c.Event, c => c);
    }

    internal IReadOnlyList<Check> Values => _values;

    public bool UpdateRelatedChecks(bool resetVisibilities = false)
    {
        var updated = false;
        if (resetVisibilities)
        {
            foreach (var check in _values)
            {
                updated |= !check.IsVisible;
                check.IsVisible = true;
            }
        }
        
        foreach (var group in _seed.SpriteSet.RelatedEvents.Select(g=>g.ToList()))
        {
            var foundActive = false;
            foreach (var check in (group as IEnumerable<Events>).Reverse())
            {
                if (!_checkLookup.TryGetValue(check, out var trackedCheck))
                    continue;

                if (!foundActive)
                {
                    if (trackedCheck.IsCompleted || group.First() == check)
                    {
                        foundActive = true;
                        if (!trackedCheck.IsVisible)
                            updated = true;
                        trackedCheck.IsVisible = true;
                    }
                    else
                    {
                        if (trackedCheck.IsVisible)
                            updated = true;
                        trackedCheck.IsVisible = false;
                    }
                }
                else
                {
                    if (trackedCheck.IsVisible)
                        updated = true;

                    trackedCheck.IsVisible = false;
                }
            }
        }

        return updated;
    }

    public bool Update(TimeSpan time, ReadOnlySpan<byte> events)
    {
        var updated = false;
        foreach (var check in _values)
        {
            var isComplete = events.Read<bool>(check.Id);

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

        if (updated)
            UpdateRelatedChecks();

        return updated;
    }
}