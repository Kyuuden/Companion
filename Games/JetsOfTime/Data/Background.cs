using FF.Rando.Companion.Extensions;
using FF.Rando.Companion.Rendering;
using KGySoft.CoreLibraries;
using KGySoft.Drawing.Imaging;
using KGySoft.Drawing.Shapes;
using System;
using System.Drawing;
using System.Linq;
using Brush = KGySoft.Drawing.Shapes.Brush;

namespace FF.Rando.Companion.Games.JetsOfTime.Data;

internal class Background : IDisposable
{
    private readonly IReadWriteBitmapData _background;
    private readonly IReadWriteBitmapData _topLeft;
    private readonly IReadWriteBitmapData _topRight;
    private readonly IReadWriteBitmapData _bottomRight;
    private readonly IReadWriteBitmapData _bottomLeft;
    private readonly IReadWriteBitmapData _top;
    private readonly IReadWriteBitmapData _bottom;
    private readonly IReadWriteBitmapData _left;
    private readonly IReadWriteBitmapData _right;
    private bool disposedValue;

    public Background(byte[] data, Palette palette)
    {
        var tiles = data.ReadMany<byte[]>(0x20 * 8).Select(t => t.DecodeTile(4)).ToArray();
        _background = BitmapDataFactory.CreateBitmapData(32, 32, KnownPixelFormat.Format8bppIndexed, palette);

        _topLeft = BitmapDataFactory.CreateBitmapData(8, 8, KnownPixelFormat.Format8bppIndexed, palette);
        _topRight = BitmapDataFactory.CreateBitmapData(8, 8, KnownPixelFormat.Format8bppIndexed, palette);
        _bottomLeft = BitmapDataFactory.CreateBitmapData(8, 8, KnownPixelFormat.Format8bppIndexed, palette);
        _bottomRight = BitmapDataFactory.CreateBitmapData(8, 8, KnownPixelFormat.Format8bppIndexed, palette);

        _top = BitmapDataFactory.CreateBitmapData(16, 8, KnownPixelFormat.Format8bppIndexed, palette);
        _bottom = BitmapDataFactory.CreateBitmapData(16, 8, KnownPixelFormat.Format8bppIndexed, palette);
        _left = BitmapDataFactory.CreateBitmapData(8, 16, KnownPixelFormat.Format8bppIndexed, palette);
        _right = BitmapDataFactory.CreateBitmapData(8, 16, KnownPixelFormat.Format8bppIndexed, palette);

        tiles[0].DrawInto(_topLeft, new Point(0, 0));
        tiles[1].DrawInto(_top, new Point(0, 0));
        tiles[2].DrawInto(_top, new Point(8, 0));
        tiles[3].DrawInto(_topRight, new Point(0, 0));
        tiles[4].DrawInto(_left, new Point(0, 0));
        tiles[5].DrawInto(_right, new Point(0, 0));
        tiles[6].DrawInto(_left, new Point(0, 8));
        tiles[7].DrawInto(_right, new Point(0, 8));
        tiles[8].DrawInto(_bottomLeft, new Point(0, 0));
        tiles[9].DrawInto(_bottom, new Point(0, 0));
        tiles[10].DrawInto(_bottom, new Point(8, 0));
        tiles[11].DrawInto(_bottomRight, new Point(0, 0));

        tiles[12].DrawInto(_background, new Point(0, 0));
        tiles[13].DrawInto(_background, new Point(8, 0));
        tiles[14].DrawInto(_background, new Point(0, 8));
        tiles[15].DrawInto(_background, new Point(8, 8));

        tiles[16].DrawInto(_background, new Point(16, 0));
        tiles[17].DrawInto(_background, new Point(24, 0));
        tiles[18].DrawInto(_background, new Point(16, 8));
        tiles[19].DrawInto(_background, new Point(24, 8));

        tiles[12].DrawInto(_background, new Point(16, 16));
        tiles[13].DrawInto(_background, new Point(24, 16));
        tiles[14].DrawInto(_background, new Point(16, 24));
        tiles[15].DrawInto(_background, new Point(24, 24));

        tiles[16].DrawInto(_background, new Point(0, 16));
        tiles[17].DrawInto(_background, new Point(8, 16));
        tiles[18].DrawInto(_background, new Point(0, 24));
        tiles[19].DrawInto(_background, new Point(8, 24));
    }

    public void FillBackground(IReadWriteBitmapData destination)
    {
        var texture = Brush.CreateTexture(_background, hasAlphaHint: false);
        destination.FillRectangle(texture, new Rectangle(new Point(0, 0), destination.Size));
    }

    public Bitmap? Render(Size size, bool drawborders, bool drawBackground)
    {
        if (size.Height <= 0 || size.Width <= 0)
            return null;

        using var bmp = BitmapDataFactory.CreateBitmapData(size, KnownPixelFormat.Format8bppIndexed, _background.Palette);

        if (drawBackground)
        {
            var bgX = drawborders ? _left.Width : 0;
            var bgY = drawborders ? _top.Height : 0;

            while (size.Width > bgX)
            {
                while (size.Height > bgY)
                {
                    _background.CopyTo(bmp, new Point(bgX, bgY));
                    bgY += _background.Height;
                }

                bgX += _background.Width;
                bgY = drawborders ? _top.Height : 0;
            }
        }

        if (drawborders)
        {
            var borderX = _topLeft.Width;
            while (size.Width > borderX)
            {
                _top.CopyTo(bmp, new Point(borderX, 0));
                _bottom.CopyTo(bmp, new Point(borderX, size.Height - _bottom.Height));
                borderX += _top.Width;
            }

            var borderY = _topLeft.Height;
            while (size.Height > borderY)
            {
                _left.CopyTo(bmp, new Point(0, borderY));
                _right.CopyTo(bmp, new Point(size.Width - _right.Width, borderY));
                borderY += _left.Width;
            }

            _topLeft.CopyTo(bmp);
            _topRight.CopyTo(bmp, new Point(size.Width - _topRight.Width, 0));
            _bottomLeft.CopyTo(bmp, new Point(0, size.Height - _bottomLeft.Height));
            _bottomRight.CopyTo(bmp, new Point(size.Width - _topRight.Width, size.Height - _bottomLeft.Height));
        }

        return bmp.ToBitmap();
    }

    public IReadWriteBitmapData RenderBox(Size size)
    {
        var bmp = BitmapDataFactory.CreateBitmapData(size);

        _topLeft.DrawInto(bmp, new Point(0, 0));
        _topRight.DrawInto(bmp, new Point(size.Width - 8, 0));
        _bottomLeft.DrawInto(bmp, new Point(0, size.Height - 8));
        _bottomRight.DrawInto(bmp, new Point(size.Width - 8, size.Height - 8));

        bmp.FillRectangle(Brush.CreateTexture(_top), new Rectangle(8, 0, size.Width - 16, 8));
        bmp.FillRectangle(Brush.CreateTexture(_bottom), new Rectangle(8, size.Height - 8, size.Width - 16, 8));
        bmp.FillRectangle(Brush.CreateTexture(_left), new Rectangle(0, 8, 8, size.Height - 16));
        bmp.FillRectangle(Brush.CreateTexture(_right), new Rectangle(size.Width - 8, 8, 8, size.Height - 16));

        return bmp;
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            if (disposing)
            {
                _background.Dispose();
                _topLeft.Dispose();
                _topRight.Dispose();
                _bottomRight.Dispose();
                _bottomLeft.Dispose();
                _top.Dispose();
                _bottom.Dispose();
                _left.Dispose();
                _right.Dispose();
            }

            disposedValue = true;
        }
    }

    public void Dispose()
    {
        // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}



