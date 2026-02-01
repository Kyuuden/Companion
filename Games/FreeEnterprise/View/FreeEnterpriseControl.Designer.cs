using System.Drawing;
using FF.Rando.Companion.Games.FreeEnterprise.View;

namespace FF.Rando.Companion.Games.FreeEnterprise.View;

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
        if (disposing)
        {
            components?.Dispose();
            _objectives?.Dispose();
            _stats?.Dispose();
            _bosses?.Dispose();
            _locations?.Dispose();
            _keyItems?.Dispose();
            _party?.Dispose();
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
            this._objectives = new FF.Rando.Companion.Games.FreeEnterprise.View.ObjectivesPanel();
            this._stats = new FF.Rando.Companion.Games.FreeEnterprise.View.StatsPanel();
            this._bosses = new FF.Rando.Companion.Games.FreeEnterprise.View.BossesPanel();
            this._locations = new FF.Rando.Companion.Games.FreeEnterprise.View.LocationsPanel();
            this._keyItems = new FF.Rando.Companion.Games.FreeEnterprise.View.KeyItemsPanel();
            this._party = new FF.Rando.Companion.Games.FreeEnterprise.View.PartyPanel();
            this.TopPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._objectives)).BeginInit();
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
            // objectivesControl
            // 
            this._objectives.BackColor = Color.FromArgb(0, 0, 99);
            this._objectives.Dock = System.Windows.Forms.DockStyle.Fill;
            this._objectives.Location = new System.Drawing.Point(0, 320);
            this._objectives.Margin = new System.Windows.Forms.Padding(0);
            this._objectives.Name = "_objectives";
            this._objectives.Size = new System.Drawing.Size(410, 253);
            this._objectives.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this._objectives.TabIndex = 5;
            this._objectives.TabStop = false;
            this._objectives.CanScrollChanged += CanScrollChanged;
            // 
            // _locations
            // 
            this._locations.BackColor = Color.FromArgb(0, 0, 99);
            this._locations.Dock = System.Windows.Forms.DockStyle.Fill;
            this._locations.Location = new System.Drawing.Point(0, 320);
            this._locations.Margin = new System.Windows.Forms.Padding(0);
            this._locations.Name = "_locations";
            this._locations.Size = new System.Drawing.Size(410, 253);
            this._locations.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this._locations.TabIndex = 5;
            this._locations.TabStop = false;
            this._locations.CanScrollChanged += CanScrollChanged;
            // 
            // statsControl1
            // 
            this._stats.AutoResize = true;
            this._stats.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this._stats.BackColor = Color.FromArgb(0, 0, 99);
            this._stats.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this._stats.Dock = System.Windows.Forms.DockStyle.Bottom;
            this._stats.FlowDirection = System.Windows.Forms.FlowDirection.LeftToRight;
            this._stats.Icons = false;
            this._stats.Location = new System.Drawing.Point(0, 573);
            this._stats.Margin = new System.Windows.Forms.Padding(0);
            this._stats.Name = "_stats";
            this._stats.Size = new System.Drawing.Size(410, 40);
            this._stats.TabIndex = 6;
            this._stats.WrapContents = false;
            // 
            // bossesControl1
            // 
            this._bosses.AutoResize = true;
            this._bosses.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this._bosses.BackColor = Color.FromArgb(0, 0, 99);
            this._bosses.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this._bosses.Dock = System.Windows.Forms.DockStyle.Top;
            this._bosses.FlowDirection = System.Windows.Forms.FlowDirection.LeftToRight;
            this._bosses.Icons = false;
            this._bosses.Location = new System.Drawing.Point(64, 160);
            this._bosses.Name = "bossesControl1";
            this._bosses.Size = new System.Drawing.Size(346, 160);
            this._bosses.TabIndex = 3;
            this._bosses.WrapContents = true;
            // 
            // keyItemsControl1
            // 
            this._keyItems.AutoResize = true;
            this._keyItems.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this._keyItems.BackColor = Color.FromArgb(0, 0, 99);
            this._keyItems.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this._keyItems.Dock = System.Windows.Forms.DockStyle.Top;
            this._keyItems.FlowDirection = System.Windows.Forms.FlowDirection.LeftToRight;
            this._keyItems.Icons = false;
            this._keyItems.Location = new System.Drawing.Point(64, 0);
            this._keyItems.Name = "_keyItems";
            this._keyItems.Size = new System.Drawing.Size(346, 160);
            this._keyItems.TabIndex = 1;
            this._keyItems.WrapContents = true;
            // 
            // partyControl1
            // 
            this._party.AutoResize = true;
            this._party.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this._party.BackColor = Color.FromArgb(0, 0, 99);
            this._party.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this._party.Dock = System.Windows.Forms.DockStyle.Left;
            this._party.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this._party.Icons = false;
            this._party.Location = new System.Drawing.Point(0, 0);
            this._party.MinimumSize = new System.Drawing.Size(64, 264);
            this._party.Name = "_party";
            this._party.Size = new System.Drawing.Size(64, 320);
            this._party.TabIndex = 2;
            this._party.WrapContents = false;
            // 
            // FreeEnterpriseControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = Color.FromArgb(0, 0, 99);
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Name = "FreeEnterpriseControl";
            this.Size = new System.Drawing.Size(410, 613);
            this.TopPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._objectives)).EndInit();
            this.ResumeLayout(false);

    }

    #endregion

    private PartyPanel _party;
    private KeyItemsPanel _keyItems;
    private BossesPanel _bosses;
    private LocationsPanel _locations;
    private System.Windows.Forms.Panel TopPanel;
    private ObjectivesPanel _objectives;
    private StatsPanel _stats;
}
