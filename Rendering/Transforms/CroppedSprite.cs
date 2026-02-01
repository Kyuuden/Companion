using KGySoft.Drawing.Imaging;
using System.Drawing;

namespace FF.Rando.Companion.Rendering.Transforms;

internal class CroppedSprite(ISprite source, Rectangle rectangle) : Sprite(source.Palette!), ITemporarySprite
{
    public override Size Size => rectangle.Size;

    protected override IReadableBitmapData RenderColorData()
    {
        return source.RenderData().Clone(rectangle);
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

