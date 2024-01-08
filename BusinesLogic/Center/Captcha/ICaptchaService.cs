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
        Task<DataResponseModel> GraphicCaptchaVerify(string guid, string codeValue);
        Task<DataResponseModel> PhoneCodeVerify(VerificationCodeTypeEnum codeType, string phone, string verifyCode);
        Task<DataResponseModel> SendPhoneCode(VerificationCodeTypeEnum codeType, string phone = "", long userId = 0);
    }
}