using FF.Rando.Companion.Games.WorldsCollide;
using FF.Rando.Companion.Games.WorldsCollide.Enums;
using FF.Rando.Companion.Settings;

namespace FF.Rando.Companion.Games.WorldsCollide.View;

public class DragonCountControl(Seed seed, PanelSettings settings) : StatisticControl<int>(seed, settings, Statistic.Dragon)
{
    protected override string PropertyName => nameof(Seed.DragonCount);

    protected override int GetStat() => Game.DragonCount;

    protected override string GetStatText() => $"{Stat}".PadRight(2);
}
