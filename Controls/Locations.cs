using BizHawk.FreeEnterprise.Companion.Sprites;
using System.Drawing;
using System.Linq;

namespace BizHawk.FreeEnterprise.Companion.Controls
{
    public partial class Locations : TrackerControl<State.Locations>
    {
        public Locations()
            : base(() => Properties.Settings.Default.LocationsBorder)
        {
            InitializeComponent();
        }

        public override void Update(State.Locations data)
        {
            base.Update(data);
            RefreshSize();
        }

        public override void RefreshSize()
        {
            Invalidate();
        }

        protected override void PaintData(Graphics graphics, Rectangle rect)
        {
            if (Data == null || RomData == null)
                return;

            var sX = rect.X;
            var sY = rect.Y;
            var cWidth = rect.Width / 8;
            foreach (var item in Data.GetAvailableLocations())
            {
                if (sY + 8 < rect.Height + rect.Y)
                {
                    RomData.Font.RenderText(graphics, sX, sY, $"•", TextMode.Normal);
                    sY += RomData.Font.RenderText(graphics, sX + 16, sY, cWidth - 2, item, TextMode.Normal) + 2;
                }
            }
        }

        protected override string Header => "Open Locations";
        protected override string? HeaderCount
        {
            get
            {
                if (Data == null)
                    return null;

                return FlagSet is null ? $"{Data.GetAvailableLocations().Count(),4}?" : $"{Data.GetAvailableLocations().Count(),5}";
            }
        }
    }
}
