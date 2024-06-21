using IDataSphere.ESContexts;
using Nest;
using System.Collections.Specialized;
using UtilityToolkit.Tools;
namespace DataSphere
{
    /// <summary>
    /// ES链接帮助类
    /// </summary>
    public class ElasticSearchHelper : IElasticSearchHelper
    {
        /// <summary>
        /// 获取连接
        /// </summary>
        /// <param name="indexName"></param>
        /// <returns></returns>`
        public ElasticClient GetClient(string indexName)
        {
            ConnectionSettings settings = new ConnectionSettings(new Uri(ConfigSettingTool.ElasticSearchConfig.Connection));
            // 默认索引
            settings.DefaultIndex(indexName);
            // 禁止流处理，设置为true可以获得debug信息和原始请求和返回json字符串
            settings.DisableDirectStreaming(true);
            // 默认属性名称，否则会转为小写，导致翻译dsl语句查询失败
            settings.DefaultFieldNameInferrer(p => p);
            // 强制加上?pretty=true请求参数，获得返回的json信息
            settings.PrettyJson(true);
            // 允许超时80
            settings.ConnectionLimit(80);
            // 全局查询过滤器
            //NameValueCollection nameValue = new();
            //nameValue.Add("Key", "Value");
            //settings.GlobalQueryStringParameters(nameValue);
            settings.PingTimeout(TimeSpan.FromSeconds(15));
            settings.OnRequestDataCreated(apiCallDetails =>
            {
                // 回调事件            
            });
            var client = new ElasticClient(settings);
            return client;
        }
    }
}
