using FF.Rando.Companion.Rendering;
using FF.Rando.Companion.Settings;
using KGySoft.Drawing.Imaging;
using System.Drawing;

namespace FF.Rando.Companion.Games.JetsOfTime.View;

internal class ValidatedStatistic(Seed seed, PanelSettings settings) : StatisticControl<bool>(seed, settings)
{
    protected override string Description { get; } = "Seed Validation";

    protected override string PropertyName => nameof(State.Validated);

    protected override bool GetStat() => Game.State.Validated;

    protected override string GetStatText()
    {
        return "";
    }

    protected override IReadableBitmapData Icon => Game.Sprites.GetNpc(NPCType.Cat).Get(2)!.Crop(0, 0, 12, 24).Pad(new Size(36,24)).RenderData(!GetStat());
}
