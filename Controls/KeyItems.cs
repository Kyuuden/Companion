using BizHawk.FreeEnterprise.Companion.FlagSet;
using BizHawk.FreeEnterprise.Companion.Sprites;
using BizHawk.FreeEnterprise.Companion.State;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace BizHawk.FreeEnterprise.Companion.Controls
{
    public partial class KeyItems : TrackerControl<State.KeyItems>
    {
        private Dictionary<Rectangle, KeyItemType> _keyItemsByPosition = new Dictionary<Rectangle, KeyItemType>();
        private KeyItemType lastToolTipItem;

        public KeyItems(RenderingSettings renderingSettings)
            : base(renderingSettings, () => Properties.Settings.Default.KeyItemsBorder)
        {
            InitializeComponent();
            keyItemsToolTip.SetToolTip(this, "Key Items");
            keyItemsToolTip.Description = "This is a test";
            keyItemsToolTip.Active = true;
            keyItemsToolTip.ShowAlways = true;
        }

        public override void Initialize(RomData romData, IFlagSet? flagset)
        {
            base.Initialize(romData, flagset);
            keyItemsToolTip.Initialize(romData);
        }

        public override void RefreshSize()
        {
            _keyItemsByPosition.Clear();
            switch (Properties.Settings.Default.KeyItemsStyle)
            {
                case KeyItemStyle.Text:
                    Height = RequestedHeight = MinimiumHeight + 9 * RenderingSettings.TileSize; //RenderingSettings.Scale(6 * 15-4);
                    break;
                case KeyItemStyle.Icons:
                    var iconSize = Properties.Settings.Default.KeyItemIconScaling ? RenderingSettings.Scale(32) : 32;
                    var iconSpacing = Properties.Settings.Default.KeyItemIconScaling ? RenderingSettings.TileSize : 8;
                    var numOfItems = Data?.Items.Count ?? 0;
                    var iconsPerRow = UseableWidth / (iconSize + iconSpacing);
                    var rows = iconsPerRow >= numOfItems ? 1 : (int)Math.Ceiling((double)numOfItems / (double)iconsPerRow);
                    Height = RequestedHeight = RenderingSettings.SetToTileInterval(MinimiumHeight + rows * (iconSize + iconSpacing));
                    break;
            }
            Invalidate();
        }

        private void SetToolTip(KeyItem item)
        {
            if (lastToolTipItem == item.Key)
                return;

            lastToolTipItem = item.Key;
            keyItemsToolTip.Description = item.Description;
            keyItemsToolTip.FoundAt = item.FoundAt;
            keyItemsToolTip.UsedAt = item.UsedAt;
            keyItemsToolTip.ReceivedFrom = TextLookup.GetName(item.FoundLocation);
            keyItemsToolTip.Active = true;
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            var mouseOverKey = _keyItemsByPosition.FirstOrDefault(kvp => kvp.Key.Contains(e.X, e.Y));

            if (Data == null || mouseOverKey.Value == 0)
            {
                lastToolTipItem = 0;
                keyItemsToolTip.Active = false;
            }
            else
                SetToolTip(Data[mouseOverKey.Value]);
        }

        protected override void PaintData(Graphics graphics, Rectangle rect)
        {
            if (RomData == null || Data == null)
                return;

            var sX = rect.X;
            var sY = rect.Y;
            var cWidth = rect.Width / RenderingSettings.TileSize;
            var cHeight = rect.Height / RenderingSettings.TileSize;

            //if FFIV text style
            if (Properties.Settings.Default.KeyItemsStyle == KeyItemStyle.Text)
            {
                var keyIndex = 0;
                foreach (var item in Data.Items.Values)
                {
                    var itemRect = new Rectangle(
                        sX + (keyIndex % 3) * (rect.Width / 3),
                        sY + RenderingSettings.Scale((keyIndex / 3) * 13),
                        item.ShortName.Length * RenderingSettings.TileSize,
                        RenderingSettings.TileSize);

                    _keyItemsByPosition[itemRect] = item.Key;

                    RomData.Font.RenderText(
                        graphics,
                        itemRect.X,
                        itemRect.Y,
                        item.ShortName,
                        item.Used ? TextMode.Normal : item.Found ? TextMode.Highlighted : TextMode.Disabled);
                    keyIndex++;
                }
            }
            else
            {
                var numOfItems = Data.Items.Count;
                var iconSize = Properties.Settings.Default.KeyItemIconScaling ? RenderingSettings.Scale(32) : 32;
                var iconSpacing = Properties.Settings.Default.KeyItemIconScaling ? RenderingSettings.TileSize : 8;
                var iconsPerRow = UseableWidth / (iconSize + iconSpacing);
                var rows = iconsPerRow >= numOfItems ? 1 : (int)Math.Ceiling((double)numOfItems / (double)iconsPerRow);
                iconsPerRow = numOfItems / rows;

                while (iconsPerRow * rows < numOfItems)
                    iconsPerRow++;

                var hiconSpacing = (rect.Width - iconsPerRow * iconSize) / (iconsPerRow);
                var offset = hiconSpacing / 2;
                var index = 0;
                foreach (var item in Data.Items.Values)
                {
                    var bossRect = new Rectangle(
                        sX + offset + ((index % iconsPerRow) * (iconSize + hiconSpacing)),
                        sY + ((index / iconsPerRow) * (iconSize + iconSpacing)),
                        iconSize,
                        iconSize);

                    _keyItemsByPosition[bossRect] = item.Key;

                    var iconSet = IconLookup.GetIcons(item.Key);
                    graphics.DrawImage(
                        item.Used ? iconSet.Used : item.Found ? iconSet.Found : iconSet.NotFound,
                        bossRect.X,
                        bossRect.Y,
                        bossRect.Width,
                        bossRect.Height);
                    index++;
                }
            }
        }

        protected override string Header => "Key Items";
        protected override string? HeaderCount
        {
            get
            {
                if (Data == null)
                    return null;

                var keyItemCount = Data.Items.Values.Count(k => k.Found);                
                return $"{keyItemCount,2}/17";
            }
        }
        protected override TextMode HeaderCountTextMode
        {
            get
            {
                var keyItemCount = Data?.Items.Values.Count(k => k.Found);
                return keyItemCount >= 10 && !(FlagSet?.No10KeyItemBonus ?? true) ? TextMode.Highlighted : TextMode.Normal;
            }
        }
    }
}
