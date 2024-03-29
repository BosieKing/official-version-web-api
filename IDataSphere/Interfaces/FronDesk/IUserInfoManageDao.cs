﻿using Model.Commons.Domain;
using Model.Repositotys;

namespace IDataSphere.Interfaces.FronDesk
{
    /// <summary>
    /// 前台用户中心数据访问接口
    /// </summary>
    /// <remarks>T_User</remarks>
    public interface IUserInfoManageDao : IBaseDao
    {
        Task<dynamic> GetUserInfoById(long userId);
        Task<bool> UpdateAvatar(string url, long userId);
        Task<bool> UpdatePassword(string newPassword, long userId);
        Task<bool> UpdateUserInfo(T_User user);      
    }
}