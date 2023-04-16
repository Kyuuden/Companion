using BizHawk.Client.Common;
using BizHawk.Client.EmuHawk;
using BizHawk.Emulation.Common;
using BizHawk.FreeEnterprise.Companion.Configuration;
using BizHawk.FreeEnterprise.Companion.Controls;
using BizHawk.FreeEnterprise.Companion.Database;
using BizHawk.FreeEnterprise.Companion.Extensions;
using BizHawk.FreeEnterprise.Companion.RomUtilities;
using BizHawk.FreeEnterprise.Companion.State;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
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

        [OptionalService]
        private IMemoryDomains? _memoryDomains { get; set; }

        public bool MainListenersSet { get; private set; }

        public PersistentStorage? Storage { get; private set; }

        private MemoryMapping? Memory;
        private RomData? RomData;
        private Run? Run;
        private Settings settings;

        private List<ITrackerControl> trackerControls;

        public CompanionForm()
        {
            InitializeComponent();

            var icon = new Bitmap(Assembly.GetExecutingAssembly().GetManifestResourceStream(Strings.IconResource));
            var hIcon = icon.GetHicon();
            Icon = Icon.FromHandle(hIcon);

            var namesJson = System.Text.Encoding.UTF8.GetString(Properties.Resources.Names);
            var names = JsonConvert.DeserializeObject<Names>(namesJson);
            if (names != null)
                TextLookup.Initialize(names);

            settings = new Settings();
            if (File.Exists(Strings.SettingsPath))
                settings = JsonConvert.DeserializeObject<Settings>(File.ReadAllText(Strings.SettingsPath)) ?? new Settings();

            trackerControls = new List<ITrackerControl>(new ITrackerControl[] { KeyItemsControl, PartyControl, ObjectivesControl, BossesControl, LocationsControl });

            trackerControls.ForEach(c => c.Clicked += TrackerControlClicked);
        }

        private void TrackerControlClicked()
        {
            ((MainForm)MainForm).Focus();
        }

        private bool ControlsInitialized => trackerControls.All(c => c.IsInitialized);

        private void TrackerControl_Resize(object sender, EventArgs e)
        {
           // if (ControlsInitialized)
                WideLayoutPanel.Height = Math.Max(PartyControl.RequestedHeight, KeyItemsControl.RequestedHeight + ObjectivesControl.RequestedHeight);
        }

        private void DockToScreen()
        {
            var main = (MainForm)MainForm;
            if (main == null)
                return;

            if (settings.Dock)
            {
                FormBorderStyle = FormBorderStyle.FixedSingle;
                var oldSize = Size;
                Size = new Size((APIs.EmuClient.ScreenHeight() * 16 / (settings.AspectRatio == AspectRatio._16x10 ? 10 : 9)) - APIs.EmuClient.ScreenWidth(), main.Height);
                Location = new Point(
                    settings.DockSide == DockSide.Right
                        ? main.Location.X + main.Width + settings.DockOffset
                        : main.Location.X - Width - settings.DockOffset,
                    main.Location.Y);

                if (oldSize != Size)
                    Invalidate();
            }
            else
            {
                FormBorderStyle = FormBorderStyle.Sizable;
            }

            if (!ControlsInitialized)
                return;

            bool layoutUpdated = false;

            switch (settings.Layout)
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

            if (settings == null || settings.Dock)
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
                if (!settings.Dock)
                {
                    if (settings.Maximized)
                    {
                        Location = settings.Location;
                        WindowState = FormWindowState.Maximized;
                        Size = settings.Size;
                    }
                    else if (settings.Minimized)
                    {
                        Location = settings.Location;
                        WindowState = FormWindowState.Minimized;
                        Size = settings.Size;
                    }
                    else
                    {
                        Location = settings.Location;
                        Size = settings.Size;
                    }
                }
            }
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            if (!settings.Dock)
            {
                if (WindowState == FormWindowState.Maximized)
                {
                    settings.Location = RestoreBounds.Location;
                    settings.Size = RestoreBounds.Size;
                    settings.Maximized = true;
                    settings.Minimized = false;
                }
                else if (WindowState == FormWindowState.Normal)
                {
                    settings.Location = Location;
                    settings.Size = Size;
                    settings.Maximized = false;
                    settings.Minimized = false;
                }
                else
                {
                    settings.Location = RestoreBounds.Location;
                    settings.Size = RestoreBounds.Size;
                    settings.Maximized = false;
                    settings.Minimized = true;
                }
                SaveSettings();
            }
            base.OnClosing(e);
        }

        private void Initialize()
        {
            if (Run != null)
            {
                Run.PartyUpdated -= Run_PartyUpdated;
                Run.KeyItemsUpdated -= Run_KeyItemsUpdated;
                Run.NewKeyItemFound -= Run_NewKeyItemFound;
                Run.LocationsUpdated -= Run_LocationsUpdated;
                Run.ObjectivesUpdated -= Run_ObjectivesUpdated;
                Run.CustomSettingsUpdated -= Run_CustomSettingsUpdated;
                Run.LoadComplete -= Run_LoadComplete;
                Run.Dispose();
                Run = null;
            }

            Memory = new MemoryMapping(_memoryDomains);
            Storage = new PersistentStorage(APIs.SQLite, Game.Hash);

            try
            {
                Run = new Run(Game.Hash, APIs, Memory, Storage, settings);
                Run.PartyUpdated += Run_PartyUpdated;
                Run.KeyItemsUpdated += Run_KeyItemsUpdated;
                Run.NewKeyItemFound += Run_NewKeyItemFound;
                Run.LocationsUpdated += Run_LocationsUpdated;
                Run.ObjectivesUpdated += Run_ObjectivesUpdated;
                Run.CustomSettingsUpdated += Run_CustomSettingsUpdated;
                Run.LoadComplete += Run_LoadComplete;
                RomData = new RomData(Memory.CartRom, settings);
                RomData.Overlays.SetCustomMissMessage(settings.KeyItemBonkDefaultText ? null : settings.KeyItemBonkCustomText);

                BossesControl.Update(new State.Bosses(Storage, () => Run.ElapsedTime));
                SetControlsVisibility();

                trackerControls.ForEach(c =>
                {
                    c.Initialize(RomData, Run.FlagSet, settings);
                    c.RefreshSize();
                });

                DockToScreen();
            }
            catch (Exception)
            {

            }            
        }

        private void Run_NewKeyItemFound(KeyItemType? keyitem)
            => KeyItemsControl.NewItemFound(keyitem);
        
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
                trackerControls.ForEach(c => c.Invalidate());
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
                Memory = new MemoryMapping(_memoryDomains);
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
            StopWatchLabel.Text = Run?.ElapsedTime.ToString(settings.TimeFormatString);
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

            var dialog = new SettingsDialog(settings, SaveSettings)
            {
                Owner = this,
                StartPosition = FormStartPosition.CenterParent
            };

            dialog.FormClosed += (_, _) => dialog.Dispose();
            dialog.Show();
        }

        private void SaveSettings()
        {
            File.WriteAllText(Strings.SettingsPath, JsonConvert.SerializeObject(settings));
            RomData?.Overlays.SetCustomMissMessage(settings.KeyItemBonkDefaultText ? null : settings.KeyItemBonkCustomText);
            SetControlsVisibility();
            trackerControls.ForEach(c => c.RefreshSize());
            DockToScreen();
        }

        private void SetControlsVisibility()
        {
            KeyItemsControl.Visible = Run != null && settings.KeyItemsDisplay;
            PartyControl.Visible = Run != null && settings.PartyDisplay;
            ObjectivesControl.Visible = Run != null && settings.ObjectivesDisplay;
            BossesControl.Visible = Run != null && settings.BossesDisplay;
            LocationsControl.Visible = Run != null && settings.LocationsDisplay;
        }

        private void StopWatchLabel_DoubleClick(object sender, EventArgs e)
        {
            if (Run?.Stopwatch.IsRunning == true)
                Run.Stopwatch.Stop();
            else if (Run?.Stopwatch.IsRunning == false)
                Run.Stopwatch.Start();

            TrackerControlClicked();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using var about = new AboutDialog() { Owner = this, StartPosition = FormStartPosition.CenterParent };
            about.ShowDialog();
        }

        private void StockWatchLabel_Click(object sender, EventArgs e)
        {
            TrackerControlClicked();
        }
    }
}
