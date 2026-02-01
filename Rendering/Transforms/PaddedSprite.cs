using FF.Rando.Companion.Rendering;
using KGySoft.Drawing.Imaging;
using System.Drawing;

namespace FF.Rando.Companion.Rendering.Transforms;

internal class PaddedSprite(ISprite source, Size size, HorizontalAlignment horizontalAlignment, VerticalAlignment verticalAlignment) : Sprite(source.Palette!), ITemporarySprite
{
    public override Size Size => size;

    protected override IReadableBitmapData RenderColorData()
    {
        var sourceData = source.RenderData();
        var data = BitmapDataFactory.CreateBitmapData(size, sourceData.PixelFormat.ToKnownPixelFormat(), sourceData.Palette);

        var x = horizontalAlignment switch
        {
            HorizontalAlignment.Center => (size.Width - sourceData.Width) / 2,
            HorizontalAlignment.Right => size.Width - sourceData.Width,
            _ => 0 
        };

        var y = verticalAlignment switch
        {
            VerticalAlignment.Center => (size.Height - sourceData.Height) / 2,
            VerticalAlignment.Bottom => size.Height - sourceData.Height,
            _ => 0
        };

        sourceData.DrawInto(data, new Point(x,y));
        return data;
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

