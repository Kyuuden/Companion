using System;
using System.Collections.Generic;

namespace FF.Rando.Companion.Games.JetsOfTime.Tracking;

internal class Characters(Seed seed)
{
    private readonly IReadOnlyList<Character> _values =
    [
        new Character(seed, CharacterType.Chrono),
        new Character(seed, CharacterType.Marle),
        new Character(seed, CharacterType.Lucca),
        new Character(seed, CharacterType.Robo),
        new Character(seed, CharacterType.Frog),
        new Character(seed, CharacterType.Ayla),
        new Character(seed, CharacterType.Magus),
    ];

    internal IReadOnlyList<Character> Values => _values;

    public bool Update(ReadOnlySpan<byte> partyData)
    {
        var updated = false;

        foreach (var character in _values)
        {
            var isFound = partyData.IndexOf(character.Id) != -1;

            if (isFound != character.IsFound)
            {
                updated = true;
                character.IsFound = isFound;
            }
        }

        return updated;
    }
}
