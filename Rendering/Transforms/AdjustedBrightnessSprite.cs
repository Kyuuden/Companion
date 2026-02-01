using KGySoft.Drawing.Imaging;

namespace FF.Rando.Companion.Rendering.Transforms;

internal class AdjustedBrightnessSprite(ISprite source, float adjustment) : Sprite(source.Palette!), ITemporarySprite
{
    protected override IReadableBitmapData RenderColorData()
    {
        var clone = source.RenderData().Clone();
        clone.AdjustBrightness(adjustment);
        return clone;
    }

    protected override void Dispose(bool disposing)
    {
        base.Dispose(disposing);
        if (disposing)
        {
            if (source is ITemporarySprite)
                source.Dispose();
        }
    }
}

