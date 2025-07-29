using System.Linq;
using FF.Rando.Companion.FreeEnterprise.Settings;
using KGySoft.Drawing.Imaging;

namespace FF.Rando.Companion.FreeEnterprise.View;

public class BossStatsControl : StatisticControl<int>
{
    public BossStatsControl(ISeed seed, PanelSettings settings) : base(seed, settings)
    {
    }

    protected override string PropertyName => nameof(ISeed.Bosses);

    protected override IReadableBitmapData GetIcon() => Seed.Sprites.GetNpcImage(new int[2, 2] { { 784, 785 }, { 786, 787 } }, 1);

    protected override int GetStat() => Seed.Bosses.SelectMany(b=> b.Encounters).Count(e => e.IsDefeated);

    protected override string GetStatText() => $"{Stat,2}/{Seed.Bosses.Count()}";
}