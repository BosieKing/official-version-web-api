using System.ComponentModel;
using System.Reflection;

namespace UtilityToolkit.Extensions
{
    /// <summary>
    /// 枚举帮助类
    /// </summary>
    public static class EnumUtil
    {
        /// <summary>
        /// 枚举转字典
        /// </summary>
        /// <param name="enums"></param>
        /// <returns>枚举属性名称及其描述的字典</returns>
        /// <remarks>Key为属性名称，value为属性描述</remarks>
        public static Dictionary<string, string> TryParseDic(this Type enumType)
        {
            var keyValuePairs = new Dictionary<string, string>();
            try
            {
                // 获取枚举属性值名称               
                var fieldNames = Enum.GetNames(enumType);
                foreach (var item in fieldNames)
                {
                    var value = enumType.GetField(item).GetCustomAttribute<DescriptionAttribute>().Description ?? item;
                    keyValuePairs.Add(item, value);
                }
                return keyValuePairs;
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        /// <summary>
        /// 枚举转list集合
        /// </summary>
        /// <param name="enumType"></param>
        /// <returns>枚举属性名称集合</returns>
        public static string[] GetKeyArray(this Type enumType)
        {
            return Enum.GetNames(enumType);
        }

        /// <summary>
        /// 枚举转列表
        /// </summary>
        /// <param name="enums"></param>
        /// <returns>枚举属性描述的集合</returns>
        public static List<string> TryParseList(this Type enumType)
        {
            var list = new List<string>();
            try
            {
                // 获取枚举属性值名称
                // 非枚举抛出异常
                if (!enumType.IsEnum)
                    throw new ArgumentException("Type '" + enumType.Name + "' is not an enum.");
                var fieldNames = Enum.GetNames(enumType);
                foreach (var item in fieldNames)
                {
                    var value = enumType.GetField(item).GetCustomAttribute<DescriptionAttribute>().Description ?? item;
                    list.Add(value);
                }
                return list;
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        /// <summary>
        /// 获取枚举描述信息
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetDescription(this Enum value)
        {
            return value.GetType().GetMember(value.ToString()).FirstOrDefault()?.GetCustomAttribute<DescriptionAttribute>()?.Description;
        }
    }
}
