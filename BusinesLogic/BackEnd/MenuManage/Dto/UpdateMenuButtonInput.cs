using System.ComponentModel.DataAnnotations;

namespace BusinesLogic.BackEnd.MenuManage.Dto
{
    public class UpdateMenuButtonInput : AddMenuButtonInput
    {
        // <summary>
        /// 按钮Id
        /// </summary>
        [Required(ErrorMessage = "IdRequried")]
        public long Id { get; set; }
    }
}
