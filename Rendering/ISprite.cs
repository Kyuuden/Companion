using KGySoft.Drawing.Imaging;
using System;
using System.Drawing;

namespace FF.Rando.Companion.Rendering;

public interface ISprite : IDisposable
{
    Size Size { get; }
    Palette? Palette { get; }
    IReadableBitmapData RenderData(bool greyscale = false);
    Bitmap Render(bool greyscale = false);
}