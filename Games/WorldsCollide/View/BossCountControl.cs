using FF.Rando.Companion.Games.WorldsCollide;
using FF.Rando.Companion.Games.WorldsCollide.Enums;
using FF.Rando.Companion.Settings;

namespace FF.Rando.Companion.Games.WorldsCollide.View;

public class BossCountControl(Seed seed, PanelSettings settings) : StatisticControl<int>(seed, settings, Statistic.Boss)
{
    protected override string PropertyName => nameof(Seed.BossCount);

    protected override int GetStat() => Game.BossCount;

    protected override string GetStatText() => $"{Stat}".PadRight(2);
}
