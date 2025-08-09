using FF.Rando.Companion.FreeEnterprise.RomData;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace FF.Rando.Companion.FreeEnterprise._4._6._0;
internal class Objectives : IObjectiveGroup
{
    private readonly List<Task> _tasks = [];
    private readonly List<Reward> _rewards = [];

    public string Name => "Objectives";

    public IEnumerable<ITask> Tasks => _tasks;

    public IEnumerable<IReward> Rewards => _rewards;

    public Objectives(IEnumerable<RomData.Objective> objectives, int? required, bool? winGame, bool? winCrystal)
    {
        foreach (var objective in objectives)
        {
            if (objective is not BasicObjective basicObjective)
                throw new InvalidOperationException("Unsupported objectives");

            _tasks.Add(new Task(basicObjective));
        }

        var reqstring = !required.HasValue
            ? "?"
            : required == -1
                ? "all"
                : required.ToString();

        if (winCrystal == true)
            _rewards.Add(new Reward($"Complete {reqstring} objective{(required != 1 ? "s" : string.Empty)} to win [crystal]Crystal", required));

        if (winGame == true)
            _rewards.Add(new Reward($"Complete {reqstring} objective{(required != 1 ? "s" : string.Empty)} to win the game", required));
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
    private TimeSpan? _completedAt;

    internal Task(BasicObjective basicObjective)
    {
        Description = basicObjective.Description ?? "UNKNOWN OBJECTIVE";
    }

    public bool Update(TimeSpan time, ReadOnlySpan<byte> data)
    {
        var completed = data[0] != 0;
        if (completed != IsCompleted)
        {
            IsCompleted = completed;

            if (IsCompleted)
                CompletedAt = time;

            return true;
        }

        return false;
    }

    public string Description { get; }

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
