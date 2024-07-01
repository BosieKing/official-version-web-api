using IDataSphere.ESContexts;
using IDataSphere.ESContexts.ESIndexs;
using Model.DTOs.FronDesk.PostHomePage;
using Nest;
using UtilityToolkit.Helpers;

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
        /// <param name="elasticsearchHelper"></param>
        /// <param name="indexName"></param>
        public PostIndexRepository(IElasticSearchHelper elasticsearchHelper) : base(elasticsearchHelper, indexName)
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
                    // 排序
                    Sorting = new SortingSettings()
                    {
                        Fields = nameof(Id),
                        Order = new[] { IndexSortOrder.Descending }
                    },
                };
                // 创建该索引
                var response = _elasticClient.Indices.Create(indexName, p => p.InitializeUsing(state).Map<PostIndex>(p => p.AutoMap()));
                if (!response.IsValid)
                {
                    throw new Exception("索引创建失败");
                }
            }
        }
        #endregion

        #region 查询
        /// <summary>
        /// 首页查询
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<dynamic> GetPostPage(PostSearchInput input, long userId)
        {
            // 获取当前人关注的用户列表，如果搜寻到的内容包含这些创作者，则加分
            //string[] focusUserIds = RedisMulititionHelper.GetFocusUserIds(userId);
            // 同事


            return true;
        }
        #endregion

        #region 新增
        #endregion

        #region 更新
        #endregion

        #region 删除
        #endregion
    }
}
