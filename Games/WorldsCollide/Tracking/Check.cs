using FF.Rando.Companion.Extensions;
using FF.Rando.Companion.Games.WorldsCollide.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FF.Rando.Companion.Games.WorldsCollide.Tracking;

internal interface ICheck
{
    string Description { get; }

    bool IsAvailable { get; }

    bool IsComplete { get; }

    bool Update(State state);

    public event Action? Updated;
}

internal interface IEventCheck
{
    public EventType Event { get; }
}

internal interface ICharacterGate
{
    public EventType CharacterGate { get; }
}

internal interface IRewardable
{
    public RewardType RewardType { get; }
}

internal class BasicCheck(EventType trackedEvent, RewardType rewardType) : ICheck, IEventCheck, IRewardable
{
    public virtual string Description { get; } = trackedEvent.GetDescription();
    public virtual bool IsAvailable => true;
    public bool IsComplete { get; private set; }
    public EventType Event { get; } = trackedEvent;

    public RewardType RewardType { get; } = rewardType;

    public event Action? Updated;

    public virtual bool Update(State state)
    {
        var ret = false;
        var isComplete = state.Events[Event];
        ret |= IsComplete != isComplete;
        IsComplete = isComplete;
        
        if (ret) Updated?.Invoke();
        return ret;
    }

    public override string ToString() => $"{Description} ({string.Join(" / ", RewardType.GetFlags(false).Select(f=>f.ToString()))})";
}

internal class CharacterCheck(EventType character) : BasicCheck(character.IsCharacter() ? character : throw new ArgumentException(), RewardType.None)
{
}

internal class GatedCheck(EventType trackedEvent, EventType gatedCharacter, RewardType rewardType) : BasicCheck(trackedEvent, rewardType), ICharacterGate
{
    private bool _isAvailable;

    public override bool IsAvailable => _isAvailable;

    public EventType CharacterGate { get; } = gatedCharacter.IsCharacter()
            ? gatedCharacter
            : throw new ArgumentException($"{gatedCharacter.GetDescription()} is not a character");

    public override bool Update(State state)
    {
        var ret = false;

        var isAvailable = state.Events[CharacterGate];
        ret |= IsAvailable != isAvailable;
        _isAvailable = isAvailable;

        ret |= base.Update(state);
        return ret;
    }
}

internal class ProgressiveCheck(List<EventType> stages, List<RewardType> rewards) : ICheck
{
    private List<EventType> _completedStages = [];

    public virtual string Description { get; } = new string(BizHawk.Common.StringExtensions.StringExtensions.CommonPrefix(stages.Select(s => s.GetDescription()))).Trim();

    public virtual bool IsAvailable => true;

    public bool IsComplete => _completedStages.Count == Stages.Count;

    public IReadOnlyList<EventType> Stages { get; } = (stages.Count == rewards.Count) ? [.. stages] : throw new ArgumentException("Must have same number of rewards as stages");

    public IReadOnlyList<EventType> CompletedStages => _completedStages;

    public IReadOnlyList<RewardType> StageRewards { get;} = [.. rewards];

    public event Action? Updated;

    public virtual bool Update(State state)
    {
        var ret = false;

        var completed = Stages.Where(s => state.Events[s]).ToList();
        ret |= completed.SequenceEqual(_completedStages);
        _completedStages = completed;

        if (ret) Updated?.Invoke();
        return ret;
    }

    public override string ToString()
    {
        StringBuilder sb = new();
        sb.AppendLine(Description);

        for (int i = 0; i < Stages.Count; i ++)
        {
            if (CompletedStages.Contains(Stages[i]))
                continue;

            sb.AppendLine($"{Stages[i].GetDescription()} ({string.Join(" / ", StageRewards[i].GetFlags(false).Select(f => f.ToString()))})");
        }

        return sb.ToString();
    }
}

internal class GatedProgressiveCheck(EventType gatedCharacter, List<EventType> stages, List<RewardType> rewards) : ProgressiveCheck(stages, rewards), ICharacterGate
{
    private bool _isAvailable;

    public override bool IsAvailable => _isAvailable;

    public EventType CharacterGate { get; } = gatedCharacter.IsCharacter()
            ? gatedCharacter
            : throw new ArgumentException($"{gatedCharacter.GetDescription()} is not a character");

    public override bool Update(State state)
    {
        var ret = false;

        var isAvailable = state.Events[CharacterGate];
        ret |= IsAvailable != isAvailable;
        _isAvailable = isAvailable;

        ret |= base.Update(state);
        return ret;
    }
}

internal class CustomAvailabilityCheck(EventType trackedEvent) : ICheck, IEventCheck
{
    public virtual string Description { get; } = trackedEvent.GetDescription();

    public bool IsAvailable { get; private set; }

    public bool IsComplete { get; private set; }

    public EventType Event { get; } = trackedEvent;

    public required List<Rule> Rules { get; init; }

    public event Action? Updated;

    public bool Update(State state)
    {
        var ret = false;

        var isAvailable = Rules.Any(r => r.IsActive(state));
        ret |= IsAvailable != isAvailable;
        IsAvailable = isAvailable;

        var isComplete = state.Events[Event];
        ret |= IsComplete != isComplete;
        IsComplete = isComplete;

        if (ret) Updated?.Invoke();

        return ret;
    }
}

internal static class CheckExtensions
{
    internal static RewardType GetPossibleRewards(this ICheck check)
    {
        return check switch
        {
            IRewardable hasReward => hasReward.RewardType,
            ProgressiveCheck progressive => progressive.StageRewards.Skip(progressive.CompletedStages.Count).Aggregate(RewardType.None, (acc, r) => acc | r),
            _ => RewardType.None
        };
    }
}

internal class CheckComparer : IEqualityComparer<ICheck>
{
    public bool Equals(ICheck x, ICheck y)
    {
        return x switch
        {
            GatedProgressiveCheck gpcX when y is GatedProgressiveCheck gpcY => gpcX.Stages.SequenceEqual(gpcY.Stages) && gpcX.CharacterGate == gpcY.CharacterGate,
            ProgressiveCheck pcX when y is ProgressiveCheck pcY => pcX.Stages.SequenceEqual(pcY.Stages),
            GatedCheck gX when y is GatedCheck gY => gX.Event == gY.Event && gX.CharacterGate == gY.CharacterGate,
            BasicCheck bX when y is BasicCheck bY => bX.Event == bY.Event,
            CustomAvailabilityCheck cacX when y is CustomAvailabilityCheck cacy => cacX.Event == cacy.Event,
            _ => false
        };
    }

    public int GetHashCode(ICheck c)
    {
        return c switch
        {
            ProgressiveCheck pc => pc.Stages.Last().GetHashCode(),
            IEventCheck b => b.Event.GetHashCode(),
            _ => 0
        };
    }
}