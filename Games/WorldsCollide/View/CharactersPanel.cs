using FF.Rando.Companion.Games.WorldsCollide;
using FF.Rando.Companion.Games.WorldsCollide.Settings;
using FF.Rando.Companion.View;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;

namespace FF.Rando.Companion.Games.WorldsCollide.View;

public partial class CharactersPanel : FlowPanelEx<CharacterSettings>
{
    public CharactersPanel() : base()
    {
        SpacingMode = SpacingMode.Columns;
    }

    public override DockStyle DefaultDockStyle => DockStyle.Top;

    public override void InitializeDataSources(Seed game, CharacterSettings settings)
    {
        game.Settings.PropertyChanged += Settings_PropertyChanged;
        base.InitializeDataSources(game, settings);
    }

    protected override Control[] GenerateControls(Seed seed)
        => (Game?.Characters ?? []).Select(ch => new CharacterControl(seed, Settings!, ch)).ToArray();

    protected override void Settings_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        base.Settings_PropertyChanged(sender, e);
        if (e.PropertyName == nameof(WorldsCollideSettings.CheckIcons))
            Arrange();
    }

    protected override void Dispose(bool disposing)
    {
        base.Dispose(disposing);
        if (disposing)
        {
            if (Game != null) Game.Settings.PropertyChanged -= Settings_PropertyChanged;
        }
    }
}
