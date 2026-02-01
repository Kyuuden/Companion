using System.Drawing;
using System.Windows.Forms;

namespace FF.Rando.Companion.View;

partial class FlowPanelEx<TGame, TSettings>
{
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
            foreach (Control control in Controls)
            {
                control.VisibleChanged -= Control_VisibleChanged;
            }

            if (Settings != null) Settings.PropertyChanged -= Settings_PropertyChanged;
            if (Game != null)
            {
                Game.PropertyChanged -= Settings_PropertyChanged;
                Game.Settings.BorderSettings.PropertyChanged -= Settings_PropertyChanged;
            }

            components?.Dispose();
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
            this.SuspendLayout();
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = Color.FromArgb(0, 0, 99);
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Padding = new System.Windows.Forms.Padding(0);
            this.Name = "FlowPanelControl";
            this.Size = new System.Drawing.Size(300, 300);
            this.ResumeLayout(false);
    }

    #endregion
}
