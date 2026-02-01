using FF.Rando.Companion.Games.FreeEnterprise.View;
using FF.Rando.Companion.Games.FreeEnterprise;
using FF.Rando.Companion.Games.FreeEnterprise.Settings;
using FF.Rando.Companion.View;
using System.Windows.Forms;

namespace FF.Rando.Companion.Games.FreeEnterprise.View;

public class StatsPanel : FlowPanel<StatsSettings>
{
    public StatsPanel()
        : base()
    {
        SpacingMode = SpacingMode.Columns;
        WrapContents = false;
    }

    public override DockStyle DefaultDockStyle => DockStyle.Bottom;

    protected override Control[] GenerateControls(ISeed seed)
        =>
        [
            new KeyItemStatsControl(seed, Settings!),
            new BossStatsControl(seed, Settings!),
            new TreasureStatsControl(seed, Settings!),
            new XpStatsControl(seed, Settings!)
        ];
}
