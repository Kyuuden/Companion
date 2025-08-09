using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using FF.Rando.Companion.FreeEnterprise.Settings;

namespace FF.Rando.Companion.FreeEnterprise.View;
public partial class KeyItemsPanel : FlowPanel<KeyItemSettings>
{
    public KeyItemsPanel() :base()
    {
        SpacingMode = SpacingMode.Columns;
    }

    public override DockStyle DefaultDockStyle => DockStyle.Top;

    //public override bool CanHaveFillDockStyle => true;

    protected override Control[] GenerateControls(ISeed seed) 
        => (Seed?.KeyItems ?? []).Select(ki => new KeyItemControl(seed, Settings!, ki)).ToArray();

    protected override void Settings_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        base.Settings_PropertyChanged(sender, e);
        if (e.PropertyName == nameof(KeyItemSettings.KeyItemStyle))
            Arrange();
    }
}
