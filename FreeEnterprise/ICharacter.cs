using FF.Rando.Companion.FreeEnterprise.Shared;

namespace FF.Rando.Companion.FreeEnterprise;

public interface ICharacter : IImageTracker
{
    byte Slot { get; }

    CharacterType Type { get; }

    byte Fashion { get; }

    bool IsAnchor { get; }
}
