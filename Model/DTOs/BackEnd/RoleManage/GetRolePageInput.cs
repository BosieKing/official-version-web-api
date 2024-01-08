using Model.Commons.SharedData;

namespace Model.DTOs.BackEnd.RoleManage
{
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
