using Microsoft.EntityFrameworkCore;
using Model.Repositotys.Service;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Repositotys.BasicData
{
    /// <summary>
    /// 角色表
    /// </summary>
    [Table(nameof(T_Role))]
    public class T_Role : EntityTenantDO
    {
        /// <summary>
        /// 名称
        /// </summary>
        [MaxLength(50)]
        public string Name { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [MaxLength(500)]
        public string? Remark { get; set; }

        /// <summary>
        /// 配置给哪些用户
        /// </summary>
        public List<T_User> Users { get; set; }

        /// <summary>
        /// 此角色拥有的菜单
        /// </summary>    
        public List<T_TenantMenu> Menus { get; set; }
    }
}
