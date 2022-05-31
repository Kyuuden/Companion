using BizHawk.FreeEnterprise.Companion.Configuration;
using BizHawk.FreeEnterprise.Companion.Database;
using BizHawk.FreeEnterprise.Companion.Extensions;
using BizHawk.FreeEnterprise.Companion.FlagSet;
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
        private BossType toolTipBossType;

        public Bosses()
            :base()
        {
            InitializeComponent();
            bossToolTip.SetToolTip(this, "Key Items");
            bossToolTip.Description = "This is a test";
            bossToolTip.Active = true;
            bossToolTip.ShowAlways = true;
        }

        public override void Initialize(RomData romData, IFlagSet? flagset, Settings settings)
        {
            base.Initialize(romData, flagset, settings);
            bossToolTip.Initialize(romData, settings);
        }

        protected override string Header => "Bosses";

        private void SetToolTip(BossType item)
        {
            if (toolTipBossType == item || Data == null)
                return;

            var bossAt = Data[item];

            toolTipBossType = item;
            bossToolTip.Description = item.GetDescription();

            if (bossAt.HasValue)
                bossToolTip.Description += $"\nSaw at {bossAt.Value.ToString("hh':'mm':'ss")}";

            bossToolTip.Active = true;
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            var mouseOverKey = _bossesByPosition.FirstOrDefault(kvp => kvp.Key.Contains(e.X, e.Y));

            if (Data == null || mouseOverKey.Value == 0)
            {
                toolTipBossType = 0;
                bossToolTip.Active = false;
            }
            else
                SetToolTip(mouseOverKey.Value);
        }

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
            if (Data == null || Settings == null) return;
            if (UseableWidth < 0) return;

            var iconSize = Settings.IconScaling ? Settings.Scale(32) : 32;
            var iconSpacing = Settings.IconScaling ? Settings.TileSize : 8;
            var numOfItems = Data?.Seen.Count ?? 0;
            var iconsPerRow = UseableWidth / (iconSize + iconSpacing);
            var rows = iconsPerRow >= numOfItems ? 1 : (int)Math.Ceiling((double)numOfItems / (double)iconsPerRow);
            Height = RequestedHeight = Settings.SetToTileInterval(MinimiumHeight + rows * (iconSize + iconSpacing));
            _bossesByPosition.Clear();
            Invalidate();
        }

        protected override void PaintData(Graphics graphics, Rectangle rect)
        {
            if (RomData == null || Data == null || Settings == null)
                return;

            var sX = rect.X;
            var sY = rect.Y;

            var numOfItems = Data.Seen.Count;
            var iconSize = Settings.IconScaling ? Settings.Scale(32) : 32;
            var iconSpacing = Settings.IconScaling ? Settings.TileSize : 8;
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
