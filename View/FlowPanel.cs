using FF.Rando.Companion.Settings;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace FF.Rando.Companion.View;

public abstract partial class FlowPanel<TGame, TSettings> : UserControl, IPanel where TGame : IGame where TSettings : PanelSettings
{
    bool _isloaded = false;

    public FlowPanel()
    {
        DoubleBuffered = true;
        InitializeComponent();
    }

    public bool AutoResize { get; set; } = true;

    public SpacingMode SpacingMode { get; set; } = SpacingMode.None;

    public int WrapAfter { get; set; } = int.MaxValue;

    public bool WrapContents
    {
        get => flowLayoutPanel1.WrapContents;
        set => flowLayoutPanel1.WrapContents = value;
    }

    public bool Icons { get; set; }

    protected TGame? Game { get; private set; }
    protected TSettings? Settings { get; private set; }

    public FlowDirection FlowDirection { get => flowLayoutPanel1.FlowDirection; set => flowLayoutPanel1.FlowDirection = value; }

    public abstract DockStyle DefaultDockStyle { get; }

    public bool CanHaveFillDockStyle => false;

    public virtual int Priority => Settings?.Priority ?? int.MaxValue;

    public virtual bool IsEnabled => Settings?.Enabled ?? false;

    public virtual void InitializeDataSources(TGame game, TSettings settings)
    {
        Game = game ?? throw new ArgumentNullException(nameof(game));
        Settings = settings ?? throw new ArgumentNullException(nameof(settings));
        Settings.PropertyChanged += Settings_PropertyChanged;
        Game.PropertyChanged += Settings_PropertyChanged;
        SuspendLayout();
        BackColor = Game.BackgroundColor;
        flowLayoutPanel1.SuspendLayout();
        flowLayoutPanel1.BackColor = Game.BackgroundColor;
        flowLayoutPanel1.Controls.AddRange(GenerateControls(Game));
        Visible = flowLayoutPanel1.Controls.Count > 0 && settings.Enabled;
        flowLayoutPanel1.ResumeLayout(false);
        flowLayoutPanel1.PerformLayout();
        ResumeLayout(false);
    }

    protected abstract Control[] GenerateControls(TGame seed);

    protected abstract Bitmap? GenerateBackgroundImage(Size unscaledSize);

    protected virtual void Settings_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(PanelSettings.ScaleFactor))
        {
            SuspendLayout();
            flowLayoutPanel1.SuspendLayout();
            foreach (var sc in flowLayoutPanel1.Controls.OfType<IScalableControl>())
            {
                (sc as Control)?.SuspendLayout();
                sc.Rescale();
            }
            flowLayoutPanel1.ResumeLayout(false);
            flowLayoutPanel1.PerformLayout();
            ResumeLayout(false);

            Arrange();
        }
        if (e.PropertyName == nameof(PanelSettings.Enabled))
        {
            Visible = flowLayoutPanel1.Controls.Count > 0 && (Settings?.Enabled ?? Visible);
        }

        if (e.PropertyName == nameof(IGame.BackgroundColor))
        {
            BackColor = Game?.BackgroundColor ?? BackColor;
            flowLayoutPanel1.BackColor = Game?.BackgroundColor ?? flowLayoutPanel1.BackColor;
        }
    }

    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);

        _isloaded = true;
        if (Game == null || Settings == null)
            return;

        Arrange();
    }

    protected override void OnResize(EventArgs e)
    {
        base.OnResize(e);

        if (_isloaded) Arrange();
    }

    protected virtual void SortControls(ControlCollection controlCollection, int columns)
    {

    }

    protected virtual int GetItemWidth(ControlCollection controlCollection)
    {
        return controlCollection.OfType<Control>().Where(c => c.Visible).Max(c => c.Width) + 8;
    }

    protected virtual bool CenterMultiColumnItems => false;

    protected void Arrange()
    {
        if (Game == null || Settings == null || flowLayoutPanel1.Controls.Count == 0)
            return;

        var unscaledSize = Settings.Unscale(Size);

        if (unscaledSize.Height < 8 || unscaledSize.Width < 8)
            return;

        Padding = new Padding(Settings.TileSize);

        switch (SpacingMode)
        {
            case SpacingMode.Rows:
                break;
            case SpacingMode.Columns:
                var elementsize = GetItemWidth(flowLayoutPanel1.Controls);
                var columns = Math.Min(WrapAfter, Math.Min(flowLayoutPanel1.Controls.OfType<Control>().Where(c => c.Visible).Count(), flowLayoutPanel1.Width / elementsize));
                var extra = flowLayoutPanel1.Width - (elementsize * columns);
                var divisions = columns > 1 ? (columns - 1) * 2 : 2;
                var margin = extra / divisions;

                if (columns <= 0)
                    break;

                flowLayoutPanel1.SuspendLayout();

                SortControls(flowLayoutPanel1.Controls, columns);
                var invisibleCount = 0;
                for (int i = 0; i < flowLayoutPanel1.Controls.Count; i++)
                {
                    var c = flowLayoutPanel1.Controls[i];

                    if (!c.Visible)
                    {
                        invisibleCount++;
                    }
                    else
                    {
                        if (!CenterMultiColumnItems || c.Width + 8 <= elementsize)
                        {
                            var marginAdjustment = Math.Max(0, elementsize - c.Width - 8);

                            if ((i - invisibleCount) % columns == 0)
                                c.Margin = new(4, 4, 4 + margin + marginAdjustment, 4);
                            else if ((i - invisibleCount + 1) % columns == 0)
                                c.Margin = new(4 + margin, 4, 4, 4);
                            else
                                c.Margin = new(4 + margin, 4, 4 + margin + marginAdjustment, 4);
                        }
                        else
                        {
                            var columnSpan = (int)Math.Ceiling((double)c.Width / (elementsize - 8));
                            var columnsWidth = columnSpan * elementsize + margin * (columnSpan - 1) * 2;
                            var halfRemaining = (columnsWidth - c.Width) / 2;
                            c.Margin = new(4 + halfRemaining, 4, 4 + halfRemaining, 4);
                        }
                    }

                    flowLayoutPanel1.SetFlowBreak(c, (i - invisibleCount + 1) % columns == 0);
                }
                flowLayoutPanel1.ResumeLayout();
                //flowLayoutPanel1.PerformLayout();

                break;
            case SpacingMode.None:
                Padding = new Padding(Settings.TileSize);
                break;
        }

        var lastControl = flowLayoutPanel1.Controls[^1];

        if (AutoResize)
        {
            switch (FlowDirection)
            {
                case FlowDirection.LeftToRight:
                case FlowDirection.RightToLeft:
                    Height = lastControl.Bottom + lastControl.Margin.Bottom + Padding.Vertical;
                    break;
                case FlowDirection.TopDown:
                case FlowDirection.BottomUp:
                    Width = lastControl.Right + lastControl.Margin.Right + Padding.Horizontal;
                    break;
            }

            if (!WrapContents && SpacingMode == SpacingMode.None)
            {
                switch (FlowDirection)
                {
                    case FlowDirection.LeftToRight:
                    case FlowDirection.RightToLeft:
                        Width = lastControl.Right + lastControl.Margin.Right + Padding.Horizontal;
                        break;
                    case FlowDirection.TopDown:
                    case FlowDirection.BottomUp:
                        Height = lastControl.Bottom + lastControl.Margin.Bottom + Padding.Vertical;
                        break;
                }
            }
        }

        unscaledSize = Settings.Unscale(Size);
        BackgroundImage?.Dispose();
        BackgroundImage = null;
        BackgroundImage = GenerateBackgroundImage(unscaledSize);
    }
}
