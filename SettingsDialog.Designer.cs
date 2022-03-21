
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
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.comboKeyItemsStyle = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cbKeyItemBorder = new System.Windows.Forms.CheckBox();
            this.cbKeyItemDisplay = new System.Windows.Forms.CheckBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.comboPartyPose = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cbPartyBorder = new System.Windows.Forms.CheckBox();
            this.cbPartyDisplay = new System.Windows.Forms.CheckBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.cbObjectiveBorder = new System.Windows.Forms.CheckBox();
            this.cbObjectiveDisplay = new System.Windows.Forms.CheckBox();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.cbLocationsBorder = new System.Windows.Forms.CheckBox();
            this.cbLocationsDisplay = new System.Windows.Forms.CheckBox();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.label6 = new System.Windows.Forms.Label();
            this.numericFrameCount = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.comboDockSide = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cbDock = new System.Windows.Forms.CheckBox();
            this.numericDockOffset = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.cbPartyAnimate = new System.Windows.Forms.CheckBox();
            this.tabs.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage5.SuspendLayout();
            this.tabPage4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericFrameCount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericDockOffset)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabs
            // 
            this.tabs.Controls.Add(this.tabPage1);
            this.tabs.Controls.Add(this.tabPage2);
            this.tabs.Controls.Add(this.tabPage3);
            this.tabs.Controls.Add(this.tabPage5);
            this.tabs.Controls.Add(this.tabPage4);
            this.tabs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabs.Location = new System.Drawing.Point(0, 0);
            this.tabs.Name = "tabs";
            this.tabs.SelectedIndex = 0;
            this.tabs.Size = new System.Drawing.Size(334, 139);
            this.tabs.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.comboKeyItemsStyle);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.cbKeyItemBorder);
            this.tabPage1.Controls.Add(this.cbKeyItemDisplay);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(326, 113);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Key Items";
            this.tabPage1.UseVisualStyleBackColor = true;
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
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.cbPartyAnimate);
            this.tabPage2.Controls.Add(this.comboPartyPose);
            this.tabPage2.Controls.Add(this.label2);
            this.tabPage2.Controls.Add(this.cbPartyBorder);
            this.tabPage2.Controls.Add(this.cbPartyDisplay);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(326, 113);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Characters";
            this.tabPage2.UseVisualStyleBackColor = true;
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
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.cbObjectiveBorder);
            this.tabPage3.Controls.Add(this.cbObjectiveDisplay);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(326, 113);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Objectives";
            this.tabPage3.UseVisualStyleBackColor = true;
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
            // tabPage5
            // 
            this.tabPage5.Controls.Add(this.cbLocationsBorder);
            this.tabPage5.Controls.Add(this.cbLocationsDisplay);
            this.tabPage5.Location = new System.Drawing.Point(4, 22);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Size = new System.Drawing.Size(326, 113);
            this.tabPage5.TabIndex = 4;
            this.tabPage5.Text = "Locations";
            this.tabPage5.UseVisualStyleBackColor = true;
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
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.label6);
            this.tabPage4.Controls.Add(this.numericFrameCount);
            this.tabPage4.Controls.Add(this.label5);
            this.tabPage4.Controls.Add(this.comboDockSide);
            this.tabPage4.Controls.Add(this.label4);
            this.tabPage4.Controls.Add(this.cbDock);
            this.tabPage4.Controls.Add(this.numericDockOffset);
            this.tabPage4.Controls.Add(this.label3);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Size = new System.Drawing.Size(326, 113);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "General";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(150, 84);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(38, 13);
            this.label6.TabIndex = 7;
            this.label6.Text = "frames";
            // 
            // numericFrameCount
            // 
            this.numericFrameCount.Location = new System.Drawing.Point(97, 82);
            this.numericFrameCount.Maximum = new decimal(new int[] {
            600,
            0,
            0,
            0});
            this.numericFrameCount.Name = "numericFrameCount";
            this.numericFrameCount.Size = new System.Drawing.Size(47, 20);
            this.numericFrameCount.TabIndex = 6;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(5, 84);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(86, 13);
            this.label5.TabIndex = 5;
            this.label5.Text = "Read data every";
            // 
            // comboDockSide
            // 
            this.comboDockSide.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboDockSide.FormattingEnabled = true;
            this.comboDockSide.Location = new System.Drawing.Point(78, 55);
            this.comboDockSide.Name = "comboDockSide";
            this.comboDockSide.Size = new System.Drawing.Size(121, 21);
            this.comboDockSide.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(5, 58);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(60, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Dock Side:";
            // 
            // cbDock
            // 
            this.cbDock.AutoSize = true;
            this.cbDock.Location = new System.Drawing.Point(8, 6);
            this.cbDock.Name = "cbDock";
            this.cbDock.Size = new System.Drawing.Size(109, 17);
            this.cbDock.TabIndex = 2;
            this.cbDock.Text = "Dock to BizHawk";
            this.cbDock.UseVisualStyleBackColor = true;
            // 
            // numericDockOffset
            // 
            this.numericDockOffset.Location = new System.Drawing.Point(79, 29);
            this.numericDockOffset.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.numericDockOffset.Name = "numericDockOffset";
            this.numericDockOffset.Size = new System.Drawing.Size(120, 20);
            this.numericDockOffset.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(5, 31);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Dock Offset:";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.button1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 139);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(334, 38);
            this.panel1.TabIndex = 1;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button1.Location = new System.Drawing.Point(252, 8);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Apply";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
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
            // SettingsDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(334, 177);
            this.Controls.Add(this.tabs);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Name = "SettingsDialog";
            this.Text = "Display Settings";
            this.tabs.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.tabPage5.ResumeLayout(false);
            this.tabPage5.PerformLayout();
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericFrameCount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericDockOffset)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabs;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.ComboBox comboKeyItemsStyle;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox cbKeyItemBorder;
        private System.Windows.Forms.CheckBox cbKeyItemDisplay;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.ComboBox comboPartyPose;
        private System.Windows.Forms.CheckBox cbPartyBorder;
        private System.Windows.Forms.CheckBox cbPartyDisplay;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.CheckBox cbObjectiveBorder;
        private System.Windows.Forms.CheckBox cbObjectiveDisplay;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.NumericUpDown numericDockOffset;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboDockSide;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox cbDock;
        private System.Windows.Forms.TabPage tabPage5;
        private System.Windows.Forms.CheckBox cbLocationsBorder;
        private System.Windows.Forms.CheckBox cbLocationsDisplay;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown numericFrameCount;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox cbPartyAnimate;
    }
}