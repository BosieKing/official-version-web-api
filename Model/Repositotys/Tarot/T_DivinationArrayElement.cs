using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Repositotys.Tarot
{
    /// <summary>
    /// 占卜牌阵元素
    /// </summary>
    [Table(nameof(T_DivinationArrayElement))]
    public class T_DivinationArrayElement : EntityBaseDO
    {
        /// <summary>
        /// 所属牌阵
        /// </summary>
        public long DivinationArrayId { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; } = string.Empty;
    }
}
