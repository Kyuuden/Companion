using BizHawk.FreeEnterprise.Companion.Sprites;
using System;
using System.Drawing;
using System.Linq;

namespace BizHawk.FreeEnterprise.Companion.Controls
{
    public partial class KeyItems : TrackerControl<State.KeyItems>
    {
        public KeyItems()
            : base(() => Properties.Settings.Default.KeyItemsBorder)
        {
            InitializeComponent();
        }

        public IconLookup? IconLookup { get; set; }

        public override void RefreshSize()
        {
            Height = Properties.Settings.Default.KeyItemsStyle == KeyItemStyle.Text
                ? (Properties.Settings.Default.KeyItemsBorder ? 32 : 2) + 90
                : (Properties.Settings.Default.KeyItemsBorder ? 64 : 40) + (int)Math.Ceiling(Enum.GetValues(typeof(KeyItemType)).Length / ((Width- (Properties.Settings.Default.KeyItemsBorder ? 32 : 16)) / 40.0)) * 32;
            Invalidate();
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
                    RomData.Font.RenderText(
                        graphics,
                        sX + (keyIndex % 3) * (cWidth * 8 / 3),
                        sY + (keyIndex / 3) * 13,
                        item.ShortDescription,
                        item.Used ? TextMode.Normal : item.Found ? TextMode.Highlighted : TextMode.Disabled);
                    keyIndex++;
                }
            }
            else if (IconLookup != null)
            {
                var keyIndex = 0;
                var iconsPerRow = cWidth * 8 / 40;
                var offset = (cWidth * 8 - iconsPerRow * 40) / 2;
                foreach (var item in Data.Items.Values)
                {
                    var iconSet = IconLookup.GetIcons(item.Key);
                    graphics.DrawImage(
                        item.Used ? iconSet.Used : item.Found ? iconSet.Found : iconSet.NotFound,
                        sX + offset + ((keyIndex % iconsPerRow) * 40),
                        sY + ((keyIndex / iconsPerRow) * 40), 32, 32);
                    keyIndex++;
                }
            }
        }

        protected override void DrawHeader(Graphics graphics, int x, int y, int width)
        {
            if (RomData == null)
                return;

            RomData.Font.RenderText(graphics, x, y, Header, TextMode.Normal);

            if (HeaderCount is not null && Data is not null)
            {
                var keyItemCount = Data.Items.Values.Count(k => k.Found);
                var mode = keyItemCount >= 10 && !(FlagSet?.No10KeyItemBonus ?? true) ? TextMode.Highlighted : TextMode.Normal;
                RomData.Font.RenderText(graphics, x + width - 40, y, HeaderCount, mode);
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
    }
}
