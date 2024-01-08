using System.ComponentModel.DataAnnotations;

namespace Model.DTOs.BackEnd.TenantMenuManage
{
    /// <summary>
    /// 更新租户菜单按钮信息输入类
    /// </summary>
    public class UpdateTenantMenuButtonInput : AddTenantMenuButtonInput
    {
        /// <summary>
        /// Id
        /// </summary>
        [Required(ErrorMessage = "IdRequried")]
        public long Id { get; set; }
    }
}
