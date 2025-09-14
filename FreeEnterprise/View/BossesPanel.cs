using System.Linq;
using System.Windows.Forms;
using FF.Rando.Companion.FreeEnterprise.Settings;
using FF.Rando.Companion.View;

namespace FF.Rando.Companion.FreeEnterprise.View;
public partial class BossesPanel : FlowPanel<BossSettings>
{
    public BossesPanel() : base()
    {
        SpacingMode = SpacingMode.Columns;
    }

    public override DockStyle DefaultDockStyle => DockStyle.Top;

    public override bool IsEnabled => base.IsEnabled && Game?.CanTackBosses == true;

    protected override Control[] GenerateControls(ISeed seed)
     => (Game?.Bosses ?? []).Select(b => new BossControl(seed, Settings!, b)).ToArray();
}
