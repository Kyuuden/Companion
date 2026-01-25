using FF.Rando.Companion.Settings;
using FF.Rando.Companion.View;
using System.Drawing;

namespace FF.Rando.Companion.WorldsCollide.View;

public abstract class FlowPanel<TSettings> : FlowPanel<Seed, TSettings> where TSettings : PanelSettings
{
    protected override Bitmap? GenerateBackgroundImage(Size unscaledSize)
    {
        return null;
    }
}
