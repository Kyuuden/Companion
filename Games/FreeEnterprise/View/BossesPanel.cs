using FF.Rando.Companion.Games.FreeEnterprise;
using FF.Rando.Companion.Games.FreeEnterprise.Settings;
using FF.Rando.Companion.Games.FreeEnterprise.View;
using FF.Rando.Companion.View;
using System.Linq;
using System.Windows.Forms;

namespace FF.Rando.Companion.Games.FreeEnterprise.View;
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
