using FF.Rando.Companion.Games.JetsOfTime.Settings;
using System.Linq;
using System.Windows.Forms;

namespace FF.Rando.Companion.Games.JetsOfTime.View;
internal partial class CharactersPanel : FlowPanel<CharacterSettings>
{
    public CharactersPanel() :base()
    {
        //SpacingMode = SpacingMode.Columns;
        FlowDirection = FlowDirection.TopDown;
        WrapContents = false;
    }

    //public override DockStyle DefaultDockStyle => DockStyle.Top;
    public override DockStyle DefaultDockStyle => DockStyle.Left;

    protected override Control[] GenerateControls(Seed seed) 
        => (Game?.State.Characters.Values ?? []).Select(character => new CharacterControl(seed, Settings!, character)).ToArray();
}


