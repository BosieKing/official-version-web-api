using BusinesLogic.FrontDesk.UserInfoManage.Dto;
using SharedLibrary.NormalizeModel;

namespace BusinesLogic.FrontDesk.UserInfoManage
{
    public interface IUserInfoManageService
    {      

        /// <summary>
        /// 根据id获取用户信息
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<dynamic> GetUserInfo(long userId);
       

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="password"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<bool> UpdatePassword(string password, long userId, string token);

        /// <summary>
        /// 完善资料
        /// </summary>
        /// <param name="input"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<bool> UpdateUserInfo(CompleteUserInfoInput input, long userId);

        /// <summary>
        /// 更新头像
        /// </summary>
        /// <param name="url">文件地址</param>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<bool> UploadAvatar(string url, long userId);
    }
}
