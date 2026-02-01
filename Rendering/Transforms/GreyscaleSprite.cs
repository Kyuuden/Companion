using KGySoft.Drawing.Imaging;

namespace FF.Rando.Companion.Rendering.Transforms;

internal class GreyscaleSprite(ISprite source) : Sprite(source.Palette!), ITemporarySprite 
{
    protected override IReadableBitmapData RenderColorData()
    {
        return source.RenderData().ToGrayscale();
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

