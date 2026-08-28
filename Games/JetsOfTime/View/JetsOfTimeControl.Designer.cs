using System.Drawing;

namespace FF.Rando.Companion.Games.JetsOfTime.View;

partial class JetsOfTimeControl
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
            SuspendLayout();
            _keyItems.Dispose();
            _characters.Dispose();
            _bosses.Dispose();
            //_checks.Dispose();
            TopPanel.Dispose();
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
        //this._checks = new FF.Rando.Companion.Games.JetsOfTime.View.ChecksPanel();
        this._statistics = new FF.Rando.Companion.Games.JetsOfTime.View.StatisticsPanel();
        this._maps = new FF.Rando.Companion.Games.JetsOfTime.View.MapsPanel();
        this._bosses = new FF.Rando.Companion.Games.JetsOfTime.View.BossesPanel();
        this._keyItems = new FF.Rando.Companion.Games.JetsOfTime.View.KeyItemsPanel();
        this._characters = new FF.Rando.Companion.Games.JetsOfTime.View.CharactersPanel();
        TopPanel.SuspendLayout();
        this.SuspendLayout();
        // 
        // TopPanel
        // 
        this.TopPanel.Dock = System.Windows.Forms.DockStyle.Top;
        this.TopPanel.Location = new System.Drawing.Point(0, 0);
        this.TopPanel.Name = "TopPanel";
        this.TopPanel.Size = new System.Drawing.Size(410, 320);
        this.TopPanel.TabIndex = 4;
        // 
        // _checks
        // 
        //this._checks.BackColor = Color.FromArgb(0, 0, 99);
        //this._checks.Dock = System.Windows.Forms.DockStyle.Fill;
        //this._checks.Location = new System.Drawing.Point(0, 320);
        //this._checks.Margin = new System.Windows.Forms.Padding(0);
        //this._checks.Name = "_checks";
        //this._checks.Size = new System.Drawing.Size(410, 253);
        //this._checks.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
        //this._checks.TabIndex = 5;
        //this._checks.TabStop = false;
        //this._checks.CanScrollChanged += CanScrollChanged;
        // 
        // _maps
        // 
        this._maps.BackColor = Color.FromArgb(0, 0, 99);
        this._maps.Dock = System.Windows.Forms.DockStyle.Fill;
        this._maps.Location = new System.Drawing.Point(0, 320);
        this._maps.Margin = new System.Windows.Forms.Padding(0);
        this._maps.Name = "_maps";
        this._maps.Size = new System.Drawing.Size(410, 253);
        this._maps.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
        this._maps.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
        this._maps.TabIndex = 5;
        this._maps.TabStop = false;
        this._maps.CanScrollChanged += CanScrollChanged;
        // 
        // _statistics
        // 
        //this._statistics.AutoResize = true;
        this._statistics.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
        this._statistics.BackColor = Color.FromArgb(0, 0, 99);
        this._statistics.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
        this._statistics.Dock = System.Windows.Forms.DockStyle.Top;
        this._statistics.FlowDirection = System.Windows.Forms.FlowDirection.LeftToRight;
        this._statistics.Icons = false;
        this._statistics.Location = new System.Drawing.Point(0, 0);
        this._statistics.Name = "_statistics";
        this._statistics.Size = new System.Drawing.Size(346, 160);
        this._statistics.TabIndex = 2;
        this._statistics.WrapContents = false;
        // 
        // _bosses
        // 
        this._bosses.AutoResize = true;
        this._bosses.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
        this._bosses.BackColor = Color.FromArgb(0, 0, 99);
        this._bosses.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
        this._bosses.Dock = System.Windows.Forms.DockStyle.Top;
        this._bosses.FlowDirection = System.Windows.Forms.FlowDirection.LeftToRight;
        this._bosses.Icons = false;
        this._bosses.Location = new System.Drawing.Point(0, 0);
        this._bosses.Name = "_bosses";
        this._bosses.Size = new System.Drawing.Size(346, 160);
        this._bosses.TabIndex = 2;
        this._bosses.WrapContents = true;
        // 
        // _keyItems
        // 
        this._keyItems.AutoResize = true;
        this._keyItems.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
        this._keyItems.BackColor = Color.FromArgb(0, 0, 99);
        this._keyItems.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
        this._keyItems.Dock = System.Windows.Forms.DockStyle.Top;
        this._keyItems.FlowDirection = System.Windows.Forms.FlowDirection.LeftToRight;
        this._keyItems.Icons = false;
        this._keyItems.Location = new System.Drawing.Point(0, 0);
        this._keyItems.Name = "_keyItems";
        this._keyItems.Size = new System.Drawing.Size(346, 160);
        this._keyItems.TabIndex = 2;
        this._keyItems.WrapContents = true;
        // 
        // _characters
        // 
        this._characters.AutoResize = true;
        this._characters.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
        this._characters.BackColor = Color.FromArgb(0, 0, 99);
        this._characters.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
        this._characters.Dock = System.Windows.Forms.DockStyle.Left;
        this._characters.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
        this._characters.Icons = false;
        this._characters.Location = new System.Drawing.Point(0, 0);
        this._characters.Name = "_characters";
        this._characters.Size = new System.Drawing.Size(346, 160);
        this._characters.TabIndex = 2;
        this._characters.WrapContents = false;
        // 
        // JetsOfTimeControl
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.BackColor = Color.FromArgb(0, 0, 99);
        this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
        this.Name = "JetsOfTimeControl";
        this.Size = new System.Drawing.Size(410, 613);
        this.TopPanel.ResumeLayout(false);
        this.ResumeLayout(false);
    }

    #endregion

    private KeyItemsPanel _keyItems;
    private CharactersPanel _characters;
    private BossesPanel _bosses;
    private MapsPanel _maps;
    private StatisticsPanel _statistics;
    //private ChecksPanel _checks;
    private System.Windows.Forms.Panel TopPanel;
}
