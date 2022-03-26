using BizHawk.FreeEnterprise.Companion.FlagSet;
using BizHawk.FreeEnterprise.Companion.Sprites;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace BizHawk.FreeEnterprise.Companion.Controls
{
    public interface ITrackerControl
    {
        void Initialize(RomData romData, IFlagSet? flagset);
        void RefreshSize();
        void NewFrame();

        int RequestedHeight { get; }
    }

    public abstract class TrackerControl<T> : UserControl, ITrackerControl
    {
        protected TrackerControl(
            Func<bool> borderEnabled)
        {
            BorderEnabled = borderEnabled;
        }

        protected T? Data { get; set; }

        protected RomData? RomData { get; private set; }
        protected IFlagSet? FlagSet { get; private set; }
        public Func<bool> BorderEnabled { get; }

        public int RequestedHeight { get; protected set; }

        public virtual void Initialize(RomData romData, IFlagSet? flagset)
        {
            if (romData is null)
                throw new ArgumentNullException(nameof(romData));

            RomData = romData;
            FlagSet = flagset;
        }

        public virtual void Update(T data)
        {
            Data = data;
            Invalidate();
        }

        protected int MinimiumHeight => BorderEnabled() ? 50 : 18;
        protected int UseableWidth => (Width/8*8) - (BorderEnabled() ? 32 : 16);

        public abstract void RefreshSize();

        protected override void OnPaint(PaintEventArgs e)
        {
            if (RomData == null)
                return;

            BackColor = BorderEnabled() ? Color.Transparent : RomData.Font.GetBackColor();

            base.OnPaint(e);
            var cHeight = Math.Min(Height, e.ClipRectangle.Height) / 8 - 3;
            var cWidth = e.ClipRectangle.Width / 8 - 2;

            var sX = 8;
            var sY = 8;

            if (BorderEnabled())
            {
                BackColor = Color.Transparent;
                RomData.Font.RenderBox(e.Graphics, sX, sY, Math.Min(Math.Max(12, Header.Length + 2), cWidth), 3);
                if (HeaderCount is not null && cWidth > Math.Max(12, Header.Length + 2) + 6)
                    RomData.Font.RenderBox(e.Graphics, cWidth * 8 - (6 * 8), sY, 7, 3);
                RomData.Font.RenderBox(e.Graphics, sX, sY + 24, cWidth, cHeight - 1);
                sX += 8;
                sY += 8;
                cWidth -= 2;
                cHeight -= 4;
            }

            DrawHeader(e.Graphics, sX, sY, cWidth * 8);
            sY += BorderEnabled() ? 24 : 12;

            if (Data == null)
                return;

            PaintData(e.Graphics, new Rectangle(sX, sY, cWidth * 8, cHeight * 8));
            
        }

        protected virtual void DrawHeader(Graphics graphics, int x, int y, int width)
        {
            if(RomData == null)
                return;

            RomData.Font.RenderText(graphics, x, y, Header, TextMode.Normal);
            var cWidth = Width / 8 - 2;
            if (HeaderCount is not null && cWidth > Math.Max(12, Header.Length + 2) + 6)
                RomData.Font.RenderText(graphics, x + width - 40, y, HeaderCount, HeaderCountTextMode);
        }

        protected abstract string Header { get; }
        protected abstract string? HeaderCount { get; }
        protected virtual TextMode HeaderCountTextMode => TextMode.Normal;

        protected abstract void PaintData(Graphics graphics, Rectangle rect);

        public virtual void NewFrame() { }
    }
}
