using KGySoft.Drawing.Imaging;

namespace FF.Rando.Companion.Rendering.Transforms;

internal class CustomGreyscaleSrpite(ISprite source, ISprite greyscale) : Sprite(source.Palette!), ITemporarySprite
{
    protected override IReadableBitmapData RenderColorData()
    {
        return source.RenderData();
    }

    protected override IReadableBitmapData RenderGreyscaleData()
    {
        return greyscale.RenderData(true);
    }

    protected override void Dispose(bool disposing)
    {
        base.Dispose(disposing);
        if (disposing)
        {
            if (source is ITemporarySprite)
                source.Dispose();

            if (greyscale is ITemporarySprite)
                greyscale.Dispose();
        }
    }
}

