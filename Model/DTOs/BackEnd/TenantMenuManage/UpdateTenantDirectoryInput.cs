using System.ComponentModel.DataAnnotations;

namespace Model.DTOs.BackEnd.TenantMenuManage
{
    /// <summary>
    /// 更新租户目录信息输入类
    /// </summary>
    public class UpdateTenantDirectoryInput : AddTenantDirectoryInput
    {
        /// <summary>
        /// Id
        /// </summary>
        [Required(ErrorMessage = "IdRequried")]
        public long Id { get; set; }
    }
}
