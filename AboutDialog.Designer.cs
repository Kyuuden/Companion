
namespace BizHawk.FreeEnterprise.Companion
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
            this.button1 = new System.Windows.Forms.Button();
            this.linkFE = new System.Windows.Forms.LinkLabel();
            this.linkKyuuden = new System.Windows.Forms.LinkLabel();
            this.linkScala = new System.Windows.Forms.LinkLabel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button1.Location = new System.Drawing.Point(461, 59);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "OK";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // linkFE
            // 
            this.linkFE.AutoSize = true;
            this.linkFE.Location = new System.Drawing.Point(303, 19);
            this.linkFE.Margin = new System.Windows.Forms.Padding(0);
            this.linkFE.Name = "linkFE";
            this.linkFE.Size = new System.Drawing.Size(78, 13);
            this.linkFE.TabIndex = 1;
            this.linkFE.TabStop = true;
            this.linkFE.Text = "Free Enterprise";
            this.linkFE.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkFE_LinkClicked);
            // 
            // linkKyuuden
            // 
            this.linkKyuuden.AutoSize = true;
            this.linkKyuuden.Location = new System.Drawing.Point(220, 38);
            this.linkKyuuden.Name = "linkKyuuden";
            this.linkKyuuden.Size = new System.Drawing.Size(49, 13);
            this.linkKyuuden.TabIndex = 2;
            this.linkKyuuden.TabStop = true;
            this.linkKyuuden.Text = "Kyuuden";
            this.linkKyuuden.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkKyuuden_LinkClicked);
            // 
            // linkScala
            // 
            this.linkScala.AutoSize = true;
            this.linkScala.Location = new System.Drawing.Point(128, 57);
            this.linkScala.Name = "linkScala";
            this.linkScala.Size = new System.Drawing.Size(60, 13);
            this.linkScala.TabIndex = 3;
            this.linkScala.TabStop = true;
            this.linkScala.Text = "SchalaKitty";
            this.linkScala.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkScala_LinkClicked);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(82, 19);
            this.label1.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(225, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Free Enterpise Companion is a auto-tracker for";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(377, 19);
            this.label2.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(154, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = ", a Final Fantasy IV randomizer.";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.Image = global::BizHawk.FreeEnterprise.Companion.Properties.Resources.FFIVFE_Icons_1THECrystal_Color;
            this.pictureBox1.InitialImage = global::BizHawk.FreeEnterprise.Companion.Properties.Resources.FFIVFE_Icons_1THECrystal_Color;
            this.pictureBox1.Location = new System.Drawing.Point(12, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(64, 64);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 6;
            this.pictureBox1.TabStop = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(82, 38);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(142, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Design and development by:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(82, 57);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(50, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Icons by:";
            // 
            // AboutDialog
            // 
            this.AcceptButton = this.button1;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(548, 94);
            this.ControlBox = false;
            this.Controls.Add(this.linkScala);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.linkKyuuden);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.linkFE);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "AboutDialog";
            this.Text = "About Free Enterprise Companion";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.LinkLabel linkFE;
        private System.Windows.Forms.LinkLabel linkKyuuden;
        private System.Windows.Forms.LinkLabel linkScala;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
    }
}