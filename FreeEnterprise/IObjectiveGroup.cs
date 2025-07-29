using System.Collections.Generic;

namespace FF.Rando.Companion.FreeEnterprise;

public interface IObjectiveGroup
{
    string Name { get; }
    IEnumerable<ITask> Tasks { get; }
    IEnumerable<IReward> Rewards { get; }
}
