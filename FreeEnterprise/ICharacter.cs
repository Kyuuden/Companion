using FF.Rando.Companion.FreeEnterprise.Shared;
using FF.Rando.Companion.View;

namespace FF.Rando.Companion.FreeEnterprise;

public interface ICharacter : IImageTracker
{
    byte Slot { get; }

    CharacterType Type { get; }

    byte Fashion { get; }

    bool IsAnchor { get; }
}
