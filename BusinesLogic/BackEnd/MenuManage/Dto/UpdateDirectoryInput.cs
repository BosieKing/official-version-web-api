using System.ComponentModel.DataAnnotations;

namespace BusinesLogic.BackEnd.MenuManage.Dto
{
    public class UpdateDirectoryInput : AddDirectoryInput
    {
        // <summary>
        /// 目录id
        /// </summary>
        [Required(ErrorMessage = "IdRequried")]
        public long Id { get; set; }
    }
}
