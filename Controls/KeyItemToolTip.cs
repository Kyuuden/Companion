using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BizHawk.FreeEnterprise.Companion.Controls
{
    public class KeyItemToolTip : ToolTip
    {
        public KeyItemToolTip(RenderingSettings renderSettings, IContainer container)
            :base(container)
        {
            OwnerDraw = true;            
            Popup += OnPopup;
            Draw += OnDraw;
            this.renderSettings = renderSettings;
        }

        private RomData? _data;
        private readonly RenderingSettings renderSettings;

        public void Initialize(RomData romData)
        {
            _data = romData;
        }

        private void OnDraw(object sender, DrawToolTipEventArgs e)
        {
            if (_data == null)
                return;

            _data.Font.RenderBox(e.Graphics, 0, 0, 30, e.Bounds.Height/ renderSettings.TileSize);
            _data.Font.RenderText(e.Graphics, renderSettings.TileSize, renderSettings.TileSize, 28, Description ?? string.Empty, Sprites.TextMode.Normal,0);

            if (!string.IsNullOrEmpty(ReceivedFrom))
            {
                if (FoundAt.HasValue && FoundAt.Value > TimeSpan.Zero)
                    _data.Font.RenderText(e.Graphics, renderSettings.TileSize, renderSettings.Scale(40), 28, $"Received at {FoundAt.Value.ToString("hh':'mm':'ss")} from:", Sprites.TextMode.Special, 0);
                else
                    _data.Font.RenderText(e.Graphics, renderSettings.TileSize, renderSettings.Scale(40), 28, "Received from:", Sprites.TextMode.Special, 0);
                _data.Font.RenderText(e.Graphics, renderSettings.TileSize, renderSettings.Scale(48), 28, ReceivedFrom ?? string.Empty, Sprites.TextMode.Special, 0);
            }
            else if (!FoundAt.HasValue)
            {
                _data.Font.RenderText(e.Graphics, renderSettings.TileSize, renderSettings.Scale(40), 28, "(not yet found)", Sprites.TextMode.Disabled, 0);
            }

            if (FoundAt.HasValue)
            {
                 if (!UsedAt.HasValue)
                    _data.Font.RenderText(e.Graphics, renderSettings.Scale(168), renderSettings.Scale(32), 28, "(unused)", Sprites.TextMode.Disabled, 0);
                 else if (UsedAt.Value > TimeSpan.Zero)
                    _data.Font.RenderText(e.Graphics, renderSettings.TileSize, renderSettings.Scale(56), 28, $"Used at {FoundAt.Value.ToString("hh':'mm':'ss")}", Sprites.TextMode.Special, 0);
            }
        }

        private void OnPopup(object sender, PopupEventArgs e)
        {
            var height = 64 + (UsedAt.HasValue && UsedAt.Value > TimeSpan.Zero ? 8 : 0);

            e.ToolTipSize = new Size(renderSettings.Scale(240), renderSettings.Scale(height));
        }

        public string? Description { get; set; }
        public TimeSpan? FoundAt { get; set; }
        public TimeSpan? UsedAt { get; set; }
        public string? ReceivedFrom { get; set; }

    }
}
