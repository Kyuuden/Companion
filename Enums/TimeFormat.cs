using System.ComponentModel;

namespace BizHawk.FreeEnterprise.Companion
{
    public enum TimeFormat
    {
        [Description("hh:mm:ss")]
        HHMMSS,
        [Description("hh:mm:ss.f")]
        HHMMSSF,
        [Description("hh:mm:ss.ff")]
        HHMMSSFF,
        [Description("hh:mm:ss.fff")]
        HHMMSSFFF,
        [Description("hh:mm:ss.ffff")]
        HHMMSSFFFF,
        [Description("hh:mm:ss.fffff")]
        HHMMSSFFFFF,
        [Description("hh:mm:ss.ffffff")]
        HHMMSSFFFFFF,
        [Description("hh:mm:ss.fffffff")]
        HHMMSSFFFFFFF
    }
}
