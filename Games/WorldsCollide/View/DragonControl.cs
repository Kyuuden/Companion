using FF.Rando.Companion.Games.WorldsCollide.Tracking;
using FF.Rando.Companion.Settings;
using FF.Rando.Companion.View;
using System.Drawing;
using System.Windows.Forms;

namespace FF.Rando.Companion.Games.WorldsCollide.View;

public class DragonControl : ImageControl<Seed, Dragon> 
{
    private readonly Size _imageSize = new(40, 40);
    private readonly ToolTip _toolTip;

    public DragonControl(Seed seed, PanelSettings settings, Dragon item) : base(seed, settings, item)
    {
        _toolTip = new ToolTip
        {
            ShowAlways = true
        };
        _toolTip.SetToolTip(this, item.Description);
        BackColor = Color.Transparent;
    }

    protected override Size ImageSize => _imageSize;
}