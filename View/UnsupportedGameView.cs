using FF.Rando.Companion.Settings;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace FF.Rando.Companion.View;
internal class UnsupportedGameView : RichTextBox
{
    public UnsupportedGameView(ISettings settings)
    {
        var gameDescriptions = settings.GameSettings.Select(gs => gs.Value.Description).OrderBy(s => s);
        Margin = new Padding(20);
        BackColor = Color.Black;
        ForeColor = Color.White;
        SetStyle(ControlStyles.Selectable, false);
        BorderStyle = BorderStyle.None;
        Dock = DockStyle.Fill;
        Multiline = true;
        Text = "Please load a supported game to initialize tracking.\nCurrently Supported games are:\n\n" + string.Join("\n", gameDescriptions);
    }
}
