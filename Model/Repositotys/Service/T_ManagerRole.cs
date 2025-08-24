using SharedLibrary.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Repositotys.Service
{
    /// <summary>
    /// 用户和角色
    /// </summary>
    [Table("T_ManagerRole")]
    public class T_ManagerRole : EntityBaseDO
    {
        /// <summary>
        /// 管理者id
        /// </summary>
        [Key]
        [Column("ManagerId")]
        public long ManagerId { get; set; }

        /// <summary>
        /// 管理者
        /// </summary>
        public T_Manager Manager { get; set; }

        /// <summary>
        /// 对应的角色组
        /// </summary>
        public List<T_Role> Roles { get; set; }


    }
}