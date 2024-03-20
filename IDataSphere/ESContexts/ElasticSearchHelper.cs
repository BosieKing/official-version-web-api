using Elasticsearch.Net;
using Nest;
using System.Collections.Specialized;
using UtilityToolkit.Tools;
namespace IDataSphere.ESContexts
{
    public class ElasticSearchHelper : IElasticSearchHelper
    {
        /// <summary>
        /// 获取连接
        /// </summary>
        /// <param name="indexName"></param>
        /// <returns></returns>`
        public IElasticClient GetClient(string indexName)
        {
            ConnectionSettings settings = new ConnectionSettings(new Uri(ConfigSettingTool.ElasticSearchConfig.Connection));
            settings.DefaultIndex(indexName);
            settings.DisableDirectStreaming(true);
            settings.DefaultFieldNameInferrer(p => p);
            settings.PrettyJson(true);
            settings.ConnectionLimit(80);
            settings.DefaultDisableIdInference(true);
            NameValueCollection nameValue = new();
            nameValue.Add("Key", "Value");
            settings.GlobalQueryStringParameters(nameValue);
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
