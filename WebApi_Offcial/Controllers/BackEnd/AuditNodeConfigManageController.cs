using Microsoft.AspNetCore.Mvc;
using SharedLibrary.Enums;
using SharedLibrary.Consts;
using Model.Commons.Domain;
using Model.Commons.SharedData;
using Service.BackEnd.AuditNodeConfigManage;
using Model.DTOs.BackEnd.AuditNodeConfigManage;
using WebApi_Offcial.ActionFilters.BackEnd;
using IDataSphere.Interface.BackEnd.AuditNodeConfigManage;

namespace WebApi_Offcial.Controllers.BackEnd
{
    /// <summary>
    /// 审核流程配置表控制层
    /// </summary>
    [ApiController]
    [Route("AuditNodeConfigManage")]
    [ApiDescription(SwaggerGroupEnum.BackEnd)]
    [ServiceFilter(typeof(AuditNodeConfigManageActionFilter))]
    public class AuditNodeConfigManageController : BaseController
    {
        #region 构造函数
        private readonly IAuditNodeConfigManageService _auditNodeConfigManageService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        /// <summary>
        /// 构造函数
        /// </summary>
        public AuditNodeConfigManageController(IAuditNodeConfigManageService auditNodeConfigManageService, IHttpContextAccessor httpContextAccessor)
        {
            this._auditNodeConfigManageService = auditNodeConfigManageService;
            this._httpContextAccessor = httpContextAccessor;
        }
        #endregion

        #region 查询
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet("getAuditNodeConfigPage")]
        public async Task<ActionResult<ServiceResult>> GetAuditNodeConfigPage([FromQuery] GetAuditNodeConfigPageInput input)
        {
            var data = await _auditNodeConfigManageService.GetAuditNodeConfigPage(input);
            return ServiceResult.SetData(data);
        }
        #endregion

        #region 新增
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("addAuditNodeConfig")]
        public async Task<ActionResult<ServiceResult>> AddAuditNodeConfig([FromBody] AddAuditNodeConfigInput input)
        {
            var result = await _auditNodeConfigManageService.AddAuditNodeConfig(input);
            return ServiceResult.SetData(result);
        }
        #endregion

        #region 更新
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("updateAuditNodeConfig")]
        public async Task<ActionResult<ServiceResult>> UpdateAuditNodeConfig([FromBody] UpdateAuditNodeConfigInput input)
        {
            var result = await _auditNodeConfigManageService.UpdateAuditNodeConfig(input);
            return ServiceResult.SetData(result);
        }
        #endregion

        #region 删除
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("deleteAuditNodeConfig")]
        public async Task<ActionResult<ServiceResult>> DeleteAuditNodeConfig([FromBody] IdInput input)
        {
            var result = await _auditNodeConfigManageService.DeleteAuditNodeConfig(input.Id);
            return ServiceResult.SetData(result);
        }
        #endregion
    }
}
