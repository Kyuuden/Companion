using System.Windows.Forms;

namespace BizHawk.FreeEnterprise.Companion
{
    public partial class AboutDialog : Form
    {
        public AboutDialog()
        {
            InitializeComponent();
        }

        private void linkFE_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) => System.Diagnostics.Process.Start("http://ff4fe.com/");

        private void linkKyuuden_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) => System.Diagnostics.Process.Start("https://www.twitch.tv/kyuuden");

        private void linkScala_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) => System.Diagnostics.Process.Start("https://www.twitch.tv/schalakitty");
    }
}
