using Microsoft.AspNetCore.Mvc;
using Model.Commons.Domain;
using Model.Commons.SharedData;
using Model.DTOs.BackEnd.RoleManage;
using Service.BackEnd.RoleManage;
using SharedLibrary.Consts;
using SharedLibrary.Enums;
using WebApi_Offcial.ActionFilters.BackEnd;

namespace WebApi_Offcial.Controllers.BackEnd
{
    /// <summary>
    /// 角色控制层
    /// </summary>
    [ApiController]
    [Route("RoleManage")]
    [ServiceFilter(typeof(RoleManageActionFilter))]
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
        public async Task<ActionResult<ServiceResult>> GetRolePage([FromQuery] GetRolePageInput input)
        {
            PageResult result = await _roleManageService.GetRolePage(input);
            return ServiceResult.SetData(result);
        }

        /// <summary>
        /// 获取角色配置的菜单列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet("getRoleMenu")]
        public async Task<ActionResult<ServiceResult>> GetRoleMenuList([FromQuery] IdInput input)
        {
            dynamic result = await _roleManageService.GetRoleMenuList(input);
            return ServiceResult.SetData(result);
        }
        #endregion

        #region 新增
        /// <summary>
        /// 新增角色
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("addRole")]
        public async Task<ActionResult<ServiceResult>> AddRole([FromBody] AddRoleInput input)
        {
            var result = await _roleManageService.AddRole(input);
            return ServiceResult.SetData(result);
        }

        /// <summary>
        /// 绑定菜单
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("addRoleMenu")]
        public async Task<ActionResult<ServiceResult>> AddRoleMenu([FromBody] AddRoleMenuInput input)
        {
            string tenantId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimsUserConst.TENANT_ID)?.Value;
            bool result = await _roleManageService.AddRoleMenu(input, tenantId);
            return ServiceResult.SetData(result);
        }
        #endregion

        #region 更新
        /// <summary>
        /// 更新角色
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("updateRole")]
        public async Task<ActionResult<ServiceResult>> UpdateRole([FromBody] UpdateRoleInput input)
        {
            bool result = await _roleManageService.UpdateRole(input);
            return ServiceResult.SetData(result);
        }
        #endregion

        #region 删除
        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("deleteRole")]
        public async Task<ActionResult<ServiceResult>> DeleteRole([FromBody] IdInput input)
        {
            bool result = await _roleManageService.DeleteRole(input.Id);
            return ServiceResult.SetData(result);
        }
        #endregion
    }
}
