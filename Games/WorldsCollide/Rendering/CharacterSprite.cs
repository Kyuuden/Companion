using FF.Rando.Companion.Rendering;
using KGySoft.Drawing.Imaging;
using System.Drawing;

namespace FF.Rando.Companion.Games.WorldsCollide.Rendering;

public class CharacterSprite(IReadableBitmapData bitmapData) : Sprite(bitmapData.Palette!)
{
    private readonly IReadableBitmapData _bitmapData = bitmapData;

    protected override IReadableBitmapData RenderColorData()
    {
        return _bitmapData;
    }

    public override Size Size { get; } = bitmapData.Size;
}
