using KGySoft.Drawing.Imaging;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace FF.Rando.Companion.MysticQuestRandomizer.RomData;

internal class SpriteDefinition(List<byte[,]> tileData, byte[,] tileIndexes, Palette palette) : IDisposable
{
    private Bitmap? _cached;
    private Bitmap? _cachedGreyScale;
    private IReadableBitmapData? _cachedData;
    private IReadableBitmapData? _cachedGreyScaleData;

    private bool disposedValue;

    public IReadableBitmapData RenderData(bool greyscale = false)
    {
        if (greyscale && _cachedGreyScaleData != null)
            return _cachedGreyScaleData;

        if (_cachedData != null)
            return _cachedData;

        int width = tileIndexes.GetLength(1) * 8;
        int height = tileIndexes.GetLength(0) * 8;

        var data = BitmapDataFactory.CreateBitmapData(new Size(width, height), KnownPixelFormat.Format8bppIndexed, palette);

        for (var y = 0; y < height; y++)
        {
            for (var x = 0; x < width; x++)
            {
                data.SetColorIndex(x, y, tileData[tileIndexes[y / 8, x / 8]][x % 8, y % 8]);
            }
        }

        if (greyscale)
        {
            data.MakeGrayscale();
            data.AdjustBrightness(-0.5f);
            _cachedGreyScaleData = data;
            return _cachedGreyScaleData;
        }

        _cachedData = data;
        return data;
    }

    public Bitmap Render(bool greyscale = false)
    {
        if (greyscale &&  _cachedGreyScale != null)
            return _cachedGreyScale;

        if (_cached != null)
            return _cached;

        var bmp = RenderData(greyscale).ToBitmap();

        if (greyscale)
        {
            _cachedGreyScale = bmp;
            return _cachedGreyScale;
        }

        _cached = bmp;
        return _cached;
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            if (disposing)
            {
                _cached?.Dispose();
                _cached = null;
                _cachedGreyScale?.Dispose();
                _cachedGreyScale = null;
                _cachedData?.Dispose();
                _cachedData = null;
                _cachedGreyScaleData?.Dispose();
                _cachedGreyScaleData = null;
            }

            disposedValue = true;
        }
    }

    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}
