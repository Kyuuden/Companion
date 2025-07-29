using FF.Rando.Companion.FreeEnterprise.Settings;
using KGySoft.Drawing.Imaging;

namespace FF.Rando.Companion.FreeEnterprise.View;

public class XpStatsControl : StatisticControl<decimal>
{
    public XpStatsControl(ISeed seed, PanelSettings settings) : base(seed, settings)
    {
    }

    protected override string PropertyName => nameof(ISeed.XpRate);

    protected override IReadableBitmapData GetIcon() => Seed.Sprites.GetEgg();

    protected override decimal GetStat() => Seed.XpRate ?? 1;

    protected override string GetStatText() => $"{Stat:F2}x";
}
