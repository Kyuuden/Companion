using System;
using System.Collections.Generic;
using System.Linq;
using FF.Rando.Companion.Games.FreeEnterprise;
using FF.Rando.Companion.Games.FreeEnterprise.RomData;

namespace FF.Rando.Companion.Games.FreeEnterprise._5._0._0;

internal class ObjectiveGroup : IObjectiveGroup
{
    private readonly IList<Task> _tasks;
    private readonly IList<Reward> _rewards;

    public int NumCompleted { get; private set; }

    internal ObjectiveGroup(Descriptors descriptors, GroupObjectives groupObjectives, IEnumerable<GroupObjectives> allgroups)
    {
        Name = groupObjectives.Name ?? "";
        _tasks = groupObjectives.Tasks.Select(t => new Task(descriptors, t, allgroups)).ToList();
        _rewards = groupObjectives.Rewards.Select(r => new Reward(r)).ToList();
    }

    public bool Update(ReadOnlySpan<byte> data)
    {
        if (data[0] != NumCompleted)
        {
            NumCompleted = data[0];
            return true;
        }

        return false;
    }

    public string Name { get; }

    public IEnumerable<ITask> Tasks => _tasks;

    public IEnumerable<IReward> Rewards => _rewards;
}
