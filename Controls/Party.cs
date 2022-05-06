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

        public Party()
            : base()
        {
            InitializeComponent();
        }

        public override void RefreshSize()
        {
            _charactersByPosition.Clear();
            if (RomData == null || Data == null || Settings == null) return;

            Height = RequestedHeight = Settings.Scale(48 * 5) <= UseableWidth 
                ? MinimiumHeight + Settings.Scale(48) 
                : MinimiumHeight + Settings.Scale(48) * 5 + Settings.TileSize * 4;
            
            if (Settings.Layout == Companion.Layout.Alternate)
                Width = Settings.Scale(64);

            HasRightMargin = Settings.Layout != Companion.Layout.Alternate;
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
            if (Settings != null && Settings.PartyAnimate && RomData != null)
            {
                frameIndex = RomData.CharacterSprites.GetFrameIndex(Settings.PartyPose, ++frame);
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
            if (RomData == null || Data == null || Settings == null) return;

            float sX = rect.X;
            float sY = rect.Y;
            var cWidth = rect.Width / Settings.TileSize;            

            var charactersPerRow = rect.Width / Settings.ScaleF(48) >= 5.0 ? 5 : 1;
            var offset = (cWidth * Settings.TileSize - Settings.Scale(48) * charactersPerRow)/2;

            switch (Settings.PartyPose)
            {
                case Pose.Dead:
                case Pose.Special:
                case Pose.Portrait:
                    break;
                default:
                    offset += Settings.TileSize;
                    break;
            }


            State.Character? anchor = null;
            if (Settings.PartyShowAnchor && FlagSet != null)
            {
                if (FlagSet.VanillaAgility)
                    anchor = Data.PriorityOrder.FirstOrDefault(c => c.ID != 0 && (c.Class == CharacterType.Cecil || c.Class == CharacterType.DarkKnightCecil));

                if (anchor == null && FlagSet.CHero)
                    anchor = Data.PriorityOrder.FirstOrDefault(c => c.ID == 1);

                if (anchor == null)
                    anchor = Data.PriorityOrder.FirstOrDefault(c => c.ID != 0);
            }

            graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
            int charIndex = 0;
            foreach (var c in Data.Characters)
            {
                if (c.ID != 0)
                {
                    _charactersByPosition[new Rectangle((int)sX, (int)sY, Settings.Scale(48), Settings.Scale(48))] = charIndex;

                    graphics.DrawImage(
                       RomData.CharacterSprites.GetCharacterBitmap(
                           c.ID, 
                           c.Class,
                           _characterVersions.TryGetValue(c.ID, out var palette) ? palette : 0,
                           Settings.PartyPose, 
                           frame),
                       sX + offset,
                       sY,
                       Settings.Scale(48),
                       Settings.Scale(48));

                    if (c == anchor)
                       graphics.DrawImage(Properties.Resources.SMB3_item_Anchor, sX + Settings.Scale(48) - Settings.TileSize * 2, sY, Settings.TileSize*2, Settings.TileSize * 2);
                }

                switch (charactersPerRow)
                {
                    case 1:
                        sY += (rect.Height - Settings.Scale(48)) / 4.0f;
                        break;
                    default:
                        sX += Settings.Scale(48);
                        break;
                }

                charIndex++;
            }
            graphics.InterpolationMode = Settings.InterpolationMode;
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
