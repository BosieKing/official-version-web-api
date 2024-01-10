using Model.Commons.Domain;
using SharedLibrary.Enums;

namespace BusinesLogic.Center.Captcha
{
    /// <summary>
    /// 验证码业务接口
    /// </summary>
    public interface ICaptchaService
    {
        Task<dynamic> GetGraphicCaptcha();
        Task<ServiceResult> GraphicCaptchaVerify(string guid, string codeValue);
        Task<ServiceResult> PhoneCodeVerify(VerificationCodeTypeEnum codeType, string phone, string verifyCode);
        Task<ServiceResult> SendPhoneCode(VerificationCodeTypeEnum codeType, string phone = "", long userId = 0);
    }
}