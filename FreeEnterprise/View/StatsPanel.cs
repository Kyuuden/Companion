using FF.Rando.Companion.FreeEnterprise.Settings;
using System.Windows.Forms;
using FF.Rando.Companion.View;

namespace FF.Rando.Companion.FreeEnterprise.View;

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
