using FF.Rando.Companion.Games.JetsOfTime.Tracking;
using FF.Rando.Companion.Settings;
using FF.Rando.Companion.View;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace FF.Rando.Companion.Games.JetsOfTime.View;

internal class CharacterControl : ImageControl<Seed, Character>
{
    private readonly ToolTip _toolTip;

    public CharacterControl(Seed seed, PanelSettings settings, Character character)
        : base(seed, settings, character)
    {
        _toolTip = new ToolTip
        {
            ShowAlways = true
        };
        _toolTip.SetToolTip(this, character.AltText);

        BackColor = Color.Transparent;
    }
}