using System;

namespace FF.Rando.Companion.Games.WorldsCollide.Enums;

[Flags]
public enum RewardType
{
    None = 0,
    Character = 1,
    Esper = 2,
    Item = 4,

    CharacterOrItem = Character | Item,
    CharacterOEsper = Character | Esper,
    EsperOrItem = Item | Esper,
    Any = Character | Esper | Item,
}
