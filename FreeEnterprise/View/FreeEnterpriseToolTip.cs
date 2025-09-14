using System.Drawing;
using System.Windows.Forms;
using FF.Rando.Companion.Settings;

namespace FF.Rando.Companion.FreeEnterprise.View;
internal class FreeEnterpriseToolTip : ToolTip
{
    private Bitmap? _image;
    private readonly PanelSettings _scaleSettings;

    public FreeEnterpriseToolTip(PanelSettings scaleSettings)
    {
        ShowAlways = true;
        OwnerDraw = true;
        _scaleSettings = scaleSettings;
        Popup += FreeEnterpriseToolTip_Popup;
        Draw += FreeEnterpriseToolTip_Draw;
    }

    private void FreeEnterpriseToolTip_Popup(object sender, PopupEventArgs e)
    {
        e.ToolTipSize = _scaleSettings.Scale(Image.Size);
    }

    private void FreeEnterpriseToolTip_Draw(object sender, DrawToolTipEventArgs e)
    {
        e.Graphics.DrawImage(_image, e.Bounds);
    }

    public Bitmap Image
    {
        get => _image ?? new Bitmap(0, 0);
        set
        {
            _image = value;
        }
    }
}
