using BizHawk.FreeEnterprise.Companion.Extensions;
using BizHawk.FreeEnterprise.Companion.Sprites;
using System;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace BizHawk.FreeEnterprise.Companion
{
    public partial class SettingsDialog : Form
    {
        public SettingsDialog()
        {
            InitializeComponent();
            cbKeyItemDisplay.Checked = Properties.Settings.Default.KeyItemsDisplay;
            cbKeyItemBorder.Checked = Properties.Settings.Default.KeyItemsBorder;
            comboKeyItemsStyle.BindEnumToCombobox(Properties.Settings.Default.KeyItemsStyle);

            cbPartyDisplay.Checked = Properties.Settings.Default.PartyDisplay;
            cbPartyBorder.Checked = Properties.Settings.Default.PartyBorder;
            comboPartyPose.BindEnumToCombobox(Properties.Settings.Default.PartyPose);
            cbPartyAnimate.Checked = Properties.Settings.Default.PartyAnimate;

            cbObjectiveDisplay.Checked = Properties.Settings.Default.ObjectivesDisplay;
            cbObjectiveBorder.Checked = Properties.Settings.Default.ObjectivesBorder;

            cbBossesDisplay.Checked = Properties.Settings.Default.BossesDisplay;
            cbBossesBorder.Checked = Properties.Settings.Default.BossesBorder;

            cbLocationsDisplay.Checked = Properties.Settings.Default.LocationsDisplay;
            cbLocationsBorder.Checked = Properties.Settings.Default.LocationsBorder;
            cbLocationsKI.Checked = Properties.Settings.Default.LocationsShowKeyItems;
            cbLocationsChar.Checked = Properties.Settings.Default.LocationsShowCharacters;

            cbDock.Checked = Properties.Settings.Default.Dock;
            numericDockOffset.Value = Properties.Settings.Default.DockOffset;
            comboDockSide.BindEnumToCombobox(Properties.Settings.Default.DockSide);
            numericViewScale.Value = Properties.Settings.Default.ViewScale;
            comboInterpolation.BindEnumToCombobox(Properties.Settings.Default.InterpolationMode, InterpolationMode.Invalid, InterpolationMode.HighQualityBicubic, InterpolationMode.HighQualityBilinear);

            numericFrameCount.Value = Properties.Settings.Default.RefreshEveryNFrames;

            comboAspect.BindEnumToCombobox(Properties.Settings.Default.AspectRatio);
            comboLayout.BindEnumToCombobox(Properties.Settings.Default.Layout);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.KeyItemsDisplay = cbKeyItemDisplay.Checked;
            Properties.Settings.Default.KeyItemsBorder = cbKeyItemBorder.Checked;
            Properties.Settings.Default.KeyItemsStyle = (KeyItemStyle)comboKeyItemsStyle.SelectedValue;

            Properties.Settings.Default.PartyDisplay = cbPartyDisplay.Checked;
            Properties.Settings.Default.PartyBorder = cbPartyBorder.Checked;
            Properties.Settings.Default.PartyPose = (Pose)comboPartyPose.SelectedValue;
            Properties.Settings.Default.PartyAnimate = cbPartyAnimate.Checked;

            Properties.Settings.Default.ObjectivesDisplay = cbObjectiveDisplay.Checked;
            Properties.Settings.Default.ObjectivesBorder = cbObjectiveBorder.Checked;

            Properties.Settings.Default.BossesDisplay = cbBossesDisplay.Checked;
            Properties.Settings.Default.BossesBorder = cbBossesBorder.Checked;

            Properties.Settings.Default.LocationsDisplay = cbLocationsDisplay.Checked;
            Properties.Settings.Default.LocationsBorder = cbLocationsBorder.Checked;
            Properties.Settings.Default.LocationsShowKeyItems = cbLocationsKI.Checked;
            Properties.Settings.Default.LocationsShowCharacters = cbLocationsChar.Checked;

            Properties.Settings.Default.Dock = cbDock.Checked;
            Properties.Settings.Default.DockOffset = (int)numericDockOffset.Value;
            Properties.Settings.Default.DockSide = (DockSide)comboDockSide.SelectedValue;
            Properties.Settings.Default.AspectRatio = (AspectRatio)comboAspect.SelectedValue;
            Properties.Settings.Default.Layout = (Layout)comboLayout.SelectedValue;
            Properties.Settings.Default.ViewScale = (int)numericViewScale.Value;
            Properties.Settings.Default.InterpolationMode = (System.Drawing.Drawing2D.InterpolationMode)comboInterpolation.SelectedValue;

            Properties.Settings.Default.RefreshEveryNFrames = (int)numericFrameCount.Value;

            Properties.Settings.Default.Save();
        }

        private void cbDock_CheckedChanged(object sender, EventArgs e)
        {
            numericDockOffset.Enabled = cbDock.Checked;
            comboDockSide.Enabled = cbDock.Checked;
            comboAspect.Enabled = cbDock.Checked;
        }
    }
}
