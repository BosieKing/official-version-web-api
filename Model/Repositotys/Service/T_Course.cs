using SharedLibrary.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Repositotys.Service
{
    /// <summary>
    /// 课程表
    /// </summary>
    [Table("T_Course")]
    public class T_Course : EntityBaseDO
    {
        /// <summary>
        /// 老师id
        /// </summary>   
        public long TeacherId { get; set; }

        /// <summary>
        /// 老师表
        /// </summary>
        public T_Teacher Teacher { get; set; }

        /// <summary>
        /// 课程标题
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string Title { get; set; }

        /// <summary>
        /// 班级类型（关联CourseTypeEnum枚举）
        /// </summary>
        [Required]
        [Column("CourseType")]
        public CourseTypeEnum CourseType { get; set; }

        /// <summary>
        /// 课程开始时间（仅存储时分秒）
        /// </summary>
        [Required]   
        public TimeSpan StartTime { get; set; }

        /// <summary>
        /// 课程结束时间（仅存储时分秒）
        /// </summary>
        [Required]  
        public TimeSpan EndTime { get; set; }

        /// <summary>
        /// 课程日期（仅存储年月日）
        /// </summary>
        [Required]
        [Column("CourseDate")]
        public DateTime CourseDate { get; set; }

        /// <summary>
        /// 海报图片路径
        /// </summary>
        [MaxLength(255)]
        public string PosterUrl { get; set; }

        /// <summary>
        /// 课程描述（可选）
        /// </summary>
        [MaxLength(500)]
        public string Description { get; set; }

        /// <summary>
        /// 最大参与人数
        /// </summary>
        public int? MaxParticipants { get; set; }

        /// <summary>
        /// 课程价格（单位：分）
        /// </summary>
        public int Price { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsActive { get; set; } = true;

        /// <summary>
        /// 舞蹈类型
        /// </summary>
        public DanceTypeEnum DanceType { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        public string Address { get; set; } = string.Empty;

        /// <summary>
        /// 报名表
        /// </summary>
        public List<T_CourseSignUp> CourseSignUp { get; set; }
    }
}
