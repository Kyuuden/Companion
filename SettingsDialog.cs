using BizHawk.FreeEnterprise.Companion.Configuration;
using BizHawk.FreeEnterprise.Companion.Extensions;
using BizHawk.FreeEnterprise.Companion.Sprites;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Reflection;
using System.Windows.Forms;

namespace BizHawk.FreeEnterprise.Companion
{
    public partial class SettingsDialog : Form
    {
        private readonly Settings _settings;
        private readonly Action _applySettings;

        public SettingsDialog()
        { }

        public SettingsDialog(Settings settings, Action saveSettings)
        {
            InitializeComponent();

            var icon = new Bitmap(Assembly.GetExecutingAssembly().GetManifestResourceStream(Strings.IconResource));
            var hIcon = icon.GetHicon();
            Icon = Icon.FromHandle(hIcon);

            _settings = settings;
            _applySettings = saveSettings;

            comboLayout.BindEnumToCombobox(_settings.Layout);
            numericViewScale.Value = _settings.ViewScale;
            comboInterpolation.BindEnumToCombobox(_settings.InterpolationMode, InterpolationMode.Invalid, InterpolationMode.HighQualityBicubic, InterpolationMode.HighQualityBilinear);
            ScaleIconsCheckBox.Checked = _settings.IconScaling;
            BordersCheckBox.Checked = _settings.BordersEnabled;
            comboTimeFormat.BindEnumToCombobox(_settings.TimeFormat);

            cbDock.Checked = _settings.Dock;
            numericDockOffset.Value = _settings.DockOffset;
            comboDockSide.BindEnumToCombobox(_settings.DockSide);
            comboAspect.BindEnumToCombobox(_settings.AspectRatio);

            numericFrameCount.Value = _settings.RefreshEveryNFrames;
            KeyItemsCheckBox.Checked = _settings.KeyItemsDisplay;
            KeyItemStyleComboBox.BindEnumToCombobox(_settings.KeyItemsStyle);
            PartyCheckBox.Checked = _settings.PartyDisplay;
            PartyPoseComboBox.BindEnumToCombobox(_settings.PartyPose);
            PartyPoseAnimateCheckBox.Checked = _settings.PartyAnimate;
            PartyAnchorCheckBox.Checked = _settings.PartyShowAnchor;
            ObjectivesCheckBox.Checked = _settings.ObjectivesDisplay;
            BossesCheckBox.Checked = _settings.BossesDisplay;
            LocationsCheckBox.Checked = _settings.LocationsDisplay;
            LocationsKeyItemsCheckBox.Checked = _settings.LocationsShowKeyItems;
            LocationsCharactersCheckBox.Checked = _settings.LocationsShowCharacters;

            KeyItemBonkCheckBox.Checked = _settings.KeyItemEventEnabled;
            KeyItemBonkDefaultRadio.Checked = _settings.KeyItemBonkDefaultText;
            KeyItemCustomRadio.Checked = !_settings.KeyItemBonkDefaultText;
            KeyItemCustomTextBox.Text = _settings.KeyItemBonkCustomText;
            KeyItemCustomTextBox.Enabled = !_settings.KeyItemBonkDefaultText;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _settings.Layout = (Layout)comboLayout.SelectedValue;
            _settings.ViewScale = (int)numericViewScale.Value;
            _settings.IconScaling = ScaleIconsCheckBox.Checked;
            _settings.InterpolationMode = (InterpolationMode)comboInterpolation.SelectedValue;
            _settings.BordersEnabled = BordersCheckBox.Checked;
            _settings.TimeFormat = (TimeFormat)comboTimeFormat.SelectedValue;
            _settings.Dock = cbDock.Checked;
            _settings.DockOffset = (int)numericDockOffset.Value;
            _settings.DockSide = (DockSide)comboDockSide.SelectedValue;
            _settings.AspectRatio = (AspectRatio)comboAspect.SelectedValue;

            _settings.RefreshEveryNFrames = (int)numericFrameCount.Value;
            _settings.KeyItemsDisplay = KeyItemsCheckBox.Checked;
            _settings.KeyItemsStyle = (KeyItemStyle)KeyItemStyleComboBox.SelectedValue;
            _settings.PartyDisplay = PartyCheckBox.Checked;
            _settings.PartyPose = (Pose)PartyPoseComboBox.SelectedValue;
            _settings.PartyAnimate = PartyPoseAnimateCheckBox.Checked;
            _settings.PartyShowAnchor = PartyAnchorCheckBox.Checked;
            _settings.ObjectivesDisplay = ObjectivesCheckBox.Checked;
            _settings.BossesDisplay = BossesCheckBox.Checked;
            _settings.LocationsDisplay = LocationsCheckBox.Checked;
            _settings.LocationsShowKeyItems = LocationsKeyItemsCheckBox.Checked;
            _settings.LocationsShowCharacters = LocationsCharactersCheckBox.Checked;
            _settings.KeyItemEventEnabled = KeyItemBonkCheckBox.Checked;
            _settings.KeyItemBonkDefaultText = KeyItemBonkDefaultRadio.Checked;
            _settings.KeyItemBonkCustomText = KeyItemCustomTextBox.Text;            

            _applySettings?.Invoke();
        }

        private void cbDock_CheckedChanged(object sender, EventArgs e)
        {
            numericDockOffset.Enabled = cbDock.Checked;
            comboDockSide.Enabled = cbDock.Checked;
            comboAspect.Enabled = cbDock.Checked;
        }

        private void KeyItemCustomRadio_CheckedChanged(object sender, EventArgs e)
        {
            KeyItemCustomTextBox.Enabled = KeyItemCustomRadio.Checked;
        }
    }
}
