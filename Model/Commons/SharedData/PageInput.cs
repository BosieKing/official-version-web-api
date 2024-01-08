using System.ComponentModel.DataAnnotations;

namespace Model.Commons.SharedData
{
    /// <summary>
    /// 表格查询输入类
    /// </summary>
    public class PageInput
    {
        /// <summary>
        /// 页码
        /// </summary>
        [Required]
        public int PageNo { get; set; }

        /// <summary>
        /// 容量
        /// </summary>
        [Required]
        public int PageSize { get; set; }
    }
}
