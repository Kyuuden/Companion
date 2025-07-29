using FF.Rando.Companion.Settings;
using System.Linq;
using System.Windows.Forms;

namespace FF.Rando.Companion;

partial class MainForm
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
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.StopWatchLabel = new System.Windows.Forms.Label();
            this.TrackerPanel = new System.Windows.Forms.Panel();
            this.menuStrip.SuspendLayout();
            this.StopWatchLabel.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.settingsToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(800, 24);
            this.menuStrip.TabIndex = 0;
            this.menuStrip.Text = "menuStrip";
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.settingsToolStripMenuItem.Text = "Settings";
            this.settingsToolStripMenuItem.Click += DisplayToolStripMenuItem_Click;
        
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(52, 20);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += AboutToolStripMenuItem_Click;
            // 
            // StopWatchLabel
            // 
            this.StopWatchLabel.Dock = DockStyle.Bottom;
            this.StopWatchLabel.BackColor = System.Drawing.Color.Black;
            this.StopWatchLabel.Font = new System.Drawing.Font("Lucida Console", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StopWatchLabel.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.StopWatchLabel.Padding = new System.Windows.Forms.Padding(3);
            this.StopWatchLabel.Name = "StopWatchLabel";
            this.StopWatchLabel.Size = new System.Drawing.Size(778, 37);
            this.StopWatchLabel.TabIndex = 0;
            this.StopWatchLabel.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.StopWatchLabel.Visible = false;
            // 
            // TrackerPanel
            // 
            this.TrackerPanel.BackColor = System.Drawing.Color.Gray;
            this.TrackerPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TrackerPanel.Location = new System.Drawing.Point(0, 24);
            this.TrackerPanel.Name = "TrackerPanel";
            this.TrackerPanel.Size = new System.Drawing.Size(800, 368);
            this.TrackerPanel.TabIndex = 8;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.TrackerPanel);
            this.Controls.Add(this.menuStrip);
            this.Controls.Add(this.StopWatchLabel);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.ImeMode = System.Windows.Forms.ImeMode.On;
            this.MainMenuStrip = this.menuStrip;
            this.Name = "MainForm";
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.StopWatchLabel.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

    }

    private void DisplayToolStripMenuItem_Click(object sender, System.EventArgs e)
    {
        var existing = OwnedForms.OfType<SettingsDialog>().FirstOrDefault();

        if (existing is not null)
        {
            existing.Focus();
            return;
        }

        var dialog = new SettingsDialog(_settings, APIs.Input)
        {
            Owner = this,
            StartPosition = FormStartPosition.CenterParent
        };

        dialog.FormClosed += (_, _) => dialog.Dispose();
        dialog.Show();
    }

    private void AboutToolStripMenuItem_Click(object sender, System.EventArgs e)
    {
        using var about = new AboutDialog() { Owner = this, StartPosition = FormStartPosition.CenterParent };
        about.ShowDialog();
    }

    #endregion

    private System.Windows.Forms.MenuStrip menuStrip;
    private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
    private System.Windows.Forms.Label StopWatchLabel;
    private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
    private Panel TrackerPanel;
}