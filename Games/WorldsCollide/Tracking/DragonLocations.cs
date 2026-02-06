using FF.Rando.Companion.Extensions;
using FF.Rando.Companion.Games.WorldsCollide.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FF.Rando.Companion.Games.WorldsCollide.Tracking;

internal class DragonLocations(Seed seed)
{
    private readonly IReadOnlyList<DragonLocation> _values =
    [
        new DragonLocation(seed, Events.DEFEATED_PHOENIX_CAVE_DRAGON),
        new DragonLocation(seed, Events.DEFEATED_ANCIENT_CASTLE_DRAGON),
        new DragonLocation(seed, Events.DEFEATED_MT_ZOZO_DRAGON),
        new DragonLocation(seed, Events.DEFEATED_OPERA_HOUSE_DRAGON),
        new DragonLocation(seed, Events.DEFEATED_FANATICS_TOWER_DRAGON),
        new DragonLocation(seed, Events.DEFEATED_NARSHE_DRAGON),
        new DragonLocation(seed, Events.DEFEATED_KEFKA_TOWER_DRAGON_G),
        new DragonLocation(seed, Events.DEFEATED_KEFKA_TOWER_DRAGON_S),
    ];

    internal IReadOnlyList<DragonLocation> Values => _values;


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
                bool isAvailable = check.Event.IsAvailable(events);
                if (isAvailable != check.IsAvailable)
                {
                    updated = true;
                    check.IsAvailable = isAvailable;
                }
            }
        }

        return updated;
    }
}