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
        [Column("TeacherId")]
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
        [Column("Title")]
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
        /// 海报图片路径
        /// </summary>
        [MaxLength(255)]
        [Column("PosterUrl")]
        public string PosterUrl { get; set; }

        /// <summary>
        /// 课程描述（可选）
        /// </summary>
        [MaxLength(500)]
        [Column("Description")]
        public string Description { get; set; }

        /// <summary>
        /// 最大参与人数
        /// </summary>
        [Column("MaxParticipants")]
        public int? MaxParticipants { get; set; }

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
        /// 报名表
        /// </summary>
        public List<T_CourseSignUp> CourseSignUp { get; set; }
    }
}