using FF.Rando.Companion.Games.WorldsCollide;
using FF.Rando.Companion.Games.WorldsCollide.Enums;
using FF.Rando.Companion.Settings;

namespace FF.Rando.Companion.Games.WorldsCollide.View;

public class EsperCountControl(Seed seed, PanelSettings settings) : StatisticControl<int>(seed, settings, Statistic.Esper)
{
    protected override string PropertyName => nameof(Seed.EsperCount);

    protected override int GetStat() => Game.EsperCount;

    protected override string GetStatText() => $"{Stat}".PadRight(2);
}