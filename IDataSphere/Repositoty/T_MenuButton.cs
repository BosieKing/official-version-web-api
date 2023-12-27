using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IDataSphere.Repositoty
{
    /// <summary>
    /// 菜单按钮表
    /// </summary>
    [Table("T_MenuButton")]
    public class T_MenuButton : EntityBaseDO
    {
        /// <summary>
        /// 菜单id
        /// </summary>
        public long MenuId { get; set; }

        /// <summary>
        /// 按钮名称
        /// </summary>     
        [MaxLength(50)]
        public string Name { get; set; }

        /// <summary>
        /// 按钮对应action
        /// </summary>     
        [MaxLength(50)]
        public string ActionName { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [MaxLength(500)]
        public string Remark { get; set; }
    }
}
