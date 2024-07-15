using IDataSphere.DatabaseContexts;
using IDataSphere.ESContexts.ESIndexs;
using Model.Commons.SharedData;
using Model.DTOs.FronDesk.PostHomePage;
using Nest;

namespace DataSphere.ES
{
    /// <summary>
    /// 帖子仓储
    /// </summary>
    public class PostIndexRepository : BaseElasticsearchRepository<PostIndex>
    {
        #region 构造函数
        /// <summary>
        /// 索引名称（必须小写，否则报错）
        /// </summary>
        private readonly static string indexName = nameof(PostIndex).ToLower();

        /// <summary>
        /// 当前操作人信息
        /// </summary>
        private readonly UserProvider _user;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="elasticsearchHelper"></param>
        /// <param name="indexName"></param>
        public PostIndexRepository(UserProvider user, ElasticClient elasticClient) : base(elasticClient)
        {
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
                                .Relations(r => r.Join("postindex", nameof(LikeChildIndex).ToLower(), nameof(FavoriteChildIndex).ToLower(), nameof(CommentChildIndex).ToLower())))
                    .Keyword(k => k.Name(n => n.Id))
                    .Date(d => d.Name(n => n.CreatedTime));

                // 创建该索引
                // 通过.Index<T>()指定了索引的基础类型，其余从automap中提取映射信息，尝试将这些信息合并到索引的映射中
                var response = _elasticClient.Indices.Create(indexName, p => p.Index<PostIndex>().InitializeUsing(state).Map(m => m
                                                                                                                .AutoMap<LikeChildIndex>()// 自动映射
                                                                                                                .AutoMap<FavoriteChildIndex>()
                                                                                                                .AutoMap<CommentChildIndex>()
                                                                                                                .Properties(props) // 覆盖调整具体属性
                                                                                                                .RoutingField(p => p.Required()) // 设置routing为必填，目的是将父子分片位置同步，其余必须配套配置查阅ESHelper中的PostIndexSetting方法
                                                                                                                ));
                if (!response.IsValid)
                {
                    throw new Exception("索引创建失败");
                }
            }
            this._user = user;
        }
        #endregion

        #region 查询
        /// <summary>
        /// 首页查询
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<dynamic> GetPostPage(PostSearchInput input)
        {
            var a = _elasticClient.Search<LikeChildIndex>(p => p.Routing(input.SearchValue));
            // 根据父文档找到子文档
            var getChild = _elasticClient.Search<LikeChildIndex>(like => like.Query(q => q.HasParent<PostIndex>(p => p.Query(pq => pq.Term(m => m.Field(f => f.Id).Value(input.PaperId))).InnerHits())));
            var child = _elasticClient.Search<LikeChildIndex>(s => s.Query(q => q.HasParent<PostIndex>(f => f.Query(fq => fq.Term(t => t.Field(field => field.Id).Value(1001))))));
            // 根据子文档找到父文档
            var getParent = _elasticClient.Search<PostIndex>(post => post.Query(q => q.HasChild<LikeChildIndex>(like => like.Query(lq => lq.Term(t => t.Field(f => f.Id).Value(input.SearchValue))))));
            var c = getChild.Hits;
            return true;
        }

        /// <summary>
        /// 获取前10条评论
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<dynamic> GetComment(IdInput input)
        {
            var comment = _elasticClient.Search<CommentChildIndex>(comment => comment.Query(cq => cq.HasParent<PostIndex>(post => post.Query(p => p.Term(t => t.Field(f => f.Id).Value(input.Id)))))
                                                                                     .Query(cq => cq.Term(t => t.Field(f => f.ParentComentId).Value(0)))
                                                                                     .Take(10));
            return comment.Hits.Select(p => p.Source);
        }
        #endregion

        #region 新增
        /// <summary>
        /// 添加帖子
        /// </summary>
        /// <returns></returns>
        public async Task AddPost(PostIndex postIndex)
        {
            postIndex.IndexRelations = typeof(PostIndex);
            var c = _elasticClient.Index<PostIndex>(postIndex, p => p.Routing(postIndex.Id));
        }

        /// <summary>
        /// 点赞帖子
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task AddLike(IdInput input)
        {
            LikeChildIndex like = new();
            like.IndexRelations = JoinField.Link<LikeChildIndex>(input.Id);
            like.LikeUserId = _user.GetUserId().ToString();
            like.CreatedTime = DateTimeOffset.Now;
            await _elasticClient.IndexAsync<LikeChildIndex>(like, p => p.Routing(input.Id));
        }

        /// <summary>
        /// 收藏帖子
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task AddFavorite(IdInput input)
        {
            FavoriteChildIndex favorite = new();
            favorite.IndexRelations = JoinField.Link<FavoriteChildIndex>(input.Id);
            favorite.FavoriteUserId = _user.GetUserId().ToString();
            favorite.CreatedTime = DateTimeOffset.Now;
            await _elasticClient.IndexAsync(favorite, p => p.Routing(input.Id));
        }

        /// <summary>
        /// 评论帖子
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task AddCommnet(CommentInput input)
        {
            CommentChildIndex comment = new();
            comment.ParentComentId = input.ParentComentId;
            comment.CommentContent = input.Context;
            comment.CommentUserId = _user.GetUserId().ToString();
            comment.IndexRelations = JoinField.Link<CommentChildIndex>(input.PostId);
            await _elasticClient.IndexAsync(comment, p => p.Routing(input.PostId));
        }
        #endregion

        #region 更新
        #endregion

        #region 删除
        /// <summary>
        /// 删除帖子评论
        /// </summary>
        /// <returns></returns>
        public async Task DeleteComment(IdInput input)
        {
            _elasticClient.DeleteByQuery<CommentChildIndex>(p => p.Query(q => q.Term(t => t.SubType, nameof(CommentChildIndex))));
        }
        #endregion
    }
}
