using BizHawk.Common;
using FF.Rando.Companion.Games.FreeEnterprise;
using FF.Rando.Companion.Games.FreeEnterprise.RomData;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace FF.Rando.Companion.Games.FreeEnterprise._5._0._0;

internal class Task : ITask
{
    private bool _completed = false;
    private readonly int? _required;
    private int _current = 0;

    private readonly string _baseDescription;
    private TimeSpan? _completedAt;

    internal Task(Descriptors descriptors, Games.FreeEnterprise.RomData.Task task, IEnumerable<GroupObjectives> groups)
    {
        _baseDescription = descriptors.GetTaskDescription(task);

        switch (task)
        {
            case BasicTask:
                _required = 1;
                break;
            case ThresholdTask threshold:
                _required = threshold.Threshold;
                break;
            case GroupTask groupTask:
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

    public bool IsHardRequired => false;

    public event PropertyChangedEventHandler? PropertyChanged;

    protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
