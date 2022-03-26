using BizHawk.FreeEnterprise.Companion.Sprites;
using System;
using System.Drawing;
using System.Linq;

namespace BizHawk.FreeEnterprise.Companion.Controls
{
    public partial class Objectives : TrackerControl<State.Objectives>
    {
        public Objectives()
            : base(()=>Properties.Settings.Default.ObjectivesBorder)
        {
            InitializeComponent();
        }

        public override void Update(State.Objectives data)
        {
            base.Update(data);
            RefreshSize();
        }

        public override void RefreshSize()
        {
            if (Data == null || RomData == null)
                Height = MinimiumHeight;
            else
            {
                var cWidth = UseableWidth / 8;

                RequestedHeight = Data.Descriptions.SelectMany(d => RomData.Font.Breakup(d, cWidth - 3)).Count() * 10 + Data.Descriptions.Count() * 6 - 4 + MinimiumHeight;

                if (Properties.Settings.Default.Layout == Companion.Layout.Alternate)
                    Height = Math.Max(RequestedHeight, Parent.Height - Location.Y);
                else
                    Height = RequestedHeight;
            }

            Invalidate();
        }

        protected override void PaintData(Graphics graphics, Rectangle rect)
        {
            if (Data == null || RomData == null)
                return;

            var sX = rect.X;
            var sY = rect.Y;
            var cWidth = rect.Width / 8;
            var objectiveIndex = 0;

            foreach (var item in Data.Descriptions)
            {
                var mode = Data.Completions[objectiveIndex].HasValue ? TextMode.Disabled : TextMode.Normal;
                RomData.Font.RenderText(graphics, sX, sY, $"{objectiveIndex + 1}.", mode);
                sY += RomData.Font.RenderText(graphics, sX + 24, sY, cWidth - 3, item, mode) + 6;
                objectiveIndex++;
            }
        }

        protected override string Header => "Objectives";
        protected override string? HeaderCount
        {
            get
            {
                if (Data == null)
                    return null;

                var neededObjectives = FlagSet?.RequriedObjectiveCount switch
                {
                    null => "??",
                    0 => $"{Data.Completions.Count(),2}",
                    int x => $"{x,2}"
                };

                return $"{Data.Completions.Count(o => o.HasValue),2}/{neededObjectives}";
            }
        }

        protected override TextMode HeaderCountTextMode
        {
            get
            {
                var neededObjectives = FlagSet?.RequriedObjectiveCount switch
                {
                    null => -1,
                    0 => Data?.Completions.Count(),
                    int max => max
                };
                return Data?.Completions.Count(o => o.HasValue) >= neededObjectives ? TextMode.Highlighted : TextMode.Normal;
            }
        }
    }
}
