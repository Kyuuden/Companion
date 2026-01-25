using FF.Rando.Companion.View;
using FF.Rando.Companion.WorldsCollide.Settings;
using System.Windows.Forms;

namespace FF.Rando.Companion.WorldsCollide.View;
public class StatsPanel : FlowPanel<StatsSettings>
{
    public StatsPanel()
        : base()
    {
        SpacingMode = SpacingMode.Columns;
        WrapContents = false;
    }

    public override DockStyle DefaultDockStyle => DockStyle.Bottom;

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
