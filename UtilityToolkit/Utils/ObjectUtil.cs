namespace UtilityToolkit.Utils
{
    /// <summary>
    /// object帮助类
    /// </summary>
    public static class ObjectUtil
    {
        /// <summary>
        /// 如果对象为空则抛出异常
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="message"></param>
        /// <exception cref="Exception"></exception>
        public static void IsNull(this object obj, string message) 
        {
            if (obj == null)
            {
                throw new Exception(message);
            }
        }
    }
}
