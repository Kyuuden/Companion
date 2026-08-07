using FF.Rando.Companion.Settings;
using KGySoft.Drawing.Imaging;
using System.Drawing;

namespace FF.Rando.Companion.Games.JetsOfTime.View;

internal class ValidatedStatistic(Seed seed, PanelSettings settings) : StatisticControl<bool>(seed, settings, new Size(24,24))
{
    protected override string Description { get; } = "Seed Validation";

    protected override string PropertyName => nameof(State.Validated);

    protected override bool GetStat() => Game.State.Validated;

    protected override string GetStatText()
    {
        return "";
    }

    protected override IReadableBitmapData Icon => Game.Sprites.GetNpc(NPCType.Cat).Get(2)!.RenderData(!GetStat());
}
