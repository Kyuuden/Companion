using BizHawk.FreeEnterprise.Companion.Extensions;
using BizHawk.FreeEnterprise.Companion.Sprites;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;

namespace BizHawk.FreeEnterprise.Companion
{
    public partial class SettingsDialog : Form
    {
        public SettingsDialog()
        {
            InitializeComponent();

            Icon = null;

            comboLayout.BindEnumToCombobox(Properties.Settings.Default.Layout);
            numericViewScale.Value = Properties.Settings.Default.ViewScale;
            comboInterpolation.BindEnumToCombobox(Properties.Settings.Default.InterpolationMode, InterpolationMode.Invalid, InterpolationMode.HighQualityBicubic, InterpolationMode.HighQualityBilinear);
            ScaleIconsCheckBox.Checked = Properties.Settings.Default.IconScaling;
            BordersCheckBox.Checked = Properties.Settings.Default.BordersEnabled;

            cbDock.Checked = Properties.Settings.Default.Dock;
            numericDockOffset.Value = Properties.Settings.Default.DockOffset;
            comboDockSide.BindEnumToCombobox(Properties.Settings.Default.DockSide);
            comboAspect.BindEnumToCombobox(Properties.Settings.Default.AspectRatio);            

            numericFrameCount.Value = Properties.Settings.Default.RefreshEveryNFrames;
            KeyItemsCheckBox.Checked = Properties.Settings.Default.KeyItemsDisplay;
            KeyItemStyleComboBox.BindEnumToCombobox(Properties.Settings.Default.KeyItemsStyle);
            PartyCheckBox.Checked = Properties.Settings.Default.PartyDisplay;
            PartyPoseComboBox.BindEnumToCombobox(Properties.Settings.Default.PartyPose);
            PartyPoseAnimateCheckBox.Checked = Properties.Settings.Default.PartyAnimate;
            PartyAnchorCheckBox.Checked = Properties.Settings.Default.PartyShowAnchor;
            ObjectivesCheckBox.Checked = Properties.Settings.Default.ObjectivesDisplay;
            BossesCheckBox.Checked = Properties.Settings.Default.BossesDisplay;
            LocationsCheckBox.Checked = Properties.Settings.Default.LocationsDisplay;
            LocationsKeyItemsCheckBox.Checked = Properties.Settings.Default.LocationsShowKeyItems;
            LocationsCharactersCheckBox.Checked = Properties.Settings.Default.LocationsShowCharacters;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Layout = (Layout)comboLayout.SelectedValue;
            Properties.Settings.Default.ViewScale = (int)numericViewScale.Value;
            Properties.Settings.Default.IconScaling = ScaleIconsCheckBox.Checked;
            Properties.Settings.Default.InterpolationMode = (InterpolationMode)comboInterpolation.SelectedValue;
            Properties.Settings.Default.BordersEnabled = BordersCheckBox.Checked;
            Properties.Settings.Default.Dock = cbDock.Checked;
            Properties.Settings.Default.DockOffset = (int)numericDockOffset.Value;
            Properties.Settings.Default.DockSide = (DockSide)comboDockSide.SelectedValue;
            Properties.Settings.Default.AspectRatio = (AspectRatio)comboAspect.SelectedValue;

            Properties.Settings.Default.RefreshEveryNFrames = (int)numericFrameCount.Value;
            Properties.Settings.Default.KeyItemsDisplay = KeyItemsCheckBox.Checked;
            Properties.Settings.Default.KeyItemsStyle = (KeyItemStyle)KeyItemStyleComboBox.SelectedValue;
            Properties.Settings.Default.PartyDisplay = PartyCheckBox.Checked;
            Properties.Settings.Default.PartyPose = (Pose)PartyPoseComboBox.SelectedValue;
            Properties.Settings.Default.PartyAnimate = PartyPoseAnimateCheckBox.Checked;
            Properties.Settings.Default.PartyShowAnchor = PartyAnchorCheckBox.Checked;
            Properties.Settings.Default.ObjectivesDisplay = ObjectivesCheckBox.Checked;
            Properties.Settings.Default.BossesDisplay = BossesCheckBox.Checked;
            Properties.Settings.Default.LocationsDisplay = LocationsCheckBox.Checked;
            Properties.Settings.Default.LocationsShowKeyItems = LocationsKeyItemsCheckBox.Checked;
            Properties.Settings.Default.LocationsShowCharacters = LocationsCharactersCheckBox.Checked;

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
