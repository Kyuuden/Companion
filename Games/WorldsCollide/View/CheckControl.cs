using FF.Rando.Companion.Games.WorldsCollide.Tracking;
using FF.Rando.Companion.Settings;
using FF.Rando.Companion.View;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace FF.Rando.Companion.Games.WorldsCollide.View;

public partial class CheckControl : ImageWithOverlayControl<Seed, Check>
{
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

    protected override void PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        base.PropertyChanged(sender, e);
        switch (e.PropertyName)
        {
            case nameof(Check.IsVisible):
                BeginInvoke(() => Visible = Value.IsVisible);
                break;

            case nameof(Check.Description):
                _toolTip.SetToolTip(this, Value.Description);
                break;
        }
    }
}
