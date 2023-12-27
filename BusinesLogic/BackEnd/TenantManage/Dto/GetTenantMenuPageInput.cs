using System.ComponentModel.DataAnnotations;

namespace BusinesLogic.BackEnd.TenantManage.Dto
{
    public class GetTenantMenuPageInput
    {
        /// <summary>
        /// 租户Id
        /// </summary>
        [Required(ErrorMessage = "IdRequried")]
        public long TenantId { get; set; }
    }
}
