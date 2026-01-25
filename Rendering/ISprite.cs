using KGySoft.Drawing.Imaging;
using System.Drawing;

namespace FF.Rando.Companion.Rendering;

public interface ISprite
{
    Size Size { get; }
    IReadableBitmapData RenderData(bool greyscale = false);
    Bitmap Render(bool greyscale = false);
}
