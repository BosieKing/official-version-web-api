using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Repositotys
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
        /// 菜单Id
        /// </summary>
        public long MenuId { get; set; }
    }
}
