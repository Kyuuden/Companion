using BizHawk.Client.EmuHawk;
using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace FF.Rando.Companion.Settings.TypeConverters;
public partial class KeyBindingForm : FormBase
{
    private readonly ToolTip tooltip;

    public KeyBindingForm(ITypeDescriptorContext context, string? currentValue)
    {
        InitializeComponent();
        tooltip = new ToolTip();
        label1.Text = context.PropertyDescriptor.DisplayName;
        inputWidget.SetupTooltip(tooltip, null);
        inputWidget.Bindings = currentValue;
    }

    public string Value => inputWidget.Bindings;

    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);
        inputWidget.Focus();
    }

    protected override string WindowTitleStatic => "Select Key Binding";
    public override bool BlocksInputWhenFocused => false;
}
