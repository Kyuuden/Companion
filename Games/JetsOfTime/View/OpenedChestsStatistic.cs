using FF.Rando.Companion.Settings;
using KGySoft.Drawing.Imaging;

namespace FF.Rando.Companion.Games.JetsOfTime.View;

internal class OpenedChestsStatistic(Seed seed, PanelSettings settings) : StatisticControl<int>(seed, settings)
{
    protected override string Description { get; } = "Opened Chests";

    protected override string PropertyName => nameof(State.OpenedChests);

    protected override int GetStat() => Game.State.OpenedChests;

    protected override string GetStatText() => $"{GetStat(),2}";

    protected override IReadableBitmapData Icon { get; } = seed.Locations.Get(LocationType.ZenanBridge_Present).GetOpenChest()!.RenderData();
}
