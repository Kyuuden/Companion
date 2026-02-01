using FF.Rando.Companion.Games.FreeEnterprise;
using FF.Rando.Companion.Games.FreeEnterprise.RomData;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace FF.Rando.Companion.Games.FreeEnterprise.GaleswiftFork;

internal class Objectives : IObjectiveGroup
{
    private readonly List<Task> _tasks = [];
    private readonly List<Reward> _rewards = [];

    public string Name => "Objectives";

    public IEnumerable<ITask> Tasks => _tasks;

    public IEnumerable<IReward> Rewards => _rewards;

    public Objectives(IEnumerable<Objective> objectives, IFlags? flags)
    {
        if (flags == null)
        {
            foreach (var objective in objectives)
            {
                if (objective is not BasicObjective basicObjective)
                    throw new InvalidOperationException("Unsupported objectives");

                _tasks.Add(new Task(basicObjective));
            }
        }
        else
        {
            int hardreqCnt = 0;
            byte objNum = 0;
            foreach (var objective in objectives)
            {
                if (objective is not BasicObjective basicObjective)
                    throw new InvalidOperationException("Unsupported objectives");

                var hardReq = flags.IsHardRequired(objNum++);
                hardreqCnt += hardReq ? 1 : 0;
                _tasks.Add(new Task($"{basicObjective.Description}{(hardReq ? " [crystal]" : "")}", hardReq));
            }

            if (flags.NumRequiredObjectives == -1)
            {
                if (flags.OWinCrystal)
                    _rewards.Add(new Reward($"Complete all objectives to win [crystal]Crystal", -1));
                else if (flags.OWinGame)
                    _rewards.Add(new Reward($"Complete all objectives to win the game", -1));
            }
            else
            {
                var rewardString = $"Complete {flags.NumRequiredObjectives} objective{(flags.NumRequiredObjectives != 1 ? "s" : string.Empty)}";
                if (hardreqCnt > 0)
                    rewardString += $" ({hardreqCnt} hard required)";

                if (flags.OWinCrystal)
                    rewardString += " to win [crystal]Crystal";
                else if (flags.OWinGame)
                    rewardString += " to win the game";

                _rewards.Add(new Reward(rewardString, flags.NumRequiredObjectives));
            }
        }
    }

    public bool Update(TimeSpan time, ReadOnlySpan<byte> taskProgress)
    {
        var updated = false;
        var offset = 0;

        foreach (var item in _tasks)
        {
            updated |= item.Update(time, taskProgress.Slice(offset++, 1));
        }

        return updated;
    }
}

internal class Task : ITask
{
    private bool _completed = false;
    private readonly int? _required;
    private int _current = 0;
    private readonly string _baseDescription;
    private TimeSpan? _completedAt;

    internal Task(BasicObjective basicObjective)
        : this(basicObjective.Description ?? "UNKNOWN OBJECTIVE", false)
    {
    }

    internal Task(string description, bool isHardRequired)
    {
        _baseDescription = description;
        IsHardRequired = isHardRequired;
        _required = 1;

        var match = Regex.Match(description, "Defeat (\\d*) bosses");
        if (match.Success && int.TryParse(match.Groups[1].Value, out var bossCount))
        {
            _required = bossCount;
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
            _ => $"{_baseDescription} ({Math.Min(_current, _required.Value)}/{_required})"
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

    public bool IsHardRequired { get; }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}

internal class Reward : IReward
{
    internal Reward(string? description, int? required)
    {
        Description = description ?? "Unknown";
        Required = required;
    }

    public string Description { get; }

    public int? Required { get; }
}
