using System.ComponentModel.DataAnnotations;

namespace Model.DTOs.BackEnd.TenantManage
{
    /// <summary>
    /// 更新租户信息输入类
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
