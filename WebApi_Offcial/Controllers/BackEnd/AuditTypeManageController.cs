using Microsoft.AspNetCore.Mvc;
using SharedLibrary.Enums;
using SharedLibrary.Consts;
using Model.Commons.Domain;
using Model.Commons.SharedData;
using Service.BackEnd.AuditTypeManage;
using Model.DTOs.BackEnd.AuditTypeManage;
using WebApi_Offcial.ActionFilters.BackEnd;
using IDataSphere.Interface.BackEnd.AuditTypeManage;

namespace WebApi_Offcial.Controllers.BackEnd
{
    /// <summary>
    /// 审核角色类型配置表控制层
    /// </summary>
    [ApiController]
    [Route("AuditTypeManage")]
    [ApiDescription(SwaggerGroupEnum.BackEnd)]
    [ServiceFilter(typeof(AuditTypeManageActionFilter))]
    public class AuditTypeManageController : BaseController
    {
        #region 构造函数
        private readonly IAuditTypeManageService _auditTypeManageService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        /// <summary>
        /// 构造函数
        /// </summary>
        public AuditTypeManageController(IAuditTypeManageService auditTypeManageService, IHttpContextAccessor httpContextAccessor)
        {
            this._auditTypeManageService = auditTypeManageService;
            this._httpContextAccessor = httpContextAccessor;
        }
        #endregion

        #region 查询
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet("getAuditTypePage")]
        public async Task<ActionResult<ServiceResult>> GetAuditTypePage([FromQuery] GetAuditTypePageInput input)
        {
            var data = await _auditTypeManageService.GetAuditTypePage(input);
            return ServiceResult.SetData(data);
        }
        #endregion

        #region 新增
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("addAuditType")]
        public async Task<ActionResult<ServiceResult>> AddAuditType([FromBody] AddAuditTypeInput input)
        {
            var result = await _auditTypeManageService.AddAuditType(input);
            return ServiceResult.SetData(result);
        }
        #endregion

        #region 更新
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("updateAuditType")]
        public async Task<ActionResult<ServiceResult>> UpdateAuditType([FromBody] UpdateAuditTypeInput input)
        {
            var result = await _auditTypeManageService.UpdateAuditType(input);
            return ServiceResult.SetData(result);
        }
        #endregion

        #region 删除
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("deleteAuditType")]
        public async Task<ActionResult<ServiceResult>> DeleteAuditType([FromBody] IdInput input)
        {
            var result = await _auditTypeManageService.DeleteAuditType(input.Id);
            return ServiceResult.SetData(result);
        }
        #endregion
    }
}
