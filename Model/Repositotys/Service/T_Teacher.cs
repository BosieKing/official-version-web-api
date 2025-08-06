using SharedLibrary.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Repositotys.Service
{
    /// <summary>
    /// 老师
    /// </summary>
    [Table("T_Teacher")]
    public class T_Teacher : EntityBaseDO
    {
        /// <summary>
        /// 用户名
        /// </summary>
        [MaxLength(50)]
        public string NickName { get; set; }

        /// <summary>
        /// 电话号码
        /// </summary>
        [MaxLength(50)]
        public string Phone { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        [MaxLength(200)]
        public string Password { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        [MaxLength(50)]
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// 性别
        /// </summary>
        public SexEnum Sex { get; set; }

        /// <summary>
        /// 头像路径
        /// </summary>
        [MaxLength(200)]
        public string? AvatarUrl { get; set; } = string.Empty;

        /// <summary>
        /// 是否禁止登录
        /// </summary>
        public bool IsDisableLogin { get; set; } = false;

        /// <summary>
        /// 小程序ID
        /// </summary>
        [MaxLength(50)]
        public string OpenId { get; set; }

        /// <summary>
        /// 用户身份类型
        /// </summary>
        public UserIdentityTypeEnum UserIdentityType { get; set; }

        /// <summary>
        /// 舞蹈类型集合（当为老师的时候）
        /// </summary>
        public List<T_TeacherDanceType> DanceTypes { get; set; }

        /// <summary>
        /// 负责的课程（当为老师的时候）
        /// </summary>
        public List<T_Course> Courses { get; set; }

        /// <summary>
        /// 负责的私教（当为老师的时候）
        /// </summary>
        public List<T_PersonalCourse> PersonalCourses { get; set; }

        /// <summary>
        /// 自我介绍
        /// </summary>
        public string? SelfIntroduce { get; set; } = string.Empty;
    }
}
