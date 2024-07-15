using Nest;
namespace IDataSphere.ESContexts
{
    /// <summary>
    /// ES连接帮助类
    /// </summary>
    public interface IElasticSearchHelper
    {
        /// <summary>
        /// 获取连接配置
        /// </summary>
        /// <returns></returns>
        ElasticClient GetClient();
    }
}
