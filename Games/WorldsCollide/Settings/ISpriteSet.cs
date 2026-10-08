using FF.Rando.Companion.Games.WorldsCollide.Enums;
using FF.Rando.Companion.Rendering;
using System;

namespace FF.Rando.Companion.Games.WorldsCollide.Settings;

public interface ISpriteSet : IDisposable
{
    ISprite? Get(EventType @event);

    ISprite? Get(Statistic statistic);

    ISprite? Get(DragonType dragons);
}
