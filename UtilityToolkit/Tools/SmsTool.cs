using System.Text;

namespace Util.Tools
{
    /// <summary>
    /// 短信
    /// </summary>
    public static class SmsTool
    {
        /// <summary>
        /// 发送验证码
        /// </summary>
        /// <param name="phoneNumber">手机号码</param>
        /// <param name="number">随机数</param>
        /// <param name="codeType">验证码类型</param>
        /// <returns></returns>
        public static bool SendSMS(string phoneNumber, string number, string codeType)
        {
            bool isSuccess = true;

            //try
            //{
            //    /* 必要步骤：
            //     * 实例化一个认证对象，入参需要传入腾讯云账户密钥对 secretId 和 secretKey
            //     * 本示例采用从环境变量读取的方式，需要预先在环境变量中设置这两个值
            //     * 您也可以直接在代码中写入密钥对，但需谨防泄露，不要将代码复制、上传或者分享给他人
            //     * SecretId、SecretKey 查询：https://console.cloud.tencent.com/cam/capi
            //     */
            //    Credential cred = new Credential
            //    {
            //        SecretId = ConfigSettingTool.SmsConfigOptions.SecretId,
            //        SecretKey = ConfigSettingTool.SmsConfigOptions.SecretKey
            //    };
            //    /* 非必要步骤:
            //     * 实例化一个客户端配置对象，可以指定超时时间等配置 */
            //    ClientProfile clientProfile = new ClientProfile();
            //    /* SDK 默认用 TC3-HMAC-SHA256 进行签名
            //     * 非必要请不要修改该字段 */
            //    //clientProfile.SignMethod = ClientProfile.SIGN_TC3SHA256;
            //    /* 非必要步骤
            //     * 实例化一个客户端配置对象，可以指定超时时间等配置 */
            //    //HttpProfile httpProfile = new HttpProfile();
            //    /* SDK 默认使用 POST 方法
            //     * 如需使用 GET 方法，可以在此处设置，但 GET 方法无法处理较大的请求 */
            //    //httpProfile.ReqMethod = "POST";
            //    /* SDK 有默认的超时时间，非必要请不要进行调整
            //     * 如有需要请在代码中查阅以获取最新的默认值 */
            //    //httpProfile.Timeout = 10; // 请求连接超时时间，单位为秒(默认60秒)
            //    /* 指定接入地域域名，默认就近地域接入域名为 sms.tencentcloudapi.com ，也支持指定地域域名访问，例如广州地域的域名为 sms.ap-guangzhou.tencentcloudapi.com */
            //    //httpProfile.Endpoint = "sms.tencentcloudapi.com";
            //    // 代理服务器，当您的环境下有代理服务器时设定
            //    // httpProfile.WebProxy = Environment.GetEnvironmentVariable("HTTPS_PROXY");
            //    //clientProfile.HttpProfile = httpProfile;
            //    /* 实例化 SMS 的 client 对象
            //     * 第二个参数是地域信息，可以直接填写字符串ap-guangzhou，支持的地域列表参考 https://cloud.tencent.com/document/api/382/52071#.E5.9C.B0.E5.9F.9F.E5.88.97.E8.A1.A8 */
            //    SmsClient client = new SmsClient(cred, "ap-guangzhou", clientProfile);
            //    /* 实例化一个请求对象，根据调用的接口和实际情况，可以进一步设置请求参数
            //     * 你可以直接查询SDK源码确定SendSmsRequest有哪些属性可以设置
            //     * 属性可能是基本类型，也可能引用了另一个数据结构
            //     * 推荐使用IDE进行开发，可以方便的跳转查阅各个接口和数据结构的文档说明 */
            //    SendSmsRequest req = new SendSmsRequest();

            //    /* 基本类型的设置:
            //     * SDK采用的是指针风格指定参数，即使对于基本类型你也需要用指针来对参数赋值。
            //     * SDK提供对基本类型的指针引用封装函数
            //     * 帮助链接：
            //     * 短信控制台: https://console.cloud.tencent.com/smsv2
            //     * 腾讯云短信小助手: https://cloud.tencent.com/document/product/382/3773#.E6.8A.80.E6.9C.AF.E4.BA.A4.E6.B5.81 */
            //    /* 短信应用ID: 短信SdkAppId在 [短信控制台] 添加应用后生成的实际SdkAppId，示例如1400006666 */
            //    // 应用 ID 可前往 [短信控制台](https://console.cloud.tencent.com/smsv2/app-manage) 查看
            //    req.SmsSdkAppId = ConfigSettingTool.SmsConfigOptions.SmsSdkId;

            //    /* 短信签名内容: 使用 UTF-8 编码，必须填写已审核通过的签名 */
            //    // 签名信息可前往 [国内短信](https://console.cloud.tencent.com/smsv2/csms-sign) 或 [国际/港澳台短信](https://console.cloud.tencent.com/smsv2/isms-sign) 的签名管理查看
            //    req.SignName = ConfigSettingTool.SmsConfigOptions.SmsSign;

            //    /* 模板 ID: 必须填写已审核通过的模板 ID */
            //    // 模板 ID 可前往 [国内短信](https://console.cloud.tencent.com/smsv2/csms-template) 或 [国际/港澳台短信](https://console.cloud.tencent.com/smsv2/isms-template) 的正文模板管理查看
            //    req.TemplateId = ConfigSettingTool.SmsConfigOptions.SmsTmplId;

            //    /* 模板参数: 模板参数的个数需要与 TemplateId 对应模板的变量个数保持一致，若无模板参数，则设置为空 */
            //    req.TemplateParamSet = new string[] { number, codeType };

            //    /* 下发手机号码，采用 E.164 标准，+[国家或地区码][手机号]
            //     * 示例如：+8613711112222， 其中前面有一个+号 ，86为国家码，13711112222为手机号，最多不要超过200个手机号*/
            //    req.PhoneNumberSet = new string[] { phoneNumber };

            //    /* 用户的 session 内容（无需要可忽略）: 可以携带用户侧 ID 等上下文信息，server 会原样返回 */
            //    req.SessionContext = "";

            //    /* 短信码号扩展号（无需要可忽略）: 默认未开通，如需开通请联系 [腾讯云短信小助手] */
            //    req.ExtendCode = "";

            //    /* 国际/港澳台短信 senderid（无需要可忽略）: 国内短信填空，默认未开通，如需开通请联系 [腾讯云短信小助手] */
            //    req.SenderId = "";

            //    SendSmsResponse resp = client.SendSmsSync(req);

            //    // 输出json格式的字符串回包
            //    //Console.WriteLine(AbstractModel.ToJsonString(resp));

            //    /* 当出现以下错误码时，快速解决方案参考
            //     * [FailedOperation.SignatureIncorrectOrUnapproved](https://cloud.tencent.com/document/product/382/9558#.E7.9F.AD.E4.BF.A1.E5.8F.91.E9.80.81.E6.8F.90.E7.A4.BA.EF.BC.9Afailedoperation.signatureincorrectorunapproved-.E5.A6.82.E4.BD.95.E5.A4.84.E7.90.86.EF.BC.9F)
            //     * [FailedOperation.TemplateIncorrectOrUnapproved](https://cloud.tencent.com/document/product/382/9558#.E7.9F.AD.E4.BF.A1.E5.8F.91.E9.80.81.E6.8F.90.E7.A4.BA.EF.BC.9Afailedoperation.templateincorrectorunapproved-.E5.A6.82.E4.BD.95.E5.A4.84.E7.90.86.EF.BC.9F)
            //     * [UnauthorizedOperation.SmsSdkAppIdVerifyFail](https://cloud.tencent.com/document/product/382/9558#.E7.9F.AD.E4.BF.A1.E5.8F.91.E9.80.81.E6.8F.90.E7.A4.BA.EF.BC.9Aunauthorizedoperation.smssdkappidverifyfail-.E5.A6.82.E4.BD.95.E5.A4.84.E7.90.86.EF.BC.9F)
            //     * [UnsupportedOperation.ContainDomesticAndInternationalPhoneNumber](https://cloud.tencent.com/document/product/382/9558#.E7.9F.AD.E4.BF.A1.E5.8F.91.E9.80.81.E6.8F.90.E7.A4.BA.EF.BC.9Aunsupportedoperation.containdomesticandinternationalphonenumber-.E5.A6.82.E4.BD.95.E5.A4.84.E7.90.86.EF.BC.9F)
            //     * 更多错误，可咨询[腾讯云助手](https://tccc.qcloud.com/web/im/index.html#/chat?webAppId=8fa15978f85cb41f7e2ea36920cb3ae1&title=Sms)
            //     */
            //    var code = resp.SendStatusSet[0].Code.ToUpper();
            //    if (!code.Equals("OK"))
            //        isSuccess = false;

            //}
            //catch (Exception e)
            //{
            //    isSuccess = false;
            //}
            return isSuccess;
        }

        /// <summary>
        /// 电话号码--生成6位验证码
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string GenerateRandomCode()
        {
            var result = new StringBuilder();
            for (var i = 0; i < 6; i++)
            {
                var r = new Random(Guid.NewGuid().GetHashCode());
                result.Append(r.Next(0, 10));
            }
            return result.ToString();
        }
    }
}
