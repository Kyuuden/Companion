using FF.Rando.Companion.Games.JetsOfTime.Settings;
using FF.Rando.Companion.View;
using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;

namespace FF.Rando.Companion.Games.JetsOfTime.View;
internal partial class KeyItemsPanel : FlowPanel<KeyItemSettings>
{
    public KeyItemsPanel() :base()
    {
        SpacingMode = SpacingMode.Columns;
        //WrapAfter = 6;
    }

    public override DockStyle DefaultDockStyle => DockStyle.Top;

    protected override Control[] GenerateControls(Seed seed) 
        => (Game?.State.KeyItems.Items ?? []).Select(ki => new KeyItemControl(seed, Settings!, ki)).ToArray();

    protected override void Settings_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        WrapAfter = int.MaxValue;
        base.Settings_PropertyChanged(sender, e);
        OptimizeColums();
    }

    protected override void OnResize(EventArgs e)
    {
        WrapAfter = int.MaxValue;
        base.OnResize(e);
        OptimizeColums();
    }

    private void OptimizeColums()
    {
        var visibeControls = Controls.OfType<Control>().Where(c => c.Visible).ToList();
        var cnt = visibeControls.Count();

        if (cnt == 0)
            return;

        var rows = visibeControls.Count(GetFlowBreak);
        var cols = visibeControls.FindIndex(GetFlowBreak)+1;

        if (GetFlowBreak(visibeControls.Last()))
            return;

        var wrap = cols;
        var error = cols - (cnt - (cols * rows));
        for (var i = cols - 1; i >= 0; i--)
        {
            if ((cnt - (i * rows)) > i)
                break;

            if ((cols - (cnt - (i * rows))) < error)
            {
                error = cols - (cnt - (i * rows));
                wrap = i;
            }
        }

        if (WrapAfter != wrap && wrap != cols)
        {
            WrapAfter = wrap;
            Arrange();
        }
    }
}
