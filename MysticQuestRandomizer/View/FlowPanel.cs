using FF.Rando.Companion.Settings;
using FF.Rando.Companion.View;
using KGySoft.Drawing.Imaging;
using System.Drawing;

namespace FF.Rando.Companion.MysticQuestRandomizer.View;
internal abstract class FlowPanel<TSettings> : FlowPanel<Seed, TSettings> where TSettings : PanelSettings
{
    protected FlowPanel()
        :base()
    {
        SpacingMode = SpacingMode.Columns;
    }

    protected override Bitmap? GenerateBackgroundImage(Size unscaledSize)
    {
        return Game?.Font.RenderBox(unscaledSize.Width / 8, unscaledSize.Height / 8)?.ToBitmap();
    }
}
