using System;
using System.Text;
using System.Text.RegularExpressions;

namespace BizHawk.FreeEnterprise.Companion.Extensions
{
    public static class ByteArray
    {
        /// <summary>
        /// Converts a hex string to an array of bytes.
        /// </summary>
        /// <param name="hexString">A hexadecimal string.  Must have an even number of nybbles (ie, no lone nybbles -- a whole number of bytes).
        /// 0x is allowed, but not required.  Any character that is not a valid hex character is ignored.
        /// </param>
        /// <returns>hexString converted to bytes.</returns>
        /// <exception cref="ArgumentException">If hexString contains an odd number of nybbles.</exception>
        public static byte[] FromHexString(string hexString)
        {
            hexString = Regex.Replace(hexString, @"(0x|[^0-9a-fA-F])", "");

            // string length cannot be odd because we don't know on which side of the byte the nybble should go on
            if ((hexString.Length & 0x01) != 0) throw new ArgumentException("hexString must contain an even number of nybbles");

            byte[] byteArray = new byte[hexString.Length / 2]; // two nybbles -> one byte

            for (int i = 0; i < byteArray.Length; i++)
            {
                string byteStr = hexString.Substring(i * 2, 2); // two nybbles, one byte
                byteArray[i] = (byte)int.Parse(byteStr, System.Globalization.NumberStyles.HexNumber);
            }

            return byteArray;
        }

        public static string ToHexString(this byte[] bytes)
        {
            return ToHexString(bytes, 0, bytes.Length);
        }

        public static string ToHexString(this byte[] bytes, int offset, int length)
        {
            StringBuilder builder = new StringBuilder();
            for (int i = offset; i < offset + length; i++)
            {
                builder.AppendFormat("{0:X2} ", bytes[i]);
            }
            if (builder.Length > 0)
                builder.Remove(builder.Length - 1, 1);
            return builder.ToString();
        }

        public static string ToBase64String(this byte[] bytes)
        {
            return Convert.ToBase64String(bytes);
        }

        public static byte[] FromBase64String(string base64String)
        {
            return Convert.FromBase64String(base64String);
        }
    }
}

