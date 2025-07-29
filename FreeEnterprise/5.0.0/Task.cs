using BizHawk.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace FF.Rando.Companion.FreeEnterprise._5._0._0;

internal class Task : ITask
{
    private readonly RomData.Task _task;
    private bool _completed = false;
    private int? _required;
    private int _current = 0;

    private string _baseDescription;
    private TimeSpan? _completedAt;

    internal Task(Descriptors descriptors, RomData.Task task, IEnumerable<RomData.GroupObjectives> groups)
    {
        _task = task;
        _baseDescription = descriptors.GetTaskDescription(task);

        switch (task)
        {
            case RomData.BasicTask:
                _required = 1;
                break;
            case RomData.ThresholdTask threshold:
                _required = threshold.Threshold;
                break;
            case RomData.GroupTask groupTask:
                if (int.TryParse(groupTask.Req, out int required))
                {
                    _required = required;
                }
                else
                {
                    _required = groups.First(g => g.Key == groupTask.Group).Tasks.Count();
                }

                break;
        }
    }

    public bool Update(TimeSpan time, ReadOnlySpan<byte> data)
    {
        var status = data[0];
        if (status != _current)
        {
            _current = status;
            IsCompleted = _current >= _required;

            if (IsCompleted)
                CompletedAt = time;

            return true;
        }

        return false;
    }

    public string Description 
        => _required switch
        {
            1 => _baseDescription,
            null => _baseDescription,
            _ => $"{_baseDescription} ({Math.Min(_current,_required.Value)}/{_required})"
        };

    public TimeSpan? CompletedAt
    {
        get => _completedAt;
        private set
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
        private set
        {
            if (value == _completed)
                return;

            _completed = value;
            NotifyPropertyChanged();
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
