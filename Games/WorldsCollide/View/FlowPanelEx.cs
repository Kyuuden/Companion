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
                ReplaceBackgroundImage(true);
                break;
            case nameof(Seed.SpriteSet):
                Arrange();
                break;
        }
    }

    protected override Bitmap? GenerateBackgroundImage(Size unscaledSize)
    {
        return Game?.Backgrounds.Render(Game?.SelectedBackground ?? 0, unscaledSize);
    }
}
