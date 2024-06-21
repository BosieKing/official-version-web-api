using Nest;
namespace IDataSphere.ESContexts.ESIndexs
{
    /// <summary>
    /// 帖子索引
    /// </summary>
    [ElasticsearchType(IdProperty = "Id")]
    public class PostIndex : ESIndex
    {
        /// <summary>
        /// 标题
        /// </summary>
        /// 启用ik分词器
        [Text(Name = nameof(Title), Index = true, Analyzer = "ik_max_word")]
        public string Title { get; set; }

        /// <summary>
        /// 帖子内容
        /// </summary>
        public string Context { get; set; }

        /// <summary>
        /// 喜欢子索引
        /// </summary>
        public List<LikeChildIndex> Likes { get; set; } = new();

        /// <summary>
        /// 收藏子索引
        /// </summary>
        public List<FavoriteChildIndex> Favorites { get; set; } = new();

        /// <summary>
        /// 评论子索引
        /// </summary>
        public List<CommentChildIndex> Comments { get; set; } = new();
    }
}
