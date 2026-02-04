using FF.Rando.Companion.Extensions;
using System;
using System.Collections.Generic;

namespace FF.Rando.Companion.Games.WorldsCollide.Tracking;

internal class Characters(Seed seed)
{
    private readonly IReadOnlyList<Character> _values =
    [
        new Character(seed, Enums.Events.TERRA_IN_PARTY),
        new Character(seed, Enums.Events.LOCKE_IN_PARTY),
        new Character(seed, Enums.Events.EDGAR_IN_PARTY),
        new Character(seed, Enums.Events.SABIN_IN_PARTY),
        new Character(seed, Enums.Events.SHADOW_IN_PARTY),
        new Character(seed, Enums.Events.CYAN_IN_PARTY),
        new Character(seed, Enums.Events.GAU_IN_PARTY),
        new Character(seed, Enums.Events.CELES_IN_PARTY),
        new Character(seed, Enums.Events.SETZER_IN_PARTY),
        new Character(seed, Enums.Events.MOG_IN_PARTY),
        new Character(seed, Enums.Events.STRAGO_IN_PARTY),
        new Character(seed, Enums.Events.RELM_IN_PARTY),
        new Character(seed, Enums.Events.GOGO_IN_PARTY),
        new Character(seed, Enums.Events.UMARO_IN_PARTY),
    ];

    internal IReadOnlyList<Character> Values => _values;

    public bool Update(TimeSpan time, ReadOnlySpan<byte> events)
    {
        var updated = false;
        foreach (var check in _values)
        {
            var isFound = events.Read<bool>(check.Id);

            if (isFound != check.IsFound)
            {
                updated = true;
                check.WhenFound = time;
                check.IsFound = isFound;
            }
        }

        return updated;
    }
}