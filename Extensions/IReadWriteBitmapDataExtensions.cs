using KGySoft.Drawing.Imaging;
using System;
using System.Drawing;

namespace FF.Rando.Companion.Extensions;

public static class IReadWriteBitmapDataExtensions
{
    public static IReadableBitmapData CopyRotateFlip(this IReadableBitmapData data, RotateFlipType rotateFlip)
    {
        switch (rotateFlip)
        {
            case RotateFlipType.RotateNoneFlipNone:
                return data.Clone();

            case RotateFlipType.Rotate90FlipNone:
            {
                var copy = BitmapDataFactory.CreateBitmapData(new Size(data.Height, data.Width), data.PixelFormat.ToKnownPixelFormat(), data.Palette);
                for (var y = 0; y < data.Height; y++)
                    for (var x = 0; x < data.Width; x++)
                        copy.SetColor32(y, x, data.GetColor32(x, y));

                return copy;
            }
            case RotateFlipType.Rotate180FlipNone:
            {
                var copy = data.Clone();
                for (var y = 0; y < data.Height; y++)
                    for (var x = 0; x < data.Width; x++)
                        copy.SetColor32(x, y, data.GetColor32(data.Width - x - 1, data.Height - y - 1));

                return copy;
            }

            case RotateFlipType.Rotate270FlipNone:
                return data.Clone(); //TODO

            case RotateFlipType.RotateNoneFlipX:
            {
                var copy = data.Clone();
                for (var y = 0; y < data.Height; y++)
                    for (var x = 0; x < data.Width; x++)
                        copy.SetColor32(x, y, data.GetColor32(data.Width - x -1, y));

                return copy;
            }
            case RotateFlipType.Rotate90FlipX:
                return data.Clone(); //TODO

            case RotateFlipType.Rotate180FlipX:
                return data.Clone(); //TODO

            case RotateFlipType.Rotate270FlipX:
            {
                var copy = BitmapDataFactory.CreateBitmapData(new Size(data.Height, data.Width), data.PixelFormat.ToKnownPixelFormat(), data.Palette);
                for (var y = 0; y < data.Height; y++)
                    for (var x = 0; x < data.Width; x++)
                        copy.SetColor32(y, x, data.GetColor32(data.Width - 1 - x, y));

                return copy;
            }

            default:
                throw new InvalidOperationException();
        }
    }
}