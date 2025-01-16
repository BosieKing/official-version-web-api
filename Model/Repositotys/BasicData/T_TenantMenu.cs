using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Repositotys.BasicData
{
    /// <summary>
    /// 租户菜单表
    /// </summary>
    [Table("T_TenantMenu")]
    public class T_TenantMenu : EntityTenantDO
    {
        /// <summary>
        /// 归属目录id
        /// </summary>
        public long DirectoryId { get; set; }

        /// <summary>
        /// 归属目录
        /// </summary>
        public T_TenantDirectory BelongTenantDirectory { get; set; }

        /// <summary>
        /// 菜单名称
        /// </summary>
        [MaxLength(50)]
        public string Name { get; set; }

        /// <summary>
        /// 图标--前端需要使用字段
        /// </summary>
        [MaxLength(50)]
        public string Icon { get; set; }

        /// <summary>
        /// 后端权限路由-对应的控制器名称，也对应前端的名称
        /// </summary>
        [MaxLength(100)]
        public string ControllerRouter { get; set; }

        /// <summary>
        /// 前台组件物理地址--前端维护
        /// </summary>
        [MaxLength(100)]
        public string VueComponent { get; set; }

        /// <summary>
        /// 前端浏览器路由地址--前端维护
        /// </summary>
        [MaxLength(100)]
        public string BrowserPath { get; set; }

        /// <summary>
        /// 菜单类型
        /// </summary>
        /// <see cref="SharedLibrary.Enums.MenuWeightTypeEnum"/>
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
        /// 唯一编号
        /// </summary>
        public long UniqueNumber { get; set; }

        /// <summary>
        /// 此菜单已经分配了的角色
        /// </summary>    
        public List<T_Role> Roles { get; set; }

        /// <summary>
        /// 按钮组
        /// </summary>
        public List<T_TenantMenuButton> Buttons { get; set; }
    }
}
