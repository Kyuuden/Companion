using KGySoft.Drawing.Imaging;
using System.Drawing;

namespace FF.Rando.Companion.Extensions;

public static class ColorExtensions
{
    public static Color32 ToColor(this uint snesColor)
    {
        var red = snesColor & 0x1F;
        var green = (snesColor >> 5) & 0x1F;
        var blue = (snesColor >> 10) & 0x1F;
        return new Color32((byte)(red * 255 / 31.0), (byte)(green * 255 / 31.0), (byte)(blue * 255 / 31.0));
    }

    public static Color32 ToColor(this ushort snesColor)
    {
        var red = snesColor & 0x1F;
        var green = (snesColor >> 5) & 0x1F;
        var blue = (snesColor >> 10) & 0x1F;
        return new Color32((byte)(red * 255 / 31.0), (byte)(green * 255 / 31.0), (byte)(blue * 255 / 31.0));
    }
}
