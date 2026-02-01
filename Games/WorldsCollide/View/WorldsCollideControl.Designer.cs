using System.Drawing;
using FF.Rando.Companion.Games.WorldsCollide.View;

namespace FF.Rando.Companion.Games.WorldsCollide.View;

partial class WorldsCollideControl
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

        if (disposing)
        {
            _characters.Dispose();
            _statistics.Dispose();
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
            this._characters = new FF.Rando.Companion.Games.WorldsCollide.View.CharactersPanel();
            this._checks = new FF.Rando.Companion.Games.WorldsCollide.View.ChecksPanel();
            this._statistics = new FF.Rando.Companion.Games.WorldsCollide.View.StatsPanel();
            this.SuspendLayout();
            // 
            // _characters
            // 
            this._characters.AutoResize = true;
            this._characters.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this._characters.BackColor = Color.FromArgb(0, 0, 99);
            this._characters.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this._characters.Dock = System.Windows.Forms.DockStyle.Top;
            this._characters.FlowDirection = System.Windows.Forms.FlowDirection.LeftToRight;
            this._characters.Icons = false;
            this._characters.Location = new System.Drawing.Point(0, 573);
            this._characters.Margin = new System.Windows.Forms.Padding(0);
            this._characters.Name = "_characters";
            this._characters.Size = new System.Drawing.Size(410, 40);
            this._characters.TabIndex = 5;
            this._characters.WrapContents = true;
            // 
            // _checks
            // 
            this._checks.AutoResize = true;
            this._checks.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this._checks.BackColor = Color.FromArgb(0, 0, 99);
            this._checks.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this._checks.Dock = System.Windows.Forms.DockStyle.Top;
            this._checks.FlowDirection = System.Windows.Forms.FlowDirection.LeftToRight;
            this._checks.Icons = false;
            this._checks.Location = new System.Drawing.Point(0, 573);
            this._checks.Margin = new System.Windows.Forms.Padding(0);
            this._checks.Name = "_checks";
            this._checks.Size = new System.Drawing.Size(410, 40);
            this._checks.TabIndex = 5;
            this._checks.WrapContents = true;
            // 
            // _statistics
            // 
            this._statistics.AutoResize = true;
            this._statistics.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this._statistics.BackColor = Color.FromArgb(0, 0, 99);
            this._statistics.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this._statistics.Dock = System.Windows.Forms.DockStyle.Bottom;
            this._statistics.FlowDirection = System.Windows.Forms.FlowDirection.LeftToRight;
            this._statistics.Icons = false;
            this._statistics.Location = new System.Drawing.Point(0, 0);
            this._statistics.Name = "_statistics";
            this._statistics.Size = new System.Drawing.Size(346, 160);
            this._statistics.TabIndex = 2;
            this._statistics.WrapContents = false;
            // 
            // MysticQuestRandomizerControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = Color.FromArgb(0, 0, 99);
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Name = "WorldsCollideControl";
            this.Size = new System.Drawing.Size(410, 613);
            this.Padding = new System.Windows.Forms.Padding(0,4,0,0);
            this.ResumeLayout(false);
    }

    #endregion

    private StatsPanel _statistics;
    private ChecksPanel _checks;
    private CharactersPanel _characters;
}
