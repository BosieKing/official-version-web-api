using IDataSphere.Repositoty;
using SharedLibrary.Models.CoreDataModels;

namespace IDataSphere.Interface.FronDesk
{
    /// <summary>
    /// 前台权限管理接口
    /// </summary>
    public interface IFrontDeskOAuthDao : IBaseDao<T_User>
    {
        /// <summary>
        /// 新增用户
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        Task<bool> AddUser(T_User user);

        /// <summary>
        /// 根据邀请码获取租户id
        /// </summary> 
        /// <param name="inviteCode"></param>
        /// <returns></returns>
        Task<(long Id, string Code)> GetIdByInviteCode(string inviteCode);

        /// <summary>
        /// 根据电话号码获取最近登录租户平台的用户信息
        /// </summary>
        /// <param name="phone"></param>
        /// <returns></returns>
        Task<TokenInfoModel> GetUserInfoByPhone(string phone);

        /// <summary>
        /// 无视租户，判断电话号码与密码是否匹配
        /// </summary>
        /// <param name="phone"></param>
        /// <param name="passWord"></param>
        /// <returns></returns>
        Task<bool> PassWordExiste(string phone, string password);

        /// <summary>
        /// 判断电话号码是否存在
        /// </summary>
        /// <param name="Phone"></param>
        /// <param name="tenantId"></param>
        /// <returns></returns>
        Task<bool> PhoneExiste(string Phone, long tenantId = 0);

        /// <summary>
        /// 更新最新一次登录的平台密码
        /// </summary>
        /// <param name="phone"></param>
        /// <param name="newPassword"></param>
        /// <returns></returns>
        Task<bool> UpdateLastLoginPassWord(string phone, string newPassword);

        /// <summary>
        /// 更新登录时间
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<bool> UpdateLastLoginTime(long userId);
    }
}
