using Nest;
namespace IDataSphere.ESContexts
{
    /// <summary>
    /// ES仓储基类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class BaseElasticsearchRepository<T> where T : class
    {
        protected IElasticClient _elasticClient;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="elasticsearchHelper"></param>
        /// <param name="indexName"></param>
        public BaseElasticsearchRepository(IElasticSearchHelper elasticsearchHelper, string indexName)
        {
            _elasticClient = elasticsearchHelper.GetClient(indexName);
            ExistsResponse existRespones = _elasticClient.Indices.Exists(indexName);
            // 索引不存在则创建
            if (!existRespones.Exists)
            {
                // 索引设置
                IIndexState state = new IndexState();
                state.Settings = new IndexSettings()
                {
                    // 副本数量
                    NumberOfReplicas = 1,
                    // 分片数量
                    NumberOfShards = 2,
                    // 索引随机增大
                    RoutingPartitionSize = 1,
                    // 排序
                    Sorting = new SortingSettings()
                    {
                        Fields = nameof(Id),
                        Order = new[] { IndexSortOrder.Descending }
                    },
                };
                // 类型映射
                // 动态模板创造类
                DynamicTemplateContainer dynamicTemplateContainer = new DynamicTemplateContainer();
                // 创造一个模板
                DynamicTemplate nameTemplate = new DynamicTemplate();
                // 匹配data类型的birthData字段，映射为string类型
                nameTemplate.Match = "birthData";
                nameTemplate.MatchMappingType = "data";
                nameTemplate.Mapping = new TextProperty();
                dynamicTemplateContainer.Add("string_", nameTemplate);
                state.Mappings = new TypeMapping()
                {
                    Dynamic = true,
                    DynamicTemplates = dynamicTemplateContainer,
                };
                _elasticClient.Indices.CreateAsync(indexName, p =>
                p.Map<PostIndexRepository>(p => p.AutoMap())
                 .InitializeUsing(state)
                );
                var meoo = _elasticClient.Indices.GetType().GetMethods().Select(p =>
                {                    
                    return p.Name.Contains("Async") ? p.Name.Replace("Async", "") : p.Name;
                }).GroupBy(p => p).Select(p => p.Key).OrderBy(p => p).ToList();
            }
        }
    }
}
