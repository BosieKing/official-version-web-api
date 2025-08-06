using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UtilityToolkit.Helpers.WxLogin.Dto
{
    /// <summary>
    /// 请求微信登录验证返回类型
    /// </summary>
    public class WxLoginResult
    {
        /// <summary>
        /// openId，在当前小程序内
        /// </summary>
        [JsonProperty("openid")]
        public string Openid { get; set; }

        /// <summary>
        /// 用户会话的凭证，主要是后面用于获取用户的电话号码等敏感信息
        /// </summary>
        [JsonProperty("session_key")]
        public string SessionKey { get; set; }

        /// <summary>
        /// 错误码
        /// </summary>
        [JsonProperty("errcode")]
        public int Errcode { get; set; }

        /// <summary>
        /// 错误消息
        /// </summary>
        [JsonProperty("errmsg")]
        public string Errmsg { get; set; }
    }
}
