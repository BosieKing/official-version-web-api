namespace Model.DTOs.FronDesk.Home
{
    public class GetPersonalCoureseTimeListInput
    {
        /// <summary>
        /// 老师id
        /// </summary>
        public long TeacherId { get; set; }

        /// <summary>
        /// 日期
        /// </summary>
        public DateTime CourseDate { get; set; }
    }
}
