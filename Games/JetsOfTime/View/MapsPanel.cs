using FF.Rando.Companion.Games.JetsOfTime.Settings;
using FF.Rando.Companion.Games.JetsOfTime.Tracking;
using FF.Rando.Companion.Rendering;
using FF.Rando.Companion.View;
using KGySoft.CoreLibraries;
using KGySoft.Drawing.Imaging;
using KGySoft.Drawing.Shapes;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FF.Rando.Companion.Games.JetsOfTime.View;

internal class MapsPanel : PictureBox, IPanel, IScrollablePanel
{
    private Seed? _seed;
    private MapSettings? _settings;
    private TimePeriods? _timePeriods;
    private bool _scrollingEnabled;
    private bool _canScroll;
    private int _periodIndex = -1;
    private double _aspect;

    private readonly ToolTip _toolTip = new() { ShowAlways = true };

    public virtual void InitializeDataSources(Seed seed, MapSettings settings)
    {
        _seed = seed ?? throw new ArgumentNullException(nameof(seed));
        _settings = settings ?? throw new ArgumentNullException(nameof(settings));

        _timePeriods = seed.State.TimePeriods;
        foreach (var period in _timePeriods.Periods)
            period.Updated += Period_Updated;

        _settings.PropertyChanged += PropertyChanged;

        BackColor = _seed.BackgroundColor;
        Visible = _settings.Enabled;

        _aspect = (double)_timePeriods.Periods.First().Map.Size.Width / _timePeriods.Periods.First().Map.Size.Height;

        if (_timePeriods.Periods.Any(p=> p.IsAccessable || (p.Exists && _settings.ShowAllExistingChecks)))
            ScrollRight();
    }

    private void Period_Updated(object sender, EventArgs e)
    {
        var availableCnt = _timePeriods!.Periods.Count(p => p.IsAccessable);

        CanScroll = availableCnt > 1;

        if (availableCnt == 1)
            _periodIndex = _timePeriods!.Periods.IndexOf(p=>p.IsAccessable);
        else if (_periodIndex == -1 && availableCnt > 0)
            ScrollRight();

        if (sender == _timePeriods!.Periods[_periodIndex])
            Render();
    }

    private bool Initialized => _seed != null && _settings != null;

    protected virtual void PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        switch (sender)
        {
            case MapSettings _ when e.PropertyName == nameof(MapSettings.Enabled):
                Visible = _settings?.Enabled == true;
                break;
            case MapSettings _ when e.PropertyName == nameof(MapSettings.MarkerColor):
            case MapSettings _ when e.PropertyName == nameof(MapSettings.ShowAllExistingChecks):
                Render();
                break;
            case IFlags _:
                break;
        }
    }

    protected override void OnResize(EventArgs e)
    {
        base.OnResize(e);

        if (_seed == null)
            return;

        if (Height <= 8 || Width <= 8)
            return;

        if (Size.Height > 0 && Size.Width > 0 && IsHandleCreated)
            BeginInvoke(() => HandleResize());
    }

    protected void HandleResize()
    {
        var newAspect = (double)Width / Height;
        if (_aspect / newAspect > 0.05)
        {
            if (Dock == DockStyle.Top || Dock == DockStyle.Bottom)
                Height = (int)(Width / _aspect);
            else
                Width = (int)(Height * _aspect);
        }
        else
        {
            Render();
        }
    }

    protected void Render()
    {
        if (!Initialized || _periodIndex == -1)
            return;

        RenderImage(_timePeriods?.Periods[_periodIndex]!);
    }

    private void RenderImage(TimePeriod period)
    {
        if (!Initialized)
            return;

        try
        {
            if (!Visible)
                return;

            Image?.Dispose();
            Image = null;
            var baseImage = BitmapDataFactory.CreateBitmapData(period.Map.Size);
            period.Map.RenderData().DrawInto(baseImage);
            Image = baseImage.ToBitmap();

            var sb = new StringBuilder();
            sb.AppendLine($"{period.Description}:\n");

            if (!(_seed?.Started ?? false))
                return;

            Func<CheckLocation, bool> predicate = _settings!.ShowAllExistingChecks
                ? loc => loc.Exists && !loc.IsComplete
                : loc => loc.Exists && loc.IsAccessable && !loc.IsComplete;

            foreach (var loc in period.Locations.Where(predicate))
            {
                baseImage.FillRectangle(
                    _settings!.MarkerColor,
                    loc.Location.X * 16 - 0x30,
                    (Math.Max(0, (loc.Location.Y - 1) * 16 + 8)),
                    16,
                    16);

                sb.AppendLine($"{loc.Description}:");
                foreach (var check in loc.Checks.Where(c => c.Exists && !c.IsComplete))
                {
                    if (check.IsAccessable || _settings!.ShowAllExistingChecks)
                        sb.AppendLine(check.Description);
                }
                sb.AppendLine();
            }

            _toolTip.SetToolTip(this, sb.ToString());
        }
        catch (Exception)
        {
            //This will throw when zoom level is too high, so lets not crash everything.
        }
    }

    public DockStyle DefaultDockStyle => DockStyle.Top;

    public bool CanHaveFillDockStyle => false;

    public int Priority => _settings?.Priority ?? int.MaxValue;

    public virtual bool IsEnabled => _settings?.Enabled ?? false;

    public bool IsEnabledForScrolling
    {
        get => _scrollingEnabled;
        set
        {
            if (value == _scrollingEnabled)
                return;

            _scrollingEnabled = value;
            Render();
        }
    }

    public bool CanScroll
    {
        get => _canScroll;
        private set
        {
            if (_canScroll == value)
                return;

            _canScroll = value;
            CanScrollChanged?.Invoke(this, EventArgs.Empty);
        }
    }

    public event EventHandler? CanScrollChanged;

    public void ScrollDown()
    {
    }

    public void ScrollLeft()
    {
        Func<TimePeriod, bool> predicate = _settings!.ShowAllExistingChecks
            ? loc => loc.Exists
            : loc => loc.Exists && loc.IsAccessable;

        do
        {
            _periodIndex = _periodIndex == 0 ? _timePeriods!.Periods.Count - 1 : _periodIndex - 1;
        }
        while (!predicate(_timePeriods!.Periods[_periodIndex]));

        Render();
    }

    public void ScrollRight()
    {
        Func<TimePeriod, bool> predicate = _settings!.ShowAllExistingChecks
            ? loc => loc.Exists
            : loc => loc.Exists && loc.IsAccessable;

        do
        {
            _periodIndex = (_periodIndex + 1) % _timePeriods!.Periods.Count;
        }
        while (!predicate(_timePeriods!.Periods[_periodIndex]));

        Render();
    }

    public void ScrollUp()
    {
    }
}