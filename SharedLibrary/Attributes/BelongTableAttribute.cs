namespace SharedLibrary.Attributes
{
    /// <summary>
    /// 归属类
    /// </summary>
    public class BelongTableAttribute : Attribute
    {
        /// <summary>
        /// 表
        /// </summary>
        public Type Table { get; set; }

        /// <summary>
        /// 别名
        /// </summary>
        public string AsName { get; set; }  = string.Empty;

        /// <summary>
        /// 特殊查询
        /// </summary>
        public string Special { get; set; } = string.Empty;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="table"></param>
        public BelongTableAttribute(Type table)
        {
            Table = table;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="table"></param>
        /// <param name="asName"></param>
        public BelongTableAttribute(Type table, string asName)
        {
            Table = table;
            AsName = asName;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="table"></param>
        /// <param name="asName"></param>
        public BelongTableAttribute(Type table, string asName, string special)
        {
            Table = table;
            AsName = asName;
            Special = special;
        }
    }
}
