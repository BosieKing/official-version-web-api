using IDataSphere.DatabaseContexts;
using IDataSphere.ESContexts.ESIndexs;
using Nest;

namespace DataSphere.ES
{
    /// <summary>
    /// ES仓储基类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BaseElasticsearchRepository<T> where T : ESParentChildIndex
    {
        protected ElasticClient _elasticClient;

        /// <summary>
        /// 当前操作人信息
        /// </summary>
        protected readonly UserProvider _user;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="elasticsearchHelper"></param>
        /// <param name="indexName"></param>
        public BaseElasticsearchRepository(UserProvider user, ElasticClient elasticClient)
        {
            _elasticClient = elasticClient;
            this._user = user;
        }

        /// <summary>
        /// 判断索引是否存在
        /// </summary>
        /// <param name="indexName"></param>
        /// <returns></returns>
        protected async Task<bool> IndexExist(string indexName)
        {
            var data = await _elasticClient.Indices.ExistsAsync(indexName);
            return data.Exists;
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        protected async Task<bool> AddAsync(T data)
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
        protected async Task DeleteAll()
        {
            _elasticClient.DeleteByQuery<PostIndex>(p => p.Query(q => q.MatchAll()));
        }

        /// <summary>
        /// 创建帖子索引
        /// </summary>
        /// <returns></returns>
        protected void CreatePostIndex()
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
            };
            // 动态映射属性配置
            Func<PropertiesDescriptor<PostIndex>, IPromise<IProperties>> props = props =>
            props.Text(t => t.Name(n => n.Title)
                        .Analyzer("ik_max_word")
                        .Store(true)
                        .Index(true)
                        // .Fields(f => f.Keyword(k => k.Name(n => n.Title).IgnoreAbove(25)))// 给这个title设置一个子标题用于满足精准匹配，配置字符超过25不参与索引
                        )
                .Keyword(k => k.Name(n => n.Context))
                .Keyword(t => t.Name(n => n.Tags))
                // 设定一个relation，用于指向喜欢和收藏子索引
                .Join(j => j.Name(p => p.IndexRelations)
                            .Relations(r => r.Join("postindex", nameof(LikeChildIndex).ToLower(), nameof(FavoriteChildIndex).ToLower(), nameof(PostComentIndex).ToLower())))
                .Keyword(k => k.Name(n => n.Id))
                .Date(d => d.Name(n => n.CreatedTime));
            // 创建该索引
            // 通过.Index<T>()指定了索引的基础类型，其余从automap中提取映射信息，尝试将这些信息合并到索引的映射中
            Func<CreateIndexDescriptor, ICreateIndexRequest> selector = selectory => selectory.Index<PostIndex>().InitializeUsing(state).Map(m => m
                                                                                                            .AutoMap<LikeChildIndex>()// 自动映射
                                                                                                            .AutoMap<FavoriteChildIndex>()
                                                                                                            .AutoMap<PostComentIndex>()
                                                                                                            .Properties(props) // 覆盖调整具体属性
                                                                                                            .RoutingField(p => p.Required())); // 设置routing为必填，目的是将父子分片位置同步，其余必须配套配置查阅ESHelper中的PostIndexSetting方法
            _elasticClient.Indices.Create(nameof(PostIndex).ToLower(), selector);
        }

        /// <summary>
        /// 创建帖子评论索引
        /// </summary>
        protected void CreatePostComment()
        {
            IIndexState state = new IndexState();
            state.Settings = new IndexSettings()
            {
                // 分片
                NumberOfShards = 2,
                // 副本
                NumberOfReplicas = 1,
                // 索引随机增大
                RoutingPartitionSize = 1,
            };
            Func<PropertiesDescriptor<PostComentIndex>, IPromise<IProperties>> properties = properties =>
            properties.Text(t => t.Name(n => n.PostId).Name(n => n.CommentUserId).Name(n => n.CommentContent).Name(n => n.ParentComentId))
                      .Keyword(k => k.Name(n => n.Id))
                      .Date(d => d.Name(n => n.CreatedTime));
            Func<CreateIndexDescriptor, ICreateIndexRequest> selector = selector => selector.Index<PostComentIndex>()
                                                                                            .InitializeUsing(state)
                                                                                            .Map(m => m.Properties(properties).RoutingField(p => p.Required()));
            _elasticClient.Indices.Create(nameof(PostComentIndex).ToLower(), selector);
        }
    }
}
