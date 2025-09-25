using FF.Rando.Companion.Utils;
using System;
using System.Diagnostics;
using System.Drawing;
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
        string currentVersion = typeof(AboutDialog).Assembly.GetName().Version.ToString()[..5];
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

    private void FF4Link_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
        ff4Link.LinkVisited = true;
        Process.Start(Paths.FreeEnterpriseLink);
    }

    private void FF4SourceLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
        ff4SourceLink.LinkVisited = true;
        Process.Start(Paths.FreeEnterpriseSourceLink);
    }

    private void SchalaLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
        schalaLink.LinkVisited = true;
        Process.Start(Paths.SchalaKittyLink);
    }

    private void KGySoftLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
        kgySoftLink.LinkVisited = true;
        Process.Start(Paths.KGySoftLink);
    }

    private void UpdaterLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
        updaterLink.LinkVisited = true;
        Process.Start(Paths.UpdaterLink);
    }

    private void SotnToolsLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
        sotnToolsLink.LinkVisited = true;
        Process.Start(Paths.SotnRandoToolsLink);
    }

    private void FF6Link_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
        ff6Link.LinkVisited = true;
        Process.Start(Paths.WorldsCollideLink);
    }

    private void FFmqLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
        ffmqLink.LinkVisited = true;
        Process.Start(Paths.MysticQuestRandomizerLink);
    }
}
