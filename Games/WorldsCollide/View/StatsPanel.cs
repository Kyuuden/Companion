using FF.Rando.Companion.Games.WorldsCollide;
using FF.Rando.Companion.Games.WorldsCollide.Settings;
using FF.Rando.Companion.View;
using System.Windows.Forms;

namespace FF.Rando.Companion.Games.WorldsCollide.View;
public class StatsPanel : FlowPanelEx<StatsSettings>
{
    public StatsPanel()
        : base()
    {
        SpacingMode = SpacingMode.Columns;
        WrapContents = false;
    }

    public override DockStyle DefaultDockStyle => DockStyle.Top;

    protected override Control[] GenerateControls(Seed seed)
        =>
        [
            new CharacterCountControl(seed, Settings!),
            new EsperCountControl(seed, Settings!),
            new DragonCountControl(seed, Settings!),
            new CheckCountControl(seed, Settings!),
            new BossCountControl(seed, Settings!),
            new ChestCountControl(seed, Settings!),
        ];
}
