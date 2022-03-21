using System.Drawing;

namespace BizHawk.FreeEnterprise.Companion.Sprites
{
    public class ColorProcessor
    {
        public static Color GetColor(uint colordata)
        {
            var red = colordata & 0x1F;
            var green = (colordata >> 5) & 0x1F;
            var blue = (colordata >> 10) & 0x1F;
            return Color.FromArgb((int)(red * 255 / 31.0), (int)(green * 255 / 31.0), (int)(blue * 255 / 31.0));
        }
    }
}

