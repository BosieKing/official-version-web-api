using System.ComponentModel.DataAnnotations;

namespace BusinesLogic.BackEnd.TenantMenuManage.Dto
{
    public class UpdateTenantMenuButtonInput : AddTenantMenuButtonInput
    {
        /// <summary>
        /// Id
        /// </summary>
        [Required(ErrorMessage = "IdRequried")]
        public long Id { get; set; }
    }
}
