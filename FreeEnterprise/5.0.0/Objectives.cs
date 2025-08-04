using System;
using System.Collections.Generic;
using System.Linq;

namespace FF.Rando.Companion.FreeEnterprise._5._0._0;
internal class Objectives(Descriptors descriptors, IEnumerable<RomData.GroupObjectives> groups)
{
    private readonly IList<ObjectiveGroup> _groups = groups.Select(g => new ObjectiveGroup(descriptors, g, groups)).ToList();

    public IEnumerable<IObjectiveGroup> Groups => _groups;

    public int NumCompleted { get; private set; }

    public bool Update(TimeSpan time, ReadOnlySpan<byte> taskProgress, ReadOnlySpan<byte> groupProgress)
    {
        var updated = false;
        var offset = 0;

        foreach (var item in _groups.SelectMany(g => g.Tasks).OfType<Task>())
        {
            updated |= item.Update(time, taskProgress.Slice(offset++, 1));
        }

        offset = 0;

        foreach (var item in _groups.OfType<ObjectiveGroup>())
        {
            updated |= item.Update(groupProgress.Slice(offset++, 1));
        }

        if (updated)
            NumCompleted = _groups.Sum(g => g.NumCompleted);

        return updated;
    }
}
