using FF.Rando.Companion.Games.WorldsCollide.Settings;
using FF.Rando.Companion.Settings;
using FF.Rando.Companion.View;
using System.ComponentModel;
using System.Drawing;

namespace FF.Rando.Companion.Games.WorldsCollide.View;

public abstract class FlowPanelEx<TSettings> : FlowPanelEx<Seed, TSettings> where TSettings : PanelSettings
{
    protected override void Settings_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        base.Settings_PropertyChanged(sender, e);

        switch (e.PropertyName)
        {
            case nameof(Seed.SelectedBackground):
            case nameof(Seed.BackgroundPalettes):
            case nameof(WorldsCollideBorderSettings.BackgroundsEnabled):
                ReplaceBackgroundImage(true);
                break;
            case nameof(Seed.SpriteSet):
                Arrange();
                break;
        }
    }

    protected override Bitmap? GenerateBackgroundImage(Size unscaledSize)
    {
        if (Game == null) return null;

        return Game?.Backgrounds.Render(
            Game.SelectedBackground, 
            unscaledSize, 
            Game.Settings.BorderSettings.BordersEnabled,
            ((WorldsCollideBorderSettings)Game.Settings.BorderSettings).BackgroundsEnabled);
    }
}
