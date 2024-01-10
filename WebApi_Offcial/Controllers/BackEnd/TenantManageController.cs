using BusinesLogic.BackEnd.TenantManage;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model.Commons.Domain;
using Model.Commons.SharedData;
using Model.DTOs.BackEnd.TenantManage;
using SharedLibrary.Enums;
using WebApi_Offcial.ActionFilters.BackEnd;

namespace WebApi_Offcial.Controllers.BackEnd
{
    /// <summary>
    /// 租户管理控制层
    /// </summary>
    [ApiController]
    [Route("TenantManage")]
    [ApiDescription(SwaggerGroupEnum.System)]
    [ServiceFilter(typeof(TenantManageActionFilter))]
    public class TenantManageController : BaseController
    {
        /// <summary>
        /// 租户管理业务服务接口
        /// </summary>
        private readonly ITenantManageService _tenantManagerService;

        /// <summary>
        /// 构造函数
        /// </summary>
        public TenantManageController(ITenantManageService tenantManagerService)
        {
            _tenantManagerService = tenantManagerService;
        }


        #region 构造函数
        #endregion

        #region 查询
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet("getTenantPage")]
        [AllowAnonymous]
        public async Task<ActionResult<ServiceResult>> GetTenantPage([FromQuery] GetTenantPageInput input)
        {
            var data = await _tenantManagerService.GetTenantPage(input);
            return ServiceResult.SetData(data);
        }

        /// <summary>
        /// 租户已配置的菜单
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet("getTenantMenuList")]
        [AllowAnonymous]
        public async Task<ActionResult<ServiceResult>> GetTenantMenuList([FromQuery] IdInput input)
        {
            var data = await _tenantManagerService.GetTenantMenuList(input);
            return ServiceResult.SetData(data);
        }

        /// <summary>
        /// 查询租户的目录下拉
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet("getTenantDirectoryList")]
        [AllowAnonymous]
        public async Task<ActionResult<ServiceResult>> GetTenantDirectoryList([FromQuery] IdInput input)
        {
            var data = await _tenantManagerService.GetTenantDirectoryList(input.Id);
            return ServiceResult.SetData(data);
        }
        #endregion

        #region 新增
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("addTenant")]
        public async Task<ActionResult<ServiceResult>> AddTenant([FromBody] AddTenantInput input)
        {
            var data = await _tenantManagerService.AddTenant(input);
            return ServiceResult.SetData(data);
        }

        /// <summary>
        /// 推送新菜单给租户
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("pushTenantMenu")]
        public async Task<ActionResult<ServiceResult>> PushTenantMenu([FromBody] PushTenantMenuInput input)
        {
            var data = await _tenantManagerService.PushTenantMenu(input);
            return ServiceResult.SetData(data);
        }
        #endregion

        #region 更新
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("updateTenant")]
        public async Task<ActionResult<ServiceResult>> UpdateTenantAsync([FromBody] UpdateTenantInput input)
        {
            var data = await _tenantManagerService.UpdateTenant(input);
            return ServiceResult.SetData(data);
        }

        /// <summary>
        /// 重新生成邀请码
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("uptateInviteCode")]
        public async Task<ActionResult<ServiceResult>> UptateInviteCodeAsync([FromBody] IdInput input)
        {
            var data = await _tenantManagerService.UptateInviteCode(input.Id);
            return ServiceResult.SetData(data);
        }
        #endregion

        #region 删除
        #endregion
    }
}
