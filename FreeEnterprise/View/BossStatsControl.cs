using System.Linq;
using FF.Rando.Companion.Settings;
using KGySoft.Drawing.Imaging;

namespace FF.Rando.Companion.FreeEnterprise.View;

public class BossStatsControl : StatisticControl<int>
{
    public BossStatsControl(ISeed seed, PanelSettings settings) : base(seed, settings)
    {
    }

    protected override string PropertyName => nameof(ISeed.DefeatedEncounters);

    protected override IReadableBitmapData GetIcon() => Seed.Sprites.GetNpcImage(new int[2, 2] { { 784, 785 }, { 786, 787 } }, 1);

    protected override int GetStat() => Seed.DefeatedEncounters;

    protected override string GetStatText() => $"{Stat,2}/34";
}