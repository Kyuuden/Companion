using BizHawk.FreeEnterprise.Companion.Sprites;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace BizHawk.FreeEnterprise.Companion.Controls
{
    public partial class Party : TrackerControl<State.Party>
    {
        private Dictionary<Rectangle, int> _charactersByPosition = new Dictionary<Rectangle, int>();

        private Dictionary<int, int> _characterVersions = new Dictionary<int, int>();

        int frame = 0;
        int frameIndex = 0;

        public Party(RenderingSettings renderingSettings)
            : base(renderingSettings, ()=>Properties.Settings.Default.PartyBorder)
        {
            InitializeComponent();
        }

        public override void RefreshSize()
        {
            _charactersByPosition.Clear();
            Height = RequestedHeight = RenderingSettings.Scale(48 * 5) <= UseableWidth ? MinimiumHeight + RenderingSettings.Scale(48) : MinimiumHeight + RenderingSettings.Scale(52 * 5);            
            if (Properties.Settings.Default.Layout == Companion.Layout.Alternate)
                Width = RenderingSettings.Scale(48 + 24);

            HasRightMargin = Properties.Settings.Default.Layout != Companion.Layout.Alternate;
            frame = 0;
            Invalidate();
        }

        public override void Update(State.Party data)
        {
            base.Update(data);
            frame = 0;
        }

        public override void NewFrame()
        {
            var oldIndex = frameIndex;
            if (Properties.Settings.Default.PartyAnimate && RomData != null)
            {
                frameIndex = RomData.CharacterSprites.GetFrameIndex(Properties.Settings.Default.PartyPose, ++frame);
                if (frameIndex != oldIndex)
                    Invalidate();
            }
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            var clickedCharacter = _charactersByPosition.FirstOrDefault(kvp => kvp.Key.Contains(e.X, e.Y));
            if (clickedCharacter.Key != default && Data != null && RomData != null)
            {
                var cId = Data.Characters[clickedCharacter.Value].ID;
                _characterVersions[cId] = ((_characterVersions.TryGetValue(cId, out var old) ? old : 1) + 1) % RomData.CharacterSprites.PaletteCount;
                Invalidate();
            }
            else
                base.OnMouseClick(e);
        }


        protected override void PaintData(Graphics graphics, Rectangle rect)
        {
            if (Data == null || RomData == null)
                return;

            float sX = rect.X;
            float sY = rect.Y;
            var cWidth = rect.Width / RenderingSettings.TileSize;            

            var charactersPerRow = rect.Width / RenderingSettings.ScaleF(48) >= 5.0 ? 5 : 1;
            var offset = (cWidth * RenderingSettings.TileSize - RenderingSettings.Scale(48) * charactersPerRow)/2;

            switch (Properties.Settings.Default.PartyPose)
            {
                case Pose.Dead:
                case Pose.Special:
                case Pose.Portrait:
                    break;
                default:
                    offset += RenderingSettings.TileSize;
                    break;
            }


            graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
            int charIndex = 0;
            foreach (var c in Data.Characters)
            {
                if (c.ID != 0)
                {
                    _charactersByPosition[new Rectangle((int)sX, (int)sY, RenderingSettings.Scale(48), RenderingSettings.Scale(48))] = charIndex;

                    graphics.DrawImage(
                       RomData.CharacterSprites.GetCharacterBitmap(
                           c.ID, 
                           c.Class,
                           _characterVersions.TryGetValue(c.ID, out var palette) ? palette : 0,
                           Properties.Settings.Default.PartyPose, 
                           frame),
                       sX + offset,
                       sY,
                       RenderingSettings.Scale(48),
                       RenderingSettings.Scale(48));
                }

                switch (charactersPerRow)
                {
                    case 1:
                        sY += (rect.Height - RenderingSettings.Scale(48)) / 4.0f;
                        break;
                    default:
                        sX += RenderingSettings.Scale(48);
                        break;
                }

                charIndex++;
            }
            graphics.InterpolationMode = Properties.Settings.Default.InterpolationMode;
        }

        protected override string Header => "Party";
        protected override string? HeaderCount
        {
            get
            {
                if (Data == null)
                    return null;

                var maxParty = FlagSet?.MaxParty switch
                {
                    null => "??",
                    int x => $"{x,2}"
                };


                return $"{Data.Characters.Count(c => c.ID != 0),2}/{ maxParty}";
            }
        }

        protected override TextMode HeaderCountTextMode => (FlagSet?.MaxParty ?? 0) == Data?.Characters.Count(c => c.ID != 0) ? TextMode.Highlighted : TextMode.Normal;
    }
}
