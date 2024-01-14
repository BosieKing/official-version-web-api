using Microsoft.AspNetCore.Mvc;
using Model.Commons.Domain;
using Model.Commons.SharedData;
using Model.DTOs.BackEnd.TenantMenuManage;
using Service.BackEnd.TenantMenuManage;
using SharedLibrary.Enums;
using WebApi_Offcial.ActionFilters.BackEnd;

namespace WebApi_Offcial.Controllers.BackEnd
{
    /// <summary>
    /// 租户菜单管理控制层
    /// </summary>
    [ApiController]
    [Route("TenantMenuManage")]
    [ApiDescription(SwaggerGroupEnum.BackEnd)]
    [ServiceFilter(typeof(TenantMenuManageActionFilter))]
    public class TenantMenuManageController : BaseController
    {
        #region 构造函数
        private readonly ITenantMenuManageService _tenantMenuManageService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        /// <summary>
        /// 构造函数
        /// </summary>
        public TenantMenuManageController(ITenantMenuManageService tenantMenuManageService, IHttpContextAccessor httpContextAccessor)
        {
            _tenantMenuManageService = tenantMenuManageService;
            _httpContextAccessor = httpContextAccessor;
        }
        #endregion

        #region 查询
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet("getPage")]
        public async Task<ActionResult<ServiceResult>> GetPage([FromQuery] GetTenantMenuPageInput input)
        {
            PageResult result = await _tenantMenuManageService.GetPage(input);
            return ServiceResult.SetData(result);
        }

        /// <summary>
        /// 获取目录下拉
        /// </summary>
        /// <returns></returns>
        [HttpGet("getTenantDirectory")]
        public async Task<ActionResult<ServiceResult>> GetTenantDirectory()
        {
            List<DropdownDataResult> result = await _tenantMenuManageService.GetTenantDirectory();
            return ServiceResult.SetData(result);
        }
        #endregion

        #region 新增
        /// <summary>
        /// 添加目录
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("addTenantDirectory")]
        public async Task<ActionResult<ServiceResult>> AddTenantDirectory([FromBody] AddTenantDirectoryInput input)
        {
            bool result = await _tenantMenuManageService.AddTenantDirectory(input);
            return ServiceResult.SetData(result);
        }

        /// <summary>
        /// 添加菜单
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("addTenantMenu")]
        public async Task<ActionResult<ServiceResult>> AddTenantMenu([FromBody] AddTenantMenuInput input)
        {
            bool result = await _tenantMenuManageService.AddTenantMenu(input);
            return ServiceResult.SetData(result);
        }

        /// <summary>
        /// 添加按钮
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("addTenantMenuButton")]
        public async Task<ActionResult<ServiceResult>> AddTenantMenuButton([FromBody] AddTenantMenuButtonInput input)
        {
            bool result = await _tenantMenuManageService.AddTenantMenuButton(input);
            return ServiceResult.SetData(result);
        }
        #endregion

        #region 更新
        /// <summary>
        /// 更新目录
        /// </summary>  
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("updateTenantDirectory")]
        public async Task<ActionResult<ServiceResult>> UpdateTenantDirectory([FromBody] UpdateTenantDirectoryInput input)
        {
            bool result = await _tenantMenuManageService.UpdateTenantDirectory(input);
            return ServiceResult.SetData(result);
        }

        /// <summary>
        /// 更新菜单
        /// </summary>  
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("updateTenantMenu")]
        public async Task<ActionResult<ServiceResult>> UpdateTenantMenu([FromBody] UpdateTenantMenuInput input)
        {
            bool result = await _tenantMenuManageService.UpdateTenantMenu(input);
            return ServiceResult.SetData(result);
        }

        /// <summary>
        /// 更新按钮
        /// </summary>  
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("updateTenantMenuButton")]
        public async Task<ActionResult<ServiceResult>> UpdateTenantMenuButton([FromBody] UpdateTenantMenuButtonInput input)
        {
            bool result = await _tenantMenuManageService.UpdateTenantMenuButton(input);
            return ServiceResult.SetData(result);
        }
        #endregion

        #region 删除
        /// <summary>
        /// 删除目录
        /// </summary>  
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("deleteTenantDirectory")]
        public async Task<ActionResult<ServiceResult>> DeleteTenantDirectory([FromBody] IdInput input)
        {
            bool result = await _tenantMenuManageService.DeleteTenantDirectory(input.Id);
            return ServiceResult.SetData(result);
        }

        /// <summary>
        /// 删除菜单
        /// </summary>  
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("deleteTenantMenu")]
        public async Task<ActionResult<ServiceResult>> DeleteTenantMenu([FromBody] IdInput input)
        {
            bool result = await _tenantMenuManageService.DeleteTenantMenu(input.Id);
            return ServiceResult.SetData(result);
        }

        /// <summary>
        /// 删除按钮
        /// </summary>  
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("deleteTenantMenuButton")]
        public async Task<ActionResult<ServiceResult>> DeleteTenantMenuButton([FromBody] IdInput input)
        {
            bool result = await _tenantMenuManageService.DeleteTenantMenuButton(input.Id);
            return ServiceResult.SetData(result);
        }
        #endregion
    }
}
