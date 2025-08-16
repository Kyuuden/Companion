using FF.Rando.Companion.FreeEnterprise.Settings;
using KGySoft.Drawing.Imaging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace FF.Rando.Companion.FreeEnterprise.View;

public abstract class ScrollablePanel<TSettings> : PictureBox, IPanel, IScrollablePanel where TSettings : PanelSettings
{
    protected ISeed? Seed { get; private set; }
    protected TSettings? Settings { get; private set; }

    private bool _scrollingEnabled;

    private int _scrollOffset;
    private bool _canScrollDown;
    private bool _canScrollUp;
    private int _pageIndex;
    private int _pageCount;

    private List<List<IReadableBitmapData>> _pageBitmapData = [];
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

    public virtual void InitializeDataSources(ISeed seed, TSettings settings)
    {
        Seed = seed ?? throw new ArgumentNullException(nameof(seed));
        Settings = settings ?? throw new ArgumentNullException(nameof(settings));

        Settings.PropertyChanged += PropertyChanged;
        Seed.PropertyChanged += PropertyChanged;

        BackColor = Seed.BackgroundColor;
        Visible = Settings.Enabled;

        if (Height <= 8 || Width <= 8)
            return;

        RegerateData();
        RenderImage();
    }

    private void RenderImage()
    {
        if (Settings == null || _pageBitmapData.Count == 0)
            return;

        if (CombinePages)
            RenderImage(_pageBitmapData.SelectMany(d => d));
        else
            RenderImage(_pageBitmapData[_pageIndex]);
    }

    private void RenderImage(IEnumerable<IReadableBitmapData> data)
    {
        if (Seed == null || Settings == null)
            throw new InvalidOperationException();

        try
        {
            var unscaledSize = Settings.Unscale(Size);
            unscaledSize = new Size((unscaledSize.Width / 8) * 8, (unscaledSize.Height / 8) * 8);
            var y = 8;

            if (unscaledSize.Width <= 0 || unscaledSize.Height <= 0)
                return;

            Image?.Dispose();
            Image = null;

            var baseImage = Seed.Font.RenderBox(unscaledSize.Width / 8, unscaledSize.Height / 8, Seed.Sprites.GreyScaleStickerPalette);

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
                y += (line.Height + 8);
            }

            if (_canScrollDown && IsEnabledForScrolling)
                Seed.Sprites.GetArrow(RomData.Arrow.Down).DrawInto(baseImage, new Point(unscaledSize.Width - 16, unscaledSize.Height - 20));

            if (_canScrollUp && IsEnabledForScrolling)
                Seed.Sprites.GetArrow(RomData.Arrow.Up)?.DrawInto(baseImage, new Point(unscaledSize.Width - 16, 0));

            //_seed.Sprites.GetArrow(RomData.Arrow.Left)?.DrawInto(baseImage, new Point(0, 8));
            //_seed.Sprites.GetArrow(RomData.Arrow.Right)?.DrawInto(baseImage, new Point(unscaledSize.Width - 16, 8));

            Image = baseImage.ToBitmap();
        }
        catch (Exception)
        {
            //This will throw when zoom level is too high, so lets not crash everything.
        }
    }

    protected virtual void PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(PanelSettings.ScaleFactor))
            RegerateData();
        if (e.PropertyName == nameof(ISeed.BackgroundColor))
            BackColor = Seed?.BackgroundColor ?? BackColor;
        if (e.PropertyName == nameof(PanelSettings.Enabled))
            Visible = Settings?.Enabled ?? Visible;
    }

    protected override void OnResize(EventArgs e)
    {
        base.OnResize(e);

        if (Seed == null)
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
        if (Seed == null)
            return;

        foreach (var l in _pageBitmapData)
            foreach (var b in l)
                b.Dispose();
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
}
