using CSRedis;
using Model.Commons.CoreData;
using Model.Commons.Domain;
using SharedLibrary.Consts;
using SharedLibrary.Enums;
using System.IdentityModel.Tokens.Jwt;
using UtilityToolkit.Extensions;
using UtilityToolkit.Tools;
using UtilityToolkit.Utils;

namespace UtilityToolkit.Helpers
{
    /// <summary>
    /// Redis连接帮助类
    /// </summary>
    public class RedisMulititionHelper
    {
        #region 构造函数和初始化
        /// <summary>
        /// 连接最大数量
        /// </summary>
        private const int MAX_CONNECTION_NUM = 4;
        /// <summary>
        /// Redis缓存类型类型
        /// </summary>
        private int CACHE_TYPE;
        /// <summary> 
        /// Redis连接
        /// </summary>
        private CSRedisClient redisClient;
        /// <summary>
        /// Redis连接池
        /// </summary>
        private static List<RedisMulititionHelper> ClinetPool = new List<RedisMulititionHelper>();

        /// <summary>
        /// 私有构造函数
        /// </summary>
        private RedisMulititionHelper()
        {

        }
        /// <summary>
        /// 初始化
        /// </summary>
        static RedisMulititionHelper()
        {
            if (ClinetPool.Count == 0)
            {
                Dictionary<int, string> dic = new Dictionary<int, string>();
                dic.Add((int)CacheTypeEnum.BaseData, ConfigSettingTool.RedisCacheConfigOptions.BaseDateCacheConnection);
                dic.Add((int)CacheTypeEnum.Verify, ConfigSettingTool.RedisCacheConfigOptions.VerifyCacheConnection);
                dic.Add((int)CacheTypeEnum.User, ConfigSettingTool.RedisCacheConfigOptions.UserCacheConnection);
                dic.Add((int)CacheTypeEnum.Distributed, ConfigSettingTool.RedisCacheConfigOptions.DistributeLockConnection);

                for (int i = 0; i < MAX_CONNECTION_NUM; i++)
                {
                    RedisMulititionHelper connection = new RedisMulititionHelper();
                    connection.CACHE_TYPE = dic.First().Key;
                    connection.redisClient = new CSRedisClient(dic.First().Value);
                    ClinetPool.Add(connection);
                    dic.Remove(dic.First().Key);
                }
            }
        }

        /// <summary>
        /// 获取连接
        /// </summary>
        /// <param name="cacheTypeEnum"></param>
        /// <returns></returns>
        public static CSRedisClient GetClinet(CacheTypeEnum cacheTypeEnum)
        {
            return ClinetPool.FirstOrDefault(p => p.CACHE_TYPE == (int)cacheTypeEnum).redisClient;
        }
        #endregion

        #region 基础数据
        /// <summary>
        /// 是否有权访问此接口
        /// </summary>
        /// <param name="roleIds"></param>
        /// <param name="routerName"></param>
        /// <param name="tenantId"></param>
        /// <returns></returns>
        public static bool HasRole(string[] roleIds, string routerName, long tenantId)
        {
            if (roleIds == null || roleIds.Length == 0 || routerName.IsNullOrEmpty() || tenantId == 0)
            {
                return false;
            }
            CSRedisClient client = ClinetPool.FirstOrDefault(p => p.CACHE_TYPE == (int)CacheTypeEnum.BaseData).redisClient;
            string key = BasicDataCacheConst.ROLE_TABLE + tenantId.ToString();
            IEnumerable<string> values = client.HMGet(key, roleIds).Where(p => p != null);
            if (values.Count() == 0)
            {
                return false;
            }
            IEnumerable<DropdownDataResult> routers = values.Select(p => p.ToObject<List<DropdownDataResult>>()).SelectMany(p => p);
            return routers.Count() > 0 && routers.Any(p => p.Name == routerName);
        }
        #endregion

        #region 用户信息
        /// <summary>
        /// 请求人是否是超管
        /// </summary>
        /// <param name="tenantId"></param>
        /// <returns></returns>
        public static bool IsSuperManage(string tenantId)
        {
            CSRedisClient client = ClinetPool.FirstOrDefault(p => p.CACHE_TYPE == (int)CacheTypeEnum.User).redisClient;
            bool result = client.Exists(UserCacheConst.SUPER_MANAGE_KEY + tenantId);
            return result;
        }

        /// <summary>
        /// 请求人是否是超管
        /// </summary>
        /// <param name="tenantId"></param>
        /// <returns></returns>
        public static bool IsSuperManage(string[] tenantIds)
        {
            CSRedisClient client = ClinetPool.FirstOrDefault(p => p.CACHE_TYPE == (int)CacheTypeEnum.User).redisClient;
            string[] keys = tenantIds.Select(p => UserCacheConst.SUPER_MANAGE_KEY + p).ToArray();
            long result = client.Exists(keys);
            return result >= 1;
        }

        /// <summary>
        /// 判断Token是否被拉黑
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public static async Task<bool> IsWork(string userId, string token)
        {
            CSRedisClient client = ClinetPool.FirstOrDefault(p => p.CACHE_TYPE == (int)CacheTypeEnum.User).redisClient;
            string value = await client.HGetAsync(UserCacheConst.USER_BLACK_TOKEN_TABLE, userId);
            if (!value.IsNullOrEmpty())
            {
                List<BlackTokenJsonModel> list = value.ToObject<List<BlackTokenJsonModel>>();
                return list.Any(p => p.Token == token && p.ExpirationTime <= DateTime.Now.Ticks);
            }
            return false;
        }

        /// <summary>
        /// 退出登录，拉黑Token
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public static async Task<bool> LoginOut(string userId, string token)
        {
            CSRedisClient client = ClinetPool.FirstOrDefault(p => p.CACHE_TYPE == (int)CacheTypeEnum.User).redisClient;
            var claims = TokenTool.GetClaims(token);
            BlackTokenJsonModel data = new BlackTokenJsonModel(token, claims.FirstOrDefault(p => p.Type == JwtRegisteredClaimNames.Exp).Value);
            List<BlackTokenJsonModel> newList = new List<BlackTokenJsonModel>();
            if (await client.HExistsAsync(UserCacheConst.USER_BLACK_TOKEN_TABLE, userId.ToString()))
            {
                string value = await client.HGetAsync(UserCacheConst.USER_BLACK_TOKEN_TABLE, userId);
                List<BlackTokenJsonModel> oldList = value.ToObject<List<BlackTokenJsonModel>>();
                newList.AddRange(oldList.Where(p => p.ExpirationTime <= DateTime.Now.Ticks));
                newList.Add(data);
            }
            return await client.HSetAsync(UserCacheConst.USER_BLACK_TOKEN_TABLE, userId.ToString(), newList.ToJson());
        }
        #endregion

        #region  验证信息


        #endregion

        #region 分布式锁
        #endregion

    }
}
