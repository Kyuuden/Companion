using BizHawk.Client.Common;
using BizHawk.Common;
using FF.Rando.Companion.MemoryManagement;
using KGySoft.Drawing.Imaging;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;

namespace FF.Rando.Companion.WorldsCollide.RomData;
internal class Backgrounds : IDisposable
{
    private readonly List<Background> _backgrounds = [];
    private bool disposedValue;

    public Backgrounds(IMemorySpace rom, IList<Palette> palettes)
    {
        var backgroundData = rom.ReadBytes(Addresses.ROM.Backgrounds).AsSpan();

        for (var i = 0; i < 8; i++)
        {
            var slice = backgroundData.Slice(i * 0x380, 0x380);
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

    public void UpdatePalettes(IList<Palette> palettes)
    {
        for (var i = 0; i < _backgrounds.Count; i++)
        {
            _backgrounds[i].Palette = palettes[i];
        }
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



