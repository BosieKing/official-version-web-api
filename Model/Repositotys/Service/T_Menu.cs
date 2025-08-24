using SharedLibrary.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Repositotys.Service
{
    /// <summary>
    /// 菜单表 - 用于构建前端vue-router模板
    /// </summary>
    [Table("T_Menu")]
    public class T_Menu : EntityBaseDO
    {
        /// <summary>
        /// 菜单图标
        /// </summary>
        [Display(Name = "菜单图标")]
        [MaxLength(50, ErrorMessage = "图标名称长度不能超过50个字符")]
        public string Icon { get; set; }

        /// <summary>
        /// 菜单显示名称
        /// </summary>
        [Display(Name = "菜单名称")]
        [Required(ErrorMessage = "菜单名称不能为空")]
        [MaxLength(50, ErrorMessage = "菜单名称长度不能超过50个字符")]
        public string Name { get; set; }

        /// <summary>
        /// 组件路径
        /// </summary>
        [Display(Name = "组件路径")]
        [Required(ErrorMessage = "组件路径不能为空")]
        [MaxLength(200, ErrorMessage = "组件路径长度不能超过200个字符")]
        public string ComponentPath { get; set; }

        /// <summary>
        /// 组件名称
        /// </summary>
        [Display(Name = "组件名称")]
        [Required(ErrorMessage = "组件名称不能为空")]
        [MaxLength(100, ErrorMessage = "组件名称长度不能超过100个字符")]
        public string ComponentName { get; set; }

        /// <summary>
        /// 是否隐藏
        /// </summary>
        [Display(Name = "是否隐藏")]
        public bool IsHidden { get; set; } = false;

        /// <summary>
        /// 父级菜单ID（0表示顶级菜单）
        /// </summary>
        [Display(Name = "父级菜单ID")]
        public long MasterId { get; set; } = 0;

        /// <summary>
        /// Controller路由路径
        /// </summary>
        [Display(Name = "Controller路由")]
        [MaxLength(100, ErrorMessage = "Controller路由长度不能超过100个字符")]
        public string ControllerRouter { get; set; }

        /// <summary>
        /// 备注信息
        /// </summary>
        [Display(Name = "备注")]
        [MaxLength(500, ErrorMessage = "备注长度不能超过500个字符")]
        public string Remark { get; set; }

        /// <summary>
        /// 菜单权重类型
        /// </summary>
        [Display(Name = "菜单权重类型")]
        public MenuWeightTypeEnum MenuWeightType { get; set; }
    }
}