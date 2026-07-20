using BizHawk.Client.Common;
using BizHawk.Client.EmuHawk;
using BizHawk.Emulation.Common;
using FF.Rando.Companion.Games;
using FF.Rando.Companion.Settings;
using FF.Rando.Companion.Timing;
using FF.Rando.Companion.Utils;
using FF.Rando.Companion.View;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace FF.Rando.Companion;

[ExternalToolEmbeddedIcon("FF.Rando.Companion.Resources.Crystal.png")]
[ExternalTool("Square Enix Randomizer Companion", 
    Description = "An autotracker for Chrono Trigger: Jets of Time, FF4: Free Enterprise, FF6: Worlds Collide, and Final Fantasy Mystic Quest Randomizer.")]
public partial class MainForm : ToolFormBase, IExternalToolForm
{
    protected override string WindowTitleStatic => "Square Enix Randomizer Companion";

    public ApiContainer? MaybeAPIContainer { get; set; }

    private ApiContainer APIs => MaybeAPIContainer!;

    private readonly ISettings _settings;

    [OptionalService]
    public IMemoryDomains? MemoryDomains { get; set; }

    private bool _parentFormLinked = false;

    private readonly GameViewModel _viewModel;
    private IGame? _game;
    private ITimer? _timer;

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

        TrackerPanel.Controls.Add(new UnsupportedGameView(_settings));

        CreateTimer();
    }

    private void CreateTimer()
    {
        _timer?.Dispose();

        _timer = _settings.TimerMode switch
        {
            TimerMode.Internal => new InternalTimer(),
            TimerMode.LiveSplit => new LiveSplit(),
            //TimerMode.OpenSplit => new OpenSplit(),
            _ => throw new NotSupportedException(),
        };

        _timer.Initialize();
        StopWatchLabel.Visible = _timer?.ShowLocally == true;
        _viewModel.Timer = _timer;
    }

    public override bool BlocksInputWhenFocused => false;

    private void PropertyChanged(object sender, PropertyChangedEventArgs e)
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
            case nameof(ISettings.TimerMode):
                if (_timer?.Status == TimerStatus.Running)
                {
                    MessageBox.Show("Cannot change the timer mode while a run is in progress", "Settings Error");
                }
                else
                {
                    CreateTimer();
                }
                break;
            case nameof(GameViewModel.Game) when _viewModel.Game != null:
                if (_viewModel.Game.RequiresMemoryEvents && APIs.MemoryEvents == null) 
                {
                    MessageBox.Show("Automatic timing of runs is not supported on the Snes9x core. Please use the BSNES or BSNESv115+ core to have this feature.", "Timing unavailable");
                }

                var icon = _viewModel.Game.Icon;
                if (icon != null)
                {
                    var hIcon = icon.GetHicon();
                    Icon = Icon.FromHandle(hIcon);
                }
                else
                {
                    //Icon = 
                }

                TrackerPanel.SuspendLayout();
                foreach (Control control in TrackerPanel.Controls)
                {
                    control.Dispose();
                }

                TrackerPanel.Controls.Clear();
                _game?.Dispose();

                StopWatchLabel.Visible = _timer?.ShowLocally == true;
                _timer?.Initialize();
                _game = _viewModel.Game;
                TrackerPanel.Controls.Add(_viewModel.Game.CreateControls());
                TrackerPanel.ResumeLayout(false);
                TrackerPanel.PerformLayout();
                break;
        }
    }

    private bool _docking = false;

    private BizHawk.Client.EmuHawk.MainForm? GetBizHawkForm()
    {
        return GetType()
            .GetProperty("MainForm", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
            .GetValue(this, null) as BizHawk.Client.EmuHawk.MainForm;
    }

    private void DockToScreen()
    {
        var main = GetBizHawkForm();
        if (main == null)
            return;

        _docking = true;
        var currentSize = Size;

        if (main.WindowState == FormWindowState.Minimized)
        {
            WindowState = FormWindowState.Minimized;
            return;
        }
        else
            WindowState = FormWindowState.Normal;

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

    protected override void OnClosing(CancelEventArgs e)
    {
        if (_settings.WindowStyle == WindowStyle.Custom && _settings is RootSettings root)
        {
            if (WindowState == FormWindowState.Maximized)
            {
                root.WindowPosition = RestoreBounds.Location;
                root.WindowSize = RestoreBounds.Size;
                root.IsWindowMaximized = true;
                root.IsWindowMinimized = false;
            }
            else if (WindowState == FormWindowState.Normal)
            {
                root.WindowPosition = Location;
                root.WindowSize = Size;
                root.IsWindowMaximized = false;
                root.IsWindowMinimized = false;
            }
            else
            {
               root.WindowPosition = RestoreBounds.Location;
               root.WindowSize = RestoreBounds.Size;
               root.IsWindowMaximized = false;
               root.IsWindowMinimized = true;
            }
            root.SaveToFile();
        }
        base.OnClosing(e);
    }

    protected override void OnResize(EventArgs e)
    {
        base.OnResize(e);

        if (_settings == null || _settings.WindowStyle != WindowStyle.Custom)
            return;
    }
    protected override void OnClosed(EventArgs e)
    {
        if (_parentFormLinked && GetBizHawkForm() is { } mainForm)
        {
            mainForm.Move -= OwnerMoved;
            mainForm.Resize -= OwnerResized;
            _parentFormLinked = false;
        }
    }

    private void OwnerResized(object sender, EventArgs e) => DockToScreen();

    private void OwnerMoved(object sender, EventArgs e) => DockToScreen();

    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);

        _viewModel.APIs = APIs;
        _viewModel.MemoryDomains = MemoryDomains;

        DockToScreen();
        if (!_parentFormLinked && GetBizHawkForm() is { } mainForm)
        {
            mainForm.Move += OwnerMoved;
            mainForm.Resize += OwnerResized;
            _parentFormLinked = true;

            if (_settings.WindowStyle == WindowStyle.Custom)
            {
                Location = _settings.WindowPosition;
                Size = _settings.WindowSize;
                WindowState = _settings.IsWindowMinimized
                    ? FormWindowState.Minimized
                    : _settings.IsWindowMaximized
                        ? FormWindowState.Maximized
                        : FormWindowState.Normal;
            }

            if (Game.IsNullInstance() || Game == null)
            {
                
            }
        }
    }

    protected override void UpdateAfter()
    {
        if (Game.IsNullInstance() || Game == null)
            return;

        try
        {
            if (_parentFormLinked && !_docking && APIs.Emulation.FrameCount() > 1)
                _viewModel.OnFrame(Game);
        }
        catch { } // If i've done something wrong, don't crash bizhawk.

        if (_timer?.ShowLocally == true)
        {
            StopWatchLabel.Text = _timer.Elapsed?.ToString("hh':'mm':'ss'.'ff");
        }
        else if (_timer?.Status == TimerStatus.Error)
        {
            StopWatchLabel.Visible = true;
            StopWatchLabel.Text = $"{_timer.GetType().Name} Error";
        }
        else
        {
            StopWatchLabel.Visible = false;
        }
    }

    protected override void GeneralUpdate()
    {
        if (_settings.AutoPauseTimer && APIs.EmuClient.IsPaused() && _timer?.Status == TimerStatus.Running)
        {
            _timer.Pause(true);
        }
        else if (_settings.AutoPauseTimer && !APIs.EmuClient.IsPaused() && _timer?.Status == TimerStatus.AutomaticPaused)
        {
            _timer.Resume();
        }
    }

    public override void Restart()
    {
        _viewModel.APIs = APIs;
        _viewModel.MemoryDomains = MemoryDomains;
    }

    private void DisplayToolStripMenuItem_Click(object sender, EventArgs e)
    {
        var existing = OwnedForms.OfType<SettingsDialog>().FirstOrDefault();

        if (existing is not null)
        {
            existing.Focus();
            return;
        }

        var dialog = new SettingsDialog(_settings)
        {
            Owner = this as Form,
            StartPosition = FormStartPosition.CenterParent
        };

        dialog.FormClosed += (_, _) => dialog.Dispose();
        dialog.Show();
    }

    private void AboutToolStripMenuItem_Click(object sender, EventArgs e)
    {
        using var about = new AboutDialog() { Owner = this, StartPosition = FormStartPosition.CenterParent };
        var result = about.ShowDialog();
        if (result == DialogResult.Yes)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                string path = Directory.GetCurrentDirectory();
                var updater = new ProcessStartInfo
                {
                    FileName = path + Paths.UpdaterPath,
                    UseShellExecute = false,
                    WorkingDirectory = (path + Paths.UpdaterFolderPath)
                };
                Process.Start(updater);
                Application.Exit();
            }
            else
            {
                Process.Start(Paths.LatestReleaseUrl);
            }
        }
    }
}
