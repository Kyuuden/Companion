using FF.Rando.Companion.Rendering.Transforms;
using KGySoft.Drawing.Imaging;
using System.Drawing;

namespace FF.Rando.Companion.Rendering;

public class BasicSprite(IReadableBitmapData bitmapData) : Sprite(bitmapData.Palette!), ITemporarySprite
{
    private readonly IReadableBitmapData _bitmapData = bitmapData;

    protected override IReadableBitmapData RenderColorData()
    {
        return _bitmapData;
    }

    public override Size Size { get; } = bitmapData.Size;
}
