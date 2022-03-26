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

        public void Initialize(RomData romData)
        {
            _data = romData;
        }

        private void OnDraw(object sender, DrawToolTipEventArgs e)
        {
            if (_data == null)
                return;

            _data.Font.RenderBox(e.Graphics, 0, 0, 30, 8);
            _data.Font.RenderText(e.Graphics, 8, 8, 28, Description ?? string.Empty, Sprites.TextMode.Normal,0);

            if (!string.IsNullOrEmpty(ReceivedFrom))
            {
                _data.Font.RenderText(e.Graphics, 8, 40, 28, "Received from:", Sprites.TextMode.Special, 0);
                _data.Font.RenderText(e.Graphics, 8, 48, 28, ReceivedFrom ?? string.Empty, Sprites.TextMode.Special, 0);
            }
            else if (!IsFound)
            {
                _data.Font.RenderText(e.Graphics, 8, 40, 28, "(not yet found)", Sprites.TextMode.Disabled, 0);
            }

            if (IsFound && !IsUsed)
                _data.Font.RenderText(e.Graphics, 168, 32, 28, "(unused)", Sprites.TextMode.Disabled, 0);
        }

        private void OnPopup(object sender, PopupEventArgs e)
        {
            e.ToolTipSize = new Size(240, 64);
        }

        public string? Description { get; set; }
        public bool IsFound { get; set; }
        public bool IsUsed { get; set; }
        public string? ReceivedFrom { get; set; }

    }
}
