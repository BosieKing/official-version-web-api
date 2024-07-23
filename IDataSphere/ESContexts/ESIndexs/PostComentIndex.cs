using Nest;

namespace IDataSphere.ESContexts.ESIndexs
{
    /// <summary>
    /// 评论内容索引
    /// </summary>
    public class PostComentIndex : ESBaseIndex
    {
        /// <summary>
        /// 所属的帖子id
        /// </summary>
        [Keyword(Name = nameof(PostComentIndex.PostId))]
        public string PostId { get; set; }

        /// <summary>
        /// 评论的用户id
        /// </summary>
        [Keyword(Name = nameof(PostComentIndex.CommentUserId))]
        public string CommentUserId { get; set; }

        /// <summary>
        /// 评论的内容
        /// </summary>
        [Keyword(Name = nameof(PostComentIndex.CommentContent))]
        public string CommentContent { get; set; }

        /// <summary>
        /// 父评论id
        /// </summary>
        [Keyword(Name = nameof(PostComentIndex.ParentComentId))]
        public string ParentComentId { get; set; }
    }
}
