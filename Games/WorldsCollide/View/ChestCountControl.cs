using FF.Rando.Companion.Games.WorldsCollide;
using FF.Rando.Companion.Games.WorldsCollide.Enums;
using FF.Rando.Companion.Settings;

namespace FF.Rando.Companion.Games.WorldsCollide.View;

public class ChestCountControl(Seed seed, PanelSettings settings) : StatisticControl<int>(seed, settings, Statistic.Chest)
{
    protected override string PropertyName => nameof(Seed.ChestCount);

    protected override int GetStat() => Game.ChestCount;

    protected override string GetStatText() => $"{Stat}".PadRight(2);
}
