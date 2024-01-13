using BizHawk.FreeEnterprise.Companion.Configuration;
using BizHawk.FreeEnterprise.Companion.FlagSet;
using BizHawk.FreeEnterprise.Companion.Sprites;
using BizHawk.FreeEnterprise.Companion.State;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Windows.Forms;

namespace BizHawk.FreeEnterprise.Companion.Controls
{
    public partial class KeyItems : TrackerControl<State.KeyItems>
    {
        private Dictionary<Rectangle, KeyItemType> _keyItemsByPosition = new Dictionary<Rectangle, KeyItemType>();
        private KeyItemType lastToolTipItem;

        private Queue<Size?> _missOffsets = new Queue<Size?>();
        private Queue<ColorMatrix> _fadeoutParams = new Queue<ColorMatrix>();
        private KeyItemType? _newItem;

        public KeyItems()
            : base()
        {
            InitializeComponent();
            keyItemsToolTip.SetToolTip(this, "Key Items");
            keyItemsToolTip.Description = "This is a test";
            keyItemsToolTip.Active = true;
            keyItemsToolTip.ShowAlways = true;
        }

        public override void NewFrame()
        {
            if (_missOffsets.Count > 0 || _fadeoutParams.Count > 0)
                Invalidate();
        }

        public override void Initialize(RomData romData, IFlagSet? flagset, Settings settings)
        {
            base.Initialize(romData, flagset, settings);
            keyItemsToolTip.Initialize(romData, settings);
        }

        public override void RefreshSize()
        {
            if (Settings == null) return;
            if (UseableWidth < 0) return;
            _keyItemsByPosition.Clear();
            switch (Settings.KeyItemsStyle)
            {
                case KeyItemStyle.Text:
                    Height = RequestedHeight = MinimiumHeight + 9 * Settings.TileSize; //Settings.Scale(6 * 15-4);
                    break;
                case KeyItemStyle.Icons:
                    var iconSize = Settings.IconScaling ? Settings.Scale(32) : 32;
                    var iconSpacing = Settings.IconScaling ? Settings.TileSize : 8;
                    var numOfItems = Data?.Items.Count ?? 0;
                    var iconsPerRow = UseableWidth / (iconSize + iconSpacing);
                    var rows = iconsPerRow >= numOfItems ? 1 : (int)Math.Ceiling((double)numOfItems / (double)iconsPerRow);
                    Height = RequestedHeight = Settings.SetToTileInterval(MinimiumHeight + rows * (iconSize + iconSpacing));
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
            keyItemsToolTip.FoundAt = item.WhenFound;
            keyItemsToolTip.UsedAt = item.WhenUsed;
            keyItemsToolTip.ReceivedFrom = TextLookup.GetName(item.WhereFound);
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
                SetToolTip(Data.Items[mouseOverKey.Value]);
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);
            var mouseKey = _keyItemsByPosition.FirstOrDefault(kvp => kvp.Key.Contains(e.X, e.Y));

            if (Data != null && mouseKey.Value == KeyItemType.Pass)
            {
                Data.SwapPass();
                Invalidate();
            }
        }

        public void NewItemFound(KeyItemType? item)
        { 
            if (Settings != null 
                && Settings.KeyItemEventEnabled 
                && _missOffsets.Count == 0 
                && _fadeoutParams.Count == 0
                && RomData != null)
            {
                _newItem = item;

                if (item.HasValue)
                {
                    foreach (var p in RomData.Overlays.FadeoutOffsets)
                        _fadeoutParams.Enqueue(p);
                }
                else
                {
                    foreach (var s in RomData.Overlays.MissOffsets)
                        _missOffsets.Enqueue(s);
                }
                Invalidate();
            }
        }

        protected override void PaintData(Graphics graphics, Rectangle rect)
        {
            if (RomData == null || Data == null || Settings == null) return;

            var sX = rect.X;
            var sY = rect.Y;

            //if FFIV text style
            if (Settings.KeyItemsStyle == KeyItemStyle.Text)
            {
                var keyIndex = 0;
                foreach (var item in Data.Items.Values)
                {
                    var itemRect = new Rectangle(
                        sX + (keyIndex % 3) * (rect.Width / 3),
                        sY + Settings.Scale((keyIndex / 3) * 13),
                        item.ShortName.Length * Settings.TileSize,
                        Settings.TileSize);

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
                var iconSize = Settings.IconScaling ? Settings.Scale(32) : 32;
                var iconSpacing = Settings.IconScaling ? Settings.TileSize : 8;
                var iconsPerRow = UseableWidth / (iconSize + iconSpacing);
                var rows = iconsPerRow >= numOfItems ? 1 : (int)Math.Ceiling((double)numOfItems / (double)iconsPerRow);
                iconsPerRow = numOfItems / rows;

                while (iconsPerRow * rows < numOfItems)
                    iconsPerRow++;

                var hiconSpacing = (rect.Width - iconsPerRow * iconSize) / (iconsPerRow);
                var offset = hiconSpacing / 2;
                var index = 0;
                foreach (var item in Data.Items.Values.OrderBy(v=> v.Key.ToIconOrder()))
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


            if (_missOffsets.Count > 0 && RomData != null)
            {
                var currentOffset = _missOffsets.Dequeue();
                if (currentOffset == null)
                    return;

                var missScale = (float)Settings.ViewScaleF * 3;
                var offset = new SizeF(currentOffset.Value.Width * missScale, currentOffset.Value.Height * missScale);
                var missSize = new SizeF(RomData.Overlays.MissBitmap.Width * missScale, RomData.Overlays.MissBitmap.Height * missScale);


                graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
                var loc = new PointF(Width / 2 - missSize.Width / 2, Height - (missSize.Height + Settings.TileSize)) + offset;
                graphics.DrawImage(RomData.Overlays.MissBitmap, loc.X, loc.Y, missSize.Width, missSize.Height);
            }

            if (_fadeoutParams.Count > 0 && RomData != null && _newItem.HasValue)
            {
                var currentParam = _fadeoutParams.Dequeue();
                var icon = IconLookup.GetIcons(_newItem.Value).Found;
                var scale = (float)Settings.ViewScaleF * 2;
                var size = new Size((int)(icon.Width * scale), (int)(icon.Height * scale));
                var loc = new Point(rect.Width / 2 - size.Width / 2, rect.Height / 2 - size.Height / 2);
                loc.Offset(rect.Location);

                var kiPos = _keyItemsByPosition.First(p => p.Value == _newItem.Value).Key;
                if (kiPos.Width > kiPos.Height)
                    kiPos.Width = kiPos.Height;

                var xdiff = loc.X - kiPos.X;
                var ydiff = loc.Y - kiPos.Y;
                var wdiff = size.Width - kiPos.Width;
                var hdiff = size.Height - kiPos.Height;


                using (var attributes = new ImageAttributes())
                {
                    attributes.SetColorMatrix(currentParam, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);

                    graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
                    graphics.DrawImage(icon,
                        new Rectangle(
                         (int)(kiPos.X + (xdiff * currentParam.Matrix33)),
                         (int)(kiPos.Y + (ydiff * currentParam.Matrix33)),
                         (int)(kiPos.Width + (wdiff * currentParam.Matrix33)),
                         (int)(kiPos.Height + (hdiff * currentParam.Matrix33))),
                        0, 0, icon.Width, icon.Height, GraphicsUnit.Pixel, attributes);
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

                var keyItemCount = Data.Items.Values.Where(k => k.Key != KeyItemType.Pass).Count(k => k.Found);
                return $"{keyItemCount,2}/17";
            }
        }
        protected override TextMode HeaderCountTextMode
        {
            get
            {
                var keyItemCount = Data?.Items.Values.Where(k => k.Key != KeyItemType.Pass).Count(k => k.Found);
                return keyItemCount >= 10 && !(FlagSet?.No10KeyItemBonus ?? true) ? TextMode.Highlighted : TextMode.Normal;
            }
        }
    }
}
