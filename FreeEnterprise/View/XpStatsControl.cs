using FF.Rando.Companion.Settings;
using KGySoft.Drawing.Imaging;

namespace FF.Rando.Companion.FreeEnterprise.View;

public class XpStatsControl(ISeed seed, PanelSettings settings) : StatisticControl<decimal>(seed, settings)
{
    protected override string PropertyName => nameof(ISeed.XpRate);

    protected override IReadableBitmapData GetIcon() => Game.Sprites.GetEgg();

    protected override decimal GetStat() => Game.XpRate ?? 1;

    protected override string GetStatText() => $"{Stat:F2}x";
}
