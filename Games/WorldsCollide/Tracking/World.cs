using FF.Rando.Companion.Extensions;
using FF.Rando.Companion.Games.WorldsCollide.Enums;
using FF.Rando.Companion.Rendering;
using System;
using System.Collections.Generic;

namespace FF.Rando.Companion.Games.WorldsCollide.Tracking;
internal class World(WorldMapType type)
{
    public WorldMapType Type { get; } = type;
    public string Description { get; } = type.GetDescription();

    public required ISprite Map { get; init; }

    public IReadOnlyList<CheckLocation> Locations { get; init; } = [];

    public event EventHandler? Updated;

    public bool Update(State state)
    {
        var ret = false;
        foreach (var check in Locations)
        {
            ret |= check.Update(state);
        }

        if (ret)
            Updated?.Invoke(this, null);

        return ret;
    }
}
