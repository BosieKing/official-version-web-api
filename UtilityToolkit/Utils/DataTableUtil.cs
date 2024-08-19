using System.Data;
using System.Reflection;


namespace UtilityToolkit.Utils
{
    /// <summary>
    /// datatable帮助类
    /// </summary>
    public static class DataTableUtil
    {
        /// <summary>
        /// DataTable转成List
        /// </summary>
        public static List<T> ToDataList<T>(this DataTable dt)
        {
            var list = new List<T>();
            var plist = new List<PropertyInfo>(typeof(T).GetProperties());
            foreach (DataRow item in dt.Rows)
            {
                var s = Activator.CreateInstance<T>();
                for (var i = 0; i < dt.Columns.Count; i++)
                {
                    var info = plist.Find(p => p.Name.ToLower() == dt.Columns[i].ColumnName.ToLower());
                    if (info != null)
                        if (!Convert.IsDBNull(item[i]))
                        {
                            object v = null;
                            if (info.PropertyType.ToString().Contains("System.Nullable"))
                                v = Convert.ChangeType(item[i], Nullable.GetUnderlyingType(info.PropertyType));
                            else
                                v = Convert.ChangeType(item[i], info.PropertyType);
                            info.SetValue(s, v, null);
                        }
                }
                list.Add(s);
            }
            return list;
        }
    }
}

