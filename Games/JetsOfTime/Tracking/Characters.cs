using FF.Rando.Companion.Games.JetsOfTime.Rendering;
using FF.Rando.Companion.Timing;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FF.Rando.Companion.Games.JetsOfTime.Tracking;

internal class Characters(ITimer timer, Sprites spriteDB)
{
    private readonly IReadOnlyList<Character> _values =
    [
        new Character(CharacterType.Chrono, timer, spriteDB.GetPortrait(PortraitType.Chrono)),
        new Character(CharacterType.Marle, timer, spriteDB.GetPortrait(PortraitType.Marle)),
        new Character(CharacterType.Lucca, timer, spriteDB.GetPortrait(PortraitType.Lucca)),
        new Character(CharacterType.Robo, timer, spriteDB.GetPortrait(PortraitType.Robo)),
        new Character(CharacterType.Frog, timer, spriteDB.GetPortrait(PortraitType.Frog)),
        new Character(CharacterType.Ayla, timer, spriteDB.GetPortrait(PortraitType.Ayla)),
        new Character(CharacterType.Magus, timer, spriteDB.GetPortrait(PortraitType.Magus)),
    ];

    internal IReadOnlyList<Character> Values => _values;

    public bool IsFound(CharacterType characterType) => Values.FirstOrDefault(c=> c.Type == characterType).IsFound;

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
