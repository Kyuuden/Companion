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
            Height = 68 + (Properties.Settings.Default.PartyBorder ? 32 : 2);
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

            var sX = rect.X;
            var sY = rect.Y;
            var cWidth = rect.Width / 8;
            var offset = (cWidth * 8 - 48 * 5)/2;

            switch (Properties.Settings.Default.PartyPose)
            {
                case Pose.Dead:
                case Pose.Special:
                    break;
                default:
                    offset +=8;
                    break;
            }

            graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
            var characterIndex = -1;
            foreach (var c in Data.Characters)
            {
                characterIndex++;
                if (c.ID == 0)
                    continue;

                graphics.DrawImage(
                    RomData.CharacterSprites.GetCharacterBitmap(c.ID, c.Class, Properties.Settings.Default.PartyPose, frame),
                    sX + offset + (characterIndex *48),
                    sY,
                    48,
                    48);
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

        protected override void DrawHeader(Graphics graphics, int x, int y, int width)
        {
            if (RomData == null)
                return;

            RomData.Font.RenderText(graphics, x, y, Header, TextMode.Normal);

            if (HeaderCount is not null && Data is not null)
            {
                var maxParty = (FlagSet?.MaxParty ?? 0) == Data.Characters.Count(c => c.ID != 0) ? TextMode.Highlighted : TextMode.Normal;
                RomData.Font.RenderText(graphics, x + width - 40, y, HeaderCount, maxParty);
            }
        }
    }
}
