
namespace BizHawk.FreeEnterprise.Companion
{
    partial class CompanionForm
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.displayToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.StopwatchPanel = new System.Windows.Forms.Panel();
            this.StopWatchLabel = new System.Windows.Forms.Label();
            this.WideLayoutPanel = new System.Windows.Forms.Panel();
            this.LocationsControl = new BizHawk.FreeEnterprise.Companion.Controls.Locations();
            this.BossesControl = new BizHawk.FreeEnterprise.Companion.Controls.Bosses();
            this.ObjectivesControl = new BizHawk.FreeEnterprise.Companion.Controls.Objectives();
            this.PartyControl = new BizHawk.FreeEnterprise.Companion.Controls.Party();
            this.KeyItemsControl = new BizHawk.FreeEnterprise.Companion.Controls.KeyItems();
            this.menuStrip1.SuspendLayout();
            this.StopwatchPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.settingsToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.displayToolStripMenuItem});
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.settingsToolStripMenuItem.Text = "Settings";
            // 
            // displayToolStripMenuItem
            // 
            this.displayToolStripMenuItem.Name = "displayToolStripMenuItem";
            this.displayToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.displayToolStripMenuItem.Text = "Display";
            this.displayToolStripMenuItem.Click += new System.EventHandler(this.displayToolStripMenuItem_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(52, 20);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // StopwatchPanel
            // 
            this.StopwatchPanel.Controls.Add(this.StopWatchLabel);
            this.StopwatchPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.StopwatchPanel.Location = new System.Drawing.Point(0, 392);
            this.StopwatchPanel.Name = "StopwatchPanel";
            this.StopwatchPanel.Size = new System.Drawing.Size(800, 58);
            this.StopwatchPanel.TabIndex = 7;
            // 
            // StopWatchLabel
            // 
            this.StopWatchLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.StopWatchLabel.BackColor = System.Drawing.Color.Transparent;
            this.StopWatchLabel.Font = new System.Drawing.Font("Lucida Console", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StopWatchLabel.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.StopWatchLabel.Location = new System.Drawing.Point(11, 12);
            this.StopWatchLabel.Margin = new System.Windows.Forms.Padding(0);
            this.StopWatchLabel.Name = "StopWatchLabel";
            this.StopWatchLabel.Size = new System.Drawing.Size(778, 37);
            this.StopWatchLabel.TabIndex = 0;
            this.StopWatchLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.StopWatchLabel.Click += new System.EventHandler(this.StockWatchLabel_Click);
            this.StopWatchLabel.DoubleClick += new System.EventHandler(this.StopWatchLabel_DoubleClick);
            // 
            // WideLayoutPanel
            // 
            this.WideLayoutPanel.BackColor = System.Drawing.Color.Transparent;
            this.WideLayoutPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.WideLayoutPanel.Location = new System.Drawing.Point(0, 24);
            this.WideLayoutPanel.Name = "WideLayoutPanel";
            this.WideLayoutPanel.Size = new System.Drawing.Size(800, 0);
            this.WideLayoutPanel.TabIndex = 8;
            this.WideLayoutPanel.Visible = false;
            // 
            // LocationsControl
            // 
            this.LocationsControl.BackColor = System.Drawing.Color.Transparent;
            this.LocationsControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LocationsControl.Location = new System.Drawing.Point(0, 232);
            this.LocationsControl.Margin = new System.Windows.Forms.Padding(0);
            this.LocationsControl.Name = "LocationsControl";
            this.LocationsControl.Size = new System.Drawing.Size(800, 218);
            this.LocationsControl.TabIndex = 6;
            // 
            // BossesControl
            // 
            this.BossesControl.Dock = System.Windows.Forms.DockStyle.Top;
            this.BossesControl.Location = new System.Drawing.Point(0, 180);
            this.BossesControl.Name = "BossesControl";
            this.BossesControl.Size = new System.Drawing.Size(800, 52);
            this.BossesControl.TabIndex = 5;
            // 
            // ObjectivesControl
            // 
            this.ObjectivesControl.BackColor = System.Drawing.Color.Transparent;
            this.ObjectivesControl.Dock = System.Windows.Forms.DockStyle.Top;
            this.ObjectivesControl.Location = new System.Drawing.Point(0, 124);
            this.ObjectivesControl.Margin = new System.Windows.Forms.Padding(0);
            this.ObjectivesControl.Name = "ObjectivesControl";
            this.ObjectivesControl.Size = new System.Drawing.Size(800, 56);
            this.ObjectivesControl.TabIndex = 4;
            this.ObjectivesControl.Resize += new System.EventHandler(this.TrackerControl_Resize);
            // 
            // PartyControl
            // 
            this.PartyControl.BackColor = System.Drawing.Color.Transparent;
            this.PartyControl.Dock = System.Windows.Forms.DockStyle.Top;
            this.PartyControl.Location = new System.Drawing.Point(0, 74);
            this.PartyControl.Margin = new System.Windows.Forms.Padding(0);
            this.PartyControl.Name = "PartyControl";
            this.PartyControl.Size = new System.Drawing.Size(800, 50);
            this.PartyControl.TabIndex = 3;
            this.PartyControl.Resize += new System.EventHandler(this.TrackerControl_Resize);
            // 
            // KeyItemsControl
            // 
            this.KeyItemsControl.BackColor = System.Drawing.Color.Transparent;
            this.KeyItemsControl.Dock = System.Windows.Forms.DockStyle.Top;
            this.KeyItemsControl.Location = new System.Drawing.Point(0, 24);
            this.KeyItemsControl.Margin = new System.Windows.Forms.Padding(0);
            this.KeyItemsControl.Name = "KeyItemsControl";
            this.KeyItemsControl.Padding = new System.Windows.Forms.Padding(8);
            this.KeyItemsControl.Size = new System.Drawing.Size(800, 50);
            this.KeyItemsControl.TabIndex = 2;
            this.KeyItemsControl.Resize += new System.EventHandler(this.TrackerControl_Resize);
            // 
            // CompanionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.StopwatchPanel);
            this.Controls.Add(this.LocationsControl);
            this.Controls.Add(this.BossesControl);
            this.Controls.Add(this.ObjectivesControl);
            this.Controls.Add(this.PartyControl);
            this.Controls.Add(this.KeyItemsControl);
            this.Controls.Add(this.WideLayoutPanel);
            this.Controls.Add(this.menuStrip1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "CompanionForm";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.StopwatchPanel.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem displayToolStripMenuItem;
        private Controls.KeyItems KeyItemsControl;
        private Controls.Party PartyControl;
        private Controls.Objectives ObjectivesControl;
        private Controls.Bosses BossesControl;
        private Controls.Locations LocationsControl;
        private System.Windows.Forms.Panel StopwatchPanel;
        private System.Windows.Forms.Label StopWatchLabel;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.Panel WideLayoutPanel;

    }
}