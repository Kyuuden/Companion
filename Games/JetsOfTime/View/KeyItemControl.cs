using FF.Rando.Companion.Games.JetsOfTime.Tracking;
using FF.Rando.Companion.Settings;
using FF.Rando.Companion.View;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace FF.Rando.Companion.Games.JetsOfTime.View;

internal class KeyItemControl : ImageControl<Seed, KeyItemBase>
{
    private readonly ToolTip _toolTip;

    public KeyItemControl(Seed seed, PanelSettings settings, KeyItemBase keyItem) 
        : base(seed, settings, keyItem)
    {
        _toolTip = new ToolTip
        {
            ShowAlways = true
        };
        _toolTip.SetToolTip(this, keyItem.AltText);

        BackColor = Color.Transparent;
        Visible = Value.Exists;
    }

    protected override void Value_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        base.Value_PropertyChanged(sender, e);

        if (Value == null)
            return;

        switch (e.PropertyName)
        { 
            case nameof(IImageTracker.AltText):
                _toolTip.SetToolTip(this, Value.AltText);
                break;
            case nameof(KeyItemBase.Exists):
                Visible = Value.Exists;
                break;
        }
    }
}
