using BizHawk.Common;
using FF.Rando.Companion.Games.FreeEnterprise.RomData;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace FF.Rando.Companion.Games.FreeEnterprise._5._0._0;

internal abstract class TaskBase : ITask
{
    private bool _completed = false;
    private readonly string _baseDescription;
    private readonly Seed _seed;
    private TimeSpan? _completedAt;

    protected TaskBase(Seed seed, RomData.Task task)
    {
        _seed = seed;
        _baseDescription = _seed.Descriptors.GetTaskDescription(task);
    }

    protected int Current { get; set; }
    protected int? Required { get; init; }

    public string Description
        => Required switch
        {
            1 => _baseDescription,
            null => _baseDescription,
            _ => $"{_baseDescription} ({Math.Min(Current, Required.Value)}/{Required})"
        };

    public TimeSpan? CompletedAt
    {
        get => _completedAt;
        protected set
        {
            if (value == _completedAt || _completedAt.HasValue)
                return;

            _completedAt = value;
            NotifyPropertyChanged();
        }
    }

    public bool IsCompleted
    {
        get => _completed;
        protected set
        {
            if (value == _completed)
                return;

            _completed = value;
            NotifyPropertyChanged();

            if (IsCompleted && !CompletedAt.HasValue)
                CompletedAt = _seed.Container.Timer.Elapsed;
        }
    }

    public bool IsHardRequired => false;

    public event PropertyChangedEventHandler? PropertyChanged;

    protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}

internal class Task : TaskBase
{
    internal Task(Seed seed, RomData.Task task, int? required)
        :base(seed, task)
    {
        Required = required;
    }

    public bool Update(ref readonly byte data)
    {
        var status = data;
        if (status != Current)
        {
            Current = status;
            IsCompleted = Current >= Required;
            return true;
        }

        return false;
    }
}

internal class GroupProgressTask : TaskBase
{
    private readonly int _groupIndex;

    internal GroupProgressTask(Seed seed, GroupTask task, IList<GroupObjectives> groups)
        : base(seed, task)
    {
        for (int i = 0; i < groups.Count; i++)
        {
            if (groups[i].Key == task.Group)
            {
                _groupIndex = i;
                break;
            }
        }

        Required = int.TryParse(task.Req, out var req) ? req : groups[_groupIndex].Tasks.Count();
    }

    public bool Update(ReadOnlySpan<byte> groupProgress)
    {
        var status = groupProgress[_groupIndex];
        if (status != Current)
        {
            Current = status;
            IsCompleted = Current >= Required;
            return true;
        }

        return false;
    }
}