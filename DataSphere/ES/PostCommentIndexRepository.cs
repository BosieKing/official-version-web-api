using IDataSphere.DatabaseContexts;
using IDataSphere.ESContexts.ESIndexs;
using Model.Commons.SharedData;
using Model.DTOs.FronDesk.PostHomePage;
using Nest;

namespace DataSphere.ES
{
    /// <summary>
    /// 帖子评论仓储
    /// </summary>
    public class PostCommentIndexRepository : BaseElasticsearchRepository<PostIndex>
    {
        #region 构造函数
        /// <summary>
        /// 索引名称（必须小写，否则报错）
        /// </summary>
        private readonly static string indexName = nameof(PostComentIndex).ToLower();

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="elasticClient"></param>
        /// <param name="user"></param>
        public PostCommentIndexRepository(UserProvider user, ElasticClient elasticClient) : base(user,elasticClient)
        {            
         
        }
        #endregion

        #region 查询      
        /// <summary>
        /// 获取前10条评论
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<dynamic> GetComment(IdInput input)
        {
            var comment = _elasticClient.Search<PostComentIndex>(comment => comment.Query(cq => cq.Term(t => t.Field(f => f.ParentComentId).Value(0))).Take(10));
            return comment.Hits.Select(p => p.Source);
        }
        #endregion

        #region 新增
        /// <summary>
        /// 评论帖子
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task AddCommnet(CommentInput input)
        {
            PostComentIndex comment = new();
            comment.PostId = input.PostId.ToString();
            comment.ParentComentId = input.ParentComentId;
            comment.CommentContent = input.Context;
            comment.CommentUserId = _user.GetUserId().ToString();
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
            _elasticClient.DeleteByQuery<PostComentIndex>(p => p.Query(q => q.Term(t => t.Id, input.Id)));
        }      
        #endregion
    }
}
