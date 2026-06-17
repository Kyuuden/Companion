using System;
using System.Collections.Generic;
using System.Linq;
using FF.Rando.Companion.Games.FreeEnterprise.RomData;

namespace FF.Rando.Companion.Games.FreeEnterprise._5._0._0;
internal class Objectives
{
    private readonly IList<ObjectiveGroup> _groups;
    private readonly IList<Task> _tasks;
    private readonly IList<GroupProgressTask> _groupProgressTasks;

    public Objectives(Seed seed, IEnumerable<GroupObjectives> groups)
    {
        _groups = groups.Select(g => new ObjectiveGroup(seed, g, groups)).ToList();
        _tasks = _groups.SelectMany(g => g.Tasks).OfType<Task>().ToList();
        _groupProgressTasks = _groups.SelectMany(g => g.Tasks).OfType<GroupProgressTask>().ToList();
    }

    public IEnumerable<IObjectiveGroup> Groups => _groups;

    public int NumCompleted { get; private set; }

    public bool Update(ReadOnlySpan<byte> taskProgress, ReadOnlySpan<byte> groupProgress)
    {
        var updated = false;

        for (var i = 0; i < _tasks.Count; i++)
            updated |= _tasks[i].Update(in taskProgress[i]);

        foreach (var gTask in _groupProgressTasks)
            updated |= gTask.Update(groupProgress);

        for (var i = 0; i < _groups.Count; i++)
            updated |= _groups[i].Update(in  groupProgress[i]);

        if (updated)
            NumCompleted = _groups.Sum(g => g.NumCompleted);

        return updated;
    }
}
