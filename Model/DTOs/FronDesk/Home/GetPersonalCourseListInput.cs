using SharedLibrary.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTOs.FronDesk.Home
{
    public class GetPersonalCourseListInput
    {
        /// 舞蹈类型
        /// </summary>
        public DanceTypeEnum? DanceType { get; set; }

        /// <summary>
        /// 私教老师id
        /// </summary>
        public long TeacherId { get; set; } = 0;
    }
}
