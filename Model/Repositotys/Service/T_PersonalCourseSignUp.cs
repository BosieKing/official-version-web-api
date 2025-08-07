using SharedLibrary.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Repositotys.Service
{
    /// <summary>
    /// 私教课程报名表
    /// </summary>
    [Table("T_PersonalCourseSignUp")]
    public class T_PersonalCourseSignUp : EntityBaseDO
    {
        /// <summary>
        /// 用户id
        /// </summary>
        [Column("UserId")]
        public long UserId { get; set; }

        /// <summary>
        /// 用户
        /// </summary>
        public T_User User { get; set; }

        /// <summary>
        /// 课程Id
        /// </summary>
        [Column("PersonalCourseId")]
        public long PersonalCourseId { get; set; }

        /// <summary>
        /// 课程
        /// </summary>
        public T_PersonalCourse PersonalCourse { get; set; }

        /// <summary>
        /// 消耗的卡的id
        /// </summary>
        [Column("CardId")]
        public long CardId { get; set; } = 0;

        /// <summary>
        /// 消耗的卡
        /// </summary>
        public T_MyCard Card { get; set; }

        /// <summary>
        /// 是否取消
        /// </summary>
        [Column("IsCancel")]
        public bool IsCancel { get; set; } = false;

        /// <summary>
        /// 是否完成
        /// </summary>
        [Column("IsFinish")]
        public bool IsFinish { get; set; } = false;
    }
}