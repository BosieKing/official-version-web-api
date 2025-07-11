using Model.Repositotys.Service;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Repositotys.BasicData
{
    /// <summary>
    /// 用户角色中间表
    /// </summary>
    [Table("T_UserRole")]
    public class T_UserRole : EntityBaseDO
    {
        /// <summary>
        /// 角色Id
        /// </summary>
        public long RoleId { get; set; }

        /// <summary>
        /// 归属的角色
        /// </summary>
        public T_Role Role { get; set; }

        /// <summary>
        /// 用户Id
        /// </summary>
        public long UserId { get; set; }

        /// <summary>
        /// 用户
        /// </summary>
        public T_User User { get; set; }
    }
}
