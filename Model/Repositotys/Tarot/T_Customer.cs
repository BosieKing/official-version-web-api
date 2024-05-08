using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Repositotys.Tarot
{
    /// <summary>
    /// 占卜人信息
    /// </summary>
    [Table(nameof(T_Customer))]
    public class T_Customer : EntityBaseDO
    {
        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public int Sex { get; set; }

        /// <summary>
        /// 年龄
        /// </summary>
        public int Age { get; set; }

        /// <summary>
        /// 星座
        /// </summary>
        public int ConstellationType { get; set; }

        /// <summary>
        /// Mbit类型
        /// </summary>
        public string Mbit { get; set; }
    }
}
