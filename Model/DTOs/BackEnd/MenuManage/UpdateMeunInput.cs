using System.ComponentModel.DataAnnotations;

namespace Model.DTOs.BackEnd.MenuManage
{
    /// <summary>
    /// 更新菜单信息输入类
    /// </summary>
    public class UpdateMeunInput : AddMenuInput
    {
        /// <summary>
        /// 菜单Id
        /// </summary>
        [Required(ErrorMessage = "IdRequried")]
        public long Id { get; set; }
    }
}
