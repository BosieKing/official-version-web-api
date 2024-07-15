using Nest;

namespace IDataSphere.ESContexts.ESIndexs
{
    /// <summary>
    /// 喜欢子索引
    /// </summary>
    public class LikeChildIndex : ESIndex
    {
        /// <summary>
        /// 喜欢的用户id
        /// </summary>
        [Keyword(Name = nameof(LikeChildIndex.LikeUserId))]
        public string LikeUserId { get; set; }

        /// <summary>
        /// 属于的类型
        /// </summary>
        [Keyword(Name = nameof(LikeChildIndex.SubType))]
        public string SubType { get; set; } = nameof(LikeChildIndex);
    }
}
