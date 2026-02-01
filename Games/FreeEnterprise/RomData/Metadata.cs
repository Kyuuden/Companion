using FF.Rando.Companion.Extensions;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;

namespace FF.Rando.Companion.Games.FreeEnterprise.RomData;

public class Metadata
{
    public Metadata(JObject o)
    {
        Version = o[nameof(Version).ToSnakeCase()]?.ToObject<string>();
        Flags = o[nameof(Flags).ToSnakeCase()]?.ToObject<string>();
        BinaryFlags = o[nameof(BinaryFlags).ToSnakeCase()]?.ToObject<string>();
        Seed = o[nameof(Seed).ToSnakeCase()]?.ToObject<string>();
        Objectives = (o[nameof(Objectives).ToSnakeCase()]?.Children().ToList() ?? []).Select(ObjectiveFactory.Create).ToArray();
    }

    public string? Version { get; }
    public string? Flags { get; }
    public string? BinaryFlags { get; }
    public string? Seed { get; }
    public Objective[]? Objectives { get; }
}
