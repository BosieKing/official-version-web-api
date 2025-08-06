using SharedLibrary.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Repositotys.Service
{
    /// <summary>
    /// 用户卡包表
    /// </summary>
    [Table("T_MyCard")]
    public class T_MyCard : EntityBaseDO
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public long UserId { get; set; }

        /// <summary>
        /// 用户导航属性
        /// </summary>
        public virtual T_User User { get; set; }

        /// <summary>
        /// 舞蹈类型
        /// </summary>
        public DanceTypeEnum DanceType { get; set; }

        /// <summary>
        /// 剩余可消费次数
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// 班级类型
        /// </summary>
        public CourseTypeEnum CourseType { get; set; }

        /// <summary>
        /// 是否已激活
        /// </summary>
        public bool IsActivated { get; set; } = true;

        /// <summary>
        /// 有效期开始时间
        /// </summary>
        public DateTime? ValidFrom { get; set; }

        /// <summary>
        /// 有效期结束时间
        /// </summary>
        public DateTime? ValidTo { get; set; }
    }
}