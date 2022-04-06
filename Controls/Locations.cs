using BizHawk.FreeEnterprise.Companion.Sprites;
using System.Drawing;
using System.Linq;

namespace BizHawk.FreeEnterprise.Companion.Controls
{
    public partial class Locations : TrackerControl<State.Locations>
    {
        public Locations(RenderingSettings renderingSettings)
            : base(renderingSettings, () => Properties.Settings.Default.LocationsBorder)
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
            var cWidth = rect.Width / RenderingSettings.TileSize;
            var kiLocations = Data.GetAvailableKeyItemLocations();
            var charLocations = Data.GetAvailableCharacterLocations();

            if (kiLocations.Any() && Properties.Settings.Default.LocationsShowKeyItems)
            {
                if (Properties.Settings.Default.LocationsShowCharacters)
                {
                    RomData.Font.RenderText(graphics, sX, sY, $"•", TextMode.Normal);
                    sY += RomData.Font.RenderText(graphics, sX + RenderingSettings.Scale(16), sY, cWidth - 2, "Key Item Locations:", TextMode.Normal) + RenderingSettings.Scale(4);
                    sX += RenderingSettings.Scale(16);
                }

                foreach (var item in kiLocations)
                {
                    if (sY + RenderingSettings.TileSize < rect.Height + rect.Y)
                    {
                        RomData.Font.RenderText(graphics, sX, sY, $"•", TextMode.Normal);
                        sY += RomData.Font.RenderText(graphics, sX + RenderingSettings.Scale(16), sY, cWidth - 2, item, TextMode.Normal) + RenderingSettings.Scale(4);
                    }
                }

                if (Properties.Settings.Default.LocationsShowCharacters)
                    sX -= RenderingSettings.Scale(16);
            }

            if (charLocations.Any() && Properties.Settings.Default.LocationsShowCharacters)
            {
                if (Properties.Settings.Default.LocationsShowKeyItems)
                {
                    RomData.Font.RenderText(graphics, sX, sY, $"•", TextMode.Normal);
                    sY += RomData.Font.RenderText(graphics, sX + RenderingSettings.Scale(16), sY, cWidth - 2, "Character Locations:", TextMode.Normal) + RenderingSettings.Scale(4);
                    sX += RenderingSettings.Scale(16);
                }

                foreach (var item in charLocations)
                {
                    if (sY + RenderingSettings.TileSize < rect.Height + rect.Y)
                    {
                        RomData.Font.RenderText(graphics, sX, sY, $"•", TextMode.Normal);
                        sY += RomData.Font.RenderText(graphics, sX + RenderingSettings.Scale(16), sY, cWidth - 2, item, TextMode.Normal) + RenderingSettings.Scale(4);
                    }
                }
            }
        }

        protected override string Header => "Open Locations";
        protected override string? HeaderCount => null;
    }
}
