using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Repositotys.Tarot
{
    /// <summary>
    /// 牌描述
    /// </summary>
    [Table(nameof(T_TarotCardDescription))]
    public class T_TarotCardDescription : EntityBaseDO
    {
        /// <summary>
        /// 所属牌的id
        /// </summary>
        public long TarotCardId { get; set; }

        /// <summary>
        /// 解释
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        /// <see cref="SharedLibrary.Enums.CardDescriptionTypeEnum"/>
        public int Type { get; set; }
    }
}
