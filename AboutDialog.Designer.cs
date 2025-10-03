
namespace FF.Rando.Companion
{
    partial class AboutDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AboutDialog));
            this.button1 = new System.Windows.Forms.Button();
            this.versionLabel = new System.Windows.Forms.Label();
            this.descriptionLabel = new System.Windows.Forms.Label();
            this.ff4Link = new System.Windows.Forms.LinkLabel();
            this.ff4SourceLink = new System.Windows.Forms.LinkLabel();
            this.sotnToolsLink = new System.Windows.Forms.LinkLabel();
            this.updaterLink = new System.Windows.Forms.LinkLabel();
            this.schalaLink = new System.Windows.Forms.LinkLabel();
            this.kgySoftLink = new System.Windows.Forms.LinkLabel();
            this.updateButton = new System.Windows.Forms.Button();
            this.ff6Link = new System.Windows.Forms.LinkLabel();
            this.ffmqLink = new System.Windows.Forms.LinkLabel();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button1.Location = new System.Drawing.Point(297, 313);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "OK";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // versionLabel
            // 
            this.versionLabel.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.versionLabel.Location = new System.Drawing.Point(12, 9);
            this.versionLabel.Margin = new System.Windows.Forms.Padding(3, 0, 3, 6);
            this.versionLabel.Name = "versionLabel";
            this.versionLabel.Size = new System.Drawing.Size(360, 19);
            this.versionLabel.TabIndex = 10;
            this.versionLabel.Text = "Version";
            this.versionLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // descriptionLabel
            // 
            this.descriptionLabel.AutoSize = true;
            this.descriptionLabel.Location = new System.Drawing.Point(12, 66);
            this.descriptionLabel.MaximumSize = new System.Drawing.Size(370, 0);
            this.descriptionLabel.Name = "descriptionLabel";
            this.descriptionLabel.Size = new System.Drawing.Size(268, 104);
            this.descriptionLabel.TabIndex = 11;
            this.descriptionLabel.Text = resources.GetString("descriptionLabel.Text");
            // 
            // ff4Link
            // 
            this.ff4Link.AutoSize = true;
            this.ff4Link.Location = new System.Drawing.Point(29, 175);
            this.ff4Link.Margin = new System.Windows.Forms.Padding(20, 5, 5, 5);
            this.ff4Link.Name = "ff4Link";
            this.ff4Link.Size = new System.Drawing.Size(102, 13);
            this.ff4Link.TabIndex = 12;
            this.ff4Link.TabStop = true;
            this.ff4Link.Text = "FF4: Free Enterprise";
            this.ff4Link.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.FF4Link_LinkClicked);
            // 
            // ff4SourceLink
            // 
            this.ff4SourceLink.AutoSize = true;
            this.ff4SourceLink.Location = new System.Drawing.Point(136, 175);
            this.ff4SourceLink.Margin = new System.Windows.Forms.Padding(0, 5, 5, 5);
            this.ff4SourceLink.Name = "ff4SourceLink";
            this.ff4SourceLink.Size = new System.Drawing.Size(45, 13);
            this.ff4SourceLink.TabIndex = 14;
            this.ff4SourceLink.TabStop = true;
            this.ff4SourceLink.Text = "(source)";
            this.ff4SourceLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.FF4SourceLink_LinkClicked);
            // 
            // sotnToolsLink
            // 
            this.sotnToolsLink.AutoSize = true;
            this.sotnToolsLink.Location = new System.Drawing.Point(29, 313);
            this.sotnToolsLink.Margin = new System.Windows.Forms.Padding(20, 5, 5, 5);
            this.sotnToolsLink.Name = "sotnToolsLink";
            this.sotnToolsLink.Size = new System.Drawing.Size(87, 13);
            this.sotnToolsLink.TabIndex = 15;
            this.sotnToolsLink.TabStop = true;
            this.sotnToolsLink.Text = "SotnRandoTools";
            this.sotnToolsLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.SotnToolsLink_LinkClicked);
            // 
            // updaterLink
            // 
            this.updaterLink.AutoSize = true;
            this.updaterLink.Location = new System.Drawing.Point(29, 290);
            this.updaterLink.Margin = new System.Windows.Forms.Padding(20, 5, 5, 5);
            this.updaterLink.Name = "updaterLink";
            this.updaterLink.Size = new System.Drawing.Size(144, 13);
            this.updaterLink.TabIndex = 16;
            this.updaterLink.TabStop = true;
            this.updaterLink.Text = "SimpleLatestReleaseUpdater";
            this.updaterLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.UpdaterLink_LinkClicked);
            // 
            // schalaLink
            // 
            this.schalaLink.AutoSize = true;
            this.schalaLink.Location = new System.Drawing.Point(29, 244);
            this.schalaLink.Margin = new System.Windows.Forms.Padding(20, 5, 5, 5);
            this.schalaLink.Name = "schalaLink";
            this.schalaLink.Size = new System.Drawing.Size(60, 13);
            this.schalaLink.TabIndex = 17;
            this.schalaLink.TabStop = true;
            this.schalaLink.Text = "SchalaKitty";
            this.schalaLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.SchalaLink_LinkClicked);
            // 
            // kgySoftLink
            // 
            this.kgySoftLink.AutoSize = true;
            this.kgySoftLink.Location = new System.Drawing.Point(29, 267);
            this.kgySoftLink.Margin = new System.Windows.Forms.Padding(20, 5, 5, 5);
            this.kgySoftLink.Name = "kgySoftLink";
            this.kgySoftLink.Size = new System.Drawing.Size(88, 13);
            this.kgySoftLink.TabIndex = 18;
            this.kgySoftLink.TabStop = true;
            this.kgySoftLink.Text = "KGySoft.Drawing";
            this.kgySoftLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.KGySoftLink_LinkClicked);
            // 
            // updateButton
            // 
            this.updateButton.DialogResult = System.Windows.Forms.DialogResult.Yes;
            this.updateButton.Location = new System.Drawing.Point(103, 37);
            this.updateButton.Margin = new System.Windows.Forms.Padding(3, 3, 3, 6);
            this.updateButton.Name = "updateButton";
            this.updateButton.Size = new System.Drawing.Size(179, 23);
            this.updateButton.TabIndex = 19;
            this.updateButton.Text = "Update To Latest";
            this.updateButton.UseVisualStyleBackColor = true;
            this.updateButton.Visible = false;
            // 
            // ff6Link
            // 
            this.ff6Link.AutoSize = true;
            this.ff6Link.Location = new System.Drawing.Point(29, 198);
            this.ff6Link.Margin = new System.Windows.Forms.Padding(20, 5, 5, 5);
            this.ff6Link.Name = "ff6Link";
            this.ff6Link.Size = new System.Drawing.Size(102, 13);
            this.ff6Link.TabIndex = 20;
            this.ff6Link.TabStop = true;
            this.ff6Link.Text = "FFVI: Worlds Collide";
            this.ff6Link.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.FF6Link_LinkClicked);
            // 
            // ffmqLink
            // 
            this.ffmqLink.AutoSize = true;
            this.ffmqLink.Location = new System.Drawing.Point(29, 221);
            this.ffmqLink.Margin = new System.Windows.Forms.Padding(20, 5, 5, 5);
            this.ffmqLink.Name = "ffmqLink";
            this.ffmqLink.Size = new System.Drawing.Size(127, 13);
            this.ffmqLink.TabIndex = 21;
            this.ffmqLink.TabStop = true;
            this.ffmqLink.Text = "Mystic Quest Randomizer";
            this.ffmqLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.FFmqLink_LinkClicked);
            // 
            // AboutDialog
            // 
            this.AcceptButton = this.button1;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 348);
            this.ControlBox = false;
            this.Controls.Add(this.ffmqLink);
            this.Controls.Add(this.ff6Link);
            this.Controls.Add(this.updateButton);
            this.Controls.Add(this.kgySoftLink);
            this.Controls.Add(this.schalaLink);
            this.Controls.Add(this.updaterLink);
            this.Controls.Add(this.sotnToolsLink);
            this.Controls.Add(this.ff4SourceLink);
            this.Controls.Add(this.ff4Link);
            this.Controls.Add(this.descriptionLabel);
            this.Controls.Add(this.versionLabel);
            this.Controls.Add(this.button1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "AboutDialog";
            this.Text = "About";
            this.Load += new System.EventHandler(this.AboutDialog_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label versionLabel;
        private System.Windows.Forms.Label descriptionLabel;
        private System.Windows.Forms.LinkLabel ff4Link;
        private System.Windows.Forms.LinkLabel ff4SourceLink;
        private System.Windows.Forms.LinkLabel sotnToolsLink;
        private System.Windows.Forms.LinkLabel updaterLink;
        private System.Windows.Forms.LinkLabel schalaLink;
        private System.Windows.Forms.LinkLabel kgySoftLink;
        private System.Windows.Forms.Button updateButton;
        private System.Windows.Forms.LinkLabel ff6Link;
        private System.Windows.Forms.LinkLabel ffmqLink;
    }
}