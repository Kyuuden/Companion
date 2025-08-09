using System.Linq;
using System.Windows.Forms;
using FF.Rando.Companion.FreeEnterprise.Settings;

namespace FF.Rando.Companion.FreeEnterprise.View;

public class PartyPanel : FlowPanel<PartySettings>
{
    public PartyPanel()
        : base()
    {
        FlowDirection = FlowDirection.TopDown;
        WrapContents = false;
    }

    public override DockStyle DefaultDockStyle => DockStyle.Left;

    //public override bool CanHaveFillDockStyle => false;

    protected override Control[] GenerateControls(ISeed seed) 
        => (Seed?.Party ?? []).Select(c => new CharacterControl(seed, Settings!, c)).ToArray();
}