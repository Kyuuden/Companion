using BizHawk.FreeEnterprise.Companion.Sprites;
using System;
using System.Drawing;
using System.Linq;

namespace BizHawk.FreeEnterprise.Companion.Controls
{
    public partial class Objectives : TrackerControl<State.Objectives>
    {
        public Objectives()
            : base()
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
            if (Data == null || RomData == null || Settings == null)
                Height = MinimiumHeight;
            else
            {
                if (UseableWidth < 0) return;

                var cWidth = UseableWidth / Settings.TileSize;

                RequestedHeight = Settings.SetToTileInterval(
                    Settings.Scale(
                        Data.Statuses
                            .Select(s=> s.ToString())
                            .SelectMany(d => RomData.Font.Breakup(d, cWidth - 3))
                            .Count() * 10 + Data.Statuses.Count() * 6 - 4) + MinimiumHeight);

                if (Settings.Layout == Companion.Layout.Alternate)
                    Height = Math.Max(RequestedHeight, Parent.Height - Location.Y);
                else
                    Height = RequestedHeight;
            }

            Invalidate();
        }

        protected override void PaintData(Graphics graphics, Rectangle rect)
        {
            if (RomData == null || Data == null || Settings == null) return;

            var sX = rect.X;
            var sY = rect.Y;
            var cWidth = rect.Width / Settings.TileSize;
            var objectiveIndex = 0;

            foreach (var item in Data.Statuses)
            {
                var mode = item.IsComplete ? TextMode.Disabled : TextMode.Normal;
                RomData.Font.RenderText(graphics, sX, sY, $"{objectiveIndex + 1}.", mode);
                sY += RomData.Font.RenderText(graphics, sX + Settings.Scale(24), sY, cWidth - 3, item.ToString(), mode) + Settings.Scale(6);
                objectiveIndex++;
            }
        }

        protected override string Header => "Objectives";
        protected override string? HeaderCount
        {
            get
            {
                if (Data == null || Data.Statuses.Count() == 0)
                    return null;

                var neededObjectives = FlagSet?.RequriedObjectiveCount switch
                {
                    null => "??",
                    0 => $"{Data.Statuses.Count(),2}",
                    int x => $"{x,2}"
                };

                return $"{Data.Statuses.Count(o => o.IsComplete),2}/{neededObjectives}";
            }
        }

        protected override TextMode HeaderCountTextMode
        {
            get
            {
                var neededObjectives = FlagSet?.RequriedObjectiveCount switch
                {
                    null => -1,
                    0 => Data?.Statuses.Count(),
                    int max => max
                };
                return Data?.Statuses.Count(o => o.IsComplete) >= neededObjectives ? TextMode.Highlighted : TextMode.Normal;
            }
        }
    }
}
