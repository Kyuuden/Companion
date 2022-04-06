using BizHawk.Client.Common;
using BizHawk.Client.EmuHawk;
using BizHawk.Emulation.Common;
using BizHawk.FreeEnterprise.Companion.Controls;
using BizHawk.FreeEnterprise.Companion.State;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace BizHawk.FreeEnterprise.Companion
{
    [ExternalTool("Free Enterprise Companion", Description = "An autotracker for Free Enterprise, a Final Fantasy IV randomizer")]
    [ExternalToolEmbeddedIcon("BizHawk.FreeEnterprise.Companion.Resources.Crystal.png")]
    
    public partial class CompanionForm : ToolFormBase, IExternalToolForm
    {
        protected override string WindowTitleStatic => "Free Enterprise Companion";

        public ApiContainer? _maybeAPIContainer { get; set; }

        private ApiContainer APIs => _maybeAPIContainer!;

        public bool MainListenersSet { get; private set; }
        public RenderingSettings RenderingSettings { get; private set; }

        private MemoryMapping? Memory;
        private RomData? RomData;
        private Run? Run;

        private List<ITrackerControl> trackerControls;

        public CompanionForm()
        {
            RenderingSettings = new RenderingSettings();
            InitializeComponent();

            var icon = new Bitmap(Assembly.GetExecutingAssembly().GetManifestResourceStream("BizHawk.FreeEnterprise.Companion.Resources.Crystal.png"));
            var hIcon = icon.GetHicon();
            Icon = Icon.FromHandle(hIcon);

            var namesJson = System.Text.Encoding.UTF8.GetString(Properties.Resources.Names);
            var names = JsonConvert.DeserializeObject<Names>(namesJson);
            if (names != null)
                TextLookup.Initialize(names);

            trackerControls = new List<ITrackerControl>(new ITrackerControl[] { KeyItemsControl, PartyControl, ObjectivesControl, BossesControl, LocationsControl });
            Properties.Settings.Default.PropertyChanged += SettingsChanged;
        }

        private void TrackerControl_Resize(object sender, EventArgs e)
        {
            WideLayoutPanel.Height = Math.Max(PartyControl.RequestedHeight, KeyItemsControl.RequestedHeight + ObjectivesControl.RequestedHeight);
        }

        private void SettingsChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            SetControlsVisibility();
            trackerControls.ForEach(c => c.RefreshSize());
            DockToScreen();
        }

        private void DockToScreen()
        {
            var main = (MainForm)MainForm;
            if (main == null)
                return;

            if (Properties.Settings.Default.Dock)
            {
                FormBorderStyle = FormBorderStyle.FixedSingle;
                var oldSize = Size;
                Size = new Size((APIs.EmuClient.ScreenHeight() * 16 / (Properties.Settings.Default.AspectRatio == AspectRatio._16x10 ? 10 : 9)) - APIs.EmuClient.ScreenWidth(), main.Height);
                Location = new Point(
                    Properties.Settings.Default.DockSide == DockSide.Right
                        ? main.Location.X + main.Width + Properties.Settings.Default.DockOffset
                        : main.Location.X - Width - Properties.Settings.Default.DockOffset,
                    main.Location.Y);

                if (oldSize != Size)
                    Invalidate();
            }
            else
            {
                FormBorderStyle = FormBorderStyle.Sizable;
            }

            bool layoutUpdated = false;

            switch (Properties.Settings.Default.Layout)
            {
                case Companion.Layout.Original:
                    if (KeyItemsControl.Parent != this)
                    {
                        KeyItemsControl.Parent = this;
                        PartyControl.Parent = this;
                        ObjectivesControl.Parent = this;
                        WideLayoutPanel.Visible = false;

                        PartyControl.Dock = DockStyle.Top;

                        ObjectivesControl.SendToBack();
                        PartyControl.SendToBack();
                        KeyItemsControl.SendToBack();

                        layoutUpdated = true;
                    }

                    break;
                case Companion.Layout.Alternate:
                    if (KeyItemsControl.Parent != WideLayoutPanel)
                    {
                        KeyItemsControl.Parent = WideLayoutPanel;
                        PartyControl.Parent = WideLayoutPanel;
                        PartyControl.Dock = DockStyle.Left;
                        ObjectivesControl.Parent = WideLayoutPanel;
                        WideLayoutPanel.Visible = true;

                        ObjectivesControl.SendToBack();
                        KeyItemsControl.SendToBack();
                        PartyControl.SendToBack();

                        layoutUpdated = true;
                    }
                    break;
            }

            if (layoutUpdated)
                menuStrip1.SendToBack();

            trackerControls.ForEach(f => f.RefreshSize());
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            if (Properties.Settings.Default.Dock)
                return;

            trackerControls?.ForEach(f =>
            {
                f.RefreshSize();
                (f as Control)?.Invalidate();
            });
        }

        protected override void OnClosed(EventArgs e)
        {
            if (MainListenersSet)
            {
                ((MainForm)MainForm).Move -= FreeEnterpriseCompanionForm_Move;
                ((MainForm)MainForm).Resize -= FreeEnterpriseCompanionForm_Resize;
                MainListenersSet = false;
            }
        }

        private void FreeEnterpriseCompanionForm_Resize(object sender, EventArgs e) => DockToScreen();

        private void FreeEnterpriseCompanionForm_Move(object sender, EventArgs e) => DockToScreen();

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            DockToScreen();
            if (!MainListenersSet)
            {
                ((MainForm)MainForm).Move += FreeEnterpriseCompanionForm_Move;
                ((MainForm)MainForm).Resize += FreeEnterpriseCompanionForm_Resize;
                MainListenersSet = true;
                if (!Properties.Settings.Default.Dock)
                {
                    if (Properties.Settings.Default.Maximized)
                    {
                        Location = Properties.Settings.Default.Location;
                        WindowState = FormWindowState.Maximized;
                        Size = Properties.Settings.Default.Size;
                    }
                    else if (Properties.Settings.Default.Minimized)
                    {
                        Location = Properties.Settings.Default.Location;
                        WindowState = FormWindowState.Minimized;
                        Size = Properties.Settings.Default.Size;
                    }
                    else
                    {
                        Location = Properties.Settings.Default.Location;
                        Size = Properties.Settings.Default.Size;
                    }
                }
            }
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            if (!Properties.Settings.Default.Dock)
            {
                if (WindowState == FormWindowState.Maximized)
                {
                    Properties.Settings.Default.Location = RestoreBounds.Location;
                    Properties.Settings.Default.Size = RestoreBounds.Size;
                    Properties.Settings.Default.Maximized = true;
                    Properties.Settings.Default.Minimized = false;
                }
                else if (WindowState == FormWindowState.Normal)
                {
                    Properties.Settings.Default.Location = Location;
                    Properties.Settings.Default.Size = Size;
                    Properties.Settings.Default.Maximized = false;
                    Properties.Settings.Default.Minimized = false;
                }
                else
                {
                    Properties.Settings.Default.Location = RestoreBounds.Location;
                    Properties.Settings.Default.Size = RestoreBounds.Size;
                    Properties.Settings.Default.Maximized = false;
                    Properties.Settings.Default.Minimized = true;
                }
                Properties.Settings.Default.Save();
            }
            base.OnClosing(e);
        }

        private void Initialize()
        {
            if (Run != null)
            {
                Run.PartyUpdated -= Run_PartyUpdated;
                Run.KeyItemsUpdated -= Run_KeyItemsUpdated;
                Run.LocationsUpdated -= Run_LocationsUpdated;
                Run.ObjectivesUpdated -= Run_ObjectivesUpdated;
                Run.CustomSettingsUpdated -= Run_CustomSettingsUpdated;
                Run.LoadComplete -= Run_LoadComplete;
                Run.Dispose();
                Run = null;
            }

            Memory = new MemoryMapping(APIs.Memory);

            try
            {
                Run = new Run(APIs, Memory);
                Run.PartyUpdated += Run_PartyUpdated;
                Run.KeyItemsUpdated += Run_KeyItemsUpdated;
                Run.LocationsUpdated += Run_LocationsUpdated;
                Run.ObjectivesUpdated += Run_ObjectivesUpdated;
                Run.CustomSettingsUpdated += Run_CustomSettingsUpdated;
                Run.LoadComplete += Run_LoadComplete;
                RomData = new RomData(Memory.CartRom, RenderingSettings);

                BossesControl.Update(new State.Bosses());

                trackerControls.ForEach(c =>
                {
                    c.Initialize(RomData, Run.FlagSet);
                    c.RefreshSize();
                });
            }
            catch (Exception)
            {

            }

            SetControlsVisibility();
        }

        private void Run_LoadComplete(object sender, EventArgs e)
        {
            Run_LocationsUpdated(sender, e);
            Run_KeyItemsUpdated(sender, e);
            Run_ObjectivesUpdated(sender, e);
            Run_PartyUpdated(sender, e);
            Run_CustomSettingsUpdated(sender, e);

            trackerControls.ForEach(c =>  c.RefreshSize());
        }

        private void Run_CustomSettingsUpdated(object sender, EventArgs e)
        {
            if (RomData?.Font?.UpdateBackgroundColor(Run!.BackgroundColor) ?? false)
                Invalidate();
        }

        private void Run_ObjectivesUpdated(object sender, EventArgs e)
        {
            ObjectivesControl.Update(Run!.Objectives);
            ObjectivesControl.RefreshSize();
        }

        private void Run_LocationsUpdated(object sender, EventArgs e)
            => LocationsControl.Update(Run!.Locations);

        private void Run_KeyItemsUpdated(object sender, EventArgs e) 
            => KeyItemsControl.Update(Run!.KeyItems);

        private void Run_PartyUpdated(object sender, EventArgs e) 
            => PartyControl.Update(Run!.Party);

        public override void Restart()
        {
            base.Restart();
            if (Game.IsNullInstance())
                return;

            if (Run == null || Game.Hash != Run.Hash)
                Initialize();
            else
            {
                Memory = new MemoryMapping(APIs.Memory);
                Run.UpdateApis(APIs, Memory);
            }
        }

        protected override void UpdateAfter()
        {
            base.UpdateAfter();
            if (Game.IsNullInstance())
                return;

            if (Run == null || Game.Hash != Run.Hash)
                Initialize();

            Run?.NewFrame();
            StopWatchLabel.Text = Run?.Stopwatch.Elapsed.ToString("hh':'mm':'ss'.'ff");
            trackerControls.ForEach(c => c.NewFrame());
        }

        private void displayToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var existing = OwnedForms.OfType<SettingsDialog>().FirstOrDefault();

            if (existing is not null)
            {
                existing.Focus();
                return;
            }

            var dialog = new SettingsDialog()
            {
                Owner = this,
                StartPosition = FormStartPosition.CenterParent
            };

            dialog.FormClosed += (_, _) => dialog.Dispose();
            dialog.Show();
        }

        private void SetControlsVisibility()
        {
            KeyItemsControl.Visible = Run != null && Properties.Settings.Default.KeyItemsDisplay;
            PartyControl.Visible = Run != null && Properties.Settings.Default.PartyDisplay;
            ObjectivesControl.Visible = Run != null && Properties.Settings.Default.ObjectivesDisplay;
            BossesControl.Visible = Run != null && Properties.Settings.Default.BossesDisplay;
            LocationsControl.Visible = Run != null && Properties.Settings.Default.LocationsDisplay;
        }

        private void StopWatchLabel_DoubleClick(object sender, EventArgs e)
        {
            if (Run?.Stopwatch.IsRunning == true)
                Run.Stopwatch.Stop();
            else if (Run?.Stopwatch.IsRunning == false)
                Run.Stopwatch.Start();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using var about = new AboutDialog() { Owner = this, StartPosition = FormStartPosition.CenterParent };
            about.ShowDialog();
        }
    }
}
