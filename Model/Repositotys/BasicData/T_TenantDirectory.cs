using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Repositotys.BasicData
{
    /// <summary>
    /// 租户目录
    /// </summary>
    [Table("T_TenantDirectory")]
    public class T_TenantDirectory : EntityTenantDO
    {
        /// <summary>
        /// 名称
        /// </summary>
        [MaxLength(50)]
        public string Name { get; set; }

        /// <summary>
        /// 图标
        /// </summary>
        [MaxLength(50)]
        public string Icon { get; set; }

        /// <summary>
        ///  前端浏览器路由地址--前端维护
        /// </summary>
        [MaxLength(100)]
        public string BrowserPath { get; set; }

    }
}
