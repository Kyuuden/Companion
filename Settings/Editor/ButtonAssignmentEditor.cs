using FF.Rando.Companion.Settings.TypeConverters;
using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace FF.Rando.Companion.Settings.Editor;
internal class ButtonAssignmentEditor : System.Drawing.Design.UITypeEditor
{
    public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
    {
        if (provider != null)
        {
            IWindowsFormsEditorService editorService = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));

            if (editorService != null)
            {
                using var dialog = new KeyBindingForm(context, value as string);
                if (editorService.ShowDialog(dialog) == DialogResult.OK)
                {
                    return dialog.Value;
                }
            }
        }
        // Return the original value if no change or error
        return value;
    }

    public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
    {
        return UITypeEditorEditStyle.Modal;
    }
}
