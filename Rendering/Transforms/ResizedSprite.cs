using FF.Rando.Companion.Rendering;
using KGySoft.Drawing.Imaging;
using System.Drawing;

namespace FF.Rando.Companion.Rendering.Transforms;

internal class ResizedSprite(ISprite source, Size size) : Sprite(source.Palette!), ITemporarySprite
{
    public override Size Size => size;

    protected override IReadableBitmapData RenderColorData()
    {
        var scalingMode = KGySoft.Drawing.ScalingMode.Auto;
        if (source.Size.Width * 2 == size.Width && source.Size.Height * 2 == size.Height)
            scalingMode = KGySoft.Drawing.ScalingMode.NearestNeighbor;

        return source.RenderData().Resize(Size, scalingMode, keepAspectRatio: true);
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
