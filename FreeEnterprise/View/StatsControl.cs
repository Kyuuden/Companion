using FF.Rando.Companion.FreeEnterprise.Settings;
using System.Windows.Forms;

namespace FF.Rando.Companion.FreeEnterprise.View;

public class StatsControl : FlowPanelControl<StatsSettings>
{
    public StatsControl()
        : base()
    {
        SpacingMode = SpacingMode.Columns;
        WrapContents = false;
    }

    public override DockStyle DefaultDockStyle => DockStyle.Bottom;

    public override bool CanHaveFillDockStyle => false;

    protected override Control[] GenerateControls(ISeed seed) 
        => 
        [
            new KeyItemStatsControl(seed, Settings!),
            new BossStatsControl(seed, Settings!),
            new TreasureStatsControl(seed, Settings!),
            new XpStatsControl(seed, Settings!)
        ];
}
