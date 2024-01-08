using BusinesLogic.BackEnd.TenantMenuManage;
using Microsoft.AspNetCore.Mvc;
using Model.Commons.Domain;
using Model.Commons.SharedData;
using Model.DTOs.BackEnd.TenantMenuManage;
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
        public async Task<ActionResult<DataResponseModel>> GetPage([FromQuery] GetTenantMenuPageInput input)
        {
            var data = await _tenantMenuManageService.GetPage(input);
            return DataResponseModel.SetData(data);
        }

        /// <summary>
        /// 获取目录下拉
        /// </summary>
        /// <returns></returns>
        [HttpGet("getTenantDirectory")]
        public async Task<ActionResult<DataResponseModel>> GetTenantDirectory()
        {
            var data = await _tenantMenuManageService.GetTenantDirectory();
            return DataResponseModel.SetData(data);
        }
        #endregion

        #region 新增
        /// <summary>
        /// 添加目录
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("addTenantDirectory")]
        public async Task<ActionResult<DataResponseModel>> AddTenantDirectory([FromBody] AddTenantDirectoryInput input)
        {
            var result = await _tenantMenuManageService.AddTenantDirectory(input);
            return DataResponseModel.SetData(result);
        }

        /// <summary>
        /// 添加菜单
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("addTenantMenu")]
        public async Task<ActionResult<DataResponseModel>> AddTenantMenu([FromBody] AddTenantMenuInput input)
        {
            var result = await _tenantMenuManageService.AddTenantMenu(input);
            return DataResponseModel.SetData(result);
        }

        /// <summary>
        /// 添加按钮
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("addTenantMenuButton")]
        public async Task<ActionResult<DataResponseModel>> AddTenantMenuButton([FromBody] AddTenantMenuButtonInput input)
        {
            var result = await _tenantMenuManageService.AddTenantMenuButton(input);
            return DataResponseModel.SetData(result);
        }
        #endregion

        #region 更新
        /// <summary>
        /// 更新目录
        /// </summary>  
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("updateTenantDirectory")]
        public async Task<ActionResult<DataResponseModel>> UpdateTenantDirectory([FromBody] UpdateTenantDirectoryInput input)
        {
            var result = await _tenantMenuManageService.UpdateTenantDirectory(input);
            return DataResponseModel.SetData(result);
        }

        /// <summary>
        /// 更新菜单
        /// </summary>  
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("updateTenantMenu")]
        public async Task<ActionResult<DataResponseModel>> UpdateTenantMenu([FromBody] UpdateTenantMenuInput input)
        {
            var result = await _tenantMenuManageService.UpdateTenantMenu(input);
            return DataResponseModel.SetData(result);
        }

        /// <summary>
        /// 更新按钮
        /// </summary>  
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("updateTenantMenuButton")]
        public async Task<ActionResult<DataResponseModel>> UpdateTenantMenuButton([FromBody] UpdateTenantMenuButtonInput input)
        {
            var result = await _tenantMenuManageService.UpdateTenantMenuButton(input);
            return DataResponseModel.SetData(result);
        }
        #endregion

        #region 删除
        /// <summary>
        /// 删除目录
        /// </summary>  
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("deleteTenantDirectory")]
        public async Task<ActionResult<DataResponseModel>> DeleteTenantDirectory([FromBody] IdInput input)
        {
            var result = await _tenantMenuManageService.DeleteTenantDirectory(input.Id);
            return DataResponseModel.SetData(result);
        }

        /// <summary>
        /// 删除菜单
        /// </summary>  
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("deleteTenantMenu")]
        public async Task<ActionResult<DataResponseModel>> DeleteTenantMenu([FromBody] IdInput input)
        {
            var result = await _tenantMenuManageService.DeleteTenantMenu(input.Id);
            return DataResponseModel.SetData(result);
        }

        /// <summary>
        /// 删除按钮
        /// </summary>  
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("deleteTenantMenuButton")]
        public async Task<ActionResult<DataResponseModel>> DeleteTenantMenuButton([FromBody] IdInput input)
        {
            var result = await _tenantMenuManageService.DeleteTenantMenuButton(input.Id);
            return DataResponseModel.SetData(result);
        }
        #endregion
    }
}
