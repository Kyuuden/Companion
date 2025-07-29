using System;

namespace FF.Rando.Companion.FreeEnterprise;

public interface IEncounter
{
    string Location { get; }
    bool IsDefeated { get; }
    TimeSpan? WhenDefeated { get; }
}
