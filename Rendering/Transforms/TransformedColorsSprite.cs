using KGySoft.Drawing.Imaging;
using System;

namespace FF.Rando.Companion.Rendering.Transforms;

internal class TransformedColorsSprite(ISprite source, Func<Color32, Color32> colorTransformer) : Sprite(source.Palette!), ITemporarySprite
{
    protected override IReadableBitmapData RenderColorData()
    {
        var clone = source.RenderData().Clone();
        clone.TransformColors(colorTransformer);
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

