using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Repositotys.Tarot
{
    /// <summary>
    /// 塔罗基本牌
    /// </summary>
    [Table(nameof(T_TarotCard))]
    public class T_TarotCard : EntityBaseDO
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 封面路径
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// 牌面内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 所属类型id
        /// </summary>
        public long TarotCardTypeId { get; set; }
    }
}
