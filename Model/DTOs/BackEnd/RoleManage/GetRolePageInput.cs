using Model.Commons.SharedData;

namespace Model.DTOs.BackEnd.RoleManage
{
    /// <summary>
    /// 查询角色表格数据输入类
    /// </summary>
    public class GetRolePageInput : PageInput
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; } = string.Empty;
    }
}
