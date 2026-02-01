using FF.Rando.Companion.Games.WorldsCollide;
using FF.Rando.Companion.Games.WorldsCollide.Enums;
using FF.Rando.Companion.Settings;

namespace FF.Rando.Companion.Games.WorldsCollide.View;

public class CheckCountControl(Seed seed, PanelSettings settings) : StatisticControl<int>(seed, settings, Statistic.Check)
{
    protected override string PropertyName => nameof(Seed.CheckCount);

    protected override int GetStat() => Game.CheckCount;

    protected override string GetStatText() => $"{Stat}".PadRight(2);
}
