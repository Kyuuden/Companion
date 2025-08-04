using FF.Rando.Companion.FreeEnterprise._5._0._0;
using FF.Rando.Companion.FreeEnterprise.Settings;
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

    public FreeEnterpriseControl()
    {
        Dock = DockStyle.Fill;
        Name = "FreeEnterpriseControl";
        InitializeComponent();
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

        ArrangePanels();
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

        List<IPanel> panels = [_keyItems, _party, _bosses, _objectives, _stats, _locations];

        while (TopPanel.Controls.Count > 0) 
            TopPanel.Controls.RemoveAt(0);

        while (Controls.Count > 0)
            Controls.RemoveAt(0);

        foreach (var panel in panels)
        {
            if (panel is not Control control)
                continue;

            control.Resize -= TrackerResized;
        }

        foreach (var panel in panels.Where(p => p.InTopPanel).OrderByDescending(p => p.Priority))
        {
            if (panel is not Control control)
                continue;

            control.Resize += TrackerResized;
            TopPanel.Controls.Add(panel as Control);
        }

        bool filled = false;
        foreach (var panel in panels.Where(p => !p.InTopPanel).OrderByDescending(p => p.Priority))
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

        if (Controls[0] is IPanel p && !p.CanHaveFillDockStyle)
        {
            Controls[0].SendToBack();
        }

        Controls.Add(TopPanel);
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
