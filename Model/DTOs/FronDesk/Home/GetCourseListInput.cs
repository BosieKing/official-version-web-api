using SharedLibrary.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTOs.FronDesk.Home
{
    /// <summary>
    /// 获取课程表
    /// </summary>
    public class GetCourseListInput
    {
        /// <summary>
        /// 时间
        /// </summary>
        public DateTime? CourseDate { get; set; }

        /// <summary>
        /// 舞蹈类型
        /// </summary>
        public DanceTypeEnum? DanceType { get; set; }

        /// <summary>
        /// 老师id
        /// </summary>
        public long? TeacherId { get; set; }

        /// <summary>
        /// 获取数量
        /// </summary>
        public int? Limit { get; set; } = 100;
    }
}
