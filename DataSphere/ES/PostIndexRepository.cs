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
        /// 构造函数
        /// </summary>
        /// <param name="elasticClient"></param>
        /// <param name="user"></param>
        public PostIndexRepository(UserProvider user, ElasticClient elasticClient) : base(user, elasticClient)
        {

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
            var query = await _elasticClient.SearchAsync<PostIndex>(s => s.Query(q => q.Term(t => t.Field(f => f.SubType).Value(nameof(PostIndex)))).Skip((input.PageNo - 1) * input.PageSize).Take(input.PageSize));
            return query.Hits.Select(p => p.Source);
        }

        /// <summary>
        /// 获取前10条评论
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<dynamic> GetComment(IdInput input)
        {
            var comment = _elasticClient.Search<PostComentIndex>(comment => comment.Query(cq => cq.HasParent<PostIndex>(post => post.Query(p => p.Term(t => t.Field(f => f.Id).Value(input.Id)))))
                                                                                   .Query(cq => cq.Term(t => t.Field(f => f.ParentComentId).Value(0)))
                                                                                   .Take(10));
            return comment.Hits.Select(p => p.Source);
        }

        /// <summary>
        /// 获取文章详情
        /// </summary>
        /// <param name="postId"></param>
        /// <returns></returns>
        public async Task<dynamic> Details(long postId)
        {
            // 获取阅读量、收藏数量、点赞数

            // 获取文章数据

            // 获取主页详情

            // 获取热门文章

            // 获取最新评论

            // 获取大家都在看

            // 获取最新文章

            // 获取所有专栏
            return true;

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
            LikeChildIndex like = new(input.Id);
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
            FavoriteChildIndex favorite = new(input.Id);
            favorite.FavoriteUserId = _user.GetUserId().ToString();
            favorite.CreatedTime = DateTimeOffset.Now;
            await _elasticClient.IndexAsync(favorite, p => p.Routing(input.Id));
        }
        #endregion

        #region 更新
        #endregion

        #region 删除     
        /// <summary>
        /// 删除帖子
        /// </summary>
        /// <returns></returns>
        public async Task DeletePost(IdInput input)
        {
            // 直接一键删除所有parentid等于这个id的数据
            _elasticClient.DeleteByQuery<PostComentIndex>(p => p.Query(q => q.ParentId(t => t.Id(input.Id))));
        }
        #endregion
    }
}
