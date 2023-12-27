using System.ComponentModel.DataAnnotations;

namespace BusinesLogic.BackEnd.RoleManage.Dto
{
    public class UpdateRoleInput : AddRoleInput
    {
        /// <summary>
        /// Id
        /// </summary>
        [Required(ErrorMessage = "IdRequried")]
        public long Id { get; set; }
    }
}
