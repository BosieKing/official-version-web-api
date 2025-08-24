using SharedLibrary.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Repositotys.Service
{
    /// <summary>
    /// 权限类型
    /// </summary>
    [Table("T_Role")]
    public class T_Role : EntityBaseDO
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 权限集合
        /// </summary>
        public List<T_RoleMenu> RoleMenus { get; set; }
    }
}