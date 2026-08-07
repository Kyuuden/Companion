using FF.Rando.Companion.Rendering;
using FF.Rando.Companion.Settings;
using KGySoft.Drawing.Imaging;
using System.Drawing;

namespace FF.Rando.Companion.Games.JetsOfTime.View;

internal class OpenedSealedChestsStatistic(Seed seed, PanelSettings settings) : StatisticControl<int>(seed, settings)
{
    protected override string Description { get; } = "Opened Sealed Chests";

    protected override string PropertyName => nameof(State.OpenedSealedChests);

    protected override int GetStat() => Game.State.OpenedSealedChests;

    protected override string GetStatText() => $"{GetStat(),2}";

    protected override IReadableBitmapData Icon { get; } = seed.Sprites.GetNpc(NPCType.Sealed_chest).Get(0)!.Crop(new Rectangle(8, 0, 16, 16)).RenderData();
}
