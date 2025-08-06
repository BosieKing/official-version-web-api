using SharedLibrary.Enums;

namespace Model.DTOs.FronDesk.Home
{
    public class GetUseCardRecodeListInput
    {
        /// <summary>
        /// 卡id
        /// </summary>
        public int CardId { get; set; }  

        /// <summary>
        /// 课程类型枚举
        /// </summary>
        public CourseTypeEnum CourseType { get; set; }
    }
}
