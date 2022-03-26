using BizHawk.FreeEnterprise.Companion.Sprites;
using System.Drawing;
using System.Linq;

namespace BizHawk.FreeEnterprise.Companion.Controls
{
    public partial class Party : TrackerControl<State.Party>
    {
        int frame = 0;
        int frameIndex = 0;

        public Party()
            :base(()=>Properties.Settings.Default.PartyBorder)
        {
            InitializeComponent();
        }

        public override void RefreshSize()
        {
            Height = RequestedHeight = 48 * 5 <= UseableWidth ? MinimiumHeight + 48 : MinimiumHeight + 52 * 5;
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

        protected override void PaintData(Graphics graphics, Rectangle rect)
        {
            if (Data == null || RomData == null)
                return;

            float sX = rect.X;
            float sY = rect.Y;
            var cWidth = rect.Width / 8;            

            var charactersPerRow = rect.Width / 48.0 >= 5.0 ? 5 : 1;
            var offset = (cWidth * 8 - 48 * charactersPerRow)/2;

            switch (Properties.Settings.Default.PartyPose)
            {
                case Pose.Dead:
                case Pose.Special:
                case Pose.Portrait:
                    break;
                default:
                    offset +=8;
                    break;
            }

            if (Properties.Settings.Default.PartyPose != Pose.Portrait)
                graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;

            foreach (var c in Data.Characters)
            {
                if (c.ID != 0)
                {
                    graphics.DrawImage(
                       RomData.CharacterSprites.GetCharacterBitmap(c.ID, c.Class, Properties.Settings.Default.PartyPose, frame),
                       sX + offset,
                       sY,
                       48,
                       48);
                }

                switch (charactersPerRow)
                {
                    case 1:
                        sY += (rect.Height - 48) / 4.0f;
                        break;
                    default:
                        sX += 48;
                        break;
                }
            }

            graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.Default;
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
