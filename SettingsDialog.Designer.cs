
namespace BizHawk.FreeEnterprise.Companion
{
    partial class SettingsDialog
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tabs = new System.Windows.Forms.TabControl();
            this.LayoutPage = new System.Windows.Forms.TabPage();
            this.BordersCheckBox = new System.Windows.Forms.CheckBox();
            this.ScaleIconsCheckBox = new System.Windows.Forms.CheckBox();
            this.label10 = new System.Windows.Forms.Label();
            this.comboInterpolation = new System.Windows.Forms.ComboBox();
            this.numericViewScale = new System.Windows.Forms.NumericUpDown();
            this.label9 = new System.Windows.Forms.Label();
            this.comboAspect = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.comboLayout = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.comboDockSide = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cbDock = new System.Windows.Forms.CheckBox();
            this.numericDockOffset = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.TrackingPage = new System.Windows.Forms.TabPage();
            this.LocationsKeyItemsCheckBox = new System.Windows.Forms.CheckBox();
            this.LocationsCharactersCheckBox = new System.Windows.Forms.CheckBox();
            this.PartyAnchorCheckBox = new System.Windows.Forms.CheckBox();
            this.PartyPoseAnimateCheckBox = new System.Windows.Forms.CheckBox();
            this.PartyPoseComboBox = new System.Windows.Forms.ComboBox();
            this.label12 = new System.Windows.Forms.Label();
            this.KeyItemStyleComboBox = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.LocationsCheckBox = new System.Windows.Forms.CheckBox();
            this.BossesCheckBox = new System.Windows.Forms.CheckBox();
            this.ObjectivesCheckBox = new System.Windows.Forms.CheckBox();
            this.PartyCheckBox = new System.Windows.Forms.CheckBox();
            this.KeyItemsCheckBox = new System.Windows.Forms.CheckBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.numericFrameCount = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.ExtrasPage = new System.Windows.Forms.TabPage();
            this.KeyItemCustomTextBox = new System.Windows.Forms.TextBox();
            this.KeyItemCustomRadio = new System.Windows.Forms.RadioButton();
            this.KeyItemBonkDefaultRadio = new System.Windows.Forms.RadioButton();
            this.KeyItemBonkCheckBox = new System.Windows.Forms.CheckBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.tabs.SuspendLayout();
            this.LayoutPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericViewScale)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericDockOffset)).BeginInit();
            this.TrackingPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericFrameCount)).BeginInit();
            this.ExtrasPage.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabs
            // 
            this.tabs.Controls.Add(this.LayoutPage);
            this.tabs.Controls.Add(this.TrackingPage);
            this.tabs.Controls.Add(this.ExtrasPage);
            this.tabs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabs.Location = new System.Drawing.Point(0, 0);
            this.tabs.Name = "tabs";
            this.tabs.SelectedIndex = 0;
            this.tabs.Size = new System.Drawing.Size(396, 297);
            this.tabs.TabIndex = 0;
            // 
            // LayoutPage
            // 
            this.LayoutPage.Controls.Add(this.BordersCheckBox);
            this.LayoutPage.Controls.Add(this.ScaleIconsCheckBox);
            this.LayoutPage.Controls.Add(this.label10);
            this.LayoutPage.Controls.Add(this.comboInterpolation);
            this.LayoutPage.Controls.Add(this.numericViewScale);
            this.LayoutPage.Controls.Add(this.label9);
            this.LayoutPage.Controls.Add(this.comboAspect);
            this.LayoutPage.Controls.Add(this.label8);
            this.LayoutPage.Controls.Add(this.comboLayout);
            this.LayoutPage.Controls.Add(this.label7);
            this.LayoutPage.Controls.Add(this.comboDockSide);
            this.LayoutPage.Controls.Add(this.label4);
            this.LayoutPage.Controls.Add(this.cbDock);
            this.LayoutPage.Controls.Add(this.numericDockOffset);
            this.LayoutPage.Controls.Add(this.label3);
            this.LayoutPage.Location = new System.Drawing.Point(4, 22);
            this.LayoutPage.Name = "LayoutPage";
            this.LayoutPage.Size = new System.Drawing.Size(388, 271);
            this.LayoutPage.TabIndex = 3;
            this.LayoutPage.Text = "General";
            this.LayoutPage.UseVisualStyleBackColor = true;
            // 
            // BordersCheckBox
            // 
            this.BordersCheckBox.AutoSize = true;
            this.BordersCheckBox.Location = new System.Drawing.Point(11, 93);
            this.BordersCheckBox.Name = "BordersCheckBox";
            this.BordersCheckBox.Size = new System.Drawing.Size(215, 17);
            this.BordersCheckBox.TabIndex = 17;
            this.BordersCheckBox.Text = "Show FFIV Style Border around trackers";
            this.BordersCheckBox.UseVisualStyleBackColor = true;
            // 
            // ScaleIconsCheckBox
            // 
            this.ScaleIconsCheckBox.AutoSize = true;
            this.ScaleIconsCheckBox.Location = new System.Drawing.Point(130, 41);
            this.ScaleIconsCheckBox.Name = "ScaleIconsCheckBox";
            this.ScaleIconsCheckBox.Size = new System.Drawing.Size(100, 17);
            this.ScaleIconsCheckBox.TabIndex = 16;
            this.ScaleIconsCheckBox.Text = "Scale Icons too";
            this.ScaleIconsCheckBox.UseVisualStyleBackColor = true;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(8, 69);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(98, 13);
            this.label10.TabIndex = 15;
            this.label10.Text = "Interpolation Mode:";
            // 
            // comboInterpolation
            // 
            this.comboInterpolation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboInterpolation.FormattingEnabled = true;
            this.comboInterpolation.Location = new System.Drawing.Point(112, 66);
            this.comboInterpolation.Name = "comboInterpolation";
            this.comboInterpolation.Size = new System.Drawing.Size(118, 21);
            this.comboInterpolation.TabIndex = 14;
            // 
            // numericViewScale
            // 
            this.numericViewScale.Increment = new decimal(new int[] {
            25,
            0,
            0,
            0});
            this.numericViewScale.Location = new System.Drawing.Point(82, 40);
            this.numericViewScale.Maximum = new decimal(new int[] {
            300,
            0,
            0,
            0});
            this.numericViewScale.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numericViewScale.Name = "numericViewScale";
            this.numericViewScale.ReadOnly = true;
            this.numericViewScale.Size = new System.Drawing.Size(39, 20);
            this.numericViewScale.TabIndex = 13;
            this.numericViewScale.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(8, 42);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(61, 13);
            this.label9.TabIndex = 12;
            this.label9.Text = "Font Scale:";
            // 
            // comboAspect
            // 
            this.comboAspect.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboAspect.FormattingEnabled = true;
            this.comboAspect.Location = new System.Drawing.Point(82, 224);
            this.comboAspect.Name = "comboAspect";
            this.comboAspect.Size = new System.Drawing.Size(148, 21);
            this.comboAspect.TabIndex = 11;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(8, 227);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(71, 13);
            this.label8.TabIndex = 10;
            this.label8.Text = "Aspect Ratio:";
            // 
            // comboLayout
            // 
            this.comboLayout.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboLayout.FormattingEnabled = true;
            this.comboLayout.Location = new System.Drawing.Point(82, 13);
            this.comboLayout.Name = "comboLayout";
            this.comboLayout.Size = new System.Drawing.Size(148, 21);
            this.comboLayout.TabIndex = 9;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(8, 16);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(72, 13);
            this.label7.TabIndex = 8;
            this.label7.Text = "Layout Mode:";
            // 
            // comboDockSide
            // 
            this.comboDockSide.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboDockSide.FormattingEnabled = true;
            this.comboDockSide.Location = new System.Drawing.Point(82, 197);
            this.comboDockSide.Name = "comboDockSide";
            this.comboDockSide.Size = new System.Drawing.Size(148, 21);
            this.comboDockSide.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 200);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(60, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Dock Side:";
            // 
            // cbDock
            // 
            this.cbDock.AutoSize = true;
            this.cbDock.Location = new System.Drawing.Point(11, 148);
            this.cbDock.Name = "cbDock";
            this.cbDock.Size = new System.Drawing.Size(109, 17);
            this.cbDock.TabIndex = 2;
            this.cbDock.Text = "Dock to BizHawk";
            this.cbDock.UseVisualStyleBackColor = true;
            this.cbDock.CheckedChanged += new System.EventHandler(this.cbDock_CheckedChanged);
            // 
            // numericDockOffset
            // 
            this.numericDockOffset.Location = new System.Drawing.Point(82, 171);
            this.numericDockOffset.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.numericDockOffset.Name = "numericDockOffset";
            this.numericDockOffset.ReadOnly = true;
            this.numericDockOffset.Size = new System.Drawing.Size(39, 20);
            this.numericDockOffset.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 173);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Dock Offset:";
            // 
            // TrackingPage
            // 
            this.TrackingPage.Controls.Add(this.LocationsKeyItemsCheckBox);
            this.TrackingPage.Controls.Add(this.LocationsCharactersCheckBox);
            this.TrackingPage.Controls.Add(this.PartyAnchorCheckBox);
            this.TrackingPage.Controls.Add(this.PartyPoseAnimateCheckBox);
            this.TrackingPage.Controls.Add(this.PartyPoseComboBox);
            this.TrackingPage.Controls.Add(this.label12);
            this.TrackingPage.Controls.Add(this.KeyItemStyleComboBox);
            this.TrackingPage.Controls.Add(this.label11);
            this.TrackingPage.Controls.Add(this.LocationsCheckBox);
            this.TrackingPage.Controls.Add(this.BossesCheckBox);
            this.TrackingPage.Controls.Add(this.ObjectivesCheckBox);
            this.TrackingPage.Controls.Add(this.PartyCheckBox);
            this.TrackingPage.Controls.Add(this.KeyItemsCheckBox);
            this.TrackingPage.Controls.Add(this.textBox1);
            this.TrackingPage.Controls.Add(this.label6);
            this.TrackingPage.Controls.Add(this.numericFrameCount);
            this.TrackingPage.Controls.Add(this.label5);
            this.TrackingPage.Location = new System.Drawing.Point(4, 22);
            this.TrackingPage.Name = "TrackingPage";
            this.TrackingPage.Size = new System.Drawing.Size(388, 271);
            this.TrackingPage.TabIndex = 6;
            this.TrackingPage.Text = "Tracking";
            this.TrackingPage.UseVisualStyleBackColor = true;
            // 
            // LocationsKeyItemsCheckBox
            // 
            this.LocationsKeyItemsCheckBox.AutoSize = true;
            this.LocationsKeyItemsCheckBox.Location = new System.Drawing.Point(120, 225);
            this.LocationsKeyItemsCheckBox.Name = "LocationsKeyItemsCheckBox";
            this.LocationsKeyItemsCheckBox.Size = new System.Drawing.Size(192, 17);
            this.LocationsKeyItemsCheckBox.TabIndex = 24;
            this.LocationsKeyItemsCheckBox.Text = "Show Available Key Item Locations";
            this.LocationsKeyItemsCheckBox.UseVisualStyleBackColor = true;
            // 
            // LocationsCharactersCheckBox
            // 
            this.LocationsCharactersCheckBox.AutoSize = true;
            this.LocationsCharactersCheckBox.Location = new System.Drawing.Point(120, 248);
            this.LocationsCharactersCheckBox.Name = "LocationsCharactersCheckBox";
            this.LocationsCharactersCheckBox.Size = new System.Drawing.Size(197, 17);
            this.LocationsCharactersCheckBox.TabIndex = 23;
            this.LocationsCharactersCheckBox.Text = "Show Available Character Locations";
            this.LocationsCharactersCheckBox.UseVisualStyleBackColor = true;
            // 
            // PartyAnchorCheckBox
            // 
            this.PartyAnchorCheckBox.AutoSize = true;
            this.PartyAnchorCheckBox.Location = new System.Drawing.Point(120, 156);
            this.PartyAnchorCheckBox.Name = "PartyAnchorCheckBox";
            this.PartyAnchorCheckBox.Size = new System.Drawing.Size(249, 17);
            this.PartyAnchorCheckBox.TabIndex = 22;
            this.PartyAnchorCheckBox.Text = "Show Anchor (C:Hero and Vanilla:Agility aware)";
            this.PartyAnchorCheckBox.UseVisualStyleBackColor = true;
            // 
            // PartyPoseAnimateCheckBox
            // 
            this.PartyPoseAnimateCheckBox.AutoSize = true;
            this.PartyPoseAnimateCheckBox.Location = new System.Drawing.Point(256, 133);
            this.PartyPoseAnimateCheckBox.Name = "PartyPoseAnimateCheckBox";
            this.PartyPoseAnimateCheckBox.Size = new System.Drawing.Size(124, 17);
            this.PartyPoseAnimateCheckBox.TabIndex = 21;
            this.PartyPoseAnimateCheckBox.Text = "Animate (If available)";
            this.PartyPoseAnimateCheckBox.UseVisualStyleBackColor = true;
            // 
            // PartyPoseComboBox
            // 
            this.PartyPoseComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.PartyPoseComboBox.FormattingEnabled = true;
            this.PartyPoseComboBox.Location = new System.Drawing.Point(156, 131);
            this.PartyPoseComboBox.Name = "PartyPoseComboBox";
            this.PartyPoseComboBox.Size = new System.Drawing.Size(94, 21);
            this.PartyPoseComboBox.TabIndex = 20;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(117, 134);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(34, 13);
            this.label12.TabIndex = 19;
            this.label12.Text = "Pose:";
            // 
            // KeyItemStyleComboBox
            // 
            this.KeyItemStyleComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.KeyItemStyleComboBox.FormattingEnabled = true;
            this.KeyItemStyleComboBox.Location = new System.Drawing.Point(156, 104);
            this.KeyItemStyleComboBox.Name = "KeyItemStyleComboBox";
            this.KeyItemStyleComboBox.Size = new System.Drawing.Size(94, 21);
            this.KeyItemStyleComboBox.TabIndex = 18;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(117, 107);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(33, 13);
            this.label11.TabIndex = 17;
            this.label11.Text = "Style:";
            // 
            // LocationsCheckBox
            // 
            this.LocationsCheckBox.AutoSize = true;
            this.LocationsCheckBox.Location = new System.Drawing.Point(11, 225);
            this.LocationsCheckBox.Name = "LocationsCheckBox";
            this.LocationsCheckBox.Size = new System.Drawing.Size(103, 17);
            this.LocationsCheckBox.TabIndex = 16;
            this.LocationsCheckBox.Text = "Track Locations";
            this.LocationsCheckBox.UseVisualStyleBackColor = true;
            // 
            // BossesCheckBox
            // 
            this.BossesCheckBox.AutoSize = true;
            this.BossesCheckBox.Location = new System.Drawing.Point(11, 202);
            this.BossesCheckBox.Name = "BossesCheckBox";
            this.BossesCheckBox.Size = new System.Drawing.Size(142, 17);
            this.BossesCheckBox.TabIndex = 15;
            this.BossesCheckBox.Text = "Track Bosses (Manually)";
            this.BossesCheckBox.UseVisualStyleBackColor = true;
            // 
            // ObjectivesCheckBox
            // 
            this.ObjectivesCheckBox.AutoSize = true;
            this.ObjectivesCheckBox.Location = new System.Drawing.Point(11, 179);
            this.ObjectivesCheckBox.Name = "ObjectivesCheckBox";
            this.ObjectivesCheckBox.Size = new System.Drawing.Size(107, 17);
            this.ObjectivesCheckBox.TabIndex = 14;
            this.ObjectivesCheckBox.Text = "Track Objectives";
            this.ObjectivesCheckBox.UseVisualStyleBackColor = true;
            // 
            // PartyCheckBox
            // 
            this.PartyCheckBox.AutoSize = true;
            this.PartyCheckBox.Location = new System.Drawing.Point(11, 133);
            this.PartyCheckBox.Name = "PartyCheckBox";
            this.PartyCheckBox.Size = new System.Drawing.Size(81, 17);
            this.PartyCheckBox.TabIndex = 13;
            this.PartyCheckBox.Text = "Track Party";
            this.PartyCheckBox.UseVisualStyleBackColor = true;
            // 
            // KeyItemsCheckBox
            // 
            this.KeyItemsCheckBox.AutoSize = true;
            this.KeyItemsCheckBox.Location = new System.Drawing.Point(11, 106);
            this.KeyItemsCheckBox.Name = "KeyItemsCheckBox";
            this.KeyItemsCheckBox.Size = new System.Drawing.Size(103, 17);
            this.KeyItemsCheckBox.TabIndex = 12;
            this.KeyItemsCheckBox.Text = "Track Key Items";
            this.KeyItemsCheckBox.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox1.Location = new System.Drawing.Point(11, 44);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(369, 56);
            this.textBox1.TabIndex = 11;
            this.textBox1.Text = "The game runs internally at 60 frames per second, so a setting of 60 will refresh" +
    " all data once a second. If you expierence audio glitches or slowdowns, increase" +
    " this value.";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(194, 15);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(38, 13);
            this.label6.TabIndex = 10;
            this.label6.Text = "frames";
            // 
            // numericFrameCount
            // 
            this.numericFrameCount.Location = new System.Drawing.Point(141, 13);
            this.numericFrameCount.Maximum = new decimal(new int[] {
            600,
            0,
            0,
            0});
            this.numericFrameCount.Name = "numericFrameCount";
            this.numericFrameCount.Size = new System.Drawing.Size(47, 20);
            this.numericFrameCount.TabIndex = 9;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(8, 15);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(127, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "Read tracking data every";
            // 
            // ExtrasPage
            // 
            this.ExtrasPage.Controls.Add(this.KeyItemCustomTextBox);
            this.ExtrasPage.Controls.Add(this.KeyItemCustomRadio);
            this.ExtrasPage.Controls.Add(this.KeyItemBonkDefaultRadio);
            this.ExtrasPage.Controls.Add(this.KeyItemBonkCheckBox);
            this.ExtrasPage.Location = new System.Drawing.Point(4, 22);
            this.ExtrasPage.Name = "ExtrasPage";
            this.ExtrasPage.Size = new System.Drawing.Size(388, 271);
            this.ExtrasPage.TabIndex = 7;
            this.ExtrasPage.Text = "Extras";
            this.ExtrasPage.UseVisualStyleBackColor = true;
            // 
            // KeyItemCustomTextBox
            // 
            this.KeyItemCustomTextBox.Location = new System.Drawing.Point(94, 61);
            this.KeyItemCustomTextBox.MaxLength = 13;
            this.KeyItemCustomTextBox.Name = "KeyItemCustomTextBox";
            this.KeyItemCustomTextBox.Size = new System.Drawing.Size(100, 20);
            this.KeyItemCustomTextBox.TabIndex = 16;
            // 
            // KeyItemCustomRadio
            // 
            this.KeyItemCustomRadio.AutoSize = true;
            this.KeyItemCustomRadio.Location = new System.Drawing.Point(25, 62);
            this.KeyItemCustomRadio.Name = "KeyItemCustomRadio";
            this.KeyItemCustomRadio.Size = new System.Drawing.Size(63, 17);
            this.KeyItemCustomRadio.TabIndex = 15;
            this.KeyItemCustomRadio.TabStop = true;
            this.KeyItemCustomRadio.Text = "Custom:";
            this.KeyItemCustomRadio.UseVisualStyleBackColor = true;
            this.KeyItemCustomRadio.CheckedChanged += new System.EventHandler(this.KeyItemCustomRadio_CheckedChanged);
            // 
            // KeyItemBonkDefaultRadio
            // 
            this.KeyItemBonkDefaultRadio.AutoSize = true;
            this.KeyItemBonkDefaultRadio.Location = new System.Drawing.Point(25, 39);
            this.KeyItemBonkDefaultRadio.Name = "KeyItemBonkDefaultRadio";
            this.KeyItemBonkDefaultRadio.Size = new System.Drawing.Size(87, 17);
            this.KeyItemBonkDefaultRadio.TabIndex = 14;
            this.KeyItemBonkDefaultRadio.TabStop = true;
            this.KeyItemBonkDefaultRadio.Text = "Default Zonk";
            this.KeyItemBonkDefaultRadio.UseVisualStyleBackColor = true;
            // 
            // KeyItemBonkCheckBox
            // 
            this.KeyItemBonkCheckBox.AutoSize = true;
            this.KeyItemBonkCheckBox.Location = new System.Drawing.Point(8, 16);
            this.KeyItemBonkCheckBox.Name = "KeyItemBonkCheckBox";
            this.KeyItemBonkCheckBox.Size = new System.Drawing.Size(141, 17);
            this.KeyItemBonkCheckBox.TabIndex = 13;
            this.KeyItemBonkCheckBox.Text = "New Key Item Animation";
            this.KeyItemBonkCheckBox.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.button1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 297);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(396, 38);
            this.panel1.TabIndex = 1;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button1.Location = new System.Drawing.Point(314, 8);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Apply";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // SettingsDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(396, 335);
            this.Controls.Add(this.tabs);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Name = "SettingsDialog";
            this.Text = "Display Settings";
            this.tabs.ResumeLayout(false);
            this.LayoutPage.ResumeLayout(false);
            this.LayoutPage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericViewScale)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericDockOffset)).EndInit();
            this.TrackingPage.ResumeLayout(false);
            this.TrackingPage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericFrameCount)).EndInit();
            this.ExtrasPage.ResumeLayout(false);
            this.ExtrasPage.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabs;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TabPage LayoutPage;
        private System.Windows.Forms.NumericUpDown numericDockOffset;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboDockSide;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox cbDock;
        private System.Windows.Forms.ComboBox comboLayout;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox comboAspect;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.NumericUpDown numericViewScale;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox comboInterpolation;
        private System.Windows.Forms.TabPage TrackingPage;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown numericFrameCount;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.CheckBox ScaleIconsCheckBox;
        private System.Windows.Forms.CheckBox BordersCheckBox;
        private System.Windows.Forms.CheckBox LocationsCheckBox;
        private System.Windows.Forms.CheckBox BossesCheckBox;
        private System.Windows.Forms.CheckBox ObjectivesCheckBox;
        private System.Windows.Forms.CheckBox PartyCheckBox;
        private System.Windows.Forms.CheckBox KeyItemsCheckBox;
        private System.Windows.Forms.CheckBox LocationsKeyItemsCheckBox;
        private System.Windows.Forms.CheckBox LocationsCharactersCheckBox;
        private System.Windows.Forms.CheckBox PartyAnchorCheckBox;
        private System.Windows.Forms.CheckBox PartyPoseAnimateCheckBox;
        private System.Windows.Forms.ComboBox PartyPoseComboBox;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.ComboBox KeyItemStyleComboBox;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TabPage ExtrasPage;
        private System.Windows.Forms.CheckBox KeyItemBonkCheckBox;
        private System.Windows.Forms.RadioButton KeyItemBonkDefaultRadio;
        private System.Windows.Forms.RadioButton KeyItemCustomRadio;
        private System.Windows.Forms.TextBox KeyItemCustomTextBox;
    }
}