using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizHawk.FreeEnterprise.Companion
{
    public class RenderingSettings
    {
        public RenderingSettings() { }

        public int TileSize => Scale(8);
        public float TileSizeF => ScaleF(8);

        public double ViewScale => Properties.Settings.Default.ViewScale / 100.0;

        public int Scale(int size)
            => (int)(size * ViewScale);

        public float ScaleF(float size)
            => (float)(size * ViewScale);
    }
}
