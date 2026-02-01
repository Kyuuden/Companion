using FF.Rando.Companion.Games.FreeEnterprise.Shared;
using FF.Rando.Companion.View;

namespace FF.Rando.Companion.Games.FreeEnterprise;

public interface ICharacter : IImageTracker
{
    byte Slot { get; }

    CharacterType Type { get; }

    byte Fashion { get; }

    bool IsAnchor { get; }
}
