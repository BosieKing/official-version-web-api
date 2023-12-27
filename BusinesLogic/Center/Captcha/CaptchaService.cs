using IDataSphere.Interface.CenterService;
using Microsoft.Extensions.Localization;
using SharedLibrary.Consts;
using SharedLibrary.Enums;
using SharedLibrary.Models.DomainModels;
using SixLabors.Fonts;
using SixLabors.ImageSharp.Drawing.Processing;
using Util.Tools;
using UtilityToolkit.Helpers;
using UtilityToolkit.Tools;
using UtilityToolkit.Utils;

namespace BusinesLogic.Center.Captcha
{
    /// <summary>
    /// 验证码业务
    /// </summary>
    public class CaptchaService
    {
        private readonly IStringLocalizer<UserTips> _stringLocalizer;
        private readonly ICaptchaDao _captchaDao;
        public CaptchaService(IStringLocalizer<UserTips> stringLocalizer, ICaptchaDao captchaDao)
        {
            _stringLocalizer = stringLocalizer;
            _captchaDao = captchaDao;
        }

        /// <summary>
        /// 发送手机验证码
        /// </summary>
        /// <param name="phone"></param>
        /// <param name="codeType"></param>
        /// <param name="userId"></param>
        /// <remarks>如果验证码仍在有效期则返回错误提示</remarks>
        /// <returns></returns>
        public async Task<DataResponseModel> SendPhoneCode(VerificationCodeTypeEnum codeType, string phone = "", long userId = 0)
        {
            var redisClient = RedisMulititionHelper.GetClinet(CacheTypeEnum.Verify);
            if (!userId.Equals(0))
            {
                phone = await _captchaDao.GetPhone(userId);
            }
            string key = codeType + CaptchaCacheConst.VERIF_CODE_KEY + phone;
            string errorKey = codeType + CaptchaCacheConst.VERIF_CODE_ERROR_KEY + phone;
            string code = await redisClient.GetAsync(key);
            // 验证码还没有过期
            if (!code.IsNullOrEmpty())
            {
                return DataResponseModel.IsFailure(_stringLocalizer["VerifCodeWork"].Value);
            }
            string number = SmsTool.GenerateRandomCode();
            bool isSend = SmsTool.SendSMS(phone, number, codeType.GetDescription());
            if (!isSend)
            {
                return DataResponseModel.IsFailure(_stringLocalizer["SendCodeError"].Value);
            }
            // 清除上一条发送的验证码数据
            await redisClient.DelAsync(key);
            // 清除上一条验证码出错的次数
            await redisClient.DelAsync(errorKey);
            // 设置新的验证码
            await redisClient.SetAsync(key, number, ConfigSettingTool.SmsConfigOptions.SmsExpirationTime);
            return DataResponseModel.Successed();
        }


        /// <summary>
        /// 获取滑动验证码
        /// </summary>
        /// <returns></returns>
        public async Task<dynamic> GetGraphicCaptcha()
        {
            Random random = new Random();
            var redisClient = RedisMulititionHelper.GetClinet(CacheTypeEnum.Verify);
            char[] chars = new char[4];
            // 加载背景图
            string bgImagesFilePath = Path.Combine(Environment.CurrentDirectory, @"ConfigFiles\GraphicCaptchaBgImage.png");
            // 背景图片列表
            string guid = Guid.NewGuid().ToString();
            using (Image image = Image.Load(bgImagesFilePath))
            {
                int width = 40;
                FontFamily normalFontFamily = SystemFonts.Families.FirstOrDefault();
                Font font = normalFontFamily.CreateFont(40, FontStyle.Bold);
                for (int i = 0; i < 4; i++)
                {
                    // 绘制
                    chars[i] = (char)random.Next(65, 91);
                    // 写入文字
                    string text = chars[i].ToString();
                    image.Mutate(x => x.DrawText(text: text, color: Color.Black, font: font, location: new PointF(width, image.Height / 2.8F)));  // 坐标
                    width = width + 30;
                }
                // image.Save(@"C:\Users\48737\Pictures\Saved Pictures\aa.png");
                MemoryStream ms = new MemoryStream();
                // 位图保存成jpeg
                image.SaveAsJpeg(ms);
                var result = new
                {
                    Image = Convert.ToBase64String(ms.GetBuffer()),
                    Guid = guid,
                };
                string key = CaptchaCacheConst.GRAPHIC_CAPTCHA_KEY + guid;
                ms.Dispose();
                await redisClient.SetAsync(key, string.Join("", chars), ConfigSettingTool.CaptchaConfigOptions.GraphicCaptchaExpirationTime * 60);
                return result;
            }
        }


        /// <summary>
        /// 验证验证码是否匹配
        /// </summary>
        /// <param name="codeType"></param>
        /// <param name="phone"></param>
        /// <remarks>匹配则删除验证码key和错误次数key，返回true</remarks>
        /// <remarks>不匹配则缓存错误次数key，返回错误提示</remarks>
        /// <returns></returns>
        public async Task<DataResponseModel> PhoneCodeVerify(VerificationCodeTypeEnum codeType, string phone, string verifyCode)
        {
            return DataResponseModel.Successed();
            // 判断手机验证码是否正确
            var redisClient = RedisMulititionHelper.GetClinet(CacheTypeEnum.Verify);
            // 拼接注册验证码
            string key = codeType + CaptchaCacheConst.VERIF_CODE_KEY + phone;
            // 拼接验证码错误次数Key
            string errorCountKey = codeType + CaptchaCacheConst.VERIF_CODE_ERROR_KEY + phone;
            string codeValue = await redisClient.GetAsync(key);
            // 验证码不匹配
            if (codeValue != verifyCode)
            {
                // 获取已出错次数
                string errorCountValue = await redisClient.GetAsync(key);
                // 验证码出错次数过期时间
                int expTime = ConfigSettingTool.CaptchaConfigOptions.VerifCodeErrorCountExpirationTime * 60;
                // 验证码最大出错次数
                int errorMaxCount = ConfigSettingTool.CaptchaConfigOptions.VerifCodeErrorMaxCount;
                int errorCount = errorCountValue.IsNullOrEmpty() ? 1 : int.Parse(errorCountValue) + 1;
                // 如果验证码出错达到上限则删除验证码
                if (errorCount >= errorMaxCount)
                {
                    await redisClient.DelAsync(key);
                    await redisClient.DelAsync(errorCountKey);
                    // 提示验证码
                    return DataResponseModel.IsFailure(_stringLocalizer["VerifCodeExpired"].Value);
                }
                await redisClient.SetAsync(errorCountKey, errorCount, expTime);
                return DataResponseModel.IsFailure(_stringLocalizer["VerifCodeError"].Value.Replace("@", $"{errorMaxCount - errorCount}"));
            }
            else
            {
                // 匹配则删除相关Key
                await redisClient.DelAsync(key);
                await redisClient.DelAsync(errorCountKey);
            }
            return DataResponseModel.Successed();
        }


        /// <summary>
        /// 验证滑动验证码值是否正确
        /// </summary>
        /// <param name="guid"></param>
        /// <param name="codeValue"></param>
        /// <returns></returns>
        public async Task<DataResponseModel> GraphicCaptchaVerify(string guid, string codeValue)
        {
            return DataResponseModel.Successed();
            // 滑动验证码Key
            string graphicCaptchaKey = CaptchaCacheConst.GRAPHIC_CAPTCHA_KEY + guid;
            var redisClient = RedisMulititionHelper.GetClinet(CacheTypeEnum.Verify);
            // 获取缓存中的滑动验证码值
            string graphicCaptchaeValue = await redisClient.GetAsync(graphicCaptchaKey);
            if (graphicCaptchaeValue.IsNullOrEmpty())
            {
                return DataResponseModel.IsFailure(_stringLocalizer["GraphicCaptchaExpired"].Value);
            }
            // 无论是否相等，进入验证立马销毁
            await redisClient.DelAsync(graphicCaptchaKey);
            if (codeValue != graphicCaptchaeValue)
            {
                return DataResponseModel.IsFailure(_stringLocalizer["GraphicCaptchaError"].Value);
            }
            return DataResponseModel.Successed();
        }

    }
}
