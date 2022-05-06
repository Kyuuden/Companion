using System.IO;
using System.Reflection;

namespace BizHawk.FreeEnterprise.Companion.Configuration
{
    public static class Strings
    {
        public static string IconResource { get; } = "BizHawk.FreeEnterprise.Companion.Resources.Crystal.png";
        public static string SettingsPath { get; } = "BizHawk.FreeEnterprise.Companion.Settings.json";
    }

    public static class PathExtensions
    {
        public static string ToPath(this string file)
            => Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
    }
}
