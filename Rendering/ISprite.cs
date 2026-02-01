using FF.Rando.Companion.Extensions;
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


public interface ITransform
{
    IReadWriteBitmapData ApplyTo(IReadWriteBitmapData IReadWriteBitmapData);
}

public class Crop(Rectangle Rectangle) : ITransform
{
    public IReadWriteBitmapData ApplyTo(IReadWriteBitmapData data)
    {
        data.Clip(Rectangle);
        return data;
    }
}

public class Overlay(ISprite sprite) : ITransform
{
    public IReadWriteBitmapData ApplyTo(IReadWriteBitmapData data)
    {
        if (data.Palette != null && sprite.Palette != null)
            data.TrySetPalette(PaletteExtensions.Combine(data.Palette, sprite.Palette));

        sprite.RenderData().DrawInto(data);
        return data;
    }
}

public class Resize(Size Size) : ITransform
{
    public IReadWriteBitmapData ApplyTo(IReadWriteBitmapData data)
    {
        return data.Resize(Size, keepAspectRatio: true);
    }
}