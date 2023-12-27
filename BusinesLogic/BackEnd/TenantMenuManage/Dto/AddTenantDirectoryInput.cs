using System.ComponentModel.DataAnnotations;

namespace BusinesLogic.BackEnd.TenantMenuManage.Dto
{
    public class AddTenantDirectoryInput
    {
        /// <summary>
        /// 名称
        /// </summary>
        [MaxLength(50)]
        [Required(ErrorMessage = "NameRequired")]
        public string Name { get; set; }

        /// <summary>
        /// 图标
        /// </summary>
        [Required(ErrorMessage = "IconRequired")]
        [MaxLength(50, ErrorMessage = "IconTooLong50")]
        public string Icon { get; set; }

        /// <summary>
        ///  前端浏览器路由地址--前端维护
        /// </summary>
        [Required(ErrorMessage = "PathRequired")]
        [MaxLength(100, ErrorMessage = "PathTooLong100")]
        public string Path { get; set; }
    }
}
