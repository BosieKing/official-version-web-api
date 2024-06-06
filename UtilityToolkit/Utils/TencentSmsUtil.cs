using System.Text;
using TencentCloud.Common;
using TencentCloud.Common.Profile;
using TencentCloud.Sms.V20210111;
using TencentCloud.Sms.V20210111.Models;
using UtilityToolkit.Tools;

namespace UtilityToolkit.Utils
{
    /// <summary>
    /// 腾讯云发送短信类
    /// </summary>
    public static class TencentSmsUtil
    {
        /// <summary>
        /// 发送手机验证码短信
        /// </summary>
        /// <param name="phone">发送电话号码</param>
        /// <param name="number">参数：验证码数字</param>
        /// <param name="codeTypeName">参数：验证码类型</param>
        /// <returns></returns>
        public static async Task<bool> SeedVerifCode(string phone, string number, string codeTypeName)
        {
            await SeedMsg(new string[] { phone }, new string[] { number, codeTypeName }, ConfigSettingTool.TencentSmsConfigOptions.SmsTmplId);
            return true;
        }

        /// <summary>
        /// 发送短信
        /// </summary>
        /// <param name="phones"></param>
        /// <param name="paramSet"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static async Task SeedMsg(string[] phones, string[] paramSet, string templateId)
        {
            /* 必要步骤：
             * 实例化一个认证对象，入参需要传入腾讯云账户密钥对 secretId 和 secretKey
             * 本示例采用从环境变量读取的方式，需要预先在环境变量中设置这两个值
             * 您也可以直接在代码中写入密钥对，但需谨防泄露，不要将代码复制、上传或者分享给他人
             * SecretId、SecretKey 查询：https://console.cloud.tencent.com/cam/capi
             */
            Credential cred = new Credential
            {
                SecretId = ConfigSettingTool.TencentSmsConfigOptions.SecretId,
                SecretKey = ConfigSettingTool.TencentSmsConfigOptions.SecretKey
            };
            /* 非必要步骤:
            * 实例化一个客户端配置对象，可以指定超时时间等配置 */
            ClientProfile clientProfile = new ClientProfile();
            /* SDK 默认用 TC3-HMAC-SHA256 进行签名
             * 非必要请不要修改该字段 */
            clientProfile.SignMethod = ClientProfile.SIGN_TC3SHA256;
            /* 非必要步骤
             * 实例化一个客户端配置对象，可以指定超时时间等配置 */
            HttpProfile httpProfile = new HttpProfile();
            /* SDK 默认使用 POST 方法
             * 如需使用 GET 方法，可以在此处设置，但 GET 方法无法处理较大的请求 */
            httpProfile.ReqMethod = "POST";
            /* SDK 有默认的超时时间，非必要请不要进行调整
             * 如有需要请在代码中查阅以获取最新的默认值 */
            httpProfile.Timeout = 10; // 请求连接超时时间，单位为秒(默认60秒)
            /* 指定接入地域域名，默认就近地域接入域名为 sms.tencentcloudapi.com ，也支持指定地域域名访问，例如广州地域的域名为 sms.ap-guangzhou.tencentcloudapi.com */
            httpProfile.Endpoint = "sms.tencentcloudapi.com";

            httpProfile.Timeout = ConfigSettingTool.TencentSmsConfigOptions.SmsExpirationTime;
            // 代理服务器，当您的环境下有代理服务器时设定
            // httpProfile.WebProxy = Environment.GetEnvironmentVariable("HTTPS_PROXY");
            clientProfile.HttpProfile = httpProfile;
            /* 实例化 SMS 的 client 对象
             * 第二个参数是地域信息，可以直接填写字符串ap-guangzhou，支持的地域列表参考 https://cloud.tencent.com/document/api/382/52071#.E5.9C.B0.E5.9F.9F.E5.88.97.E8.A1.A8 */
            SmsClient client = new SmsClient(cred, "ap-guangzhou", clientProfile);
            /* 实例化一个请求对象，根据调用的接口和实际情况，可以进一步设置请求参数
             * 您可以直接查询 SDK 源码确定 SendSmsRequest 有哪些属性可以设置
             * 属性可能是基本类型，也可能引用了另一个数据结构
             * 推荐使用 IDE 进行开发，可以方便地跳转查阅各个接口和数据结构的文档说明 */
            SendSmsRequest req = new SendSmsRequest();


            /* 基本类型的设置:
             * SDK 采用的是指针风格指定参数，即使对于基本类型也需要用指针来对参数赋值
             * SDK 提供对基本类型的指针引用封装函数
             * 帮助链接：
             * 短信控制台：https://console.cloud.tencent.com/smsv2
             * 腾讯云短信小助手：https://cloud.tencent.com/document/product/382/3773#.E6.8A.80.E6.9C.AF.E4.BA.A4.E6.B5.81 */

            // 模板id
            req.TemplateId = templateId;
            // sdkid
            req.SmsSdkAppId = ConfigSettingTool.TencentSmsConfigOptions.SmsSdkAppId;
            // 发送的电话号码
            req.PhoneNumberSet = phones;
            // 替换模板的参数内容
            req.TemplateParamSet = paramSet;
            // 签名
            req.SignName = ConfigSettingTool.TencentSmsConfigOptions.SmsSign;

            SendSmsResponse respones = await client.SendSms(req);
            // 发送失败
            if (respones.SendStatusSet.Any(p => p.Code.ToLower() != "ok"))
            {
                throw new Exception("发送失败");
            }
        }

        /// <summary>
        /// 电话号码--生成6位验证码
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string GenerateRandomCode()
        {
            StringBuilder result = new();
            for (var i = 0; i < 6; i++)
            {
                Random r = new Random(Guid.NewGuid().GetHashCode());
                result.Append(r.Next(0, 10));
            }
            return result.ToString();
        }
    }
}