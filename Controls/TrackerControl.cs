using BizHawk.FreeEnterprise.Companion.Configuration;
using BizHawk.FreeEnterprise.Companion.FlagSet;
using BizHawk.FreeEnterprise.Companion.Sprites;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace BizHawk.FreeEnterprise.Companion.Controls
{
    public interface ITrackerControl
    {
        bool IsInitialized { get; }
        void Initialize(RomData romData, IFlagSet? flagset, Settings settings);
        void RefreshSize();
        void NewFrame();

        void Invalidate();

        int RequestedHeight { get; }
    }

    public abstract class TrackerControl<T> : UserControl, ITrackerControl
    {
        protected TrackerControl()
        {
        }

        protected T? Data { get; set; }

        protected RomData? RomData { get; private set; }
        protected IFlagSet? FlagSet { get; private set; }
        public bool BorderEnabled => Settings?.BordersEnabled ?? true;
        public int RequestedHeight { get; protected set; }

        protected bool HasRightMargin { get; set; } = true;
        protected Settings? Settings { get; private set; }

        public bool IsInitialized => RomData != null && Settings != null;

        public virtual void Initialize(RomData romData, IFlagSet? flagset, Settings settings)
        {
            if (romData is null)
                throw new ArgumentNullException(nameof(romData));

            RomData = romData;
            FlagSet = flagset;
            Settings = settings;
        }

        public virtual void Update(T data)
        {
            var wasNull = Data == null;
            Data = data;
            if (wasNull)
                RefreshSize();

            Invalidate();
        }

        protected int MinimiumHeight => Settings == null ? 8 : BorderEnabled ? Settings.TileSize * 5 : Settings.TileSize * 3;
        protected int UseableWidth => Settings == null ? 8 : (Width / Settings.TileSize * Settings.TileSize) - Settings.Scale(16);

        public abstract void RefreshSize();

        protected override void OnPaint(PaintEventArgs e)
        {
            if (RomData == null || Settings == null) return;

            BackColor =  BorderEnabled ? Color.Transparent : RomData.Font.GetBackColor();

            //e.Graphics.DrawRectangle(new Pen(new SolidBrush(Color.Red)), e.ClipRectangle);

            base.OnPaint(e);
            var cHeight = Math.Min(Height, e.ClipRectangle.Height) / Settings.TileSize - 2;
            var cWidth = e.ClipRectangle.Width / Settings.TileSize;// - (HasRightMargin ? 2 : 1);

            var sX = BorderEnabled ? 0 : Settings.TileSize;
            var sY = BorderEnabled ? 0 : Settings.TileSize;

            if (BorderEnabled)
            {
                BackColor = Color.Transparent;
                RomData.Font.RenderBox(e.Graphics, sX, sY, Math.Min(Math.Max(12, Header.Length + 2), cWidth), 3);
                if (HeaderCount is not null && cWidth > Math.Max(12, Header.Length + 2) + 6)
                    RomData.Font.RenderBox(e.Graphics, cWidth * Settings.TileSize - Settings.Scale(7 * 8), sY, 7, 3);
                RomData.Font.RenderBox(e.Graphics, sX, sY + Settings.Scale(24), cWidth, cHeight - 1);
                sX += Settings.TileSize;
                sY += Settings.TileSize;                
                cHeight -= 4;
            }

            cWidth -= 2;

            DrawHeader(e.Graphics, sX, sY, Settings.Scale(cWidth * 8));
            sY += Settings.Scale(BorderEnabled ? 24 : 16);

            if (Data == null)
                return;

            PaintData(e.Graphics, new Rectangle(sX, sY, cWidth * Settings.TileSize, cHeight * Settings.TileSize));
            
        }

        protected virtual void DrawHeader(Graphics graphics, int x, int y, int width)
        {
            if(RomData == null || Settings == null)
                return;

            RomData.Font.RenderText(graphics, x, y, Header, TextMode.Normal);
            var cWidth = Width / Settings.TileSize;
            if (HeaderCount is not null && cWidth > Math.Max(12, Header.Length + 2) + 6)
                RomData.Font.RenderText(graphics, x + width - Settings.Scale(40), y, HeaderCount, HeaderCountTextMode);
        }

        protected abstract string Header { get; }
        protected abstract string? HeaderCount { get; }
        protected virtual TextMode HeaderCountTextMode => TextMode.Normal;

        protected abstract void PaintData(Graphics graphics, Rectangle rect);

        public virtual void NewFrame() { }
    }
}
