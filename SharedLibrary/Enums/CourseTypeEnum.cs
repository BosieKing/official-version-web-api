using System.ComponentModel;

namespace SharedLibrary.Enums
{
    /// <summary>
    /// 班类型
    /// </summary>
    public enum CourseTypeEnum
    {
        /// <summary>
        /// 团课（多人同时上课）15-20人
        /// </summary>
        [Description("团课")]
        Group = 1,

        /// <summary>
        /// 一对一（私教专属课程）
        /// </summary>
        [Description("一对一")]
        OneOnOne = 2,

        /// <summary>
        /// 进修（教师进阶培训）5人
        /// </summary>
        [Description("进修")]
        AdvancedTraining = 3,

        /// <summary>
        /// 精品（小班优质课程）10人
        /// </summary>
        [Description("精品")]
        Premium = 4
    }
}

