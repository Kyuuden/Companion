using KGySoft.Drawing.Imaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

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

    public static Palette DecodePalette(this byte[] paletteData, Color32? colorZero = null, byte maxColors = byte.MaxValue)
    {
        return DecodePalette(paletteData.AsReadOnlySpan(), colorZero, maxColors);
    }

    public static Palette DecodePalette(this ReadOnlySpan<byte> paletteData, Color32? colorZero = null, int maxColors = 256)
    {
        bool first = true;
        List<Color32> colors = [];
        foreach (var color in MemoryMarshal.Cast<byte, ushort>(paletteData))
        {
            if (first && colorZero.HasValue)
            {
                colors.Add(colorZero.Value);
                first = false;
            }
            else
            {
                colors.Add(color.ToColor());
            }
        }
        return new Palette(colors.Take(maxColors));
    }
}
