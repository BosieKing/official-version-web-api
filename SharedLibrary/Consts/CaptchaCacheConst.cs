namespace SharedLibrary.Consts
{
    /// <summary>
    /// 验证码缓存库相关常量
    /// </summary>
    public class CaptchaCacheConst
    {
        /// <summary>
        /// 用户验证码Key。
        /// </summary>
        /// <remarks>Key：验证码类型+常量+电话号码 Value：验证码</remarks>
        public const string VERIF_CODE_KEY = "_Verif_Code_Key_";

        /// <summary>
        /// 用户验证码输入错误次数缓存Key。
        /// </summary>
        /// <remarks>Key：验证码类型+常量+电话号码 Value：出错次数</remarks>
        public const string VERIF_CODE_ERROR_KEY = "_Verif_Code_Error_Key_";

        /// <summary>
        /// 用户滑动验证码Key
        /// </summary>
        /// <remarks>Key：常量拼接随机Guid Value：验证码值</remarks>
        public const string GRAPHIC_CAPTCHA_KEY = "GraphicCaptcha_Key_";

        /// <summary>
        /// 用户密码出错次数Key
        /// </summary>
        /// <remarks>Key：常量拼接电话号码：出错次数</remarks>
        public const string PASSWORD_ERROR_COUNT_KEY = "Password_Error_Count_Key_";


    }
}
