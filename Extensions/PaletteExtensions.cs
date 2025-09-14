using KGySoft.Drawing.Imaging;
using System.Collections.Generic;
using System.Linq;

namespace FF.Rando.Companion.Extensions;

public static class PaletteExtensions
{
    public static Palette Combine(params Palette[] palettes)
    {
        return new Palette(palettes.SelectMany(p => p.GetEntries()).Distinct());
    }

    public static Palette Combine(IEnumerable<Palette> palettes)
    {
        return new Palette(palettes.SelectMany(p => p.GetEntries()).Distinct());
    }
}
