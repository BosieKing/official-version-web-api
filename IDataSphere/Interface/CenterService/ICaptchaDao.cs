namespace IDataSphere.Interface.CenterService
{
    public interface ICaptchaDao
    {
        Task<string> GetPhone(long userId);
    }
}
