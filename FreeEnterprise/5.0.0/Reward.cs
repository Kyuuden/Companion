namespace FF.Rando.Companion.FreeEnterprise._5._0._0;

internal class Reward : IReward
{
    internal Reward(RomData.Reward reward)
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