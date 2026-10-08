using KGySoft.Drawing.Imaging;
using System.Collections.Generic;

namespace FF.Rando.Companion.Extensions;

public static class ColorExtensions
{
    private static readonly Dictionary<int, byte> _iColorLookupTable = [];
    private static readonly Dictionary<uint, byte> _uColorLookupTable = [];
    static ColorExtensions()
    {
        for (var i = 0; i < 32; i++)
        {
            _iColorLookupTable[i] = (byte)(i / 31.0 * 255);
            _uColorLookupTable[(uint)i] = (byte)(i / 31.0 * 255);
        }
    }

    public static Color32 ToColor(this uint snesColor)
    {
        var red = snesColor & 0x1F;
        var green = (snesColor >> 5) & 0x1F;
        var blue = (snesColor >> 10) & 0x1F;
        return new Color32(_uColorLookupTable[red], _uColorLookupTable[green], _uColorLookupTable[blue]);
    }

    public static Color32 ToColor(this ushort snesColor)
    {
        var red = snesColor & 0x1F;
        var green = (snesColor >> 5) & 0x1F;
        var blue = (snesColor >> 10) & 0x1F;
        return new Color32(_iColorLookupTable[red], _iColorLookupTable[green], _iColorLookupTable[blue]);
    }
}
