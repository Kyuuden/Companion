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

        public KeyItems()
            : base(() => Properties.Settings.Default.KeyItemsBorder)
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
                    Height = RequestedHeight = MinimiumHeight + 6 * 15-4;
                    break;
                case KeyItemStyle.Icons:
                    var numOfItems = Data?.Items.Count ?? 0;
                    var iconsPerRow = UseableWidth / 40;
                    var rows = iconsPerRow >= numOfItems ? 1 : (int)Math.Ceiling((double)numOfItems / (double)iconsPerRow);
                    Height = RequestedHeight = MinimiumHeight + (rows * 32) + ((rows - 1) * 8) - (BorderEnabled() ? 2 : 0);
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
            keyItemsToolTip.IsFound = item.Found;
            keyItemsToolTip.IsUsed = item.Used;
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
            var cWidth = rect.Width / 8;
            var cHeight = rect.Height / 8;

            //if FFIV text style
            if (Properties.Settings.Default.KeyItemsStyle == KeyItemStyle.Text)
            {
                var keyIndex = 0;
                foreach (var item in Data.Items.Values)
                {
                    _keyItemsByPosition[new Rectangle(sX + (keyIndex % 3) * (cWidth * 8 / 3), sY + (keyIndex / 3) * 15, item.ShortName.Length * 8, 8)] = item.Key;

                    RomData.Font.RenderText(
                        graphics,
                        sX + (keyIndex % 3) * (cWidth * 8 / 3),
                        sY + (keyIndex / 3) * 15,
                        item.ShortName,
                        item.Used ? TextMode.Normal : item.Found ? TextMode.Highlighted : TextMode.Disabled);
                    keyIndex++;
                }
            }
            else
            {
                var keyIndex = 0;
                var iconsPerRow = cWidth * 8 / 40;
                var offset = (cWidth * 8 - iconsPerRow * 40) / 2;
                foreach (var item in Data.Items.Values)
                {
                    _keyItemsByPosition[new Rectangle(sX + offset + ((keyIndex % iconsPerRow) * 40), sY + ((keyIndex / iconsPerRow) * 40), 32, 32)] = item.Key;

                    var iconSet = IconLookup.GetIcons(item.Key);
                    graphics.DrawImage(
                        item.Used ? iconSet.Used : item.Found ? iconSet.Found : iconSet.NotFound,
                        sX + offset + ((keyIndex % iconsPerRow) * 40),
                        sY + ((keyIndex / iconsPerRow) * 40), 32, 32);
                    keyIndex++;
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
