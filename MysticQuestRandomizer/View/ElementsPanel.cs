using FF.Rando.Companion.MysticQuestRandomizer.Settings;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;

namespace FF.Rando.Companion.MysticQuestRandomizer.View;
internal partial class ElementsPanel : FlowPanel<ElementsSettings>
{
    public ElementsPanel() :base()
    {
    }

    public override DockStyle DefaultDockStyle => DockStyle.Top;

    protected override Control[] GenerateControls(Seed seed)
    {
        if (Game == null || Settings == null)
            return [];

        return Game.Elements.Select(e=> new ElementControl(Game, Settings, e)).ToArray();
    }

    protected override void Settings_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        base.Settings_PropertyChanged(sender, e);
        if (e.PropertyName == nameof(ElementsSettings.ElementsStyle))
            Arrange();
    }
}
