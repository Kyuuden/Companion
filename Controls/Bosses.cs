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
            :base(renderingSettings)
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
            var iconSize = Properties.Settings.Default.IconScaling ? RenderingSettings.Scale(32) : 32;
            var iconSpacing = Properties.Settings.Default.IconScaling ? RenderingSettings.TileSize : 8;
            var numOfItems = Data?.Seen.Count ?? 0;
            var iconsPerRow = UseableWidth / (iconSize + iconSpacing);
            var rows = iconsPerRow >= numOfItems ? 1 : (int)Math.Ceiling((double)numOfItems / (double)iconsPerRow);
            Height = RequestedHeight = RenderingSettings.SetToTileInterval(MinimiumHeight + rows * (iconSize + iconSpacing));
            _bossesByPosition.Clear();
            Invalidate();
        }

        protected override void PaintData(Graphics graphics, Rectangle rect)
        {
            if (RomData == null || Data == null)
                return;

            var sX = rect.X;
            var sY = rect.Y;

            var numOfItems = Data.Seen.Count;
            var iconSize = Properties.Settings.Default.IconScaling ? RenderingSettings.Scale(32) : 32;
            var iconSpacing = Properties.Settings.Default.IconScaling ? RenderingSettings.TileSize : 8;
            var iconsPerRow = UseableWidth / (iconSize + iconSpacing);
            var rows = iconsPerRow >= numOfItems ? 1 : (int)Math.Ceiling((double)numOfItems / (double)iconsPerRow);
            iconsPerRow = numOfItems / rows;

            while (iconsPerRow * rows < numOfItems)
                iconsPerRow++;

            var offset = (rect.Width - iconsPerRow * (iconSize + iconSpacing)) / 2;
            var index = 0;
            foreach (var item in Data.Seen)
            {
                var bossRect = new Rectangle(
                    sX + offset + ((index % iconsPerRow) * (iconSize + iconSpacing)),
                    sY + ((index / iconsPerRow) * (iconSize + iconSpacing)),
                    iconSize,
                    iconSize);

                _bossesByPosition[bossRect] = item.Key;

                var iconSet = IconLookup.GetIcons(item.Key);
                graphics.DrawImage(
                    item.Value ? iconSet.Found : iconSet.NotFound,
                    bossRect.X,
                    bossRect.Y,
                    bossRect.Width,
                    bossRect.Height);
                index++;
            }
        }
    }
}
