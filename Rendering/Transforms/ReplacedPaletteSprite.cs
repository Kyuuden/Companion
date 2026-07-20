using KGySoft.Drawing.Imaging;

namespace FF.Rando.Companion.Rendering.Transforms;

internal class ReplacedPaletteSprite : Sprite, ITemporarySprite
{
    private readonly ISprite _source;
    private readonly Palette _palette;

    public ReplacedPaletteSprite(ISprite source, Palette palette) : base(palette)
    {
        _source = source;
        _palette = palette;
    }

    protected override IReadableBitmapData RenderColorData()
    {
        var clone = BitmapDataFactory.CreateBitmapData(_source.Size, KnownPixelFormat.Format8bppIndexed, _palette);
        _source.RenderData().DrawInto(clone);
        return clone;
    }

    protected override void Dispose(bool disposing)
    {
        base.Dispose(disposing);
        if (disposing)
        {
            if (_source is ITemporarySprite)
                _source.Dispose();
        }
    }
}

