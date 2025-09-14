using System;
using FF.Rando.Companion.View;

namespace FF.Rando.Companion.FreeEnterprise;

public interface IKeyItem : IImageTracker
{
    int Id { get; }

    string Name { get; }

    string Description { get; }

    string WhereFound { get; }

    TimeSpan? WhenFound { get; }

    TimeSpan? WhenUsed { get; }

    bool IsFound { get; }

    bool IsUsed { get; }

    bool IsTrackable { get; }
}
