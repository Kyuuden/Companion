using FF.Rando.Companion.Extensions;
using Newtonsoft.Json.Linq;

namespace FF.Rando.Companion.Games.FreeEnterprise.RomData;

public record class Reward
{
    public Reward(JToken o)
    {
        Description = o[nameof(Description).ToSnakeCase()]?.ToObject<string>();
        Req = o[nameof(Req).ToSnakeCase()]?.ToObject<string>();
    }

    public string? Description { get; }
    public string? Req { get; }
}
