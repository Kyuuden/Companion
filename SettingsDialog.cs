using BizHawk.FreeEnterprise.Companion.Sprites;
using System;
using System.Windows.Forms;

namespace BizHawk.FreeEnterprise.Companion
{
    public partial class SettingsDialog : Form
    {
        public SettingsDialog()
        {
            InitializeComponent();
            comboPartyPose.Items.AddRange(Enum.GetNames(typeof(Sprites.Pose)));
            comboDockSide.Items.AddRange(Enum.GetNames(typeof(DockSide)));
            comboKeyItemsStyle.Items.AddRange(Enum.GetNames(typeof(KeyItemStyle)));

            cbKeyItemDisplay.Checked = Properties.Settings.Default.KeyItemsDisplay;
            cbKeyItemBorder.Checked = Properties.Settings.Default.KeyItemsBorder;
            comboKeyItemsStyle.SelectedItem = Properties.Settings.Default.KeyItemsStyle.ToString();

            cbPartyDisplay.Checked = Properties.Settings.Default.PartyDisplay;
            cbPartyBorder.Checked = Properties.Settings.Default.PartyBorder;
            comboPartyPose.SelectedItem = Properties.Settings.Default.PartyPose.ToString();
            cbPartyAnimate.Checked = Properties.Settings.Default.PartyAnimate;

            cbObjectiveDisplay.Checked = Properties.Settings.Default.ObjectivesDisplay;
            cbObjectiveBorder.Checked = Properties.Settings.Default.ObjectivesBorder;

            cbLocationsDisplay.Checked = Properties.Settings.Default.LocationsDisplay;
            cbLocationsBorder.Checked = Properties.Settings.Default.LocationsBorder;

            cbDock.Checked = Properties.Settings.Default.Dock;
            numericDockOffset.Value = Properties.Settings.Default.DockOffset;
            comboDockSide.SelectedItem = Properties.Settings.Default.DockSide.ToString();

            numericFrameCount.Value = Properties.Settings.Default.RefreshEveryNFrames;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.KeyItemsDisplay = cbKeyItemDisplay.Checked;
            Properties.Settings.Default.KeyItemsBorder = cbKeyItemBorder.Checked;
            Properties.Settings.Default.KeyItemsStyle = (KeyItemStyle)Enum.Parse(typeof(KeyItemStyle), comboKeyItemsStyle.SelectedItem.ToString());

            Properties.Settings.Default.PartyDisplay = cbPartyDisplay.Checked;
            Properties.Settings.Default.PartyBorder = cbPartyBorder.Checked;
            Properties.Settings.Default.PartyPose = (Pose)Enum.Parse(typeof(Pose), comboPartyPose.SelectedItem.ToString());
            Properties.Settings.Default.PartyAnimate = cbPartyAnimate.Checked;

            Properties.Settings.Default.ObjectivesDisplay = cbObjectiveDisplay.Checked;
            Properties.Settings.Default.ObjectivesBorder = cbObjectiveBorder.Checked;

            Properties.Settings.Default.LocationsDisplay = cbLocationsDisplay.Checked;
            Properties.Settings.Default.LocationsBorder = cbLocationsBorder.Checked;

            Properties.Settings.Default.Dock = cbDock.Checked;
            Properties.Settings.Default.DockOffset = (int)numericDockOffset.Value;
            Properties.Settings.Default.DockSide = (DockSide)Enum.Parse(typeof(DockSide), comboDockSide.SelectedItem.ToString());

            Properties.Settings.Default.RefreshEveryNFrames = (int)numericFrameCount.Value;

            Properties.Settings.Default.Save();
        }
    }
}
