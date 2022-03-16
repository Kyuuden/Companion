using System;

namespace BizHawk.FreeEnterprise.Companion.Extensions
{
    public static class MathExt
    {
        public static int FloorLog2(Int64 val)
        {
            if (val <= 0) return -1;
            var pow = 0;
            while (val > 1)
            {
                val >>= 1;
                pow++;
            }

            return pow;
        }

        public static int FloorLog2(UInt64 val)
        {
            if (val == 0) return -1;
            var pow = 0;
            while (val > 1)
            {
                val >>= 1;
                pow++;
            }

            return pow;
        }
    }
}
