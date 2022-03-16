using BizHawk.Client.Common;
using BizHawk.Client.EmuHawk;
using BizHawk.Common;
using BizHawk.Emulation.Common;
using BizHawk.FreeEnterprise.Companion.Controls;
using BizHawk.FreeEnterprise.Companion.State;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace BizHawk.FreeEnterprise.Companion
{
    [ExternalTool("Free Enterprise Companion")]
    public partial class CompanionForm : ToolFormBase, IExternalToolForm
    {
        protected override string WindowTitleStatic => "Free Enterprise Companion";

        public ApiContainer? _maybeAPIContainer { get; set; }

        private ApiContainer APIs => _maybeAPIContainer!;

        public bool MainListenersSet { get; private set; }
        
        private MemoryMapping? Memory;
        private RomData? RomData;
        private Run? Run;

        private List<ITrackerControl> trackerControls;

        public CompanionForm()
        {
            InitializeComponent();
            using var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("BizHawk.FreeEnterprise.Companion.Resources.Names.json");
            if (stream != null)
                using (var reader = new StreamReader(stream))
                {
                    var namesJson = reader.ReadToEnd();
                    var names = JsonConvert.DeserializeObject<Names>(namesJson);
                    if (names != null)
                        DescriptionLookup.Initialize(names);
                }

            KeyItemsControl.IconLookup = new IconLookup();
            trackerControls = new List<ITrackerControl>(new ITrackerControl[] { KeyItemsControl, PartyControl, ObjectivesControl, LocationsControl });
            Log.EnableDomain("FE");

            Properties.Settings.Default.PropertyChanged += SettingsChanged;
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
                Size = new Size((APIs.EmuClient.ScreenHeight() * 16 / 10) - APIs.EmuClient.ScreenWidth(), main.Height);
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

            trackerControls.ForEach(f => f.RefreshSize());
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
            }           
        }

        private void Initialize()
        {
            if (Run != null)
            {
                Run.PartyUpdated -= GameState_PartyUpdated;
                Run.KeyItemsUpdated -= GameState_KeyItemsUpdated;
                Run.LocationsUpdated -= GameState_LocationsUpdated;
                Run.ObjectivesUpdated -= GameState_ObjectivesUpdated;
                Run.CustomSettingsUpdated -= GameState_CustomSettingsUpdated;
                Run.Dispose();
                Run = null;
            }

            Memory = new MemoryMapping(APIs.Memory);

            Run = new Run(APIs, Memory);
            Run.PartyUpdated += GameState_PartyUpdated;
            Run.KeyItemsUpdated += GameState_KeyItemsUpdated;
            Run.LocationsUpdated += GameState_LocationsUpdated;
            Run.ObjectivesUpdated += GameState_ObjectivesUpdated;
            Run.CustomSettingsUpdated += GameState_CustomSettingsUpdated;

            RomData = new RomData(Memory.CartRom);

            trackerControls.ForEach(c =>
            {
                c.Initialize(RomData, Run.FlagSet);
                c.RefreshSize();
            });

            SetControlsVisibility();
        }

        private void GameState_CustomSettingsUpdated(object sender, EventArgs e)
        {
            if (RomData?.Font?.UpdateBackgroundColor(Run!.BackgroundColor) ?? false)
                Invalidate();
        }

        private void GameState_ObjectivesUpdated(object sender, EventArgs e)
        {
            ObjectivesControl.Update(Run!.Objectives);
            ObjectivesControl.RefreshSize();
        }

        private void GameState_LocationsUpdated(object sender, EventArgs e)
            => LocationsControl.Update(Run!.Locations);

        private void GameState_KeyItemsUpdated(object sender, EventArgs e) 
            => KeyItemsControl.Update(Run!.KeyItems);

        private void GameState_PartyUpdated(object sender, EventArgs e) 
            => PartyControl.Update(Run!.Party);

        public override void Restart()
        {
            base.Restart();
            if (Game.IsNullInstance())
                return;

            if (Run == null || Game.Hash != Run.Hash)
                Initialize();
        }

        protected override void UpdateAfter()
        {
            base.UpdateAfter();
            if (Game.IsNullInstance())
                return;

            if (Run == null || Game.Hash != Run.Hash)
                Initialize();

            Run?.NewFrame();
            StopWatchLabel.Text = Run?.Stopwatch.Elapsed.ToString("c");
        }

        private void displayToolStripMenuItem_Click(object sender, EventArgs e)
        {
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
            KeyItemsControl.Visible = Properties.Settings.Default.KeyItemsDisplay;
            PartyControl.Visible = Properties.Settings.Default.PartyDisplay;
            ObjectivesControl.Visible = Properties.Settings.Default.ObjectivesDisplay;
            LocationsControl.Visible = Properties.Settings.Default.LocationsDisplay;
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
