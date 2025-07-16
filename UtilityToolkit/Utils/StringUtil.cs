using Newtonsoft.Json;
using System.Collections;
using TencentCloud.Tcr.V20190924.Models;

namespace UtilityToolkit.Utils
{
    /// <summary>
    /// 字符串操作工具类
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
        /// 判断2个字符串是否相等（不区分大小写）
        /// </summary>
        /// <param name="srcString"></param>
        /// <param name="desString"></param>
        /// <returns></returns>
        public static bool EqualIgnoreCase(this string srcString, string desString)
        {
            string lowerSrcString = srcString?.ToLower();
            string lowerDesString = desString?.ToLower();
            return lowerDesString.Equals(lowerSrcString);
        }

        /// <summary>
        /// 判断是否为null、空、空白字符组合
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsNullOrWhiteSpace(this string str)
        {
            return string.IsNullOrWhiteSpace(str);
        }

        /// <summary>
        /// 字符串(json)转对象集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="json"></param>
        /// <returns></returns>
        public static List<T> JsonToList<T>(this string json) where T : class, new()
        {
            JsonSerializer serializer = new JsonSerializer();
            StringReader sr = new StringReader(json);
            object o = serializer.Deserialize(new JsonTextReader(sr), typeof(List<T>));
            List<T> list = o as List<T>;
            return list;
        }

        /// <summary>
        /// 字符串(json)转dic集合
        /// </summary>
        /// <param name="jsonData"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static Dictionary<string, object> JsonToDictionary(this string jsonData)
        {
            // 实例化JavaScriptSerializer类的新实例
            JsonSerializer serializer = new JsonSerializer();
            try
            {
                StringReader sr = new StringReader(jsonData);
                // 将指定的 JSON 字符串转换为 Dictionary<string, object> 类型的对象
                var data = serializer.Deserialize<Dictionary<string, object>>(new JsonTextReader(sr));
                return data;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// 字符串(json)转Hashtable
        /// </summary>
        /// <param name="jsonData"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static Hashtable JsonToHashTable(this string jsonData)
        {
            // 实例化JavaScriptSerializer类的新实例
            JsonSerializer serializer = new JsonSerializer();
            try
            {
                StringReader sr = new StringReader(jsonData);
                // 将指定的 JSON 字符串转换为 Dictionary<string, object> 类型的对象
                var data = serializer.Deserialize<Hashtable>(new JsonTextReader(sr));
                return data;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
