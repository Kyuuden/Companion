using FF.Rando.Companion.Settings;
using FF.Rando.Companion.View;
using KGySoft.Drawing.Imaging;
using System.Drawing;

namespace FF.Rando.Companion.FreeEnterprise.View;

public abstract class FlowPanel<TSettings> : FlowPanel<ISeed, TSettings> where TSettings : PanelSettings
{
    protected override Bitmap? GenerateBackgroundImage(Size unscaledSize)
    {
        return Game?.Font.RenderBox(unscaledSize.Width / 8, unscaledSize.Height / 8)?.ToBitmap();
    }
}