using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    /// <summary>
    /// BLL层的扩展方法。
    /// </summary>
    internal static class BLLExtend
    {
        /// <summary>
        /// 将字符串转换为bool。
        /// </summary>
        /// <param name="s">字符串。</param>
        /// <returns>转换后的bool值。</returns>
        public static bool ToBool(this string s)
        {
            bool b;
            bool.TryParse(s, out b);
            return b;
        }

        /// <summary>
        /// 将字符串转换为int。
        /// </summary>
        /// <param name="s">字符串。</param>
        /// <returns>转换后的int值。</returns>
        public static int ToInt(this string s)
        {
            int i;
            int.TryParse(s, out i);
            return i;
        }

        /// <summary>
        /// 将字符串转换为DateTime。
        /// </summary>
        /// <param name="s">字符串。</param>
        /// <returns>转换后的DateTime值。</returns>
        public static DateTime ToDateTime(this string s)
        {
            DateTime d;
            DateTime.TryParse(s, out d);
            return d;
        }
    }
}
