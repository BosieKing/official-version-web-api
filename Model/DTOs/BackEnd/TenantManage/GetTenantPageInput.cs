using Model.Commons.SharedData;

namespace Model.DTOs.BackEnd.TenantManage
{
    /// <summary>
    /// 查询租户表格数据输入类
    /// </summary>
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
