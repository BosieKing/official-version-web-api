using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Repositotys.Tarot
{
    /// <summary>
    /// 占卜日志内容
    /// </summary>
    [Table(nameof(TL_DivinationContentLog))]
    public class TL_DivinationContentLog : EntityBaseDO
    {
        /// <summary>
        /// 所属占卜日志id
        /// </summary>
        public long DivinationLogId { get; set; }

        /// <summary>
        /// 所在牌位id
        /// </summary>
        public long DivinationArrayElementId { get; set; }

        /// <summary>
        /// 代表的卡牌id
        /// </summary>
        public long TarotCardId { get; set; }

        /// <summary>
        /// 解读内容
        /// </summary>
        public string Content { get; set; }
    }
}
