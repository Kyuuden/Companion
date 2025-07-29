using BizHawk.Client.Common;
using BizHawk.Client.EmuHawk;
using BizHawk.Emulation.Common;
using FF.Rando.Companion.Settings;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace FF.Rando.Companion;

[ExternalToolEmbeddedIcon("FF.Rando.Companion.Resources.Crystal.png")]
[ExternalTool("Final Fantasy Rando Companion", Description = "An autotracker (currently) for Free Enterprise, a Final Fantasy IV randomizer")]
public partial class MainForm : ToolFormBase, IExternalToolForm
{
    protected override string WindowTitleStatic => "Final Fantasy Rando Companion";

    public ApiContainer? _maybeAPIContainer { get; set; }

    private ApiContainer APIs => _maybeAPIContainer!;

    private ISettings _settings;

    [OptionalService]
    public IMemoryDomains? _memoryDomains { get; set; }

    private bool _parentFormLinked = false;

    private GameViewModel _viewModel;
    private IGame? _game;

    public MainForm() 
    {
        InitializeComponent();

        _settings = new RootSettings();
        _settings.PropertyChanged += PropertyChanged;
        _viewModel = new GameViewModel(_settings);
        _viewModel.PropertyChanged += PropertyChanged;

        StopWatchLabel.Font = _settings.Font;
        StopWatchLabel.ForeColor = _settings.TextColor;
        StopWatchLabel.Height = StopWatchLabel.PreferredHeight;
    }

    public override bool BlocksInputWhenFocused => false;

    private void PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        switch (e.PropertyName)
        {
            case nameof(ISettings.DockOffset):
            case nameof(ISettings.DockSide):
            case nameof(ISettings.WindowStyle):
                DockToScreen();
                break;
            case nameof(ISettings.Font):
                StopWatchLabel.Font = _settings.Font;
                StopWatchLabel.Height = StopWatchLabel.PreferredHeight;
                break;
            case nameof(ISettings.TextColor):
                StopWatchLabel.ForeColor = _settings.TextColor;
                break;
            case nameof(GameViewModel.Game) when _viewModel.Game != null:

                var icon = _viewModel.Game.Icon;
                var hIcon = icon.GetHicon();
                Icon = Icon.FromHandle(hIcon);

                TrackerPanel.SuspendLayout();
                if (_game != null)
                {
                    _game.PropertyChanged -= Game_PropertyChanged;
                    foreach (Control control in TrackerPanel.Controls)
                    {
                        control.Dispose();
                    }

                    TrackerPanel.Controls.Clear();
                    _game.Dispose();
                }

                StopWatchLabel.Visible = true;
                _game = _viewModel.Game;
                _viewModel.Game.PropertyChanged += Game_PropertyChanged;
                TrackerPanel.Controls.Add(_viewModel.Game.CreateControls());
                TrackerPanel.ResumeLayout(false);
                TrackerPanel.PerformLayout();
                break;
        }
    }

    private void Game_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        switch (e.PropertyName)
        {
            case nameof(IGame.Started):
                break;
        }
    }

    private bool _docking = false;

    private void DockToScreen()
    {
        var main = (BizHawk.Client.EmuHawk.MainForm)MainForm;
        if (main == null)
            return;

        if (main.WindowState == FormWindowState.Minimized)
        {
            WindowState = FormWindowState.Minimized;
            return;
        }
        else
            WindowState = FormWindowState.Normal;

        _docking = true;
        var currentSize = Size;

        switch (_settings.WindowStyle)
        {
            case WindowStyle.Dock_16x9:
                FormBorderStyle = FormBorderStyle.FixedSingle;
                Size = new Size((int)(APIs.EmuClient.ScreenHeight() * 16.0 / 9.0) - APIs.EmuClient.ScreenWidth(), main.Height);
                Location = new Point(
                    _settings.DockSide == DockSide.Right
                        ? main.Location.X + main.Width + _settings.DockOffset
                        : main.Location.X - Width - _settings.DockOffset,
                    main.Location.Y);

                if (currentSize != Size)
                    Invalidate();

                break;
            case WindowStyle.Dock_16x10:
                FormBorderStyle = FormBorderStyle.FixedSingle;
                Size = new Size((int)(APIs.EmuClient.ScreenHeight() * 16.0 / 10.0) - APIs.EmuClient.ScreenWidth(), main.Height);
                Location = new Point(
                    _settings.DockSide == DockSide.Right
                        ? main.Location.X + main.Width + _settings.DockOffset
                        : main.Location.X - Width - _settings.DockOffset,
                    main.Location.Y);

                if (currentSize != Size)
                    Invalidate();

                break;
            case WindowStyle.Custom:
            default:
                FormBorderStyle = FormBorderStyle.Sizable;
                break;
        }

        _docking = false;
    }

    protected override void OnResize(EventArgs e)
    {
        base.OnResize(e);

        if (_settings == null || _settings.WindowStyle != WindowStyle.Custom)
            return;
    }
    protected override void OnClosed(EventArgs e)
    {
        if (_parentFormLinked)
        {
            ((BizHawk.Client.EmuHawk.MainForm)MainForm).Move -= OwnerMoved;
            ((BizHawk.Client.EmuHawk.MainForm)MainForm).Resize -= OwnerResized;
            _parentFormLinked = false;
        }
    }

    private void OwnerResized(object sender, EventArgs e) => DockToScreen();

    private void OwnerMoved(object sender, EventArgs e) => DockToScreen();

    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);

        _viewModel.APIs = APIs;
        _viewModel.MemoryDomains = _memoryDomains;

        DockToScreen();
        if (!_parentFormLinked)
        {
            ((BizHawk.Client.EmuHawk.MainForm)MainForm).Move += OwnerMoved;
            ((BizHawk.Client.EmuHawk.MainForm)MainForm).Resize += OwnerResized;
            _parentFormLinked = true;
        }
    }

    private bool _paused = false;

    protected override void UpdateAfter()
    {
        if (Game.IsNullInstance() || Game == null)
            return;

        if (_parentFormLinked && !_docking) 
            _viewModel.OnFrame(Game);

        StopWatchLabel.Text = _game?.Elapsed.ToString("hh':'mm':'ss'.'ff");
    }

    protected override void GeneralUpdate()
    {
        if (!_paused && APIs.EmuClient.IsPaused() && _settings.AutoPauseTimer)
        {
            _paused = true;
            _viewModel.Game?.Pause();
        }
        else if (_paused && !APIs.EmuClient.IsPaused())
        {
            _paused = false;
            _viewModel.Game?.Unpause();
        }
    }

    public override void Restart()
    {
        _viewModel.APIs = APIs;
        _viewModel.MemoryDomains = _memoryDomains;
        _viewModel.Initialize(Game);
    }
}
