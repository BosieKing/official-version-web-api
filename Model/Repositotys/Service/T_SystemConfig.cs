using SharedLibrary.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Repositotys.Service
{
    /// <summary>
    /// 系统配置
    /// </summary>
    [Table("T_SystemConfig")]
    public class T_SystemConfig : EntityBaseDO
    {
        /// <summary>
        /// 提前多少分钟允许取消私教预约，设置为-1代表任何时间端都能取消，设置为0代表不允许取消，
        /// </summary>
        [Display(Name = "私教预约取消提前时间(分钟)")]
        [Range(0, int.MaxValue, ErrorMessage = "提前时间不能为负数")]
        public int PrivateLessonCancelAdvanceMinutes { get; set; } = 60; // 默认提前60分钟可取消

        /// <summary>
        /// 提前多少分钟允许取消课程预约，设置为-1代表任何时间端都能取消，设置为0代表不允许取消，
        /// </summary>
        [Display(Name = "课程预约取消提前时间(分钟)")]
        [Range(0, int.MaxValue, ErrorMessage = "提前时间不能为负数")]
        public int CourseCancelAdvanceMinutes { get; set; } = 30; // 默认提前30分钟可取消

    }
}