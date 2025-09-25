using FF.Rando.Companion.Settings;
using FF.Rando.Companion.View;
using KGySoft.CoreLibraries;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace FF.Rando.Companion.MysticQuestRandomizer.View;
public partial class MysticQuestRandomizerControl : UserControl
{
    private Seed? _seed;
    private List<IScrollablePanel> _scrollables = [];
    private int _scrollIndex = 0;

    public MysticQuestRandomizerControl()
    {
        Dock = DockStyle.Fill;
        Name = "MysticQuestRandomizerControl";
        
        InitializeComponent();
    }

    public void InitializeDataSources(Seed seed)
    {
        _seed = seed ?? throw new ArgumentNullException(nameof(seed));

        _equipment.InitializeDataSources(seed, seed.Settings.Equipment);
        _elements.InitializeDataSources(seed, seed.Settings.Elements);
        _companions.InitializeDataSources(seed, seed.Settings.Companions);
        //_statistics.InitializeDataSources(seed, seed.Settings.Stats);

        _seed.PropertyChanged += Seed_PropertyChanged;
        seed.Settings.Equipment.PropertyChanged += Seed_PropertyChanged;
        seed.Settings.Elements.PropertyChanged += Seed_PropertyChanged;
        seed.Settings.Companions.PropertyChanged += Seed_PropertyChanged;
       // seed.Settings.Stats.PropertyChanged += Seed_PropertyChanged;

        seed.ButtonPressed += Seed_ButtonPressed;
        ArrangePanels();
    }

    private void Seed_ButtonPressed(string obj)
    {
        if (_seed?.RootSettings == null || _scrollables.Count == 0 || !_scrollables.TryGetElementAt(_scrollIndex, out var target))
            return;

        if (target.CanScroll && obj.Equals(_seed.RootSettings.NextPageButton, StringComparison.InvariantCultureIgnoreCase))
        {
            target.ScrollRight();
        }
        else if (target.CanScroll && obj.Equals(_seed.RootSettings.PreviousPageButton, StringComparison.InvariantCultureIgnoreCase))
        {
            target.ScrollLeft();
        }
        else if (target.CanScroll && obj.Equals(_seed.RootSettings.ScrollDownButton, StringComparison.InvariantCultureIgnoreCase))
        {
            target.ScrollDown();
        }
        else if (target.CanScroll && obj.Equals(_seed.RootSettings.ScrollUpButton, StringComparison.InvariantCultureIgnoreCase))
        {
            target.ScrollUp();
        }
        else if (obj.Equals(_seed.RootSettings.NextPanelButton, StringComparison.InvariantCultureIgnoreCase))
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
            case nameof(PanelSettings.Enabled):
                ArrangePanels();
                break;
        }
    }

    private void ArrangePanels()
    {
        SuspendLayout();

        List<IPanel> panels = [_equipment, _elements, /*_statistics,*/ _companions];
        panels.Sort((x, y) => x.Priority > y.Priority ? 1 : -1);

        while (Controls.Count > 0)
            Controls.RemoveAt(0);

        bool filled = false;

        foreach (var panel in panels.Where(p => p.IsEnabled).Reverse())
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

        ResumeLayout();
    }

    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);
        BackColor = _seed?.BackgroundColor ?? Color.FromArgb(0, 0, 99);
    }
}
