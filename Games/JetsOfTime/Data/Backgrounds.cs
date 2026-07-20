using FF.Rando.Companion.Extensions;
using FF.Rando.Companion.MemoryManagement;
using KGySoft.Drawing.Imaging;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace FF.Rando.Companion.Games.JetsOfTime.Data;

internal class Backgrounds : IDisposable
{
    private readonly List<Background> _backgrounds = [];
    private bool disposedValue;

    public Backgrounds(IMemorySpace rom)
    {
        var backgroundData = rom.ReadBytes(Addresses.ROM.Backgrounds).AsReadOnlySpan();
        var palettes = rom.ReadBytes(Addresses.ROM.BackgroundPalettes).ReadMany<byte[]>(0, 8 * 0x10, 8).Select(p => p.DecodePalette(new Color32(), 8)).ToList();

        for (var i = 0; i < 8; i++)
        {
            var slice = backgroundData.Slice(i * 0x280, 0x280);
            _backgrounds.Add(new Background(slice.ToArray(), palettes[i]));
        }
    }

    public void FillBackground(int backgroundId, IReadWriteBitmapData destination)
    {
        _backgrounds[backgroundId].FillBackground(destination);
    }

    public IReadWriteBitmapData RenderBox(int backgroundId, Size size)
    {
        return _backgrounds[backgroundId].RenderBox(size);
    }

    public Bitmap? Render(int backgroundId, Size size, bool drawBorder, bool drawBg)
    {
        return _backgrounds[backgroundId].Render(size, drawBorder, drawBg);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            if (disposing)
            {
                foreach (var value in _backgrounds) value.Dispose();
                _backgrounds.Clear();
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



