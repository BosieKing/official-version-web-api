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
        /// 关键词
        /// </summary>
        public string KeyWord { get; set; }

        /// <summary>
        /// 卡牌类型
        /// </summary>
        /// <see cref="SharedLibrary.Enums.TarotCardTypeEnum"/>
        public int TarotCardType {get; set; }

        /// <summary>
        /// 1为正位、2为逆位
        /// </summary>
        /// <see cref="SharedLibrary.Enums.ForwardOrReverse"/>
        public int ForwardOrReverse { get; set; }
    }
}
