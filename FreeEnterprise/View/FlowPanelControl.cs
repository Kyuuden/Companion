using FF.Rando.Companion.FreeEnterprise._5._0._0;
using FF.Rando.Companion.FreeEnterprise.Settings;
using KGySoft.Drawing.Imaging;
using System;
using System.Linq;
using System.Windows.Forms;

namespace FF.Rando.Companion.FreeEnterprise.View;

public enum SpacingMode
{
    None, Columns, Rows
}

public abstract partial class FlowPanelControl<T> : UserControl where T: PanelSettings
{
    bool _isloaded = false;

    public FlowPanelControl()
    {
        DoubleBuffered = true;
        InitializeComponent();
    }

    public bool AutoResize { get; set; } = true;

    public SpacingMode SpacingMode { get; set; } = SpacingMode.None;

    public bool WrapContents
    {
        get => flowLayoutPanel1.WrapContents;
        set => flowLayoutPanel1.WrapContents = value;
    }

    public bool Icons { get; set; }

    protected ISeed? Seed {  get; private set ; }
    protected T? Settings { get; private set; }

    public FlowDirection FlowDirection { get => flowLayoutPanel1.FlowDirection; set => flowLayoutPanel1.FlowDirection = value; }

    public virtual void InitializeDataSources(ISeed seed, T settings)
    {
        Seed = seed ?? throw new ArgumentNullException(nameof(seed));
        Settings = settings ?? throw new ArgumentNullException(nameof(settings));
        Visible = settings.Enabled;
        Settings.PropertyChanged += Settings_PropertyChanged;
        Seed.PropertyChanged += Settings_PropertyChanged;
        SuspendLayout();
        BackColor = seed.BackgroundColor;
        flowLayoutPanel1.SuspendLayout();
        flowLayoutPanel1.BackColor = seed.BackgroundColor;
        flowLayoutPanel1.Controls.AddRange(GenerateControls(Seed));
        flowLayoutPanel1.ResumeLayout(false);
        flowLayoutPanel1.PerformLayout();
        ResumeLayout(false);

    }

    protected abstract Control[] GenerateControls(ISeed seed);

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
            Visible = Settings?.Enabled ?? Visible;
        }

        if (e.PropertyName == nameof(ISeed.BackgroundColor))
        {
            BackColor = Seed?.BackgroundColor ?? BackColor;
            flowLayoutPanel1.BackColor = Seed?.BackgroundColor ?? flowLayoutPanel1.BackColor;
        }
    }

    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);

        _isloaded = true;
        if (Seed == null || Settings == null)
            return;

        Arrange();
    }

    protected override void OnResize(EventArgs e)
    {
        base.OnResize(e);

        if (_isloaded) Arrange();
    }

    private bool _arranging = false;
    private void DelayedArrange()
    {
        if (!_arranging)
            BeginInvoke(() =>
            {
                _arranging = true;
                Arrange();
                _arranging = false;
            });
    }

    protected void Arrange()
    {
        if (Seed == null || Settings == null)
            return;

        var unscaledSize = Settings.Unscale(Size);

        if (unscaledSize.Height < 8 || unscaledSize.Width < 8)
            return;
        
        switch (SpacingMode)
        {
            case SpacingMode.Rows:
                break;
            case SpacingMode.Columns:
                var elementsize = (flowLayoutPanel1.Controls.OfType<Control>().Max(c => c.Width) + 8);
                var columns = Math.Min(flowLayoutPanel1.Controls.Count, flowLayoutPanel1.Size.Width / elementsize);
                var extra = (flowLayoutPanel1.Size.Width - (elementsize * columns)) - 8;
                var divisions = columns > 1 ? (columns - 1) * 2 : 2;
                var margin =  Math.Max(4, extra / divisions);

                if (columns <= 0)
                    break;

                flowLayoutPanel1.SuspendLayout();

                if (extra > (margin * divisions))
                {
                    var paddingAjustment = (extra - (margin * divisions)) / 2 + 1;
                    Padding = new Padding(Settings.TileSize+ paddingAjustment, Settings.TileSize, Settings.TileSize+ paddingAjustment, Settings.TileSize);
                }
                else
                {
                    Padding = new Padding(Settings.TileSize);
                }

                for (int i = 0; i < flowLayoutPanel1.Controls.Count; i++)
                {
                    var c = flowLayoutPanel1.Controls[i];

                    if (i % columns == 0)
                        c.Margin = new(4, 4, margin, 4);
                    else if ((i + 1) % columns == 0)
                        c.Margin = new(margin, 4, 4, 4);
                    else
                        c.Margin = new(margin, 4, margin, 4);
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

        BackgroundImage = Seed?.Font.RenderBox(unscaledSize.Width / 8, unscaledSize.Height / 8)?.ToBitmap();
    }
}
