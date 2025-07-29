using System.Linq;
using System.Windows.Forms;
using FF.Rando.Companion.FreeEnterprise.Settings;

namespace FF.Rando.Companion.FreeEnterprise.View;
public partial class BossesControl : FlowPanelControl<BossSettings>
{
    public BossesControl() : base()
    {
        SpacingMode = SpacingMode.Columns;
    }

    protected override Control[] GenerateControls(ISeed seed)
     => (Seed?.Bosses ?? []).Select(b => new BossControl(seed, Settings!, b)).ToArray();
}
