using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace FF.Rando.Companion.Games.WorldsCollide.Tracking;

internal class CheckLocation(string description)
{
    public string Description { get; } = description;

    public Point Location { get; init; }

    public bool ChecksAvailable { get; protected set; }
 
    public IReadOnlyList<ICheck> Checks { get; init; } = [];

    public bool Update(State state)
    {
        var ret = false;

        foreach (var check in Checks)
        {
            ret |= check.Update(state);
        }

        var checksAvailable = Checks.Any(c => c.IsAvailable && !c.IsComplete);
        ret |= ChecksAvailable != checksAvailable;
        ChecksAvailable = checksAvailable;

        return ret;
    }

    public override string ToString() => Description;
}
