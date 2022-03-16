using System.Text;

namespace BizHawk.FreeEnterprise.Companion.Extensions
{
    public static class EncodingHelpers
    {
        public static string GetString(this Encoding e, byte[] b)
        {
            return e.GetString(b, 0, b.Length);
        }
    }
}

