using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Repositotys.BasicData
{
    /// <summary>
    /// 权限菜单中间表
    /// </summary>
    [Table("T_RoleMenu")]
    public class T_RoleMenu : EntityTenantDO
    {
        /// <summary>
        /// 角色Id
        /// </summary>
        public long RoleId { get; set; }

        /// <summary>
        /// 归属的角色
        /// </summary>
        public T_Role BelongRole { get; set; }

        /// <summary>
        /// 菜单Id
        /// </summary>
        public long MenuId { get; set; }

        /// <summary>
        /// 归属的菜单
        /// </summary>
        public T_TenantMenu BelongTenantMenu { get; set; }
    }
}
