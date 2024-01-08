using Model.Commons.SharedData;

namespace Model.DTOs.BackEnd.TenantManage
{
    public class GetTenantPageInput : PageInput
    {
        /// <summary>
        /// 租户名称
        /// </summary>
        public string Name { get; set; } = string.Empty;


        /// <summary>
        /// 租户编号
        /// </summary>
        public string Code { get; set; } = string.Empty;
    }
}
