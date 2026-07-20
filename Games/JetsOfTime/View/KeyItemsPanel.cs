using FF.Rando.Companion.Games.JetsOfTime.Settings;
using FF.Rando.Companion.View;
using System.Linq;
using System.Windows.Forms;

namespace FF.Rando.Companion.Games.JetsOfTime.View;
internal partial class KeyItemsPanel : FlowPanel<KeyItemSettings>
{
    public KeyItemsPanel() :base()
    {
        SpacingMode = SpacingMode.Columns;
        WrapAfter = 6;
    }

    public override DockStyle DefaultDockStyle => DockStyle.Top;

    protected override Control[] GenerateControls(Seed seed) 
        => (Game?.KeyItems.Items ?? []).Select(ki => new KeyItemControl(seed, Settings!, ki)).ToArray();
}
