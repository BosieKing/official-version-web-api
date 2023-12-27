using System.ComponentModel.DataAnnotations;

namespace BusinesLogic.BackEnd.MenuManage.Dto
{
    /// <summary>
    /// 修改菜单输入
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
