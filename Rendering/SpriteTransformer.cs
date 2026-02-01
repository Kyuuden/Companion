using FF.Rando.Companion.Rendering.Transforms;
using System.Drawing;

namespace FF.Rando.Companion.Rendering;

public enum HorizontalAlignment
{
    Center, Left, Right
}

public enum VerticalAlignment
{
    Center, Top, Bottom
}

public static class SpriteTransformer
{
    public static ISprite Crop(this ISprite sprite, Rectangle rectangle) => new CroppedSprite(sprite, rectangle);

    public static ISprite Overlay(this ISprite sprite, ISprite other, Point destination) => new OverlayedSprite(sprite, other, destination);

    public static ISprite Resize(this ISprite sprite, Size size) => new ResizedSprite(sprite, size);

    public static ISprite Pad(this ISprite sprite, Size size, HorizontalAlignment horizontalAlignment, VerticalAlignment verticalAlignment) 
        => new PaddedSprite(sprite, size, horizontalAlignment, verticalAlignment);

    public static ISprite RotateFlip(this ISprite sprite, RotateFlipType rotateFlipType)
        => new FlippedRotatedSprite(sprite, rotateFlipType);

    public static ISprite Greyscale(this ISprite sprite) => new GreyscaleSprite(sprite);

    public static ISprite AdjustBrightness(this ISprite sprite, float adjustment) => new AdjustedBrightnessSprite(sprite, adjustment);
}