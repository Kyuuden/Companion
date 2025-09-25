using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms.Design;
using System.Windows.Forms;
using System.Linq;

namespace FF.Rando.Companion.Settings.Editor;

internal class EnumListEditor<T> : UITypeEditor where T : Enum
{
    public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
    {
        return UITypeEditorEditStyle.DropDown;
    }

    public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
    {
        var service = provider?.GetService(typeof(IWindowsFormsEditorService)) as IWindowsFormsEditorService;
        if (service == null)
            return value;

        var currentValues = ((value as string)?.Split([',', ' ', ';'], StringSplitOptions.RemoveEmptyEntries) ?? Enumerable.Empty<string>())
           .Select(x => x.Trim())
           .Select(x => Enum.Parse(typeof(T), x))
           .OfType<T>()
           .ToList();

        var allValues = Enum.GetValues(typeof(T)).OfType<T>();

        var listBox = new CheckedListBox();
        foreach (var v in allValues)
        {
            listBox.Items.Add(v, currentValues.Contains(v));
        }

        service.DropDownControl(listBox);

        value = string.Join(", ", listBox.CheckedItems.OfType<object>().Select(x => x.ToString()) ?? []);
        return value;
    }
}
