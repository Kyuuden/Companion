using FF.Rando.Companion.Games.WorldsCollide;
using FF.Rando.Companion.Settings;
using FF.Rando.Companion.View;
using KGySoft.CoreLibraries;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace FF.Rando.Companion.Games.WorldsCollide.View;

public partial class WorldsCollideControl : UserControl
{
    private Seed? _seed;

    public WorldsCollideControl()
    {
        Dock = DockStyle.Fill;
        Name = "WorldsCollideControl";
        
        InitializeComponent();
    }

    public void InitializeDataSources(Seed seed)
    {
        _seed = seed ?? throw new ArgumentNullException(nameof(seed));
        
        _characters.InitializeDataSources(seed, seed.Settings.Characters);
        _checks.InitializeDataSources(seed, seed.Settings.Checks);
        _dragons.InitializeDataSources(seed, seed.Settings.Dragons);
        _statistics.InitializeDataSources(seed, seed.Settings.Stats);

        _seed.PropertyChanged += Seed_PropertyChanged;
        seed.Settings.Checks.PropertyChanged += Seed_PropertyChanged;
        seed.Settings.Characters.PropertyChanged += Seed_PropertyChanged;
        seed.Settings.Dragons.PropertyChanged += Seed_PropertyChanged;
        seed.Settings.Stats.PropertyChanged += Seed_PropertyChanged;

        //seed.ButtonPressed += Seed_ButtonPressed;
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
            case nameof(PanelSettings.Enabled):
                ArrangePanels();
                break;
        }
    }

    private void ArrangePanels()
    {
        SuspendLayout();

        List<IPanel> panels = [_characters, _checks, _dragons, _statistics];
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

        ResumeLayout();
    }

    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);
        BackColor = _seed?.BackgroundColor ?? Color.FromArgb(0, 0, 99);
    }
}
