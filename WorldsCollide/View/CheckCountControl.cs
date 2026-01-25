using FF.Rando.Companion.Settings;
using FF.Rando.Companion.WorldsCollide.Settings;

namespace FF.Rando.Companion.WorldsCollide.View;

public class CheckCountControl(Seed seed, PanelSettings settings) : StatisticControl<int>(seed, settings, Statistic.Check)
{
    protected override string PropertyName => nameof(Seed.CheckCount);

    protected override int GetStat() => Game.CheckCount;

    protected override string GetStatText() => $"{Stat,4}";
}
