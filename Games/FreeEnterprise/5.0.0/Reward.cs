using FF.Rando.Companion.Games.FreeEnterprise;

namespace FF.Rando.Companion.Games.FreeEnterprise._5._0._0;

internal class Reward : IReward
{
    internal Reward(Games.FreeEnterprise.RomData.Reward reward)
    {
        Description = reward.Description ?? "Unknown";
        Required = reward.Req == null
            ? null
            : reward.Req.ToLower() == "all"
                ? -1
                : int.Parse(reward.Req);
    }

    public string Description { get; }

    public int? Required { get; }
}