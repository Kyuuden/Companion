using FF.Rando.Companion.Settings;
using FF.Rando.Companion.WorldsCollide.Settings;

namespace FF.Rando.Companion.WorldsCollide.View;

public class ChestCountControl(Seed seed, PanelSettings settings) : StatisticControl<int>(seed, settings, Statistic.Chest)
{
    protected override string PropertyName => nameof(Seed.ChestCount);

    protected override int GetStat() => Game.ChestCount;

    protected override string GetStatText() => $"{Stat,4}";
}
