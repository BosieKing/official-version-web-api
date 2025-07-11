using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Repositotys.BasicData
{
    /// <summary>
    /// 方法
    /// </summary>
    [Table("T_Function")]
    public class T_Function : EntityBaseDO
    {       
        /// <summary>
        /// 方法名称
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
        public string? Remark { get; set; }

        #region 导航属性
        /// <summary>
        /// 菜单id
        /// </summary>
        public long MenuId { get; set; }

        /// <summary>
        /// 归属菜单
        /// </summary>
        public T_Menu BelongMenu { get; set; }
        #endregion
    }
}
