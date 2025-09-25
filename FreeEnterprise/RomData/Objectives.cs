using FF.Rando.Companion.Extensions;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;

namespace FF.Rando.Companion.FreeEnterprise.RomData;

public static class ObjectiveFactory
{
    public static Objective Create(JToken o)
    {
        if (o.Type == JTokenType.String)
            return new BasicObjective(o.ToObject<string>());

        return new GroupObjectives(
            o[nameof(GroupObjectives.Key).ToSnakeCase()]?.ToObject<string>(),
            o[nameof(GroupObjectives.Name).ToSnakeCase()]?.ToObject<string>(),
            (o[nameof(GroupObjectives.Tasks).ToSnakeCase()]?.Children().ToList() ?? []).Select(TaskFactory.Create).ToArray(),
            (o[nameof(GroupObjectives.Rewards).ToSnakeCase()]?.Children().ToList() ?? []).Select(ob => new Reward(ob)).ToArray());
    }
}

public abstract record class Objective;
public record class BasicObjective(string? Description) : Objective;
public record class GroupObjectives(string? Key, string? Name, Task[]? Tasks, Reward[]? Rewards) : Objective;