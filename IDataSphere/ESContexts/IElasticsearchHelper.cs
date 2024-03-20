using Nest;
namespace IDataSphere.ESContexts
{
    /// <summary>
    /// ES连接帮助类
    /// </summary>
    public interface IElasticSearchHelper
    {
        /// <summary>
        /// 获取连接
        /// </summary>
        /// <param name="indexName">索引名称</param>
        /// <returns></returns>
        IElasticClient GetClient(string indexName);
    }
}
