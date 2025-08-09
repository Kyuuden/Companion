using FF.Rando.Companion.FreeEnterprise._5._0._0;
using FF.Rando.Companion.FreeEnterprise.Settings;
using KGySoft.CoreLibraries;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace FF.Rando.Companion.FreeEnterprise.View;
public partial class FreeEnterpriseControl : UserControl
{
    private ISeed? _seed;
    private List<IScrollablePanel> _scrollables = [];
    private int _scrollIndex = 0;

    public FreeEnterpriseControl()
    {
        Dock = DockStyle.Fill;
        Name = "FreeEnterpriseControl";
        InitializeComponent();
        _party.Resize += TrackerResized;
    }

    public void InitializeDataSources(ISeed seed)
    {
        _seed = seed ?? throw new ArgumentNullException(nameof(seed));

        _keyItems.InitializeDataSources(seed, seed.Settings.KeyItems);
        _party.InitializeDataSources(seed, seed.Settings.Party);
        _bosses.InitializeDataSources(seed, seed.Settings.Bosses);
        _objectives.InitializeDataSources(seed, seed.Settings.Objectives);
        _stats.InitializeDataSources(seed, seed.Settings.Stats);
        _locations.InitializeDataSources(seed, seed.Settings.Locations);
        _seed.PropertyChanged += Seed_PropertyChanged;
        seed.Settings.KeyItems.PropertyChanged += Seed_PropertyChanged;
        seed.Settings.Party.PropertyChanged += Seed_PropertyChanged;
        seed.Settings.Bosses.PropertyChanged += Seed_PropertyChanged;
        seed.Settings.Objectives.PropertyChanged += Seed_PropertyChanged;
        seed.Settings.Stats.PropertyChanged += Seed_PropertyChanged;
        seed.Settings.Locations.PropertyChanged += Seed_PropertyChanged;

        seed.ButtonPressed += Seed_ButtonPressed;
        ArrangePanels();
    }

    private void Seed_ButtonPressed(string obj)
    {
        var keyBindings = _seed?.Settings?.KeyBindings;
        if (keyBindings == null || _scrollables.Count == 0 || !_scrollables.TryGetElementAt(_scrollIndex, out var target))
            return;

        if (target.CanScroll && obj.Equals(keyBindings.NextPageButton, StringComparison.InvariantCultureIgnoreCase))
        {
            target.ScrollRight();
        }
        else if (target.CanScroll && obj.Equals(keyBindings.PreviousPageButton, StringComparison.InvariantCultureIgnoreCase))
        {
            target.ScrollLeft();
        }
        else if (target.CanScroll && obj.Equals(keyBindings.ScrollDownButton, StringComparison.InvariantCultureIgnoreCase))
        {
            target.ScrollDown();
        }
        else if (target.CanScroll && obj.Equals(keyBindings.ScrollUpButton, StringComparison.InvariantCultureIgnoreCase))
        {
            target.ScrollUp();
        }
        else if (obj.Equals(keyBindings.NextPanelButton, StringComparison.InvariantCultureIgnoreCase))
        {
            target.IsEnabledForScrolling = false;
            _scrollIndex = (_scrollIndex + 1) % _scrollables.Count;
            _scrollables[_scrollIndex].IsEnabledForScrolling = true;
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
            case nameof(PanelSettings.InTopPanel):
            case nameof(PanelSettings.Enabled):
                ArrangePanels();
                break;
        }
    }

    private void ArrangePanels()
    {
        SuspendLayout();
        TopPanel.SuspendLayout();

        List<IPanel> panels = [_keyItems, _bosses, _objectives, _stats, _locations];
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

            if (!filled && panel.CanHaveFillDockStyle && control.Visible)
            {
                control.Dock = DockStyle.Fill;
                filled = true;
            }
            else
            {
                control.Dock = panel.DefaultDockStyle;
            }
            control.Resize += TrackerResized;
            TopPanel.Controls.Add(control);
        }

        TopPanel.Controls.Add(_party);

        if (TopPanel.Controls[0] is IPanel { CanHaveFillDockStyle: false })
        {
            TopPanel.Controls[0].SendToBack();
            _party.SendToBack();
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

        if (Controls[0] is IPanel { CanHaveFillDockStyle: false })
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

    private void TrackerResized(object sender, EventArgs e)
        => ResizeTopPanel();

    private void ResizeTopPanel()
    {
        TopPanel.Height = Math.Max(
            TopPanel.Controls.OfType<Control>().Where(c => c.Dock is DockStyle.Left or DockStyle.Right).Sum(c => c.Height),
            TopPanel.Controls.OfType<Control>().Where(c => c.Dock is DockStyle.Top or DockStyle.Bottom).Sum(c => c.Height));
    }
}
