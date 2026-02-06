using FF.Rando.Companion.Games.FreeEnterprise;
using FF.Rando.Companion.Settings;
using KGySoft.Drawing.Imaging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace FF.Rando.Companion.View;
public abstract class ScrollablePanel<TGame, TSettings> : PictureBox, IPanel, IScrollablePanel where TGame : IGame where TSettings : PanelSettings
{
    protected TGame? Game { get; private set; }
    protected TSettings? Settings { get; private set; }

    protected ScrollablePanel()
    {
        BackgroundImageLayout = ImageLayout.Stretch;
    }

    private bool _scrollingEnabled;

    private int _scrollOffset;
    private bool _canScrollDown;
    private bool _canScrollUp;
    private int _pageIndex;

    private readonly List<List<IReadableBitmapData>> _pageBitmapData = [];
    private bool canScroll;

    public event EventHandler? CanScrollChanged;

    public DockStyle DefaultDockStyle => DockStyle.Top;

    public bool CanHaveFillDockStyle => true;

    public int Priority => Settings?.Priority ?? int.MaxValue;

    public virtual bool IsEnabled => Settings?.Enabled ?? false;

    public bool IsEnabledForScrolling
    {
        get => _scrollingEnabled;
        set
        {
            if (value == _scrollingEnabled)
                return;

            _scrollingEnabled = value;
            RenderImage();
        }
    }

    public virtual void InitializeDataSources(TGame game, TSettings settings)
    {
        Game = game ?? throw new ArgumentNullException(nameof(game));
        Settings = settings ?? throw new ArgumentNullException(nameof(settings));

        Settings.PropertyChanged += PropertyChanged;
        Game.PropertyChanged += PropertyChanged;
        Game.Settings.BorderSettings.PropertyChanged += PropertyChanged;

        BackColor = Game.BackgroundColor;
        Visible = Settings.Enabled;

        if (Height <= 8 || Width <= 8)
            return;

        RegerateData();
        RenderImage();
    }

    protected void RenderImage()
    {
        if (Settings == null || _pageBitmapData.Count == 0 || Game == null)
            return;

        Padding = Game.Settings.BorderSettings.BordersEnabled
            ? new Padding(Game.Settings.BorderSettings.BorderScaleFactor.TileSize())
            : Padding.Empty;

        if (CombinePages)
            RenderImage(_pageBitmapData.SelectMany(d => d).ToList());
        else
            RenderImage(_pageBitmapData[_pageIndex]);
    }

    protected virtual int SpaceBetweenLines => 8;

    protected abstract Bitmap? GenerateBackgroundImage(Size unscaledSize);

    protected abstract IReadableBitmapData GenerateArrow(Arrow direction);

    protected abstract IReadableBitmapData GeneragePageCounter(int current, int total);

    protected Size EffectiveSize => Game?.Settings.BorderSettings.BordersEnabled == true
                ? new Size(Size.Width - Padding.Horizontal, Size.Height - Padding.Vertical)
                : Size;


    protected virtual void HandleArrows(bool showUp, bool showDown, int currentLine, int maxLines, IReadWriteBitmapData data)
    {
        if (showDown)
            GenerateArrow(Arrow.Down).DrawInto(data, new Point(data.Width - 16, data.Height - 20));

        if (showUp)
        {
            var arrow = GenerateArrow(Arrow.Up);
            arrow.DrawInto(data, new Point(data.Width - 16, 16 - arrow.Height));
        }
    }


    private void RenderImage(IList<IReadableBitmapData> data)
    {
        if (Game == null || Settings == null)
            throw new InvalidOperationException();

        try
        {
            var unscaledSize = EffectiveSize.Unscale(Settings.ScaleFactor);
            unscaledSize = new Size((unscaledSize.Width / 8) * 8, (unscaledSize.Height / 8) * 8);
            var y = 8;

            if (unscaledSize.Width <= 0 || unscaledSize.Height <= 0)
                return;

            if (!Visible)
                return;

            Image?.Dispose();
            Image = null;
            var baseImage = BitmapDataFactory.CreateBitmapData(unscaledSize);

            _canScrollDown = false;
            foreach (var line in data.Skip(_scrollOffset))
            {
                if (line.Height + y > baseImage.Height - 8)
                {
                    _canScrollDown = true;
                    SetScrollSate();
                    break;
                }

                line.CopyTo(baseImage, new Point(8, y));
                y += (line.Height + SpaceBetweenLines);
            }

            if (IsEnabledForScrolling)
            {
                HandleArrows(_canScrollUp, _canScrollDown, _scrollOffset, data.Count, baseImage);
            }

            if (!CombinePages && _pageBitmapData.Count > 1)
            {
                var pageCount = GeneragePageCounter(_pageIndex + 1, _pageBitmapData.Count);
                pageCount?.CopyTo(baseImage, new Point(unscaledSize.Width - pageCount.Width - 8, unscaledSize.Height - pageCount.Height));
            }

            Image = baseImage.ToBitmap();
        }
        catch (Exception)
        {
            //This will throw when zoom level is too high, so lets not crash everything.
        }
    }

    protected virtual void PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        switch (e.PropertyName)
        {
            case nameof(PanelSettings.ScaleFactor):
            case nameof(BorderSettings.BorderScaleFactor):
            case nameof(BorderSettings.BordersEnabled):
                RegerateData();
                break;
        }
        if (e.PropertyName == nameof(ISeed.BackgroundColor))
            BackColor = Game?.BackgroundColor ?? BackColor;
        if (e.PropertyName == nameof(PanelSettings.Enabled))
            Visible = Settings?.Enabled ?? Visible;
    }

    protected override void OnResize(EventArgs e)
    {
        base.OnResize(e);

        if (Game == null)
            return;

        if (Height <= 8 || Width <= 8)
            return;

        _canScrollUp = false;
        _canScrollDown = false;
        _scrollOffset = 0;

        if (Size.Height > 0 && Size.Width > 0 && IsHandleCreated)
            BeginInvoke(() => RegerateData());
    }

    protected void RegerateData()
    {
        if (Game == null)
            return;

        foreach (var l in _pageBitmapData)
            foreach (var b in l)
                b.Dispose();

        BackgroundImage?.Dispose();
        BackgroundImage = null;
        if (Game.Settings.BorderSettings.BordersEnabled)
            BackgroundImage = GenerateBackgroundImage(Size.Unscale(Game.Settings.BorderSettings.BorderScaleFactor));

        _pageBitmapData.Clear();
        _pageBitmapData.AddRange(GeneratePageBitmaps());
        SetScrollSate();
        RenderImage();
    }

    public void ScrollDown()
    {
        if (_canScrollDown)
        {
            _scrollOffset += ScrollLines;
            _canScrollUp = true;
            SetScrollSate();
            RenderImage();
        }
    }

    public void ScrollUp()
    {
        if (_canScrollUp)
        {
            _scrollOffset -= ScrollLines;
            _canScrollUp = _scrollOffset > 0;
            SetScrollSate();
            RenderImage();
        }
    }

    public void ScrollLeft()
    {
        if (CombinePages)
            return;

        _pageIndex = _pageIndex == 0 ? _pageBitmapData.Count - 1 : _pageIndex - 1;
        _scrollOffset = 0;
        _canScrollUp = false;
        SetScrollSate();
        RenderImage();
    }

    public void ScrollRight()
    {
        if (CombinePages)
            return;

        _pageIndex = (_pageIndex + 1) % _pageBitmapData.Count;
        _scrollOffset = 0;
        _canScrollUp = false;
        SetScrollSate();
        RenderImage();
    }

    private void SetScrollSate()
    {
        CanScroll = _canScrollUp || _canScrollDown || (!CombinePages && _pageBitmapData.Count > 1);
    }

    public bool CanScroll
    {
        get => canScroll;
        private set
        {
            if (canScroll == value)
                return;

            canScroll = value;
            CanScrollChanged?.Invoke(this, EventArgs.Empty);
        }
    }

    protected abstract IEnumerable<List<IReadableBitmapData>> GeneratePageBitmaps();
    protected abstract bool CombinePages { get; }
    protected abstract int ScrollLines { get; }

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            if (Settings != null)
                Settings.PropertyChanged -= PropertyChanged;

            if (Game != null)
                Game.PropertyChanged -= PropertyChanged;
        }
        base.Dispose(disposing);
    }
}
