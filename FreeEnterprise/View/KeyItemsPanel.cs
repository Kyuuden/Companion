using FF.Rando.Companion.FreeEnterprise.Settings;
using FF.Rando.Companion.View;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;

namespace FF.Rando.Companion.FreeEnterprise.View;
public partial class KeyItemsPanel : FlowPanel<KeyItemSettings>
{
    public KeyItemsPanel() :base()
    {
        SpacingMode = SpacingMode.Columns;
    }

    public override DockStyle DefaultDockStyle => DockStyle.Top;

    protected override Control[] GenerateControls(ISeed seed) 
        => (Game?.KeyItems ?? []).Select(ki => new KeyItemControl(seed, Settings!, ki)).ToArray();

    protected override void Settings_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        base.Settings_PropertyChanged(sender, e);
        if (e.PropertyName == nameof(KeyItemSettings.KeyItemStyle))
            Arrange();
    }
}
