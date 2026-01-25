using FF.Rando.Companion.Settings;
using FF.Rando.Companion.WorldsCollide.Settings;

namespace FF.Rando.Companion.WorldsCollide.View;

public class BossCountControl(Seed seed, PanelSettings settings) : StatisticControl<int>(seed, settings, Statistic.Boss)
{
    protected override string PropertyName => nameof(Seed.BossCount);

    protected override int GetStat() => Game.BossCount;

    protected override string GetStatText() => $"{Stat,4}";
}
