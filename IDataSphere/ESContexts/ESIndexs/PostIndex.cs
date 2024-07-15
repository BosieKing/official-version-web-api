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
        /// Index = 允许索引
        /// Store = 启用存储（默认情况下，字段值被索引可以允许他们能被搜索到，但是不会被store存储，即允许查询，但是不允许通过查询返回这个值）
        /// Analyzer = 启用ik分词器
        [Text(Name = nameof(Title), Index = true, Store = true, Analyzer = "ik_max_word")]
        public string Title { get; set; }

        /// <summary>
        /// 帖子内容
        /// </summary>
        [Keyword(Name = nameof(Context))]
        public string Context { get; set; }

        /// <summary>
        /// 标签
        /// </summary>
        public string[] Tags { get; set; }
    }
}
