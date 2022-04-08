
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
            this.GamePage = new System.Windows.Forms.TabPage();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.numericFrameCount = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.KeyItemPage = new System.Windows.Forms.TabPage();
            this.cbKeyItemScaling = new System.Windows.Forms.CheckBox();
            this.comboKeyItemsStyle = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cbKeyItemBorder = new System.Windows.Forms.CheckBox();
            this.cbKeyItemDisplay = new System.Windows.Forms.CheckBox();
            this.PartyPage = new System.Windows.Forms.TabPage();
            this.cbPartyAnimate = new System.Windows.Forms.CheckBox();
            this.comboPartyPose = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cbPartyBorder = new System.Windows.Forms.CheckBox();
            this.cbPartyDisplay = new System.Windows.Forms.CheckBox();
            this.ObjectivesPage = new System.Windows.Forms.TabPage();
            this.cbObjectiveBorder = new System.Windows.Forms.CheckBox();
            this.cbObjectiveDisplay = new System.Windows.Forms.CheckBox();
            this.BossesPage = new System.Windows.Forms.TabPage();
            this.cbBossesScaling = new System.Windows.Forms.CheckBox();
            this.cbBossesBorder = new System.Windows.Forms.CheckBox();
            this.cbBossesDisplay = new System.Windows.Forms.CheckBox();
            this.LocationsPage = new System.Windows.Forms.TabPage();
            this.cbLocationsKI = new System.Windows.Forms.CheckBox();
            this.cbLocationsChar = new System.Windows.Forms.CheckBox();
            this.cbLocationsBorder = new System.Windows.Forms.CheckBox();
            this.cbLocationsDisplay = new System.Windows.Forms.CheckBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.cbShowAnchor = new System.Windows.Forms.CheckBox();
            this.tabs.SuspendLayout();
            this.LayoutPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericViewScale)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericDockOffset)).BeginInit();
            this.GamePage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericFrameCount)).BeginInit();
            this.KeyItemPage.SuspendLayout();
            this.PartyPage.SuspendLayout();
            this.ObjectivesPage.SuspendLayout();
            this.BossesPage.SuspendLayout();
            this.LocationsPage.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabs
            // 
            this.tabs.Controls.Add(this.LayoutPage);
            this.tabs.Controls.Add(this.GamePage);
            this.tabs.Controls.Add(this.KeyItemPage);
            this.tabs.Controls.Add(this.PartyPage);
            this.tabs.Controls.Add(this.ObjectivesPage);
            this.tabs.Controls.Add(this.BossesPage);
            this.tabs.Controls.Add(this.LocationsPage);
            this.tabs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabs.Location = new System.Drawing.Point(0, 0);
            this.tabs.Name = "tabs";
            this.tabs.SelectedIndex = 0;
            this.tabs.Size = new System.Drawing.Size(396, 264);
            this.tabs.TabIndex = 0;
            // 
            // LayoutPage
            // 
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
            this.LayoutPage.Size = new System.Drawing.Size(388, 238);
            this.LayoutPage.TabIndex = 3;
            this.LayoutPage.Text = "General";
            this.LayoutPage.UseVisualStyleBackColor = true;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(8, 81);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(98, 13);
            this.label10.TabIndex = 15;
            this.label10.Text = "Interpolation Mode:";
            // 
            // comboInterpolation
            // 
            this.comboInterpolation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboInterpolation.FormattingEnabled = true;
            this.comboInterpolation.Location = new System.Drawing.Point(112, 78);
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
            this.numericViewScale.Location = new System.Drawing.Point(82, 52);
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
            this.label9.Location = new System.Drawing.Point(8, 54);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(61, 13);
            this.label9.TabIndex = 12;
            this.label9.Text = "Font Scale:";
            // 
            // comboAspect
            // 
            this.comboAspect.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboAspect.FormattingEnabled = true;
            this.comboAspect.Location = new System.Drawing.Point(85, 201);
            this.comboAspect.Name = "comboAspect";
            this.comboAspect.Size = new System.Drawing.Size(121, 21);
            this.comboAspect.TabIndex = 11;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(8, 204);
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
            this.comboLayout.Size = new System.Drawing.Size(121, 21);
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
            this.comboDockSide.Location = new System.Drawing.Point(85, 174);
            this.comboDockSide.Name = "comboDockSide";
            this.comboDockSide.Size = new System.Drawing.Size(121, 21);
            this.comboDockSide.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 177);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(60, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Dock Side:";
            // 
            // cbDock
            // 
            this.cbDock.AutoSize = true;
            this.cbDock.Location = new System.Drawing.Point(11, 125);
            this.cbDock.Name = "cbDock";
            this.cbDock.Size = new System.Drawing.Size(109, 17);
            this.cbDock.TabIndex = 2;
            this.cbDock.Text = "Dock to BizHawk";
            this.cbDock.UseVisualStyleBackColor = true;
            this.cbDock.CheckedChanged += new System.EventHandler(this.cbDock_CheckedChanged);
            // 
            // numericDockOffset
            // 
            this.numericDockOffset.Location = new System.Drawing.Point(85, 148);
            this.numericDockOffset.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.numericDockOffset.Name = "numericDockOffset";
            this.numericDockOffset.ReadOnly = true;
            this.numericDockOffset.Size = new System.Drawing.Size(121, 20);
            this.numericDockOffset.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 150);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Dock Offset:";
            // 
            // GamePage
            // 
            this.GamePage.Controls.Add(this.textBox1);
            this.GamePage.Controls.Add(this.label6);
            this.GamePage.Controls.Add(this.numericFrameCount);
            this.GamePage.Controls.Add(this.label5);
            this.GamePage.Location = new System.Drawing.Point(4, 22);
            this.GamePage.Name = "GamePage";
            this.GamePage.Size = new System.Drawing.Size(388, 238);
            this.GamePage.TabIndex = 6;
            this.GamePage.Text = "Game";
            this.GamePage.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox1.Enabled = false;
            this.textBox1.Location = new System.Drawing.Point(8, 44);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(372, 119);
            this.textBox1.TabIndex = 11;
            this.textBox1.Text = "The game runs internally at 60 frames per second, so a setting of 60 will refresh" +
    " all data once a second. If you expierence audio glitches or slowdowns, increase" +
    " this value.";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(153, 15);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(38, 13);
            this.label6.TabIndex = 10;
            this.label6.Text = "frames";
            // 
            // numericFrameCount
            // 
            this.numericFrameCount.Location = new System.Drawing.Point(100, 13);
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
            this.label5.Size = new System.Drawing.Size(86, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "Read data every";
            // 
            // KeyItemPage
            // 
            this.KeyItemPage.Controls.Add(this.cbKeyItemScaling);
            this.KeyItemPage.Controls.Add(this.comboKeyItemsStyle);
            this.KeyItemPage.Controls.Add(this.label1);
            this.KeyItemPage.Controls.Add(this.cbKeyItemBorder);
            this.KeyItemPage.Controls.Add(this.cbKeyItemDisplay);
            this.KeyItemPage.Location = new System.Drawing.Point(4, 22);
            this.KeyItemPage.Name = "KeyItemPage";
            this.KeyItemPage.Padding = new System.Windows.Forms.Padding(3);
            this.KeyItemPage.Size = new System.Drawing.Size(388, 238);
            this.KeyItemPage.TabIndex = 0;
            this.KeyItemPage.Text = "Key Items";
            this.KeyItemPage.UseVisualStyleBackColor = true;
            // 
            // cbKeyItemScaling
            // 
            this.cbKeyItemScaling.AutoSize = true;
            this.cbKeyItemScaling.Location = new System.Drawing.Point(8, 76);
            this.cbKeyItemScaling.Name = "cbKeyItemScaling";
            this.cbKeyItemScaling.Size = new System.Drawing.Size(128, 17);
            this.cbKeyItemScaling.TabIndex = 4;
            this.cbKeyItemScaling.Text = "Scale Icons with Font";
            this.cbKeyItemScaling.UseVisualStyleBackColor = true;
            // 
            // comboKeyItemsStyle
            // 
            this.comboKeyItemsStyle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboKeyItemsStyle.FormattingEnabled = true;
            this.comboKeyItemsStyle.Location = new System.Drawing.Point(47, 49);
            this.comboKeyItemsStyle.Name = "comboKeyItemsStyle";
            this.comboKeyItemsStyle.Size = new System.Drawing.Size(121, 21);
            this.comboKeyItemsStyle.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 52);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(33, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Style:";
            // 
            // cbKeyItemBorder
            // 
            this.cbKeyItemBorder.AutoSize = true;
            this.cbKeyItemBorder.Location = new System.Drawing.Point(8, 29);
            this.cbKeyItemBorder.Name = "cbKeyItemBorder";
            this.cbKeyItemBorder.Size = new System.Drawing.Size(138, 17);
            this.cbKeyItemBorder.TabIndex = 1;
            this.cbKeyItemBorder.Text = "Show FFIV Style Border";
            this.cbKeyItemBorder.UseVisualStyleBackColor = true;
            // 
            // cbKeyItemDisplay
            // 
            this.cbKeyItemDisplay.AutoSize = true;
            this.cbKeyItemDisplay.Location = new System.Drawing.Point(8, 6);
            this.cbKeyItemDisplay.Name = "cbKeyItemDisplay";
            this.cbKeyItemDisplay.Size = new System.Drawing.Size(109, 17);
            this.cbKeyItemDisplay.TabIndex = 0;
            this.cbKeyItemDisplay.Text = "Display Key Items";
            this.cbKeyItemDisplay.UseVisualStyleBackColor = true;
            // 
            // PartyPage
            // 
            this.PartyPage.Controls.Add(this.cbShowAnchor);
            this.PartyPage.Controls.Add(this.cbPartyAnimate);
            this.PartyPage.Controls.Add(this.comboPartyPose);
            this.PartyPage.Controls.Add(this.label2);
            this.PartyPage.Controls.Add(this.cbPartyBorder);
            this.PartyPage.Controls.Add(this.cbPartyDisplay);
            this.PartyPage.Location = new System.Drawing.Point(4, 22);
            this.PartyPage.Name = "PartyPage";
            this.PartyPage.Padding = new System.Windows.Forms.Padding(3);
            this.PartyPage.Size = new System.Drawing.Size(388, 238);
            this.PartyPage.TabIndex = 1;
            this.PartyPage.Text = "Party";
            this.PartyPage.UseVisualStyleBackColor = true;
            // 
            // cbPartyAnimate
            // 
            this.cbPartyAnimate.AutoSize = true;
            this.cbPartyAnimate.Location = new System.Drawing.Point(152, 50);
            this.cbPartyAnimate.Name = "cbPartyAnimate";
            this.cbPartyAnimate.Size = new System.Drawing.Size(124, 17);
            this.cbPartyAnimate.TabIndex = 8;
            this.cbPartyAnimate.Text = "Animate (If available)";
            this.cbPartyAnimate.UseVisualStyleBackColor = true;
            // 
            // comboPartyPose
            // 
            this.comboPartyPose.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboPartyPose.FormattingEnabled = true;
            this.comboPartyPose.Location = new System.Drawing.Point(48, 48);
            this.comboPartyPose.Name = "comboPartyPose";
            this.comboPartyPose.Size = new System.Drawing.Size(98, 21);
            this.comboPartyPose.TabIndex = 7;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 51);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(34, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Pose:";
            // 
            // cbPartyBorder
            // 
            this.cbPartyBorder.AutoSize = true;
            this.cbPartyBorder.Location = new System.Drawing.Point(8, 29);
            this.cbPartyBorder.Name = "cbPartyBorder";
            this.cbPartyBorder.Size = new System.Drawing.Size(138, 17);
            this.cbPartyBorder.TabIndex = 5;
            this.cbPartyBorder.Text = "Show FFIV Style Border";
            this.cbPartyBorder.UseVisualStyleBackColor = true;
            // 
            // cbPartyDisplay
            // 
            this.cbPartyDisplay.AutoSize = true;
            this.cbPartyDisplay.Location = new System.Drawing.Point(8, 6);
            this.cbPartyDisplay.Name = "cbPartyDisplay";
            this.cbPartyDisplay.Size = new System.Drawing.Size(87, 17);
            this.cbPartyDisplay.TabIndex = 4;
            this.cbPartyDisplay.Text = "Display Party";
            this.cbPartyDisplay.UseVisualStyleBackColor = true;
            // 
            // ObjectivesPage
            // 
            this.ObjectivesPage.Controls.Add(this.cbObjectiveBorder);
            this.ObjectivesPage.Controls.Add(this.cbObjectiveDisplay);
            this.ObjectivesPage.Location = new System.Drawing.Point(4, 22);
            this.ObjectivesPage.Name = "ObjectivesPage";
            this.ObjectivesPage.Padding = new System.Windows.Forms.Padding(3);
            this.ObjectivesPage.Size = new System.Drawing.Size(388, 238);
            this.ObjectivesPage.TabIndex = 2;
            this.ObjectivesPage.Text = "Objectives";
            this.ObjectivesPage.UseVisualStyleBackColor = true;
            // 
            // cbObjectiveBorder
            // 
            this.cbObjectiveBorder.AutoSize = true;
            this.cbObjectiveBorder.Location = new System.Drawing.Point(8, 29);
            this.cbObjectiveBorder.Name = "cbObjectiveBorder";
            this.cbObjectiveBorder.Size = new System.Drawing.Size(138, 17);
            this.cbObjectiveBorder.TabIndex = 3;
            this.cbObjectiveBorder.Text = "Show FFIV Style Border";
            this.cbObjectiveBorder.UseVisualStyleBackColor = true;
            // 
            // cbObjectiveDisplay
            // 
            this.cbObjectiveDisplay.AutoSize = true;
            this.cbObjectiveDisplay.Location = new System.Drawing.Point(8, 6);
            this.cbObjectiveDisplay.Name = "cbObjectiveDisplay";
            this.cbObjectiveDisplay.Size = new System.Drawing.Size(113, 17);
            this.cbObjectiveDisplay.TabIndex = 2;
            this.cbObjectiveDisplay.Text = "Display Objectives";
            this.cbObjectiveDisplay.UseVisualStyleBackColor = true;
            // 
            // BossesPage
            // 
            this.BossesPage.Controls.Add(this.cbBossesScaling);
            this.BossesPage.Controls.Add(this.cbBossesBorder);
            this.BossesPage.Controls.Add(this.cbBossesDisplay);
            this.BossesPage.Location = new System.Drawing.Point(4, 22);
            this.BossesPage.Name = "BossesPage";
            this.BossesPage.Size = new System.Drawing.Size(388, 238);
            this.BossesPage.TabIndex = 5;
            this.BossesPage.Text = "Bosses";
            this.BossesPage.UseVisualStyleBackColor = true;
            // 
            // cbBossesScaling
            // 
            this.cbBossesScaling.AutoSize = true;
            this.cbBossesScaling.Location = new System.Drawing.Point(8, 52);
            this.cbBossesScaling.Name = "cbBossesScaling";
            this.cbBossesScaling.Size = new System.Drawing.Size(128, 17);
            this.cbBossesScaling.TabIndex = 8;
            this.cbBossesScaling.Text = "Scale Icons with Font";
            this.cbBossesScaling.UseVisualStyleBackColor = true;
            // 
            // cbBossesBorder
            // 
            this.cbBossesBorder.AutoSize = true;
            this.cbBossesBorder.Location = new System.Drawing.Point(8, 29);
            this.cbBossesBorder.Name = "cbBossesBorder";
            this.cbBossesBorder.Size = new System.Drawing.Size(138, 17);
            this.cbBossesBorder.TabIndex = 7;
            this.cbBossesBorder.Text = "Show FFIV Style Border";
            this.cbBossesBorder.UseVisualStyleBackColor = true;
            // 
            // cbBossesDisplay
            // 
            this.cbBossesDisplay.AutoSize = true;
            this.cbBossesDisplay.Location = new System.Drawing.Point(8, 6);
            this.cbBossesDisplay.Name = "cbBossesDisplay";
            this.cbBossesDisplay.Size = new System.Drawing.Size(97, 17);
            this.cbBossesDisplay.TabIndex = 6;
            this.cbBossesDisplay.Text = "Display Bosses";
            this.cbBossesDisplay.UseVisualStyleBackColor = true;
            // 
            // LocationsPage
            // 
            this.LocationsPage.Controls.Add(this.cbLocationsKI);
            this.LocationsPage.Controls.Add(this.cbLocationsChar);
            this.LocationsPage.Controls.Add(this.cbLocationsBorder);
            this.LocationsPage.Controls.Add(this.cbLocationsDisplay);
            this.LocationsPage.Location = new System.Drawing.Point(4, 22);
            this.LocationsPage.Name = "LocationsPage";
            this.LocationsPage.Size = new System.Drawing.Size(388, 238);
            this.LocationsPage.TabIndex = 4;
            this.LocationsPage.Text = "Locations";
            this.LocationsPage.UseVisualStyleBackColor = true;
            // 
            // cbLocationsKI
            // 
            this.cbLocationsKI.AutoSize = true;
            this.cbLocationsKI.Location = new System.Drawing.Point(8, 52);
            this.cbLocationsKI.Name = "cbLocationsKI";
            this.cbLocationsKI.Size = new System.Drawing.Size(192, 17);
            this.cbLocationsKI.TabIndex = 7;
            this.cbLocationsKI.Text = "Show Available Key Item Locations";
            this.cbLocationsKI.UseVisualStyleBackColor = true;
            // 
            // cbLocationsChar
            // 
            this.cbLocationsChar.AutoSize = true;
            this.cbLocationsChar.Location = new System.Drawing.Point(8, 75);
            this.cbLocationsChar.Name = "cbLocationsChar";
            this.cbLocationsChar.Size = new System.Drawing.Size(197, 17);
            this.cbLocationsChar.TabIndex = 6;
            this.cbLocationsChar.Text = "Show Available Character Locations";
            this.cbLocationsChar.UseVisualStyleBackColor = true;
            // 
            // cbLocationsBorder
            // 
            this.cbLocationsBorder.AutoSize = true;
            this.cbLocationsBorder.Location = new System.Drawing.Point(8, 29);
            this.cbLocationsBorder.Name = "cbLocationsBorder";
            this.cbLocationsBorder.Size = new System.Drawing.Size(138, 17);
            this.cbLocationsBorder.TabIndex = 5;
            this.cbLocationsBorder.Text = "Show FFIV Style Border";
            this.cbLocationsBorder.UseVisualStyleBackColor = true;
            // 
            // cbLocationsDisplay
            // 
            this.cbLocationsDisplay.AutoSize = true;
            this.cbLocationsDisplay.Location = new System.Drawing.Point(8, 6);
            this.cbLocationsDisplay.Name = "cbLocationsDisplay";
            this.cbLocationsDisplay.Size = new System.Drawing.Size(109, 17);
            this.cbLocationsDisplay.TabIndex = 4;
            this.cbLocationsDisplay.Text = "Display Locations";
            this.cbLocationsDisplay.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.button1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 264);
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
            // cbShowAnchor
            // 
            this.cbShowAnchor.AutoSize = true;
            this.cbShowAnchor.Location = new System.Drawing.Point(8, 75);
            this.cbShowAnchor.Name = "cbShowAnchor";
            this.cbShowAnchor.Size = new System.Drawing.Size(249, 17);
            this.cbShowAnchor.TabIndex = 9;
            this.cbShowAnchor.Text = "Show Anchor (C:Hero and Vanilla:Agility aware)";
            this.cbShowAnchor.UseVisualStyleBackColor = true;
            // 
            // SettingsDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(396, 302);
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
            this.GamePage.ResumeLayout(false);
            this.GamePage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericFrameCount)).EndInit();
            this.KeyItemPage.ResumeLayout(false);
            this.KeyItemPage.PerformLayout();
            this.PartyPage.ResumeLayout(false);
            this.PartyPage.PerformLayout();
            this.ObjectivesPage.ResumeLayout(false);
            this.ObjectivesPage.PerformLayout();
            this.BossesPage.ResumeLayout(false);
            this.BossesPage.PerformLayout();
            this.LocationsPage.ResumeLayout(false);
            this.LocationsPage.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabs;
        private System.Windows.Forms.TabPage KeyItemPage;
        private System.Windows.Forms.ComboBox comboKeyItemsStyle;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox cbKeyItemBorder;
        private System.Windows.Forms.CheckBox cbKeyItemDisplay;
        private System.Windows.Forms.TabPage PartyPage;
        private System.Windows.Forms.ComboBox comboPartyPose;
        private System.Windows.Forms.CheckBox cbPartyBorder;
        private System.Windows.Forms.CheckBox cbPartyDisplay;
        private System.Windows.Forms.TabPage ObjectivesPage;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.CheckBox cbObjectiveBorder;
        private System.Windows.Forms.CheckBox cbObjectiveDisplay;
        private System.Windows.Forms.TabPage LayoutPage;
        private System.Windows.Forms.NumericUpDown numericDockOffset;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboDockSide;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox cbDock;
        private System.Windows.Forms.TabPage LocationsPage;
        private System.Windows.Forms.CheckBox cbLocationsBorder;
        private System.Windows.Forms.CheckBox cbLocationsDisplay;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox cbPartyAnimate;
        private System.Windows.Forms.ComboBox comboLayout;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox comboAspect;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TabPage BossesPage;
        private System.Windows.Forms.CheckBox cbBossesBorder;
        private System.Windows.Forms.CheckBox cbBossesDisplay;
        private System.Windows.Forms.NumericUpDown numericViewScale;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox comboInterpolation;
        private System.Windows.Forms.CheckBox cbLocationsKI;
        private System.Windows.Forms.CheckBox cbLocationsChar;
        private System.Windows.Forms.TabPage GamePage;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown numericFrameCount;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.CheckBox cbKeyItemScaling;
        private System.Windows.Forms.CheckBox cbBossesScaling;
        private System.Windows.Forms.CheckBox cbShowAnchor;
    }
}