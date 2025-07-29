using System.Linq;
using System.Windows.Forms;
using FF.Rando.Companion.FreeEnterprise.Settings;

namespace FF.Rando.Companion.FreeEnterprise.View;

public class PartyControl : FlowPanelControl<PartySettings>
{
    public PartyControl()
        : base()
    {
        FlowDirection = FlowDirection.TopDown;
        WrapContents = false;
    }

    protected override Control[] GenerateControls(ISeed seed) 
        => (Seed?.Party ?? []).Select(c => new CharacterControl(seed, Settings!, c)).ToArray();
}