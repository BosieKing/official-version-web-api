using System.ComponentModel.DataAnnotations.Schema;
namespace Model.Repositotys
{
    /// <summary>
    /// 角色屏蔽按钮
    /// </summary>
    [Table("T_RoleBlockButton")]
    public class T_RoleBlockButton : EntityTenantDO
    {
        /// <summary>
        /// 角色Id
        /// </summary>
        public long RoleId { get; set; }

        /// <summary>
        /// 需要屏蔽的按钮id
        /// </summary>
        public long ButtonId { get; set; }
    }
}
