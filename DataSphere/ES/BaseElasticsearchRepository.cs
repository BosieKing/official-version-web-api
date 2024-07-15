using IDataSphere.ESContexts;
using IDataSphere.ESContexts.ESIndexs;
using Nest;

namespace DataSphere.ES
{
    /// <summary>
    /// ES仓储基类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BaseElasticsearchRepository<T> where T : ESIndex
    {
        protected ElasticClient _elasticClient;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="elasticsearchHelper"></param>
        /// <param name="indexName"></param>
        public BaseElasticsearchRepository(ElasticClient elasticClient)
        {
            _elasticClient = elasticClient;
        }


        #region
        
        #endregion

        /// <summary>
        /// 判断索引是否存在
        /// </summary>
        /// <param name="indexName"></param>
        /// <returns></returns>
        public async Task<bool> IndexExist(string indexName)
        {
            var data = await _elasticClient.Indices.ExistsAsync(indexName);
            return data.Exists;
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        public async Task<bool> AddAsync(T data)
        {
            IndexResponse result = await _elasticClient.IndexDocumentAsync(data);
            if (!result.ApiCall.Success)
            {
                throw new Exception("插入错误");
            }
            return true;
        }

        /// <summary>
        /// 清空所有doc
        /// </summary>
        /// <returns></returns>
        public async Task DeleteAll()
        {
            _elasticClient.DeleteByQuery<PostIndex>(p => p.Query(q => q.MatchAll()));
        }
    }
}
