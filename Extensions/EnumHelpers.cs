using System;
using System.Collections.Generic;
using System.Linq;

namespace BizHawk.FreeEnterprise.Companion.Extensions
{
    public static class EnumHelpers
    {
        #region From: http://stackoverflow.com/questions/1086618/comparing-enum-flags-in-c
        /// <summary>
        /// Verifies a type is an enum with the [Flags] attribute.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        private static void CheckEnumWithFlags<T>()
        {
            if (!typeof(T).IsEnum)
                throw new ArgumentException(string.Format("Type '{0}' is not an enum", typeof(T).FullName));
            if (!Attribute.IsDefined(typeof(T), typeof(FlagsAttribute)))
                throw new ArgumentException(string.Format("Type '{0}' doesn't have the 'Flags' attribute", typeof(T).FullName));
        }        

        /// <summary>
        /// Determines whether a flag enum has a particular flag set.
        /// Performs (value &amp; flag != 0).
        /// </summary>
        /// <typeparam name="T">an enum with the [Flags] attribute</typeparam>
        /// <param name="value"></param>
        /// <param name="flag"></param>
        /// <returns>true if the flag is set, otherwise false</returns>
        /// <exception cref="ArgumentException">if T is not an enum type with a [Flags] attribute</exception>
        public static bool IsFlagSet<T>(this T value, T flag) where T : struct
        {
            CheckEnumWithFlags<T>();
            long lValue = Convert.ToInt64(value);
            long lFlag = Convert.ToInt64(flag);
            return (lValue & lFlag) != 0;
        }

        public static bool IsAnyFlagSet<T>(this T value, params T[] flags) where T : struct
        {
            CheckEnumWithFlags<T>();
            return flags.Length == 0 ? Convert.ToInt64(value) != 0 : flags.Any(f => value.IsFlagSet(f));
        }

        /// <summary>
        /// Converts a flag enum variable to a sequence of the individual flags that are set. 
        /// </summary>
        /// <typeparam name="T">an enum with the [Flags] attribute</typeparam>
        /// <param name="value"></param>
        /// <returns>sequence of each set flag in value</returns>
        /// <exception cref="ArgumentException">if T is not an enum type with a [Flags] attribute</exception>
        public static IEnumerable<T> GetFlags<T>(this T value) where T : struct
        {
            CheckEnumWithFlags<T>();

            foreach (var val in typeof(T).GetFields())
            {
                if (val.FieldType == typeof(T))
                {
                    if (value.IsFlagSet((T)val.GetValue(typeof(T))))
                        yield return (T)val.GetValue(typeof(T));
                }
            }
        }

        public static T SetFlags<T>(this T value, T flags, bool on) where T : struct
        {
            CheckEnumWithFlags<T>();
            long lValue = Convert.ToInt64(value);
            long lFlag = Convert.ToInt64(flags);
            if (on)
            {
                lValue |= lFlag;
            }
            else
            {
                lValue &= (~lFlag);
            }
            return (T)Enum.ToObject(typeof(T), lValue);
        }

        public static T SetFlags<T>(this T value, T flags) where T : struct
        {
            return value.SetFlags(flags, true);
        }

        public static T ClearFlags<T>(this T value, T flags) where T : struct
        {
            return value.SetFlags(flags, false);
        }

        /// <summary>
        /// Converts a sequence of flag values to a single value with all flags in the sequence set.
        /// Inverse operation of GetFlags().
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="flags"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException">if T is not an enum type with a [Flags] attribute</exception>
        public static T CombineFlags<T>(this IEnumerable<T> flags) where T : struct
        {
            CheckEnumWithFlags<T>();
            long lValue = 0;
            foreach (T flag in flags)
            {
                long lFlag = Convert.ToInt64(flag);
                lValue |= lFlag;
            }
            return (T)Enum.ToObject(typeof(T), lValue);
        }
        #endregion
    }
}

