using FF.Rando.Companion.Extensions;
using System;
using System.Collections.Generic;

namespace FF.Rando.Companion.Games.MysticQuestRandomizer.Tracking;

internal class Spells(Seed seed)
{
    private readonly IReadOnlyList<Spell> _items =
        [
            new Spell(seed, SpellType.Exit),
            new Spell(seed, SpellType.Cure),
            new Spell(seed, SpellType.Heal),
            new Spell(seed, SpellType.Life),
            new Spell(seed, SpellType.Quake),
            new Spell(seed, SpellType.Blizzard),
            new Spell(seed, SpellType.Fire),
            new Spell(seed, SpellType.Aero),
            new Spell(seed, SpellType.Thunder),
            new Spell(seed, SpellType.White),
            new Spell(seed, SpellType.Meteor),
            new Spell(seed, SpellType.Flare),
        ];

    public IReadOnlyList<Spell> Items => _items;

    public bool Update(ReadOnlySpan<byte> found)
    {
        var updated = false;

        foreach (var keyitem in _items)
        {
            var isfound = found.Read<bool>(keyitem.Id);
            if (isfound != keyitem.IsFound)
            {
                updated = true;
                keyitem.IsFound = isfound;
            }
        }

        return updated;
    }

}
