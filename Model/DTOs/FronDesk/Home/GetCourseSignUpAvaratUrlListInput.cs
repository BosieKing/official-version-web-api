namespace Model.DTOs.FronDesk.Home
{
    /// <summary>
    /// 获取课程的预约列表
    /// </summary>
    public class GetCourseSignUpAvaratUrlListInput
    {
        /// <summary>
        /// 课程id
        /// </summary>
        public long CourseId { get; set; }

        /// <summary>
        /// 获取数量
        /// </summary>
        public int Limit { get; set; }
    }
}
