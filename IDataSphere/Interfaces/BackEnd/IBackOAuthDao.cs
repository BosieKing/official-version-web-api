using Model.DTOs.BackEnd.BackEndOAuth;
using Model.Repositotys.Service;

namespace IDataSphere.Interfaces.BackEnd
{
    public interface IBackOAuthDao : IRepository<T_User>
    {
        Task<dynamic> LoginByPassword(BackEndLoginByPasswordInput input);
    }
}
