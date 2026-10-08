using FF.Rando.Companion.Games.WorldsCollide.Enums;
using System.Collections.Generic;

namespace FF.Rando.Companion.Games.WorldsCollide.Tracking;

internal class Rule : IRule
{
    public List<EventType> Events { get; init; } = [];

    public List<DragonType> DefeatedDragons { get; init; } = [];

    public int? RequiredChecks { get; init; }
    public int? RequiredCharacters { get; init; }

    public virtual bool IsActive(State state)
    {
        var ret = true;

        foreach (var evnt in Events)
            ret &= state.Events[evnt];

        return ret;
    }

    public static implicit operator Rule(EventType eventType) => new() { Events = [eventType] };
    public static implicit operator Rule(DragonType dragon) => new() { DefeatedDragons = [dragon] };
}

internal class SimpleEventRule : IRule
{
    public required EventType Event { get; init; }

    public virtual bool IsActive(State state)
    {
        return state.Events[Event];
    }

    public static implicit operator SimpleEventRule(EventType eventType) => new() { Event = eventType };
}

internal class NullRule : IRule 
{ 
    public static NullRule Instance { get; } = new NullRule();

    public virtual bool IsActive(State _) => true; 
}