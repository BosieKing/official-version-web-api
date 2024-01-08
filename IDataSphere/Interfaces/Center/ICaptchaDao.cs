namespace IDataSphere.Interfaces.Center
{
    /// <summary>
    /// 验证码业务访问数据接口
    /// </summary>
    public interface ICaptchaDao
    {
        Task<string> GetPhone(long userId);
    }
}
