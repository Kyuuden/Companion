using KGySoft.Drawing.Imaging;
using System;

namespace FF.Rando.Companion.Rendering.SNES;

public static class ColorExtensions
{
    public static Color32 BlendWith(this Color32 foreColor, Color32 backColor, ColorMathMode mode)
    {
        return mode switch
        {
            ColorMathMode.Additive =>
                new Color32(
                    (byte)Math.Min(255, foreColor.R + backColor.R),
                    (byte)Math.Min(255, foreColor.G + backColor.G),
                    (byte)Math.Min(255, foreColor.B + backColor.B)),

            ColorMathMode.Subtractive =>
                new Color32(
                    (byte)Math.Max(0, foreColor.R - backColor.R),
                    (byte)Math.Max(0, foreColor.G - backColor.G),
                    (byte)Math.Max(0, foreColor.B - backColor.B)),

            ColorMathMode.Average =>
                new Color32(
                    (byte)Math.Min(255, (foreColor.R + backColor.R) >> 1),
                    (byte)Math.Min(255, (foreColor.G + backColor.G) >> 1),
                    (byte)Math.Min(255, (foreColor.B + backColor.B) >> 1)),

            ColorMathMode.SubtractiveAverage =>
                new Color32(
                    (byte)Math.Max(0, (foreColor.R - backColor.R) >> 1),
                    (byte)Math.Max(0, (foreColor.G - backColor.G) >> 1),
                    (byte)Math.Max(0, (foreColor.B - backColor.B) >> 1)),

            _ => foreColor
        };
    }
}
