using System;
using System.ComponentModel;

namespace FF.Rando.Companion.Settings.TypeConverters;

public class EnumListConverter<T> : TypeConverter where T : Enum
{
    public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
    {
        return false;
    }

    public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
    {
        if (destinationType == typeof(string)) return true;

        return base.CanConvertFrom(context, destinationType);
    }
}

