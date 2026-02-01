using KGySoft.Drawing.Imaging;
using System;
using System.Drawing;

namespace FF.Rando.Companion.Rendering;

public abstract class Sprite(Palette palette) : ISprite, IDisposable
{
    private Bitmap? _cached;
    private Bitmap? _cachedGreyScale;
    private IReadableBitmapData? _cachedData;
    private IReadableBitmapData? _cachedGreyScaleData;
    private bool disposedValue;

    public Palette Palette { get; } = palette;

    public virtual Size Size
    {
        get
        {
            var size = _cached?.Size ?? _cachedGreyScale?.Size ?? _cachedData?.Size ?? _cachedGreyScaleData?.Size;
            if (size.HasValue)
                return size.Value;

            return RenderData().Size;
        }
    }

    public IReadableBitmapData RenderData(bool greyscale = false)
    {
        if (greyscale && _cachedGreyScaleData != null)
            return _cachedGreyScaleData;

        if (!greyscale && _cachedData != null)
            return _cachedData;

        if (greyscale)
        {
            _cachedGreyScaleData = RenderGreyscaleData();
            return _cachedGreyScaleData;
        }

        _cachedData = RenderColorData();
        return _cachedData;
    }

    protected virtual IReadableBitmapData RenderGreyscaleData()
    {
        using var tmp = RenderColorData();
        var greyscale = tmp.ToGrayscale();
        greyscale.AdjustBrightness(-0.25f);
        return greyscale;
    }

    protected abstract IReadableBitmapData RenderColorData();

    public Bitmap Render(bool greyscale = false)
    {
        if (greyscale && _cachedGreyScale != null)
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
