namespace Model.Commons.CoreData
{
    /// <summary>
    /// 黑名单token
    /// </summary>
    public class BlackTokenModel
    {
        /// <summary>
        /// token
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// 刷新token的过期时间
        /// </summary>
        public long ExpirationTime { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="token"></param>
        /// <param name="refreshToken"></param>
        /// <param name="expirationTime"></param>
        public BlackTokenModel(string token, string expirationTime)
        {
            Token = token;
            ExpirationTime = long.Parse(expirationTime);
        }
    }
}
