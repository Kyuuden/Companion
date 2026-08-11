using FF.Rando.Companion.Extensions;
using FF.Rando.Companion.Rendering;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace FF.Rando.Companion.Games.JetsOfTime.Tracking;
internal class TimePeriod(TimePeriodType timePeriod)
{
    public TimePeriodType Period { get; } = timePeriod;

    public string Description { get; } = timePeriod.GetDescription();

    public bool IsAccessable { get; private set; }

    public bool Exists { get; private set; }

    public IReadOnlyList<CheckLocation> Locations { get; init; } = [];

    public List<ExistanceRule> ExistanceRules { get; init; } = [];

    public List<AccessRule> AccessRules { get; init; } = [];

    public required ISprite Map { get; init; }

    public bool Update(State state)
    {
        var ret = false;

        var exists = ExistanceRules.Count == 0 || ExistanceRules.Any(r => r.Exists(state));
        if (exists != Exists)
        {
            ret = true;
            Exists = exists;
        }

        var canAccess = AccessRules.Count == 0 || AccessRules.Any(r => r.IsAccessable(state));
        if (canAccess != IsAccessable)
        {
            ret = true;
            IsAccessable = canAccess;
        }

        foreach (var check in Locations)
        {
            ret |= check.Update(state);
        }

        if (ret)
            Updated?.Invoke(this, null);

        return ret;
    }

    public event EventHandler? Updated;
}

internal class CheckLocation(CheckLocationType type)
{
    public string Description { get; } = type.GetDescription();

    public List<ExistanceRule> ExistanceRules { get; init; } = [];

    public List<AccessRule> AccessRules { get; init; } = [];

    public List<Check> Checks { get; init; } = [];

    public bool Exists { get; protected set; }

    public bool IsAccessable { get; protected set; }

    public bool IsComplete { get; protected set; }

    public Point Location { get; init; }

    public bool Update(State state)
    {
        var ret = false;

        foreach (var check in Checks)
        {
            ret |= check.Update(state);
        }

        var exists = (ExistanceRules.Count == 0 || ExistanceRules.Any(r => r.Exists(state))) && Checks.Any(c => c.Exists);
        if (exists != Exists)
        {
            ret = true;
            Exists = exists;
        }

        var canAccess = (AccessRules.Count == 0 || AccessRules.Any(r => r.IsAccessable(state))) && Checks.Any(c=>c.IsAccessable && c.Exists && !c.IsComplete);
        if (canAccess != IsAccessable)
        {
            ret = true;
            IsAccessable = canAccess;
        }

        var isComplete = Checks.Where(c => c.Exists).All(c => c.IsComplete);
        if (isComplete != IsComplete)
        {
            ret = true;
            IsComplete = isComplete;
        }

        return ret;
    }

    public override string ToString() => Description;
}

internal class SealedChestsCheck : ChestsCheck
{
    public SealedChestsCheck(string name = "Sealed Chests") : base(name, 0)
    {
        ExistanceRules = [Flag.Chronosanity];
        AccessRules = [AccessRule.Sealed];
        CompleteRules = [];
        IsKeyItem = true;
    }
}

internal class ChestsCheck : Check
{
    protected uint ChestIdOffset { get; }

    public List<uint> ChestIds { get; init; } = [];

    public int OpenedChests { get; private set; } = 0;

    public ChestsCheck(string name = "Chests", uint offset = 8) : base(name)
    {
        CompleteRules = [];
        IsKeyItem = true;
        ChestIdOffset = offset;
    }

    public override bool Update(State state)
    {
        var ret = false;

        var exists = (ExistanceRules.Count == 0 || ExistanceRules.Any(r => r.Exists(state))) && state.Flags[Flag.Chronosanity] == true;
        if (exists != Exists)
        {
            ret = true;
            Exists = exists;
        }

        var canAccess = AccessRules.Count == 0 || AccessRules.Any(r => r.IsAccessable(state));
        if (canAccess != IsAccessable)
        {
            ret = true;
            IsAccessable = canAccess;
        }

        var openedCount = ChestIds.Count(cid => state.Events.EventData.Read<bool>(cid + ChestIdOffset));
        ret |= openedCount != OpenedChests;
        OpenedChests = openedCount;

        var isComplete = openedCount == ChestIds.Count;
        if (isComplete != IsComplete)
        {
            ret = true;
            IsComplete = isComplete;
        }

        return ret;
    }
}

internal class Check(string name)
{
    public string Description { get; } = name;

    public bool Exists { get; protected set; }

    public bool IsAccessable { get; protected set; }

    public bool IsComplete { get; protected set; }

    public List<ExistanceRule> ExistanceRules { get; init; } = [];

    public List<AccessRule> AccessRules { get; init; } = [];

    public List<CompleteRule> CompleteRules { get; init; } = [];

    public bool IsGoMode { get; init; }

    public bool IsKeyItem { get; init; }

    public virtual bool Update(State state)
    {
        var ret = false;

        var exists = ExistanceRules.Count == 0 || ExistanceRules.Any(r=> r.Exists(state));
        if (exists != Exists)
        {
            ret = true;
            Exists = exists;
        }

        var canAccess = AccessRules.Count == 0 || AccessRules.Any(r => r.IsAccessable(state));
        if (canAccess != IsAccessable)
        {
            ret = true;
            IsAccessable = canAccess;
        }

        var isComplete = CompleteRules.Any(r => r.IsComplete(state));

        if (isComplete != IsComplete)
        {
            ret = true;
            IsComplete = isComplete;
        }

        return ret;
    }

    public override string ToString()
    {
        return $"{Description} Exists: {Exists}, IsAccessable: {IsAccessable}, IsComplete: {IsComplete}, IsKeyItem: {IsKeyItem}, IsGoMode: {IsGoMode}";
    }
}

internal class CompleteRule
{
    public List<KeyItemType> KeyItems { get; init; } = [];
    public List<EventType> Events { get; init; } = [];

    public virtual bool IsComplete(State state)
    {
        var ret = true;

        foreach (var ki in KeyItems)
            ret &= state.KeyItems.IsFound(ki);

        foreach (var evnt in Events)
            ret &= state.Events[evnt];

        return ret;
    }

    public static implicit operator CompleteRule(EventType eventType) => new() { Events = [eventType] };
    public static implicit operator CompleteRule(KeyItemType type) => new() { KeyItems = [type] };
}

internal class ExistanceRule
{
    public bool Negate { get; init; } = false;
    public GameMode? GameMode { get; init; }
    public List<Flag> Flags { get; init; } = [];

    public virtual bool Exists(State state)
    {
        var ret = true;

        if (GameMode.HasValue)
            ret &= (state.Flags.Mode == GameMode.Value);

        foreach (var flag in Flags)
            ret &= state.Flags[flag] == true;

        if (Negate)
            ret = !ret;

        return ret;
    }

    public static implicit operator ExistanceRule(GameMode mode) => new() { GameMode = mode };
    public static implicit operator ExistanceRule(Flag flag) => new() { Flags = [flag] };
}

internal class AccessRule
{
    public bool Negate { get; init; } = false;
    public List<CharacterType> Characters { get; init; } = [];
    public List<KeyItemType> KeyItems { get; init; } = [];
    public List<EventType> Events { get; init; } = [];
    public List<Flag> Flags { get; init; } = [];
    public bool CanFly { get; init; } = false;
    public bool CanOpenSealed { get; init; } = false;
    public uint? Gold { get; init; } = null;
    public GameMode? GameMode { get; init; }

    public virtual bool IsAccessable(State state)
    {
        var ret = true;

        if (GameMode.HasValue)
            ret &= (state.Flags.Mode == GameMode.Value);

        foreach (var ch in Characters)
            ret &= state.Characters.IsFound(ch);

        foreach (var ki in KeyItems)
            ret &= state.KeyItems.IsFound(ki);

        foreach (var evnt in Events)
            ret &= state.Events[evnt];

        foreach (var flag in Flags)
            ret &= state.Flags[flag] == true;

        if (CanFly)
            ret &= state.CanFly;

        if (CanOpenSealed)
            ret &= state.CanOpenSealedChests;

        if (Gold.HasValue)
            ret &= state.Gold > Gold.Value;

        if (Negate)
            ret = !ret;

        return ret;
    }

    public static implicit operator AccessRule(EventType eventType) => new() { Events = [eventType] };
    public static implicit operator AccessRule(KeyItemType type) => new() {  KeyItems = [type] };
    public static implicit operator AccessRule(CharacterType type) => new() { Characters = [type] };
    public static implicit operator AccessRule(GameMode mode) => new() { GameMode = mode };
    public static implicit operator AccessRule(Flag flag) => new() { Flags = [flag] };

    public static AccessRule Sealed => new() { CanOpenSealed = true };
    public static AccessRule Flight => new() { CanFly = true };
}