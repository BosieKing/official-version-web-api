using System.ComponentModel.DataAnnotations;

namespace BusinesLogic.BackEnd.TenantManage.Dto
{
    /// <summary>
    /// 修改租户输入类
    /// </summary>
    public class UpdateTenantInput : AddTenantInput
    {
        /// <summary>
        /// 租户Id
        /// </summary>
        [Required(ErrorMessage = "IdRequried")]
        public long Id { get; set; }
    }
}
