using System.ComponentModel.DataAnnotations;
namespace Model.DTOs.BackEnd.TenantMenuManage
{
    /// <summary>
    /// 更新租户菜单信息输入类
    /// </summary>
    public class UpdateTenantMenuInput : AddTenantMenuInput
    {
        /// <summary>
        /// Id
        /// </summary>
        [Required(ErrorMessage = "IdRequried")]
        public long Id { get; set; }
    }

}