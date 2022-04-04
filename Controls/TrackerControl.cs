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
            RenderingSettings renderingSettings,
            Func<bool> borderEnabled)
        {
            RenderingSettings = renderingSettings;
            BorderEnabled = borderEnabled;
        }

        protected RenderingSettings RenderingSettings { get; private set; }

        protected T? Data { get; set; }

        protected RomData? RomData { get; private set; }
        protected IFlagSet? FlagSet { get; private set; }
        public Func<bool> BorderEnabled { get; }
        public int RequestedHeight { get; protected set; }

        protected bool HasRightMargin { get; set; } = true;

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

        protected int MinimiumHeight => RenderingSettings.Scale(BorderEnabled() ? 50 : 18);
        protected int UseableWidth => (Width / RenderingSettings.TileSize * RenderingSettings.TileSize) - RenderingSettings.Scale(BorderEnabled() ? 32 : 16);

        public abstract void RefreshSize();

        protected override void OnPaint(PaintEventArgs e)
        {
            if (RomData == null)
                return;

            BackColor =  BorderEnabled() ? Color.Transparent : RomData.Font.GetBackColor();


            base.OnPaint(e);
            var cHeight = Math.Min(Height, e.ClipRectangle.Height) / RenderingSettings.TileSize - 3;
            var cWidth = e.ClipRectangle.Width / RenderingSettings.TileSize - (HasRightMargin ? 2 : 1);

            var sX = RenderingSettings.TileSize;
            var sY = RenderingSettings.TileSize;

            if (BorderEnabled())
            {
                BackColor = Color.Transparent;
                RomData.Font.RenderBox(e.Graphics, sX, sY, Math.Min(Math.Max(12, Header.Length + 2), cWidth), 3);
                if (HeaderCount is not null && cWidth > Math.Max(12, Header.Length + 2) + 6)
                    RomData.Font.RenderBox(e.Graphics, cWidth * RenderingSettings.TileSize - RenderingSettings.Scale(6 * 8), sY, 7, 3);
                RomData.Font.RenderBox(e.Graphics, sX, sY + RenderingSettings.Scale(24), cWidth, cHeight - 1);
                sX += RenderingSettings.TileSize;
                sY += RenderingSettings.TileSize;
                cWidth -= 2;
                cHeight -= 4;
            }

            DrawHeader(e.Graphics, sX, sY, RenderingSettings.Scale(cWidth * 8));
            sY += RenderingSettings.Scale(BorderEnabled() ? 24 : 12);

            if (Data == null)
                return;

            PaintData(e.Graphics, new Rectangle(sX, sY, cWidth * RenderingSettings.TileSize, cHeight * RenderingSettings.TileSize));
            
        }

        protected virtual void DrawHeader(Graphics graphics, int x, int y, int width)
        {
            if(RomData == null)
                return;

            RomData.Font.RenderText(graphics, x, y, Header, TextMode.Normal);
            var cWidth = Width / RenderingSettings.TileSize - 2;
            if (HeaderCount is not null && cWidth > Math.Max(12, Header.Length + 2) + 6)
                RomData.Font.RenderText(graphics, x + width - RenderingSettings.Scale(40), y, HeaderCount, HeaderCountTextMode);
        }

        protected abstract string Header { get; }
        protected abstract string? HeaderCount { get; }
        protected virtual TextMode HeaderCountTextMode => TextMode.Normal;

        protected abstract void PaintData(Graphics graphics, Rectangle rect);

        public virtual void NewFrame() { }
    }
}
