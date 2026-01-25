using FF.Rando.Companion.Settings;
using FF.Rando.Companion.WorldsCollide.Settings;
using KGySoft.Drawing.Imaging;

namespace FF.Rando.Companion.WorldsCollide.View;

public class CharacterCountControl(Seed seed, PanelSettings settings) : StatisticControl<int>(seed, settings, Statistic.Character)
{
    protected override string PropertyName => nameof(Seed.CharacterCount);

    protected override int GetStat() => Game.CharacterCount;

    protected override string GetStatText() => $"{Stat,4}";
}
