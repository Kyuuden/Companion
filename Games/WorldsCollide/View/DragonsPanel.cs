using FF.Rando.Companion.Games.WorldsCollide.Settings;
using FF.Rando.Companion.View;
using System.Linq;
using System.Windows.Forms;

namespace FF.Rando.Companion.Games.WorldsCollide.View;

public class DragonsPanel : FlowPanelEx<DragonSettings>
{
    public DragonsPanel() : base() 
    { 
        SpacingMode = SpacingMode.Columns; 
    }

    public override DockStyle DefaultDockStyle => DockStyle.Top;

    protected override Control[] GenerateControls(Seed seed)
        => (Game?.Dragons ?? []).Select(d => new DragonControl(seed, Settings!, d)).ToArray();
}
