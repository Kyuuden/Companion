using FF.Rando.Companion.Settings;
using FF.Rando.Companion.View;
using System;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace FF.Rando.Companion.Games.WorldsCollide.View;
internal class StatsPanel : FlowPanelEx<StatsPanel.StatsPanelSettings>
{
    public StatsPanel()
        : base()
    {
        AutoResize = false;
        SpacingMode = SpacingMode.Rows;
        FlowDirection = FlowDirection.TopDown;
        WrapContents = false;
    }

    public StatsPanel(Seed seed)
        : base()
    {
        AutoResize = false;
        DefaultColumnSpacing = 0;
        DefaultRowSpacing = 0;
        SpacingMode = SpacingMode.Rows;
        FlowDirection = FlowDirection.TopDown;
        WrapContents = false;
        InitializeDataSources(seed, new StatsPanelSettings(seed.State));
    }

    protected override void OnHandleCreated(EventArgs e)
    {
        BeginInvoke(SetSize);
    }

    protected override void Settings_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        base.Settings_PropertyChanged(sender, e);

        if (sender == Settings && e.PropertyName == nameof(PanelSettings.ScaleFactor) ||
            sender == Game?.Settings.BorderSettings)
        {
            SetSize();
        }
    }

    protected override void OnDockChanged(EventArgs e)
    {
        if (IsHandleCreated)
        {
            SetSize();
            Arrange();
        }
    }

    private void SetSize()
    {
        var firstStat = Controls.OfType<StatisticControl>().FirstOrDefault();
        if (firstStat != null)
        {
            switch (Dock)
            {
                case DockStyle.Left:
                case DockStyle.Right:
                    Width = firstStat.Width + firstStat.Margin.Horizontal + Padding.Horizontal;
                    break;
                case DockStyle.Top:
                case DockStyle.Bottom:
                    Height = firstStat.Height + firstStat.Margin.Vertical + Padding.Vertical;
                    break;
            }
        }
    }

    public override DockStyle DefaultDockStyle => DockStyle.Right;

    protected override Control[] GenerateControls(Seed seed)
        =>
        [
            new StatisticControl(seed, Settings!, Enums.Statistic.CharacterCount),
            new StatisticControl(seed, Settings!, Enums.Statistic.EsperCount),
            new StatisticControl(seed, Settings!, Enums.Statistic.DragonCount),
            new StatisticControl(seed, Settings!, Enums.Statistic.CheckCount),
            new StatisticControl(seed, Settings!, Enums.Statistic.BossCount),
            new StatisticControl(seed, Settings!, Enums.Statistic.ChestCount),
            new EventStatisticControl(seed, Settings!, Enums.EventType.UNLOCKED_FINAL_KEFKA),
            new EventStatisticControl(seed, Settings!, Enums.EventType.UNLOCKED_KT_SKIP)
        ];

    internal class StatsPanelSettings : IPanelSettings
    {
        private float _scaleFactor;

        public StatsPanelSettings(State state)
        {
            _scaleFactor = state.EffectiveZoomRate;
            state.PropertyChanged += State_PropertyChanged;
        }

        private void State_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(State.EffectiveZoomRate) && sender is State state)
            {
                ScaleFactor = state.EffectiveZoomRate;
            }
        }

        public bool Enabled => true;

        public float ScaleFactor
        {
            get => _scaleFactor;
            set
            {
                if (_scaleFactor == value) return;
                _scaleFactor = value;
                NotifyPropertyChanged();
            }
        }

        public int Priority => 1;

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        } 
    }
}
