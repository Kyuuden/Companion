using FF.Rando.Companion.Games.FreeEnterprise;
using FF.Rando.Companion.Settings;
using KGySoft.Drawing.Imaging;

namespace FF.Rando.Companion.Games.FreeEnterprise.View;

public class BossStatsControl(ISeed seed, PanelSettings settings) : StatisticControl<int>(seed, settings)
{
    protected override string PropertyName => nameof(ISeed.DefeatedEncounters);

    protected override IReadableBitmapData GetIcon() => Game.Sprites.GetNpcImage(new int[2, 2] { { 784, 785 }, { 786, 787 } }, 1);

    protected override int GetStat() => Game.DefeatedEncounters;

    protected override string GetStatText() => $"{Stat,2}/34";
}