using FF.Rando.Companion.Games;
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

    public virtual bool CanHaveFillDockStyle => false;

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
                SuspendLayout();
                foreach (var sc in Controls.OfType<IScalableControl>())
                {
                    (sc as Control)?.SuspendLayout();
                    sc.Rescale();
                    (sc as Control)?.ResumeLayout();
                }
                ResumeLayout(false);
                PerformLayout();
                Arrange();
                break;
            case nameof(BorderSettings.BordersEnabled):
            case nameof(BorderSettings.BorderScaleFactor):
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

    private Size _lastSize;

    protected override void OnResize(EventArgs e)
    {
        base.OnResize(e);

        if (_lastSize.IsEmpty || _lastSize != Size)
        {
            _lastSize = Size;
            Arrange();
        }
    }

    protected virtual void SortControls(ControlCollection controlCollection, int columns)
    {

    }
   
    public int DefaultColumnSpacing { get; set; } = 4;
    public int DefaultRowSpacing { get; set; } = 4;

    protected Padding DefaultItemMargin => new (DefaultColumnSpacing / 2, DefaultRowSpacing / 2, DefaultColumnSpacing / 2, DefaultRowSpacing / 2);

    protected virtual int GetItemWidth(ControlCollection controlCollection)
    {
        var visibleControls = controlCollection.OfType<Control>().Where(c => c.Visible).ToList();
        if (visibleControls.Any())
            return visibleControls.Max(c => c.Width) + DefaultColumnSpacing;

        return controlCollection.OfType<Control>().Max(c => c.Width) + DefaultColumnSpacing;
    }

    protected virtual int GetItemHeight(ControlCollection controlCollection)
    {
        var visibleControls = controlCollection.OfType<Control>().Where(c => c.Visible).ToList();
        if (visibleControls.Any())
            return visibleControls.Max(c => c.Height) + DefaultRowSpacing;

        return controlCollection.OfType<Control>().Max(c => c.Height) + DefaultRowSpacing;
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
                foreach (Control control in Controls)
                    control.Margin = DefaultItemMargin;
                break;
            case SpacingMode.Columns:
                var elementsize = GetItemWidth(Controls);
                var columns = WrapContents
                    ? Math.Min(WrapAfter, Math.Min(Controls.OfType<Control>().Where(c => c.Visible).Count(), paddedWidth / elementsize))
                    : Controls.OfType<Control>().Where(c => c.Visible).Count();

                var extra = paddedWidth - (elementsize * columns);
                var divisions = columns > 1 ? (columns - 1) * 2 : 2;
                var margin = Math.Max(0, extra / divisions);

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
                        if (!CenterMultiColumnItems || c.Width + DefaultColumnSpacing <= elementsize)
                        {
                            var nonStandardWidthAdjustment = Math.Max(0, elementsize - c.Width - DefaultColumnSpacing);
                            Padding adjustment = ((i - invisibleCount) % columns) switch
                            {
                                0 => new(0, 0, margin + nonStandardWidthAdjustment, 0), // first column
                                _ when (i - invisibleCount + 1) % columns == 0 => new(margin, 0, 0, 0), //last column 
                                _ => new Padding(margin, 0, margin + nonStandardWidthAdjustment, 0)
                            };

                            c.Margin = DefaultItemMargin + adjustment;
                        }
                        else
                        {
                            var columnSpan = (int)Math.Ceiling((double)c.Width / (elementsize - DefaultColumnSpacing));
                            var columnsWidth = columnSpan * elementsize + margin * (columnSpan - 1) * 2;
                            var halfRemaining = (columnsWidth - c.Width) / 2;
                            c.Margin = DefaultItemMargin + new Padding(halfRemaining, 0, halfRemaining, 0);
                        }
                    }

                    SetFlowBreak(c, (i - invisibleCount + 1) % columns == 0);
                }

                if (Dock == DockStyle.Fill)
                {
                    var height = Size.Height - Padding.Vertical;
                    var itemHeight = GetItemHeight(Controls);

                    var rows = (Controls.Count - invisibleCount) / columns;
                    if ((Controls.Count - invisibleCount) % columns != 0)
                        rows++;

                    var extraHeight = height - (elementsize * rows);
                    var extraMargin = (extraHeight / rows) / 2;

                    for (int i = 0; i < Controls.Count; i++)
                    {
                        Controls[i].Margin += new Padding(0, extraMargin, 0, extraMargin);
                    }
                }

                ResumeLayout();

                break;
            case SpacingMode.None:
                foreach (Control control in Controls)
                    control.Margin = DefaultItemMargin;
                break;
        }

        var farthestRightControl = Controls.OfType<Control>().Where(c => c.Visible).OrderByDescending(c=>c.Right).FirstOrDefault() as Control;
        var farthestBottomControl = Controls.OfType<Control>().Where(c => c.Visible).OrderByDescending(c => c.Bottom).FirstOrDefault() as Control;

        if (AutoResize && Dock != DockStyle.Fill)
        {
            switch (FlowDirection)
            {
                case FlowDirection.LeftToRight:
                case FlowDirection.RightToLeft:
                    Height = farthestBottomControl.Bottom + farthestBottomControl.Margin.Bottom + Padding.Bottom;
                    break;
                case FlowDirection.TopDown:
                case FlowDirection.BottomUp:
                    Width = farthestRightControl.Right + farthestRightControl.Margin.Right + Padding.Right;
                    break;
            }

            if (!WrapContents && SpacingMode == SpacingMode.None)
            {
                switch (FlowDirection)
                {
                    case FlowDirection.LeftToRight:
                    case FlowDirection.RightToLeft:
                        Width = farthestRightControl.Right + farthestRightControl.Margin.Right + Padding.Right;
                        break;
                    case FlowDirection.TopDown:
                    case FlowDirection.BottomUp:
                        Height = farthestBottomControl.Bottom + farthestBottomControl.Margin.Bottom + Padding.Bottom;
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
