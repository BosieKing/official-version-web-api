using Elasticsearch.Net;
using IDataSphere.ESContexts;
using IDataSphere.ESContexts.ESIndexs;
using Nest;
using UtilityToolkit.Tools;
namespace DataSphere.ES
{
    /// <summary>
    /// ES链接帮助类
    /// </summary>
    public class ElasticSearchHelper : IElasticSearchHelper
    {
        /// <summary>
        /// 获取基础的配置
        /// </summary>
        /// <param name="indexName"></param>
        /// <returns></returns>`
        protected ConnectionSettings GetBaseClientSetting()
        {
            // 构建单节点连接池，如果后续转成集群架构，请更改为嗅探连接池
            SingleNodeConnectionPool pool = new(new Uri(ConfigSettingTool.ElasticSearchConfig.Connection));
            // 将连接池传入配置文件。
            ConnectionSettings settings = new(pool);
            // 禁止流处理，设置为true可以获得debug信息和原始请求和返回json字符串
            settings.DisableDirectStreaming(true);
            // 默认属性名称，否则会转为小写，导致翻译dsl语句查询失败
            settings.DefaultFieldNameInferrer(p => p);
            // 强制加上?pretty=true请求参数，获得返回的json信息
            settings.PrettyJson(true);
            // 允许超时80
            settings.ConnectionLimit(80);
            settings.PingTimeout(TimeSpan.FromSeconds(15));
            // 数据创建成功后，触发的事件
            settings.OnRequestDataCreated(apiCallDetails =>
            {
                // 回调事件
            });
            // 在请求完成后，触发的事件
            settings.OnRequestCompleted(apiCall =>
            {
                // 由于通过nest调用客户端，不会直接在调用接口报错，所以注册回调判断接口请求不成功则抛出异常
                if (!apiCall.Success)
                {
                    throw new Exception(apiCall.DebugInformation);
                }                
            });
            return settings;
        }

        /// <summary>
        /// 获取连接
        /// </summary>
        /// <returns></returns>
        public ElasticClient GetClient()
        {
            var settings = GetBaseClientSetting();
            PostIndexSetting(settings);
            return new ElasticClient(settings);
        }

        /// <summary>
        /// 帖子模块特殊配置
        /// </summary>
        /// <param name="settings"></param>
        /// <returns></returns>
        private void PostIndexSetting(ConnectionSettings settings)
        {
            // 由于PostIndex存在父子文档
            string parentIndeName = nameof(PostIndex).ToLower();
            settings.DefaultIndex(parentIndeName);
            // DefaultMappingFor：当查找LikeChildIndex指定PostIndex中去，因为父子文档存在同一个Index中
            // RoutingProperty(p => p.Id) 指定路由字段值取PostIndexid，所有postindexid中具有相同值的都会被分配在同一个分片上
            settings.DefaultMappingFor<LikeChildIndex>(m => m.IndexName(parentIndeName));
            settings.DefaultMappingFor<FavoriteChildIndex>(m => m.IndexName(parentIndeName));     
            settings.DefaultMappingFor<PostIndex>(m => m.IndexName(parentIndeName).RoutingProperty(p => p.Id));
            settings.DefaultMappingFor<PostComentIndex>(m => m.IndexName(nameof(PostComentIndex)));
        }
    }
}
