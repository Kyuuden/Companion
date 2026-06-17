using FF.Rando.Companion.Games.FreeEnterprise.RomData;
using System.Collections.Generic;
using System.Linq;

namespace FF.Rando.Companion.Games.FreeEnterprise._5._0._0;

internal class ObjectiveGroup : IObjectiveGroup
{
    private readonly IList<ITask> _tasks;
    private readonly IList<Reward> _rewards;

    public int NumCompleted { get; private set; }

    internal ObjectiveGroup(Seed seed, GroupObjectives groupObjectives, IEnumerable<GroupObjectives> allgroups)
    {
        Name = groupObjectives.Name ?? "";

        _tasks = [];
        foreach (var task in groupObjectives.Tasks ?? [])
        {
            switch (task)
            {
                case BasicTask:
                    _tasks.Add(new Task(seed, task, 1));
                    break;
                case ThresholdTask threshold:
                    _tasks.Add(new Task(seed, task, threshold.Threshold));
                    break;
                case GroupTask groupTask:
                    _tasks.Add(new GroupProgressTask(seed, groupTask, allgroups.ToList()));
                    break;
            }
        }

        _rewards = groupObjectives.Rewards.Select(r => new Reward(r)).ToList();
    }

    public bool Update(ref readonly byte data)
    {
        if (data != NumCompleted)
        {
            NumCompleted = data;
            return true;
        }

        return false;
    }

    public string Name { get; }

    public IEnumerable<ITask> Tasks => _tasks;

    public IEnumerable<IReward> Rewards => _rewards;
}
