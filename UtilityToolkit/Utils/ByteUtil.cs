namespace UtilityToolkit.Utils
{
    /// <summary>
    /// Byte帮助类
    /// </summary>
    public static class ByteUtil
    {
        /// <summary>
        /// byet转string
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static string BytesToString(this byte[] bytes) => System.Text.Encoding.UTF8.GetString(bytes);      
    }
}
