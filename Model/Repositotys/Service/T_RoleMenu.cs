using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Repositotys.Service
{
    /// <summary>
    /// 角色菜单表
    /// </summary>
    [Table("T_RoleMenu")]
    public class T_RoleMenu : EntityBaseDO
    {
        /// <summary>
        /// 角色id
        /// </summary>
        public long RoleId { get; set; }

        /// <summary>
        /// 菜单id
        /// </summary>
        public long MenuId { get; set; }

        /// <summary>
        /// 对应的菜单
        /// </summary>
        public T_Menu Menu { get; set; }

}
}
