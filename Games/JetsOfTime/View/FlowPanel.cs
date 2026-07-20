using FF.Rando.Companion.Games.JetsOfTime.Settings;
using FF.Rando.Companion.Settings;
using FF.Rando.Companion.View;
using System.ComponentModel;
using System.Drawing;

namespace FF.Rando.Companion.Games.JetsOfTime.View;

internal abstract class FlowPanel<TSettings> : FlowPanelEx<Seed, TSettings> where TSettings : PanelSettings
{
    protected override void Settings_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        base.Settings_PropertyChanged(sender, e);

        switch (e.PropertyName)
        {
            case nameof(Seed.SelectedBackground):
            case nameof(JetsOfTimeBorderSettings.BackgroundsEnabled):
                ReplaceBackgroundImage(true);
                break;
        }
    }

    protected override Bitmap? GenerateBackgroundImage(Size unscaledSize)
    {
        if (Game == null) 
            return null;

        return Game?.Backgrounds.Render(
            Game.SelectedBackground,
            unscaledSize,
            Game.Settings.BorderSettings.BordersEnabled,
            ((JetsOfTimeBorderSettings)Game.Settings.BorderSettings).BackgroundsEnabled);
    }
}
