using System.Linq;
using System.Windows.Forms;
using FF.Rando.Companion.FreeEnterprise.Settings;

namespace FF.Rando.Companion.FreeEnterprise.View;
public partial class BossesPanel : FlowPanel<BossSettings>
{
    public BossesPanel() : base()
    {
        SpacingMode = SpacingMode.Columns;
    }

    public override DockStyle DefaultDockStyle => DockStyle.Top;

    //public override bool CanHaveFillDockStyle => true;

    public override bool IsEnabled => base.IsEnabled && Seed?.CanTackBosses == true;

    protected override Control[] GenerateControls(ISeed seed)
     => (Seed?.Bosses ?? []).Select(b => new BossControl(seed, Settings!, b)).ToArray();
}
