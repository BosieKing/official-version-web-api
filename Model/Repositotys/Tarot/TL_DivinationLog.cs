using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Repositotys.Tarot
{
    /// <summary>
    /// 占卜日志
    /// </summary>
    [Table(nameof(TL_DivinationLog))]
    public class TL_DivinationLog : EntityBaseDO
    {
        /// <summary>
        /// 占卜人
        /// </summary>
        public long UserId { get; set; }

        /// <summary>
        /// 占卜时间
        /// </summary>
        public DateTime DivinationTime { get; set; }

        /// <summary>
        /// 占卜类型
        /// </summary>
        public long DivinationTypeId { get; set; }

        /// <summary>
        /// 使用的牌阵
        /// </summary>
        public long DivinationArrayId { get; set; }

    }
}
