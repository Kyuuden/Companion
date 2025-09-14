using System.Collections.Generic;
using FF.Rando.Companion.View;

namespace FF.Rando.Companion.FreeEnterprise;

public interface IBoss : IImageTracker
{
    int Id { get; }
    string Name { get; }
    IEnumerable<IEncounter> Encounters { get; }
}
