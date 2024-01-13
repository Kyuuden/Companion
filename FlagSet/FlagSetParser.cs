using BizHawk.FreeEnterprise.Companion.Extensions;
using System.Linq;

namespace BizHawk.FreeEnterprise.Companion.FlagSet
{
    public class FlagSetParser
    {
        public static IFlagSet? Parse(string? flags)
        {
            if (flags == null || flags.Contains("hidden"))
                return null;

            flags = flags.Substring(1);
            while (flags.Length % 4 != 0)
                flags += '=';

            flags = flags.Replace('-', '+').Replace('_', '/');

            var data = ByteArray.FromBase64String(flags);
            var ver = $"{data[0]}.{data[1]}.{data[2]}";
            data = data.Skip(3).ToArray();

            switch (ver)
            {
                case "4.5.0":
                    return new _4._5._0.FlagSet(data);
                case "4.6.0":
                    return new _4._6._0.FlagSet(data);
                default:
                    return null;
            }

        }
    }
}
