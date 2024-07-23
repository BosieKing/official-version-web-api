using Nest;

namespace IDataSphere.ESContexts.ESIndexs
{
    /// <summary>
    /// 收藏子索引
    /// </summary>
    public class FavoriteChildIndex : ESParentChildIndex
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="postId"></param>
        public FavoriteChildIndex(long postId)
        {
            this.SubType = nameof(FavoriteChildIndex);
            this.IndexRelations = JoinField.Link<FavoriteChildIndex>(postId);
        }

        /// <summary>
        /// 收藏的用户id
        /// </summary>
        [Keyword(Name = nameof(FavoriteChildIndex.FavoriteUserId))]
        public string FavoriteUserId { get; set; }
    }
}
