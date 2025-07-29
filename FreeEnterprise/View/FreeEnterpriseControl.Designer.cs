using System.Drawing;

namespace FF.Rando.Companion.FreeEnterprise.View;

partial class FreeEnterpriseControl
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

    #region Component Designer generated code

    /// <summary> 
    /// Required method for Designer support - do not modify 
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
            this.TopPanel = new System.Windows.Forms.Panel();
            this.objectivesControl = new FF.Rando.Companion.FreeEnterprise.View.ObjectivesControl();
            this.statsControl1 = new FF.Rando.Companion.FreeEnterprise.View.StatsControl();
            this.bossesControl1 = new FF.Rando.Companion.FreeEnterprise.View.BossesControl();
            this.keyItemsControl1 = new FF.Rando.Companion.FreeEnterprise.View.KeyItemsControl();
            this.partyControl1 = new FF.Rando.Companion.FreeEnterprise.View.PartyControl();
            this.TopPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.objectivesControl)).BeginInit();
            this.SuspendLayout();
            // 
            // TopPanel
            // 
            this.TopPanel.Controls.Add(this.bossesControl1);
            this.TopPanel.Controls.Add(this.keyItemsControl1);
            this.TopPanel.Controls.Add(this.partyControl1);
            this.TopPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.TopPanel.Location = new System.Drawing.Point(0, 0);
            this.TopPanel.Name = "TopPanel";
            this.TopPanel.Size = new System.Drawing.Size(410, 320);
            this.TopPanel.TabIndex = 4;
            // 
            // objectivesControl
            // 
            this.objectivesControl.BackColor = Color.FromArgb(0, 0, 99);
            this.objectivesControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.objectivesControl.Location = new System.Drawing.Point(0, 320);
            this.objectivesControl.Margin = new System.Windows.Forms.Padding(0);
            this.objectivesControl.Name = "objectivesControl";
            this.objectivesControl.Size = new System.Drawing.Size(410, 253);
            this.objectivesControl.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.objectivesControl.TabIndex = 5;
            this.objectivesControl.TabStop = false;
            // 
            // statsControl1
            // 
            this.statsControl1.AutoResize = true;
            this.statsControl1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.statsControl1.BackColor = Color.FromArgb(0, 0, 99);
            this.statsControl1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.statsControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.statsControl1.FlowDirection = System.Windows.Forms.FlowDirection.LeftToRight;
            this.statsControl1.Icons = false;
            this.statsControl1.Location = new System.Drawing.Point(0, 573);
            this.statsControl1.Margin = new System.Windows.Forms.Padding(0);
            this.statsControl1.Name = "statsControl1";
            this.statsControl1.Size = new System.Drawing.Size(410, 40);
            this.statsControl1.TabIndex = 6;
            this.statsControl1.WrapContents = false;
            // 
            // bossesControl1
            // 
            this.bossesControl1.AutoResize = true;
            this.bossesControl1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.bossesControl1.BackColor = Color.FromArgb(0, 0, 99);
            this.bossesControl1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.bossesControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.bossesControl1.FlowDirection = System.Windows.Forms.FlowDirection.LeftToRight;
            this.bossesControl1.Icons = false;
            this.bossesControl1.Location = new System.Drawing.Point(64, 160);
            this.bossesControl1.Name = "bossesControl1";
            this.bossesControl1.Size = new System.Drawing.Size(346, 160);
            this.bossesControl1.TabIndex = 3;
            this.bossesControl1.WrapContents = true;
            this.bossesControl1.Resize += new System.EventHandler(this.TrackerResized);
            // 
            // keyItemsControl1
            // 
            this.keyItemsControl1.AutoResize = true;
            this.keyItemsControl1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.keyItemsControl1.BackColor = Color.FromArgb(0, 0, 99);
            this.keyItemsControl1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.keyItemsControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.keyItemsControl1.FlowDirection = System.Windows.Forms.FlowDirection.LeftToRight;
            this.keyItemsControl1.Icons = false;
            this.keyItemsControl1.Location = new System.Drawing.Point(64, 0);
            this.keyItemsControl1.Name = "keyItemsControl1";
            this.keyItemsControl1.Size = new System.Drawing.Size(346, 160);
            this.keyItemsControl1.TabIndex = 1;
            this.keyItemsControl1.WrapContents = true;
            this.keyItemsControl1.Resize += new System.EventHandler(this.TrackerResized);
            // 
            // partyControl1
            // 
            this.partyControl1.AutoResize = true;
            this.partyControl1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.partyControl1.BackColor = Color.FromArgb(0, 0, 99);
            this.partyControl1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.partyControl1.Dock = System.Windows.Forms.DockStyle.Left;
            this.partyControl1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.partyControl1.Icons = false;
            this.partyControl1.Location = new System.Drawing.Point(0, 0);
            this.partyControl1.MinimumSize = new System.Drawing.Size(64, 264);
            this.partyControl1.Name = "partyControl1";
            this.partyControl1.Size = new System.Drawing.Size(64, 320);
            this.partyControl1.TabIndex = 2;
            this.partyControl1.WrapContents = false;
            this.partyControl1.Resize += new System.EventHandler(this.TrackerResized);
            // 
            // FreeEnterpriseControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = Color.FromArgb(0, 0, 99);
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Controls.Add(this.objectivesControl);
            this.Controls.Add(this.statsControl1);
            this.Controls.Add(this.TopPanel);
            this.Name = "FreeEnterpriseControl";
            this.Size = new System.Drawing.Size(410, 613);
            this.TopPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.objectivesControl)).EndInit();
            this.ResumeLayout(false);

    }

    #endregion

    private PartyControl partyControl1;
    private KeyItemsControl keyItemsControl1;
    private BossesControl bossesControl1;
    private System.Windows.Forms.Panel TopPanel;
    private ObjectivesControl objectivesControl;
    private StatsControl statsControl1;
}
