using FF.Rando.Companion.Rendering;
using FF.Rando.Companion.Settings;
using KGySoft.Drawing.Imaging;
using System.Drawing;

namespace FF.Rando.Companion.Games.JetsOfTime.View;

internal class LearnedMagicStatistic(Seed seed, PanelSettings settings) : StatisticControl<bool>(seed, settings)
{
    protected override string Description { get; } = "Learned Magic";

    protected override string PropertyName => nameof(Tracking.Events.LearnMagic);

    protected override bool GetStat() => Game.State.Events.LearnMagic;

    protected override string GetStatText()
    {
        return "";
    }

    protected override IReadableBitmapData Icon => Game.Sprites.GetNpc(NPCType.Save_point).Get(1)!.Pad(new Size(36, 24)).RenderData(!GetStat());
}
