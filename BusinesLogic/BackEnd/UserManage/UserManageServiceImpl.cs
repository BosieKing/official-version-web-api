using BusinesLogic.BackEnd.UserManage.Dto;
using IDataSphere.Interface.BackEnd.UserManage;
using IDataSphere.Repositoty;
using Mapster;
using SharedLibrary.Consts;
using SharedLibrary.Enums;
using SharedLibrary.Models.DomainModels;
using UtilityToolkit.Helpers;

namespace BusinesLogic.BackEnd.UserManage
{
    /// <summary>
    /// 用户管理业务类
    /// </summary>
    public class UserManageServiceImpl : IUserManageService
    {
        #region 构造函数
        private readonly IUserManageDao _userManageDao;
        public UserManageServiceImpl(IUserManageDao userManageDao)
        {
            _userManageDao = userManageDao;
        }
        #endregion

        #region 查询
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<PaginationResultModel> GetUserPage(GetUserPageInput input)
        {
            return await _userManageDao.GetUserPage(input);
        }

        /// <summary>
        /// 查询角色列表
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<List<DropdownSelectionModel>> GetUserRoleList(long id)
        {
            return await _userManageDao.GetUserRoleList(id);
        }
        #endregion

        #region 新增
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<bool> AddUser(AddUserInput input)
        {
            T_User data = input.Adapt<T_User>();
            return await _userManageDao.AddAsync(data);
        }

        /// <summary>
        /// 绑定角色
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<bool> AddUserRole(AddUserRoleInput input)
        {
            await _userManageDao.BatchDeleteAsync<T_UserRole>(p => p.UserId == input.UserId);
            if (input.RoleIds.Count() > 0)
            {
                var list = input.RoleIds.Select(p => new T_UserRole { UserId = input.UserId, RoleId = p });
                await RedisMulititionHelper.GetClinet(CacheTypeEnum.User).HSetAsync(UserCacheConst.MAKE_IN_TABLE, input.UserId.ToString(), input.UserId.ToString());
                return await _userManageDao.BatchAddAsync<T_UserRole>(list.ToList());
            }
            else
            {
                return true;
            }
        }
        #endregion

        #region 更新
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<bool> UpdateUser(UpdateUserInput input)
        {
            T_User data = input.Adapt<T_User>();
            data.Id = input.Id;
            return await _userManageDao.UpdateUser(data);
        }

        /// <summary>
        /// 更新密码
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<bool> ResetPassword(ResetPasswordInput input)
        {
            return await _userManageDao.ResetPassword(input.UserId, input.Password);
        }     
        #endregion

        #region 删除
        /// <summary>
        /// 更新是否允许登录
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> UpdateIsDisableLogin(UpdateIsDisableLoginInput input)
        {
            return await _userManageDao.UpdateIsDisableLogin(input.UserId, input.IsDisableLogin);
        }
        #endregion
    }
}
