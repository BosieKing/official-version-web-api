namespace Model.DTOs.FronDesk.PostHomePage
{
    /// <summary>
    /// 查询条件
    /// </summary>
    public class PostSearchInput
    {
        /// <summary>
        /// 查询字符串
        /// </summary>
        public string SearchValue { get; set; } = string.Empty;

        public string PaperId { get; set; } = string.Empty;
    }
}
