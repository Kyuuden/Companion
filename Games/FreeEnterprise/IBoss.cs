using FF.Rando.Companion.View;
using System.Collections.Generic;

namespace FF.Rando.Companion.Games.FreeEnterprise;

public interface IBoss : IImageTracker
{
    int Id { get; }
    IEnumerable<IEncounter> Encounters { get; }
}
