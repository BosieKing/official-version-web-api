using Nest;

namespace IDataSphere.ESContexts.ESIndexs
{
    /// <summary>
    /// 收藏子索引
    /// </summary>
    public class FavoriteChildIndex : ESIndex
    {
        /// <summary>
        /// 收藏的用户id
        /// </summary>
        [Keyword(Name = nameof(FavoriteChildIndex.FavoriteUserId))]
        public string FavoriteUserId { get; set; }

        /// <summary>
        /// 属于的类型
        /// </summary>
        [Keyword(Name = nameof(FavoriteChildIndex.SubType))]
        public string SubType { get; set; } = nameof(FavoriteChildIndex);
    }
}
