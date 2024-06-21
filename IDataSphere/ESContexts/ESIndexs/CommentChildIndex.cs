using Nest;

namespace IDataSphere.ESContexts.ESIndexs
{
    /// <summary>
    /// 评论子索引
    /// </summary>
    public class CommentChildIndex
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
        /// 评论的时间
        /// </summary>
        public DateTimeOffset CommentTime { get; set; }

    }
}
