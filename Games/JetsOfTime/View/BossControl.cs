using FF.Rando.Companion.Games.JetsOfTime.Tracking;
using FF.Rando.Companion.Settings;
using FF.Rando.Companion.View;
using System.Drawing;
using System.Windows.Forms;

namespace FF.Rando.Companion.Games.JetsOfTime.View;

internal class BossControl : ImageControl<Seed, Boss>
{
    private readonly ToolTip _toolTip;

    protected override Size ImageSize { get; } = new Size(48, 48);

    public BossControl(Seed seed, PanelSettings settings, Boss boss)
        : base(seed, settings, boss)
    {
        _toolTip = new ToolTip
        {
            ShowAlways = true
        };
        _toolTip.SetToolTip(this, boss.AltText);

        BackColor = Color.Transparent;
    }
}
