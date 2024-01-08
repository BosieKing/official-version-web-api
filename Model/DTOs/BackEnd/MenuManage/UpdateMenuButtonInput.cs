using System.ComponentModel.DataAnnotations;

namespace Model.DTOs.BackEnd.MenuManage
{
    /// <summary>
    /// 更新菜单按钮信息输入类
    /// </summary>
    public class UpdateMenuButtonInput : AddMenuButtonInput
    {
        /// <summary>
        /// 按钮Id
        /// </summary>
        [Required(ErrorMessage = "IdRequried")]
        public long Id { get; set; }
    }
}
