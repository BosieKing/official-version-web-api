using Model.Commons.SharedData;
using SharedLibrary.Enums;

namespace Model.DTOs.FronDesk.Home
{
    /// <summary>
    /// 获取老师列表
    /// </summary>
    public class GetTeacherListInput : PageInput
    {
        /// <summary>
        /// 舞蹈类型
        /// </summary>
        public DanceTypeEnum? DanceType { get; set; }
    }
}
