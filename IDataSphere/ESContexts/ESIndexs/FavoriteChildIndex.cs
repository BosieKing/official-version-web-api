using Nest;

namespace IDataSphere.ESContexts.ESIndexs
{
    /// <summary>
    /// 收藏子索引
    /// </summary>
    public class FavoriteChildIndex
    {
        /// <summary>
        /// 喜欢的用户id
        /// </summary>
        [Keyword(Name = nameof(FavoriteChildIndex.FavoriteUserId))]
        public string FavoriteUserId { get; set; }

        /// <summary>
        /// 喜欢的时间
        /// </summary>
        public DateTimeOffset FavoriteTime { get; set; }
    }
}
