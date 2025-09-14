using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using FF.Rando.Companion.MysticQuestRandomizer.Settings;

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
}
