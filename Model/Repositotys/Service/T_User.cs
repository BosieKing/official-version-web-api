using SharedLibrary.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Repositotys.Service
{
    /// <summary>
    /// 用户表
    /// </summary>
    [Table("T_User")]
    public class T_User : EntityBaseDO
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
        /// 报名参加课程表
        /// </summary>
        public List<T_CourseSignUp> CourseSignUps { get; set; }

        /// <summary>
        /// 报名参加私教表
        /// </summary>
        public List<T_PersonalCourseSignUp> PersonalCourseSignUps { get; set; }

        /// <summary>
        /// 我的卡包
        /// </summary>
        public List<T_MyCard>  MyCards { get; set; }

        /// <summary>
        /// 自我介绍
        /// </summary>
        public string? SelfIntroduce { get; set; } = string.Empty;
    }
}
