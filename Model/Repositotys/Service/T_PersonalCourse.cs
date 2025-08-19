using SharedLibrary.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Repositotys.Service
{
    /// <summary>
    /// 私教课程表
    /// </summary>
    [Table("T_PersonalCourse")]
    public class T_PersonalCourse : EntityBaseDO
    {
        /// <summary>
        /// 老师id
        /// </summary>   
        [Column("TeacherId")]
        public long TeacherId { get; set; }

        /// <summary>
        /// 老师
        /// </summary>
        public T_Teacher Teacher { get; set; }

        /// <summary>
        /// 课程开始时间（仅存储时分秒）
        /// </summary>
        [Required]
        [Column("StartTime")]
        public DateTime StartTime { get; set; }

        /// <summary>
        /// 课程结束时间（仅存储时分秒）
        /// </summary>
        [Required]
        [Column("EndTime")]
        public DateTime EndTime { get; set; }

        /// <summary>
        /// 课程日期（仅存储年月日）
        /// </summary>
        [Required]
        [Column("CourseDate")]
        public DateTime CourseDate { get; set; }

        /// <summary>
        /// 课程描述（可选）
        /// </summary>
        [MaxLength(500)]
        [Column("Description")]
        public string Description { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        [Column("IsActive")]
        public bool IsActive { get; set; } = true;

        /// <summary>
        /// 舞蹈类型
        /// </summary>
        [Column("DanceType")]
        public DanceTypeEnum DanceType { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        [Column("Address")]
        public string Address { get; set; } = string.Empty;

        /// <summary>
        /// 是否被预约
        /// </summary>
        [Column("IsBooking")]
        public bool IsBooking { get; set; } = false;

        /// <summary>
        /// 报名表
        /// </summary>
        public List<T_PersonalCourseSignUp> PersonalCourseSignUp { get; set; }
    }
}