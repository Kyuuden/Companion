using FF.Rando.Companion.Settings;
using FF.Rando.Companion.WorldsCollide.Settings;

namespace FF.Rando.Companion.WorldsCollide.View;

public class EsperCountControl(Seed seed, PanelSettings settings) : StatisticControl<int>(seed, settings, Statistic.Esper)
{
    protected override string PropertyName => nameof(Seed.EsperCount);

    protected override int GetStat() => Game.EsperCount;

    protected override string GetStatText() => $"{Stat,4}";
}