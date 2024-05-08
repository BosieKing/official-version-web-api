using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Repositotys.BasicData
{
    /// <summary>
    /// 菜单目录表
    /// </summary>
    [Table("T_Directory")]
    public class T_Directory : EntityBaseDO
    {
        /// <summary>
        /// 名称
        /// </summary>
        [MaxLength(50)]
        public string Name { get; set; }

        /// <summary>
        /// 图标
        /// </summary>
        [MaxLength(50)]
        public string Icon { get; set; }

        /// <summary>
        ///  前端浏览器路由地址--前端维护
        /// </summary>
        [MaxLength(100)]
        public string BrowserPath { get; set; }

        /// <summary>
        /// 子菜单
        /// </summary>
        public virtual ICollection<T_Menu> Menus { get; set; }
    }
}
