using FF.Rando.Companion.Games.FreeEnterprise;
using FF.Rando.Companion.Settings;
using FF.Rando.Companion.View;
using KGySoft.Drawing.Imaging;
using System.Drawing;

namespace FF.Rando.Companion.Games.FreeEnterprise.View;

public abstract class FlowPanel<TSettings> : FlowPanelEx<ISeed, TSettings> where TSettings : PanelSettings
{
    protected override Bitmap? GenerateBackgroundImage(Size unscaledSize)
    {
        return Game?.Font.RenderBox(unscaledSize.Width / 8, unscaledSize.Height / 8)?.ToBitmap();
    }
}