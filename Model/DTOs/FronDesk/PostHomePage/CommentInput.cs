namespace Model.DTOs.FronDesk.PostHomePage
{
    /// <summary>
    /// 评论输入类
    /// </summary>
    public class CommentInput
    {
        /// <summary>
        /// 帖子id
        /// </summary>
        public long PostId { get; set; }

        /// <summary>
        /// 父评论id
        /// </summary>
        public string ParentComentId { get; set; }

        /// <summary>
        /// 评论文本内容
        /// </summary>
        public string Context { get; set; }
    }
}
