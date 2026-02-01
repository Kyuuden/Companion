using FF.Rando.Companion.Games.WorldsCollide;
using FF.Rando.Companion.Games.WorldsCollide.Tracking;
using FF.Rando.Companion.Settings;
using FF.Rando.Companion.View;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace FF.Rando.Companion.Games.WorldsCollide.View;

public partial class CheckControl : ImageControl<Seed, Check>
{
    private readonly Size _imageSize = new(40, 40);
    private readonly ToolTip _toolTip;

    public CheckControl(Seed seed, PanelSettings settings, Check item) : base(seed, settings, item)
    {
        _toolTip = new ToolTip
        {
            ShowAlways = true
        };
        _toolTip.SetToolTip(this, item.Description);
        BackColor = Color.Transparent;
        Visible = Value.IsVisible;
    }

    protected override void Value_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        base.Value_PropertyChanged(sender, e);
        if (e.PropertyName == nameof(Check.IsVisible))
        {
            //if (InvokeRequired)
                BeginInvoke(() => Visible = Value.IsVisible);
            //else
            //    Visible = Value.IsVisible;
        }
    }

    protected override Size ImageSize => _imageSize;
}