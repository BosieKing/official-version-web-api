using Nest;

namespace IDataSphere.ESContexts.ESIndexs
{
    /// <summary>
    /// 喜欢子索引
    /// </summary>
    public class LikeChildIndex : ESParentChildIndex
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="postId"></param>
        public LikeChildIndex(long postId)
        {
            this.SubType = nameof(LikeChildIndex);
            this.IndexRelations = JoinField.Link<LikeChildIndex>(postId);
        }

        /// <summary>
        /// 喜欢的用户id
        /// </summary>
        [Keyword(Name = nameof(LikeChildIndex.LikeUserId))]
        public string LikeUserId { get; set; }
    }
}
