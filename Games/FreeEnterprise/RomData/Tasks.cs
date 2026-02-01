using FF.Rando.Companion.Extensions;
using Newtonsoft.Json.Linq;
using System;

namespace FF.Rando.Companion.Games.FreeEnterprise.RomData;

public static class TaskFactory
{
    public static Task Create(JToken o)
    {
        if (o.Type == JTokenType.String)
            return new BasicTask(o.ToObject<string>());

        var objective = o[nameof(ThresholdTask.Objective).ToSnakeCase()];
        var threshold = o[nameof(ThresholdTask.Threshold).ToSnakeCase()];
        var group = o[nameof(GroupTask.Group).ToSnakeCase()];
        var required = o[nameof(GroupTask.Req).ToSnakeCase()];

        if (objective != null && threshold != null)
            return new ThresholdTask(objective.ToObject<string>(), threshold.ToObject<int>());

        if (group != null && required != null)
            return new GroupTask(group.ToObject<string>(), required.ToObject<string>());

        throw new NotSupportedException();
    }
}

public abstract record class Task;
public record class BasicTask(string? Description) : Task;
public record class ThresholdTask(string? Objective, int? Threshold) : Task;
public record class GroupTask(string? Group, string? Req) : Task;
