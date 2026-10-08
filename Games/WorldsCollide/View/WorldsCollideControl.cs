using FF.Rando.Companion.Games.WorldsCollide.Settings;
using FF.Rando.Companion.Settings;
using FF.Rando.Companion.View;
using KGySoft.CoreLibraries;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;

namespace FF.Rando.Companion.Games.WorldsCollide.View;

internal class WorldsCollideControl : UserControl
{
    private readonly Seed _seed;
    private readonly StatsPanel _statistics;
    private readonly ChecksPanel _checks;
    private readonly MapsPanel _maps;
    private readonly Panel _mapAndStatsPanel;

    private readonly List<IScrollablePanel> _scrollables = [];
    private int _scrollIndex = 0;

    public WorldsCollideControl(Seed seed)
    {
        Padding = new(0, 4, 0, 0);
        Dock = DockStyle.Fill;
        Name = "WorldsCollideControl";
        _seed = seed ?? throw new ArgumentNullException(nameof(seed));
        seed.Container.ButtonPressed += Seed_ButtonPressed;
        seed.Settings.PropertyChanged += Settings_PropertyChanged;

        SuspendLayout();
        _mapAndStatsPanel = new Panel() { Dock = DockStyle.Fill };
        _mapAndStatsPanel.SuspendLayout();

        _maps = new MapsPanel(_seed) { Dock = DockStyle.Fill };
        _checks = new ChecksPanel(_seed) { Dock = DockStyle.Top };
        _statistics = new StatsPanel(_seed) { Dock = DockStyle.Right };

        _mapAndStatsPanel.Resize += MapAndStatsPanel_Resize;
        _mapAndStatsPanel.Controls.Add(_maps);
        _mapAndStatsPanel.Controls.Add(_statistics);

        Controls.Add(_mapAndStatsPanel);
        Controls.Add(_checks);

        _mapAndStatsPanel.ResumeLayout(false);
        ResumeLayout(false);

        _scrollables = Controls.OfType<IScrollablePanel>().Concat(Controls.OfType<Control>().SelectMany(c => c.Controls.OfType<IScrollablePanel>())).ToList();
        var enable = true;
        foreach (var item in _scrollables)
        {
            if (item.CanScroll)
            {
                item.IsEnabledForScrolling = enable;
                enable = false;
            }
            else
                item.IsEnabledForScrolling = false;
        }
    }

    private void ArrangeControls()
    {
        switch (_seed.Settings.MapPosition)
        {
            //case MapPosition.Left:
            //    _maps.Visible = true;
            //    _checks.Dock = DockStyle.Right;
            //    break;

            case MapPosition.Top:
                _maps.Visible = true;
                _checks.Dock = DockStyle.Bottom;
                break;

            //case MapPosition.Right:
            //    _maps.Visible = true;
            //    _checks.Dock = DockStyle.Left;
            //    break;

            case MapPosition.Bottom:
                _maps.Visible = true;
                _checks.Dock = DockStyle.Top;
                break;

            case MapPosition.None:
                _maps.Visible = false;
                _checks.Dock = DockStyle.Top;
                break;
        }

        MapAndStatsPanel_Resize(_mapAndStatsPanel, new EventArgs());
        Refresh();
    }

#if DEBUG
    protected override void OnResize(EventArgs e)
    {
        base.OnResize(e);
        Debug.WriteLine($"{Size} vs {Parent?.Size}");
    }
#endif

    private void Settings_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        switch (e.PropertyName)
        {
            case nameof(WorldsCollideSettings.MapPosition):
                BeginInvoke(() => ArrangeControls());
                break;
        }
    }

    private void MapAndStatsPanel_Resize(object sender, EventArgs e)
    {
        if (_mapAndStatsPanel.Height > _mapAndStatsPanel.Width || _seed.Settings.MapPosition == Settings.MapPosition.None)
        {
            _statistics.SpacingMode = SpacingMode.Columns;
            _statistics.FlowDirection = FlowDirection.LeftToRight;
            _statistics.Dock = DockStyle.Top;
        }
        else
        {
            _statistics.SpacingMode = SpacingMode.Rows;
            _statistics.FlowDirection = FlowDirection.TopDown;
            _statistics.Dock = DockStyle.Right;
        }
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            _seed.Container.ButtonPressed -= Seed_ButtonPressed;

            _checks.Dispose();
            _statistics.Dispose();
            _maps.Dispose();
            _mapAndStatsPanel.Dispose();
        }

        base.Dispose(disposing);
    }

    private void Seed_ButtonPressed(InputAction action)
    {
        if (_scrollables.Count == 0 || !_scrollables.TryGetElementAt(_scrollIndex, out var target))
            return;

        switch (action)
        {
            case InputAction.NextPanel:
                target.IsEnabledForScrolling = false;
                _scrollIndex = (_scrollIndex + 1) % _scrollables.Count;
                _scrollables[_scrollIndex].IsEnabledForScrolling = true;
                break;
            case InputAction.NextPage:
                target.ScrollRight();
                break;
            case InputAction.PreviousPage:
                target.ScrollLeft();
                break;
            case InputAction.ScrollDown:
                target.ScrollDown();
                break;
            case InputAction.ScrollUp:
                target.ScrollUp();
                break;
        }
    }

    private void CanScrollChanged(object sender, System.EventArgs e)
    {
        var enable = true;
        foreach (var item in _scrollables)
        {
            if (item.CanScroll)
            {
                item.IsEnabledForScrolling = enable;
                enable = false;
            }
            else
                item.IsEnabledForScrolling = false;
        }
    }

    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);
        BackColor = _seed.BackgroundColor;
        ArrangeControls();
    }
}
