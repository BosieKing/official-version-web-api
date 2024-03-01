using Model.ESIndexs;
using Nest;

namespace IDataSphere.ESContexts
{
    /// <summary>
    /// 错误日志仓储
    /// </summary>
    public class PostIndexRepository : BaseElasticsearchRepository<PostIndex>
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="elasticsearchHelper"></param>
        /// <param name="indexName"></param>
        public PostIndexRepository(IElasticsearchHelper elasticsearchHelper, string indexName) : base(elasticsearchHelper, indexName)
        {    
        }

        
    }
}
