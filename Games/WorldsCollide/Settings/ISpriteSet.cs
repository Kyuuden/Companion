using FF.Rando.Companion.Games.WorldsCollide.Enums;
using FF.Rando.Companion.Rendering;
using System;
using System.Collections.Generic;

namespace FF.Rando.Companion.Games.WorldsCollide.Settings;

public interface ISpriteSet : IDisposable
{
    ISprite? Get(Events @event);

    ISprite? Get(Statistic statistic);

    ISprite? Get(Dragons dragons);

    IEnumerable<IEnumerable<Events>> RelatedEvents { get; }
}
