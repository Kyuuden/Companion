using FF.Rando.Companion.Extensions;
using KGySoft.Drawing.Imaging;
using System.Drawing;

namespace FF.Rando.Companion.Rendering.Transforms;

internal class FlippedRotatedSprite(ISprite source, RotateFlipType rotateFlipType) : Sprite(source.Palette!), ITemporarySprite
{
    protected override IReadableBitmapData RenderColorData()
    {
        return source.RenderData().CopyRotateFlip(rotateFlipType);
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