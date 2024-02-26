namespace System
{
    /// <summary>
    /// 字符串静态帮助类
    /// </summary>
    public static class StringUtil
    {
        /// <summary>
        /// 判断字符串是否为空或者为null
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsNullOrEmpty(this string value)
        {
            return string.IsNullOrEmpty(value);
        }

        /// <summary>
        /// 判断字符串是否为null、空、空白字符组合
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsNullOrWhiteSpace(this string str)
        {
            return string.IsNullOrWhiteSpace(str);
        }
    }
}
