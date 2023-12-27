using System.ComponentModel.DataAnnotations;
namespace BusinesLogic.BackEnd.UserManage.Dto
{
    public class UpdateUserInput : AddUserInput
    {
        /// <summary>
        /// Id
        /// </summary>
        [Required(ErrorMessage = "IdRequried")]
        public long Id { get; set; }
    }

}