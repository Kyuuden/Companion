using System.Drawing;

namespace FF.Rando.Companion.MysticQuestRandomizer.View;

partial class MysticQuestRandomizerControl
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
            this._equipment = new FF.Rando.Companion.MysticQuestRandomizer.View.EquipmentPanel();
            this._elements = new FF.Rando.Companion.MysticQuestRandomizer.View.ElementsPanel();
            this._companions = new FF.Rando.Companion.MysticQuestRandomizer.View.CompanionsPanel();
            this.SuspendLayout();
            // 
            // _companions
            // 
            this._companions.BackColor = Color.FromArgb(0, 0, 99);
            this._companions.Dock = System.Windows.Forms.DockStyle.Fill;
            this._companions.Location = new System.Drawing.Point(0, 320);
            this._companions.Margin = new System.Windows.Forms.Padding(0);
            this._companions.Name = "_companions";
            this._companions.Size = new System.Drawing.Size(410, 253);
            this._companions.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this._companions.TabIndex = 6;
            this._companions.TabStop = false;
            this._companions.CanScrollChanged += CanScrollChanged;
            // 
            // _elements
            // 
            this._elements.AutoResize = true;
            this._elements.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this._elements.BackColor = Color.FromArgb(0, 0, 99);
            this._elements.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this._elements.Dock = System.Windows.Forms.DockStyle.Top;
            this._elements.FlowDirection = System.Windows.Forms.FlowDirection.LeftToRight;
            this._elements.Icons = false;
            this._elements.Location = new System.Drawing.Point(0, 573);
            this._elements.Margin = new System.Windows.Forms.Padding(0);
            this._elements.Name = "_elements";
            this._elements.Size = new System.Drawing.Size(410, 40);
            this._elements.TabIndex = 5;
            this._elements.WrapContents = true;
            // 
            // _equipment
            // 
            this._equipment.AutoResize = true;
            this._equipment.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this._equipment.BackColor = Color.FromArgb(0, 0, 99);
            this._equipment.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this._equipment.Dock = System.Windows.Forms.DockStyle.Left;
            this._equipment.FlowDirection = System.Windows.Forms.FlowDirection.LeftToRight;
            this._equipment.Icons = false;
            this._equipment.Location = new System.Drawing.Point(0, 0);
            this._equipment.Name = "_equipment";
            this._equipment.Size = new System.Drawing.Size(346, 160);
            this._equipment.TabIndex = 2;
            this._equipment.WrapContents = true;
            // 
            // MysticQuestRandomizerControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = Color.FromArgb(0, 0, 99);
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Name = "MysticQuestRandomizerControl";
            this.Size = new System.Drawing.Size(410, 613);
            this.ResumeLayout(false);
    }

    #endregion

    private EquipmentPanel _equipment;
    private ElementsPanel _elements;
    private CompanionsPanel _companions;
}
