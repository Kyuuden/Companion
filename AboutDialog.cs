using System.Reflection;
using System.Windows.Forms;

namespace FF.Rando.Companion;

public partial class AboutDialog : Form
{
    public AboutDialog()
    {
        InitializeComponent();
        Text += $" {Assembly.GetAssembly(this.GetType()).GetName().Version}";
    }

    private void richTextBox1_LinkClicked(object sender, LinkClickedEventArgs e)
    {
        System.Diagnostics.Process.Start(e.LinkText);
    }
}
