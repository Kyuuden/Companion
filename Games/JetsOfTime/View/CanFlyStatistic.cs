using FF.Rando.Companion.Rendering;
using FF.Rando.Companion.Settings;
using KGySoft.Drawing.Imaging;
using System.Drawing;

namespace FF.Rando.Companion.Games.JetsOfTime.View;

internal class CanFlyStatistic(Seed seed, PanelSettings settings) : StatisticControl<bool>(seed, settings)
{
    protected override string Description { get; } = "Can Fly";

    protected override string PropertyName => nameof(State.CanFly);

    protected override bool GetStat() => Game.State.CanFly;

    protected override string GetStatText()
    {
        return "";
    }

    protected override IReadableBitmapData Icon => Game.Sprites.GetNpc(NPCType.Flying_map_Epoch).Get(0)!.Pad(new Size(36, 24)).RenderData(!GetStat());
}