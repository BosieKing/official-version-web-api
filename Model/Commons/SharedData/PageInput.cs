using System.ComponentModel.DataAnnotations;

namespace Model.Commons.SharedData
{
    /// <summary>
    /// 分页模型输入类
    /// </summary>
    public class PageInput
    {
        /// <summary>
        /// 请求页码
        /// </summary>
        [Required]
        public int PageNo { get; set; }

        /// <summary>
        /// 请求容量
        /// </summary>
        [Required]
        public int PageSize { get; set; }
    }
}
