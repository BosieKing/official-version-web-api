using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Repositotys
{
    /// <summary>
    /// 租户菜单表
    /// </summary>
    [Table("T_TenantMenu")]
    public class T_TenantMenu : EntityTenantDO
    {
        /// <summary>
        /// 名称
        /// </summary>
        [MaxLength(50)]
        public string Name { get; set; }

        /// <summary>
        /// 图标--前端需要使用字段
        /// </summary>
        [MaxLength(50)]
        public string Icon { get; set; }

        /// <summary>
        /// 后端权限路由-对应的控制器名称
        /// </summary>
        [MaxLength(100)]
        public string Router { get; set; }

        /// <summary>
        /// 前台组件物理地址--前端维护
        /// </summary>
        [MaxLength(100)]
        public string Component { get; set; }

        /// <summary>
        /// 前端浏览器路由地址--前端维护
        /// </summary>
        [MaxLength(100)]
        public string BrowserPath { get; set; }

        /// <summary>
        /// 菜单类型
        /// </summary>
        /// <see cref="Resource.Enums.MenuWeightTypeEnum"/>
        public int Weight { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [MaxLength(500)]
        public string? Remark { get; set; }

        /// <summary>
        /// 是否隐藏
        /// </summary>
        public bool IsHidden { get; set; } = false;

        /// <summary>
        /// 归属目录id
        /// </summary>
        public long DirectoryId { get; set; }

        /// <summary>
        /// 唯一编号
        /// </summary>
        public long UniqueNumber { get; set; }
    }
}
