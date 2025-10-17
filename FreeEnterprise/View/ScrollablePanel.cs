using FF.Rando.Companion.Settings;
using FF.Rando.Companion.View;
using KGySoft.Drawing.Imaging;
using System.Drawing;

namespace FF.Rando.Companion.FreeEnterprise.View;

public abstract class ScrollablePanel<TSettings> : ScrollablePanel<ISeed, TSettings> where TSettings : PanelSettings
{
    protected override IReadableBitmapData GenerateArrow(Arrow direction)
    {
        return Game?.Sprites.GetArrow(direction)!;
    }

    protected override IReadWriteBitmapData GenerateBackgroundImage(Size unscaledSize)
    {
        return Game?.Font.RenderBox(unscaledSize.Width / 8, unscaledSize.Height / 8, Game.Sprites.GreyScaleStickerPalette)!;
    }

    protected override IReadableBitmapData GeneragePageCounter(int current, int total)
    {
        return Game?.Font.RenderText($"{current}/{total}", TextMode.Normal)!;
    }
}
