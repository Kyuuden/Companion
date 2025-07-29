using FF.Rando.Companion.FreeEnterprise._5._0._0;
using System;
using System.ComponentModel;
using System.Drawing;
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

        keyItemsControl1.InitializeDataSources(seed, seed.Settings.KeyItems);
        partyControl1.InitializeDataSources(seed, seed.Settings.Party);
        bossesControl1.InitializeDataSources(seed, seed.Settings.Bosses);
        objectivesControl.InitializeDataSources(seed, seed.Settings.Objectives);
        statsControl1.InitializeDataSources(seed, seed.Settings.Stats);
        _seed.PropertyChanged += Seed_PropertyChanged;
    }

    private void Seed_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        if (_seed == null) return;

        switch (e.PropertyName)
        {
            case nameof(Seed.BackgroundColor):
                BackColor = _seed.BackgroundColor;
                break;
        }
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
        TopPanel.Height = Math.Max(partyControl1.Height, bossesControl1.Height + keyItemsControl1.Height);
    }
}
