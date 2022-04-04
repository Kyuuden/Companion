using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BizHawk.FreeEnterprise.Companion.Controls
{
    public partial class Bosses : TrackerControl<State.Bosses>
    {
        private Dictionary<Rectangle, BossType> _bossesByPosition = new Dictionary<Rectangle, BossType>();

        public Bosses(RenderingSettings renderingSettings)
            :base(renderingSettings, () => Properties.Settings.Default.BossesBorder)
        {
            InitializeComponent();
        }

        protected override string Header => "Bosses";

        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);
            var mouseBoss = _bossesByPosition.FirstOrDefault(kvp => kvp.Key.Contains(e.X, e.Y));

            if (Data != null && mouseBoss.Value != 0)
            {
                Data.Swap(mouseBoss.Value);
                Invalidate();
            }
        }

        protected override string? HeaderCount => $"{Data?.SeenCount,2}/{Data?.Seen.Count,2}";

        public override void RefreshSize()
        {
            var numOfItems = Data?.Seen.Count ?? 0;
            var iconsPerRow = UseableWidth / 40;
            var rows = iconsPerRow >= numOfItems ? 1 : (int)Math.Ceiling((double)numOfItems / (double)iconsPerRow);
            Height = RequestedHeight = MinimiumHeight + (rows * 32) + ((rows - 1) * 8) - 2;
            Invalidate();
        }

        protected override void PaintData(Graphics graphics, Rectangle rect)
        {
            if (RomData == null || Data == null)
                return;

            var sX = rect.X;
            var sY = rect.Y;
            var cWidth = rect.Width / 8;

            var keyIndex = 0;
            var iconsPerRow = cWidth * 8 / 40;
            var offset = (cWidth * 8 - iconsPerRow * 40) / 2;
            foreach (var item in Data.Seen)
            {
                _bossesByPosition[new Rectangle(sX + offset + ((keyIndex % iconsPerRow) * 40), sY + ((keyIndex / iconsPerRow) * 40), 32, 32)] = item.Key;

                var iconSet = IconLookup.GetIcons(item.Key);
                graphics.DrawImage(
                    item.Value ? iconSet.Found : iconSet.NotFound,
                    sX + offset + ((keyIndex % iconsPerRow) * 40),
                    sY + ((keyIndex / iconsPerRow) * 40), 32, 32);
                keyIndex++;
            }
        }
    }
}
