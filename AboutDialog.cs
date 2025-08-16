using FF.Rando.Companion.Utils;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

namespace FF.Rando.Companion;

public partial class AboutDialog : Form
{
    public AboutDialog()
    {
        InitializeComponent();
    }

    private async void AboutDialog_Load(object sender, EventArgs e)
    {
        string currentVersion = typeof(AboutDialog).Assembly.GetName().Version.ToString().Substring(0, 5);
        versionLabel.Text = "Version " + currentVersion;

        if (await WebRequests.NewReleaseAvaiable(currentVersion))
        {
            updateButton.Visible = true;
            versionLabel.Text += " ❌";
            versionLabel.ForeColor = Color.PaleVioletRed;
        }
        else
        {
            updateButton.Visible = false;
            versionLabel.Text += " ✔️";
            versionLabel.ForeColor = Color.SpringGreen;
        }
    }

    private void ff4Link_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
        ff4Link.LinkVisited = true;
        Process.Start(Paths.FreeEnterpriseLink);
    }

    private void ff4SourceLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
        ff4SourceLink.LinkVisited = true;
        Process.Start(Paths.FreeEnterpriseSourceLink);
    }

    private void schalaLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
        schalaLink.LinkVisited = true;
        Process.Start(Paths.SchalaKittyLink);
    }

    private void kgySoftLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
        kgySoftLink.LinkVisited = true;
        Process.Start(Paths.KGySoftLink);
    }

    private void updaterLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
        updaterLink.LinkVisited = true;
        Process.Start(Paths.UpdaterLink);
    }

    private void sotnToolsLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
        sotnToolsLink.LinkVisited = true;
        Process.Start(Paths.SotnRandoToolsLink);
    }

    private void ff6Link_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
        ff6Link.LinkVisited = true;
        Process.Start(Paths.WorldsCollideLink);
    }

    private void ffmqLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
        ffmqLink.LinkVisited = true;
        Process.Start(Paths.MysticQuestRandomizerLink);
    }
}
