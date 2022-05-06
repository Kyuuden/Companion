using BizHawk.FreeEnterprise.Companion.Sprites;
using System.Drawing;
using System.Linq;

namespace BizHawk.FreeEnterprise.Companion.Controls
{
    public partial class Locations : TrackerControl<State.Locations>
    {
        public Locations()
            : base()
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
            if (RomData == null || Data == null || Settings == null) return;

            var sX = rect.X;
            var sY = rect.Y;
            var cWidth = rect.Width / Settings.TileSize;
            var kiLocations = Data.GetAvailableKeyItemLocations();
            var charLocations = Data.GetAvailableCharacterLocations();
            var more = false;

            if (kiLocations.Any() && Settings.LocationsShowKeyItems)
            {
                if (Settings.LocationsShowCharacters)
                {
                    if (sY + Settings.TileSize < rect.Height + rect.Y)
                    {
                        RomData.Font.RenderText(graphics, sX, sY, $"•", TextMode.Normal);
                        sY += RomData.Font.RenderText(graphics, sX + Settings.Scale(16), sY, cWidth - 2, "Key Item Locations:", TextMode.Normal) + Settings.Scale(4);
                        sX += Settings.Scale(16);
                    }
                    else more = true;
                }

                foreach (var item in kiLocations)
                {
                    if (sY + Settings.TileSize < rect.Height + rect.Y)
                    {
                        RomData.Font.RenderText(graphics, sX, sY, $"•", TextMode.Normal);
                        sY += RomData.Font.RenderText(graphics, sX + Settings.Scale(16), sY, cWidth - 2, item, TextMode.Normal) + Settings.Scale(4);
                    }
                    else more = true;
                }

                if (Settings.LocationsShowCharacters)
                    sX -= Settings.Scale(16);
            }

            if (charLocations.Any() && Settings.LocationsShowCharacters)
            {
                if (Settings.LocationsShowKeyItems)
                {
                    if (sY + Settings.TileSize < rect.Height + rect.Y)
                    {
                        RomData.Font.RenderText(graphics, sX, sY, $"•", TextMode.Normal);
                        sY += RomData.Font.RenderText(graphics, sX + Settings.Scale(16), sY, cWidth - 2, "Character Locations:", TextMode.Normal) + Settings.Scale(4);
                        sX += Settings.Scale(16);
                    }
                    else more = true;
                }

                foreach (var item in charLocations)
                {
                    if (sY + Settings.TileSize < rect.Height + rect.Y)
                    {
                        RomData.Font.RenderText(graphics, sX, sY, $"•", TextMode.Normal);
                        sY += RomData.Font.RenderText(graphics, sX + Settings.Scale(16), sY, cWidth - 2, item, TextMode.Normal) + Settings.Scale(4);
                    }
                    else more = true;
                }
            }

            if (more)
                RomData.Font.RenderText(graphics, (cWidth - 3) * Settings.TileSize, rect.Y + rect.Height - Settings.TileSize, $"...", TextMode.Normal);
        }

        protected override string Header => "Open Locations";
        protected override string? HeaderCount => null;
    }
}
