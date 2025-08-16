using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using UtilityToolkit.Helpers.WxLogin.Dto;
using UtilityToolkit.Tools;
using UtilityToolkit.Utils;

namespace UtilityToolkit.Helpers.WxLogin
{
    /// <summary>
    /// 微信登录帮助类
    /// </summary>
    public class WxLoginHelper
    {
        /// <summary>
        /// 微信登录
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<(string phone ,string openId)> Login(WxLoginByOpenIdInput input)
        {
            // 测试小程序id
            string appId = "wx20d923cb97bb2abf";
            // 密钥
            string appSecret = "765b08052408626854482802029936b0";

            //// 小程序id
            //string appId = ConfigSettingTool.WXConfig.AppId;
            //// 密钥
            //string appSecret = "ConfigSettingTool.WXConfig.AppSecret";

            // 调用微信接口换取 openid
            using (var client = new HttpClient())
            {
                // 发送请求
                string url = $"https://api.weixin.qq.com/sns/jscode2session?appid={appId}&secret={appSecret}&js_code={input.Code}&grant_type=authorization_code";
                HttpResponseMessage response = await client.GetAsync(url);
                // 读取结果
                string responseBody = await response.Content.ReadAsStringAsync();
                // 解析微信返回
                var wxResult = JsonConvert.DeserializeObject<WxLoginResult>(responseBody);
                if (wxResult.Errcode != 0)
                {
                    throw new Exception($"微信登录失败: {wxResult.Errmsg}");
                }
                if (!input.Phone.IsNullOrEmpty())
                {
                    // 开始解密手机号
                    string phone = DecryptPhoneNumber(input.Phone, wxResult.SessionKey, input.Iv);
                    return (phone, wxResult.Openid);
                }
                else
                {
                    return ("", wxResult.Openid);
                }
            }
        }

        /// <summary>
        /// 解密手机号码
        /// </summary>
        /// <param name="phone">电话号码</param>
        /// <param name="sessionKey"></param>
        /// <param name="iv"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public string DecryptPhoneNumber(string phone, string sessionKey, string iv)
        {
            try
            {
                byte[] key = Convert.FromBase64String(sessionKey);
                byte[] ivBytes = Convert.FromBase64String(iv);
                byte[] data = Convert.FromBase64String(phone);

                using (Aes aes = Aes.Create())
                {
                    aes.Key = key;
                    aes.IV = ivBytes;
                    aes.Mode = CipherMode.CBC;
                    aes.Padding = PaddingMode.PKCS7;

                    ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
                    using (MemoryStream ms = new MemoryStream(data))
                    using (CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                    using (StreamReader sr = new StreamReader(cs))
                    {
                        string json = sr.ReadToEnd();
                        var obj = JObject.Parse(json);
                        return obj["purePhoneNumber"]?.ToString(); // 提取手机号
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("解密失败: " + ex.Message);
            }
        }

    }
}
