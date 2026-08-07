using FF.Rando.Companion.Settings;
using KGySoft.Drawing.Imaging;

namespace FF.Rando.Companion.Games.JetsOfTime.View;

internal class CompletedChecksStatistic(Seed seed, PanelSettings settings) : StatisticControl<int>(seed, settings)
{
    protected override string Description { get; } = "Completed Checks";

    protected override string PropertyName => nameof(State.CompletedChecks);

    protected override int GetStat() => Game.State.CompletedChecks;

    protected override string GetStatText() => $"{GetStat(),2}";

    protected override IReadableBitmapData Icon { get; } = seed.Sprites.GetNpc(NPCType.Poyozo_doll).Get(0)!.RenderData();
}