using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace UtilityToolkit.Utils
{
    public static class JosnUtil
    {
        /// <summary>
        /// 对象转字符串
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string ToJson(this object source)
        {
            IsoDateTimeConverter converter = new IsoDateTimeConverter();
            converter.DateTimeFormat = "yyyy-MM-dd HH:mm:ss";
            return JsonConvert.SerializeObject(source, Formatting.None, new JsonConverter[] { converter });
        }

        /// <summary>
        /// 字符串转对象
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static T ToObject<T>(this string source) where T : class
        {
            if (source.IsNullOrEmpty())
            {
                return null;
            }
            return JsonConvert.DeserializeObject<T>(source ?? "");
        }
    }
}
