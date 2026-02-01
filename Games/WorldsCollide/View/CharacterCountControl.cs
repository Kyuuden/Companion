using FF.Rando.Companion.Games.WorldsCollide;
using FF.Rando.Companion.Games.WorldsCollide.Enums;
using FF.Rando.Companion.Settings;
using KGySoft.Drawing.Imaging;

namespace FF.Rando.Companion.Games.WorldsCollide.View;

public class CharacterCountControl(Seed seed, PanelSettings settings) : StatisticControl<int>(seed, settings, Statistic.Character)
{
    protected override string PropertyName => nameof(Seed.CharacterCount);

    protected override int GetStat() => Game.CharacterCount;

    protected override string GetStatText() => $"{Stat}".PadRight(2);
}
