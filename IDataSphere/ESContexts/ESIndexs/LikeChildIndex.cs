using Nest;

namespace IDataSphere.ESContexts.ESIndexs
{
    /// <summary>
    /// 喜欢子索引
    /// </summary>
    public class LikeChildIndex
    {
        /// <summary>
        /// 喜欢的用户id
        /// </summary>
        [Keyword(Name = nameof(LikeChildIndex.LikeUserId))]
        public string LikeUserId { get; set; }
   
        /// <summary>
        /// 喜欢的时间
        /// </summary>
        public DateTimeOffset LikeTime { get; set; }
    }
}
