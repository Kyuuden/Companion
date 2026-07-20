using FF.Rando.Companion.Settings;
using FF.Rando.Companion.View;
using KGySoft.CoreLibraries;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace FF.Rando.Companion.Games.JetsOfTime.View;
internal partial class JetsOfTimeControl : UserControl
{
    private Seed? _seed;
    private List<IScrollablePanel> _scrollables = [];
    private int _scrollIndex = 0;

    public JetsOfTimeControl()
    {
        Dock = DockStyle.Fill;
        Name = "JetsOfTimeControl";
        
        InitializeComponent();
        _characters.Resize += TrackerResized;
    }

    private void TrackerResized(object sender, EventArgs e)
         => ResizeTopPanel();

    private void ResizeTopPanel()
    {
        TopPanel.Height = Math.Max(
            TopPanel.Controls.OfType<Control>().Where(c => c.Dock is DockStyle.Left or DockStyle.Right).Sum(c => c.Height),
            TopPanel.Controls.OfType<Control>().Where(c => c.Dock is DockStyle.Top or DockStyle.Bottom).Sum(c => c.Height));
    }

    public void InitializeDataSources(Seed seed)
    {
        _seed = seed ?? throw new ArgumentNullException(nameof(seed));

        _keyItems.InitializeDataSources(seed, seed.Settings.KeyItems);
        _characters.InitializeDataSources(_seed, seed.Settings.Characters);
        _bosses.InitializeDataSources(seed, seed.Settings.Bosses);
        //_checks.InitializeDataSources(_seed, seed.Settings.Checks);

        _seed.PropertyChanged += Seed_PropertyChanged;
        seed.Settings.KeyItems.PropertyChanged += Seed_PropertyChanged;
        seed.Settings.Characters.PropertyChanged += Seed_PropertyChanged;
        seed.Settings.Bosses.PropertyChanged += Seed_PropertyChanged;
        //seed.Settings.Checks.PropertyChanged += Seed_PropertyChanged;
        seed.Container.ButtonPressed += Seed_ButtonPressed;
        ArrangePanels();
    }

    private void Seed_ButtonPressed(InputAction action)
    {
        if (_seed?.RootSettings == null || _scrollables.Count == 0 || !_scrollables.TryGetElementAt(_scrollIndex, out var target))
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

    private void Seed_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        if (_seed == null) return;

        switch (e.PropertyName)
        {
            case nameof(Seed.BackgroundColor):
                BackColor = _seed.BackgroundColor;
                break;
            case nameof(PanelSettings.Priority):
            case nameof(PanelSettings.Enabled):
                ArrangePanels();
                break;
        }
    }

    //private void ArrangePanels()
    //{
    //    SuspendLayout();

    //    List<IPanel> panels = [_keyItems, _characters, _bosses, _checks];
    //    panels.Sort((x, y) => x.Priority > y.Priority ? 1 : -1);

    //    while (Controls.Count > 0)
    //        Controls.RemoveAt(0);

    //    bool filled = false;

    //    foreach (var panel in panels.Where(p => p.IsEnabled).Reverse())
    //    {
    //        if (panel is not Control control)
    //            continue;

    //        if (!filled && panel.CanHaveFillDockStyle && control.Visible)
    //        {
    //            control.Dock = DockStyle.Fill;
    //            filled = true;
    //        }
    //        else
    //        {
    //            control.Dock = panel.DefaultDockStyle;
    //        }

    //        Controls.Add(panel as Control);
    //    }

    //    if (filled && Controls[0] is IPanel { CanHaveFillDockStyle: false })
    //    {
    //        Controls[0].SendToBack();
    //    }

    //    _scrollables = panels.OrderBy(p => p.Priority).Where(p => p.IsEnabled).OfType<IScrollablePanel>().ToList();
    //    var enable = true;
    //    foreach (var item in _scrollables)
    //    {
    //        if (item.CanScroll)
    //        {
    //            item.IsEnabledForScrolling = enable;
    //            enable = false;
    //        }
    //        else
    //            item.IsEnabledForScrolling = false;
    //    }

    //    ResumeLayout();
    //}

    private void ArrangePanels()
    {
        SuspendLayout();
        TopPanel.SuspendLayout();

        List<IPanel> panels = [_keyItems, _bosses,  /*_checks*/];
        panels.Sort((x, y) => x.Priority > y.Priority ? 1 : -1);

        foreach (var control in panels.OfType<Control>())
            control.Resize -= TrackerResized;

        while (TopPanel.Controls.Count > 0)
            TopPanel.Controls.RemoveAt(0);

        while (Controls.Count > 0)
            Controls.RemoveAt(0);

        bool filled = false;
        foreach (var panel in panels.Where(p => p.IsEnabled).Take(2).Reverse())
        {
            if (panel is not Control control)
                continue;

            control.Resize += TrackerResized;
            TopPanel.Controls.Add(control);

            if (!filled && panel.CanHaveFillDockStyle && control.Visible)
            {
                control.AutoSize = false;
                control.Dock = DockStyle.Fill;
                filled = true;
            }
            else if (control.Dock != panel.DefaultDockStyle)
            {
                control.Dock = panel.DefaultDockStyle;
            }
        }

        TopPanel.Controls.Add(_characters);

        if (filled && TopPanel.Controls[0] is IPanel { CanHaveFillDockStyle: false })
        {
            TopPanel.Controls[0].SendToBack();
            _characters.SendToBack();
        }

        filled = false;
        foreach (var panel in panels.Where(p => p.IsEnabled).Skip(2).Reverse())
        {
            if (panel is not Control control)
                continue;

            if (!filled && panel.CanHaveFillDockStyle && control.Visible)
            {
                control.Dock = DockStyle.Fill;
                filled = true;
            }
            else
            {
                control.Dock = panel.DefaultDockStyle;
            }

            Controls.Add(panel as Control);
        }

        if (filled && Controls[0] is IPanel { CanHaveFillDockStyle: false })
        {
            Controls[0].SendToBack();
        }

        Controls.Add(TopPanel);
        _scrollables = panels.OrderBy(p => p.Priority).Where(p => p.IsEnabled).OfType<IScrollablePanel>().ToList();
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

        TopPanel.ResumeLayout();
        ResumeLayout();
    }

    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);
        BackColor = _seed?.BackgroundColor ?? Color.FromArgb(0, 0, 99);
    }
}
