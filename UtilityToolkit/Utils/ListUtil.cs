using System.Data;
using System.Reflection;
namespace UtilityToolkit.Utils
{
    /// <summary>
    /// List帮助类
    /// </summary>
    public static class ListUtil
    {
        /// <summary>
        /// list转DataTable
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static DataTable ToDataTable<T>(this IEnumerable<T> list, string tableName)
        {
            var dtResult = new DataTable();
            dtResult.TableName = tableName;
            var propertiyInfos = new List<PropertyInfo>();
            //生成各列
            Array.ForEach(typeof(T).GetProperties(), p =>
            {
                propertiyInfos.Add(p);
                dtResult.Columns.Add(p.Name, p.PropertyType);
            });
            //生成各行
            foreach (var item in list)
            {
                if (item == null)
                    continue;
                var dataRow = dtResult.NewRow();
                propertiyInfos.ForEach(p => dataRow[p.Name] = p.GetValue(item, null));
                dtResult.Rows.Add(dataRow);
            }
            return dtResult;
        }
    }
}
