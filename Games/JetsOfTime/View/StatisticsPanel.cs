using FF.Rando.Companion.Games.JetsOfTime.Settings;
using FF.Rando.Companion.View;
using System.Windows.Forms;

namespace FF.Rando.Companion.Games.JetsOfTime.View;

internal class StatisticsPanel : FlowPanel<StatisticsSettings>
{
    public StatisticsPanel()
        : base()
    {
        SpacingMode = SpacingMode.Columns;
        WrapContents = false;
    }

    public override DockStyle DefaultDockStyle => DockStyle.Bottom;

    protected override Control[] GenerateControls(Seed seed)
        =>
        [
            new OpenedChestsStatistic(seed, seed.Settings.Statistics),
            new OpenedSealedChestsStatistic(seed, seed.Settings.Statistics),
            new CompletedChecksStatistic(seed, seed.Settings.Statistics),
            new ValidatedStatistic(seed, seed.Settings.Statistics),
        ];
}
