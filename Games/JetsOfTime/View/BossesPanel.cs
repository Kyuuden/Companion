using FF.Rando.Companion.Games.JetsOfTime.Settings;
using FF.Rando.Companion.View;
using System.Linq;
using System.Windows.Forms;

namespace FF.Rando.Companion.Games.JetsOfTime.View;
internal partial class BossesPanel : FlowPanel<BossSettings>
{
    public BossesPanel() :base()
    {
        SpacingMode = SpacingMode.Columns;
        
    }

    public override DockStyle DefaultDockStyle => DockStyle.Top;
    public override bool CanHaveFillDockStyle => true;

    protected override Control[] GenerateControls(Seed seed)
         => (Game?.Bosses.Values ?? []).Select(boss => new BossControl(seed, Settings!, boss)).ToArray();
}
