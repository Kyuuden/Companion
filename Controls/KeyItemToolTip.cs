using BizHawk.FreeEnterprise.Companion.Configuration;
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
        public KeyItemToolTip(IContainer container)
            :base(container)
        {
            OwnerDraw = true;            
            Popup += OnPopup;
            Draw += OnDraw;
            
        }

        private RomData? _data;
        private Settings? _settings;

        public void Initialize(RomData romData, Settings settings)
        {
            _data = romData;
            _settings = settings;
        }

        private void OnDraw(object sender, DrawToolTipEventArgs e)
        {
            if (_data == null || _settings == null)
                return;

            _data.Font.RenderBox(e.Graphics, 0, 0, 30, e.Bounds.Height/ _settings.TileSize);
            _data.Font.RenderText(e.Graphics, _settings.TileSize, _settings.TileSize, 28, Description ?? string.Empty, Sprites.TextMode.Normal,0);

            if (!string.IsNullOrEmpty(ReceivedFrom))
            {
                if (FoundAt.HasValue && FoundAt.Value > TimeSpan.Zero)
                    _data.Font.RenderText(e.Graphics, _settings.TileSize, _settings.Scale(40), 28, $"Received at {FoundAt.Value.ToString("hh':'mm':'ss")} from:", Sprites.TextMode.Special, 0);
                else
                    _data.Font.RenderText(e.Graphics, _settings.TileSize, _settings.Scale(40), 28, "Received from:", Sprites.TextMode.Special, 0);
                _data.Font.RenderText(e.Graphics, _settings.TileSize, _settings.Scale(48), 28, ReceivedFrom ?? string.Empty, Sprites.TextMode.Special, 0);
            }
            else if (!FoundAt.HasValue)
            {
                _data.Font.RenderText(e.Graphics, _settings.TileSize, _settings.Scale(40), 28, "(not yet found)", Sprites.TextMode.Disabled, 0);
            }

            if (FoundAt.HasValue)
            {
                 if (!UsedAt.HasValue)
                    _data.Font.RenderText(e.Graphics, _settings.Scale(168), _settings.Scale(32), 28, "(unused)", Sprites.TextMode.Disabled, 0);
                 else if (UsedAt.Value > TimeSpan.Zero)
                    _data.Font.RenderText(e.Graphics, _settings.TileSize, _settings.Scale(56), 28, $"Used at {UsedAt.Value.ToString("hh':'mm':'ss")}", Sprites.TextMode.Special, 0);
            }
        }

        private void OnPopup(object sender, PopupEventArgs e)
        {
            if (_data == null || _settings == null)
                return;

            var height = 64 + (UsedAt.HasValue && UsedAt.Value > TimeSpan.Zero ? 8 : 0);

            e.ToolTipSize = new Size(_settings.Scale(240), _settings.Scale(height));
        }

        public string? Description { get; set; }
        public TimeSpan? FoundAt { get; set; }
        public TimeSpan? UsedAt { get; set; }
        public string? ReceivedFrom { get; set; }

    }
}
