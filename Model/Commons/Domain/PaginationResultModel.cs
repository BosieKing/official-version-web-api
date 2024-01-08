namespace Model.Commons.Domain
{
    /// <summary>
    /// 分页模型
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PaginationResultModel
    {
        /// <summary>
        /// 当前页码
        /// </summary>
        public int PageNo { get; set; }
        /// <summary>
        /// 当前展示条数
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// 当前数据
        /// </summary>
        public object Rows { get; set; }

        /// <summary>
        /// 总页码
        /// </summary>
        public int TotalPage { get; set; }
        /// <summary>
        /// 总条数
        /// </summary>
        public int TotalRows { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="pageNo"></param>
        /// <param name="pageSize"></param>
        /// <param name="count"></param>
        /// <param name="rows"></param>
        public PaginationResultModel(int pageNo, int pageSize, int count, object rows)
        {
            PageNo = pageNo;
            PageSize = pageSize;
            TotalRows = count;
            Rows = rows;
            TotalPage = (int)Math.Ceiling(count / (double)pageSize);
        }
    }
}
