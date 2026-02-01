using FF.Rando.Companion.Extensions;
using FF.Rando.Companion.Rendering;
using KGySoft.Drawing.Imaging;
using System.Drawing;

namespace FF.Rando.Companion.Rendering.Transforms;

internal class OverlayedSprite(ISprite source, ISprite overlay, Point destination) : Sprite(source.Palette!), ITemporarySprite
{
    public override Size Size => source.Size;

    protected override IReadableBitmapData RenderColorData()
    {
        IReadWriteBitmapData data;
        if (source.Palette != null && overlay.Palette != null)
        {
            data = BitmapDataFactory.CreateBitmapData(source.Size, KnownPixelFormat.Format8bppIndexed, PaletteExtensions.Combine(source.Palette, overlay.Palette));
        }
        else
        {
            data = BitmapDataFactory.CreateBitmapData(source.Size);
        }

        source.RenderData().DrawInto(data);
        overlay.RenderData().DrawInto(data, destination);
        return data;
    }

    protected override void Dispose(bool disposing)
    {
        base.Dispose(disposing);
        if (disposing)
        {
            if (source is ITemporarySprite)
                source.Dispose();

            if (overlay is ITemporarySprite)
                overlay.Dispose();
        }
    }
}

