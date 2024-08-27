using Microsoft.EntityFrameworkCore;
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
    }
}
