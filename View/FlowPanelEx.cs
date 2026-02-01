using FF.Rando.Companion.Settings;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace FF.Rando.Companion.View;

public abstract partial class FlowPanelEx<TGame, TSettings> : FlowLayoutPanel, IPanel where TGame : IGame where TSettings : PanelSettings
{
    public FlowPanelEx()
    {
        DoubleBuffered = true;
        InitializeComponent();
    }

    public bool AutoResize { get; set; } = true;

    public SpacingMode SpacingMode { get; set; } = SpacingMode.None;

    public int WrapAfter { get; set; } = int.MaxValue;

    public bool Icons { get; set; }

    protected TGame? Game { get; private set; }

    protected TSettings? Settings { get; private set; }

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
        Game.Settings.BorderSettings.PropertyChanged += Settings_PropertyChanged;
        SuspendLayout();
        BackColor = Game.BackgroundColor;

        var controls = GenerateControls(Game);
        foreach (var control in controls)
        {
            control.VisibleChanged += Control_VisibleChanged;
        }

        Controls.AddRange(controls);
        Visible = Controls.Count > 0 && settings.Enabled;
        ResumeLayout(false);
        PerformLayout();
    }

    protected override void OnHandleCreated(EventArgs e)
    {
        base.OnHandleCreated(e);
        if (Game == null || Settings == null || !IsHandleCreated)
            return;

        BeginInvoke(() => Arrange());
    }

    private void Control_VisibleChanged(object sender, EventArgs e)
    {
        Arrange();
    }

    protected abstract Control[] GenerateControls(TGame seed);

    protected abstract Bitmap? GenerateBackgroundImage(Size unscaledSize);

    protected virtual void Settings_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        switch (e.PropertyName)
        {
            case nameof(PanelSettings.ScaleFactor):
            case nameof(BorderSettings.BorderScaleFactor):
                SuspendLayout();
                foreach (var sc in Controls.OfType<IScalableControl>())
                {
                    (sc as Control)?.SuspendLayout();
                    sc.Rescale();
                }
                ResumeLayout(false);
                PerformLayout();
                Arrange();
                break;
            case nameof(BorderSettings.BordersEnabled):
                Arrange();
                break;
            case nameof(PanelSettings.Enabled):
                Visible = Controls.Count > 0 && (Settings?.Enabled ?? Visible);
                break;
            case nameof(IGame.BackgroundColor):
                BackColor = Game?.BackgroundColor ?? BackColor;
                break;
        }
    }

    protected override void OnResize(EventArgs e)
    {
        base.OnResize(e);
        Arrange();
    }

    protected virtual void SortControls(ControlCollection controlCollection, int columns)
    {

    }

    protected virtual int GetItemWidth(ControlCollection controlCollection)
    {
        var visibleControls = controlCollection.OfType<Control>().Where(c => c.Visible).ToList();
        if (visibleControls.Any())
            return visibleControls.Max(c => c.Width) + 8;

        return controlCollection.OfType<Control>().Max(c => c.Width) + 8;
    }

    protected virtual bool CenterMultiColumnItems => false;

    protected void Arrange()
    {
        if (Game == null || Settings == null || Controls.Count == 0)
            return;

        var unscaledSize = Size.Unscale(Settings.ScaleFactor);

        if (unscaledSize.Height < 8 || unscaledSize.Width < 8)
            return;

        if (!Visible) 
            return;

        Padding = Game.Settings.BorderSettings.BordersEnabled
            ? new Padding(Game.Settings.BorderSettings.BorderScaleFactor.TileSize())
            : new Padding(0);

        var paddedWidth = Width - Padding.Horizontal;

        switch (SpacingMode)
        {
            case SpacingMode.Rows:
                break;
            case SpacingMode.Columns:
                var elementsize = GetItemWidth(Controls);
                var columns = Math.Min(WrapAfter, Math.Min(Controls.OfType<Control>().Where(c => c.Visible).Count(), paddedWidth / elementsize));
                var extra = paddedWidth - (elementsize * columns);
                var divisions = columns > 1 ? (columns - 1) * 2 : 2;
                var margin = extra / divisions;

                if (columns <= 0)
                    break;

                SuspendLayout();

                SortControls(Controls, columns);
                var invisibleCount = 0;
                for (int i = 0; i < Controls.Count; i++)
                {
                    var c = Controls[i];

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

                    SetFlowBreak(c, (i - invisibleCount + 1) % columns == 0);
                }
                ResumeLayout();

                break;
            case SpacingMode.None:
                break;
        }

        var lastControl = Controls[^1];

        if (AutoResize)
        {
            switch (FlowDirection)
            {
                case FlowDirection.LeftToRight:
                case FlowDirection.RightToLeft:
                    Height = lastControl.Bottom + lastControl.Margin.Bottom + Padding.Bottom;
                    break;
                case FlowDirection.TopDown:
                case FlowDirection.BottomUp:
                    Width = lastControl.Right + lastControl.Margin.Right + Padding.Right;
                    break;
            }

            if (!WrapContents && SpacingMode == SpacingMode.None)
            {
                switch (FlowDirection)
                {
                    case FlowDirection.LeftToRight:
                    case FlowDirection.RightToLeft:
                        Width = lastControl.Right + lastControl.Margin.Right + Padding.Right;
                        break;
                    case FlowDirection.TopDown:
                    case FlowDirection.BottomUp:
                        Height = lastControl.Bottom + lastControl.Margin.Bottom + Padding.Bottom;
                        break;
                }
            }
        }

        if (Game.Settings.BorderSettings.BordersEnabled)
        {
            ReplaceBackgroundImage();
        }
        else
        {
            BackgroundImage?.Dispose();
            BackgroundImage = null;
        }
    }

    protected virtual void ReplaceBackgroundImage(bool force = false)
    {
        var unscaledSize = Size.Unscale(Game?.Settings?.BorderSettings.BorderScaleFactor ?? 1.0f);
        if (Settings == null || (unscaledSize == BackgroundImage?.Size && !force))
            return;

        BackgroundImage?.Dispose();
        BackgroundImage = null;
        BackgroundImage = GenerateBackgroundImage(unscaledSize);
    }
}
