﻿using BusinesLogic.BackEnd.RoleManage;
using BusinesLogic.BackEnd.RoleManage.Dto;
using IDataSphere.Interface.BackEnd.RoleManage;
using Microsoft.AspNetCore.Mvc;
using SharedLibrary.Consts;
using SharedLibrary.Enums;
using SharedLibrary.Models.DomainModels;
using SharedLibrary.Models.SharedDataModels;

namespace WebApi_Offcial.Controllers.BackEnd
{
    /// <summary>
    /// 角色控制层
    /// </summary>
    [ApiController]
    [Route("RoleManage")]
    [ApiDescription(SwaggerGroupEnum.BackEnd)]
    public class RoleManageController : BaseController
    {
        #region 构造函数
        private readonly IRoleManageService _roleManageService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        /// <summary>
        /// 构造函数
        /// </summary>
        public RoleManageController(IRoleManageService roleManageService, IHttpContextAccessor httpContextAccessor)
        {
            _roleManageService = roleManageService;
            _httpContextAccessor = httpContextAccessor;
        }
        #endregion

        #region 查询
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet("getRolePage")]
        public async Task<ActionResult<DataResponseModel>> GetRolePage([FromQuery] GetRolePageInput input)
        {
            var data = await _roleManageService.GetRolePage(input);
            return DataResponseModel.SetData(data);
        }

        /// <summary>
        /// 获取角色配置的菜单列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet("getRoleMenu")]
        public async Task<ActionResult<DataResponseModel>> GetRoleMenuList([FromQuery] IdInput input)
        {
            var data = await _roleManageService.GetRoleMenuList(input);
            return DataResponseModel.SetData(data);
        }
        #endregion

        #region 新增
        /// <summary>
        /// 新增角色
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("addRole")]
        public async Task<ActionResult<DataResponseModel>> AddRole([FromBody] AddRoleInput input)
        {
            var data = await _roleManageService.AddRole(input);
            return DataResponseModel.SetData(data);
        }

        /// <summary>
        /// 绑定菜单
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("addRoleMenu")]
        public async Task<ActionResult<DataResponseModel>> addRoleMenu([FromBody] AddRoleMenuInput input)
        {
            string tenantId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimsUserConst.TENANT_ID)?.Value;
            var data = await _roleManageService.AddRoleMenu(input, tenantId);
            return DataResponseModel.SetData(data);
        }
        #endregion

        #region 更新
        /// <summary>
        /// 更新角色
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("updateRole")]
        public async Task<ActionResult<DataResponseModel>> UpdateRole([FromBody] UpdateRoleInput input)
        {
            var data = await _roleManageService.UpdateRole(input);
            return DataResponseModel.SetData(data);
        }
        #endregion

        #region 删除
        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("deleteRole")]
        public async Task<ActionResult<DataResponseModel>> DeleteRole([FromBody] IdInput input)
        {
            var data = await _roleManageService.DeleteRole(input.Id);
            return DataResponseModel.SetData(data);
        }
        #endregion
    }
}
