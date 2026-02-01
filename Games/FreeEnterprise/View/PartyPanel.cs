using FF.Rando.Companion.Games.FreeEnterprise;
using FF.Rando.Companion.Games.FreeEnterprise.Settings;
using System.Linq;
using System.Windows.Forms;

namespace FF.Rando.Companion.Games.FreeEnterprise.View;

public class PartyPanel : FlowPanel<PartySettings>
{
    public PartyPanel()
        : base()
    {
        FlowDirection = FlowDirection.TopDown;
        WrapContents = false;
    }

    public override DockStyle DefaultDockStyle => DockStyle.Left;

    protected override Control[] GenerateControls(ISeed seed)
        => (Game?.Party ?? []).Select(c => new CharacterControl(seed, Settings!, c)).ToArray();
}