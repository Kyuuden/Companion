using FF.Rando.Companion.Settings;
using FF.Rando.Companion.WorldsCollide.Settings;

namespace FF.Rando.Companion.WorldsCollide.View;

public class DragonCountControl(Seed seed, PanelSettings settings) : StatisticControl<int>(seed, settings, Statistic.Dragon)
{
    protected override string PropertyName => nameof(Seed.DragonCount);

    protected override int GetStat() => Game.DragonCount;

    protected override string GetStatText() => $"{Stat,4}";
}
