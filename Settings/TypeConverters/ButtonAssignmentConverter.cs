using System;
using System.Collections;
using System.ComponentModel;
using System.Security.AccessControl;

namespace FF.Rando.Companion.Settings.TypeConverters;

public class ButtonAssignmentConverter : TypeConverter
{
    public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
    {
        if (sourceType == typeof(string)) return false;

        return base.CanConvertFrom(context, sourceType);
    }
}