
using Nest;
namespace IDataSphere.ESContexts
{
    /// <summary>
    /// 帖子索引
    /// </summary>
    public class PostIndex
    {
        /// <summary>
        /// id
        /// </summary>
        [Text(Name = nameof(PostIndex.Title), Index = true, Analyzer = "ik_max_word")]
        public long  Id { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        [Text()]
        public string Title { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        public string Context { get; set; }

        /// <summary>
        /// 标签
        /// </summary>
        public string[] Tags { get; set; }
    }
}
