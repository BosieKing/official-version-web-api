using Nest;

namespace IDataSphere.ESContexts.ESIndexs
{
    /// <summary>
    /// 评论子索引
    /// </summary>
    public class CommentChildIndex : ESIndex
    {
        /// <summary>
        /// 评论的用户id
        /// </summary>
        [Keyword(Name = nameof(CommentChildIndex.CommentUserId))]
        public string CommentUserId { get; set; }

        /// <summary>
        /// 评论的内容
        /// </summary>
        [Keyword(Name = nameof(CommentChildIndex.CommentContent))]
        public string CommentContent { get; set; }

        /// <summary>
        /// 父评论id
        /// </summary>
        [Keyword(Name = nameof(CommentChildIndex.ParentComentId))]
        public string ParentComentId { get; set; }

        /// <summary>
        /// 属于的类型
        /// </summary>
        [Keyword(Name = nameof(CommentChildIndex.SubType))]
        public string SubType { get; set; } = nameof(CommentChildIndex);
    }
}
