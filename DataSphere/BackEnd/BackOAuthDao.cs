using IDataSphere.DatabaseContexts;
using IDataSphere.Interfaces.BackEnd;
using IDataSphere.Interfaces.FronDesk;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model.Commons.Domain;
using Model.DTOs.BackEnd.BackEndOAuth;
using Model.Repositotys.Service;
using SharedLibrary.Consts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TencentCloud.Cam.V20190116.Models;
using UtilityToolkit.Helpers.WxLogin.Dto;

namespace DataSphere.BackEnd
{
    /// <summary>
    /// 后台权限管理
    /// </summary>
    public class BackOAuthDao : Repository<T_User>, IBackEndOAuthDao
    {
        #region 构造函数
     
        public BackOAuthDao(SqlDbContext sqlDbContext) : base(sqlDbContext)
        {
        }
        #endregion


        #region 登录相关
        /// <summary>
        /// 电话号码的登录
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<dynamic> LoginByPassword(BackEndLoginByPasswordInput input) 
        { 
        
        
        
        }


        #endregion

        #region 
        #endregion


        #region 
        #endregion


        #region 
        #endregion
    }
}
