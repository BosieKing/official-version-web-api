using BusinesLogic.BackEnd.MenuManage;
using Microsoft.AspNetCore.Mvc;
using Model.Commons.Domain;
using Model.Commons.SharedData;
using Model.DTOs.BackEnd.MenuManage;
using SharedLibrary.Enums;
using WebApi_Offcial.ActionFilters.BackEnd;

namespace WebApi_Offcial.Controllers.BackEnd
{
    /// <summary>
    /// 菜单控制层
    /// </summary>
    [ApiController]
    [Route("MenuManage")]
    [ApiDescription(SwaggerGroupEnum.System)]
    [ServiceFilter(typeof(MenuManageActionFilter))]
    public class MenuManageController : BaseController
    {
        #region 构造函数
        private readonly IMenuManageService _menuManageService;

        /// <summary>
        /// 构造函数
        /// </summary>
        public MenuManageController(IMenuManageService menuManageService)
        {
            _menuManageService = menuManageService;

        }
        #endregion

        #region 查询     
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet("getMenuPage")]
        public async Task<ActionResult<ServiceResult>> GetMenuPage([FromQuery] GetMenuPageInput input)
        {
            var data = await _menuManageService.GetMenuPage(input);
            return ServiceResult.SetData(data);
        }

        /// <summary>
        /// 获取目录下拉
        /// </summary>
        /// <returns></returns>
        [HttpGet("getDirectoryList")]
        public async Task<ActionResult<ServiceResult>> GetDirectoryList()
        {
            var data = await _menuManageService.GetDirectoryList();
            return ServiceResult.SetData(data);
        }

        /// <summary>
        /// 获取菜单下拉
        /// </summary>
        /// <returns></returns>
        [HttpGet("getMenuList")]
        public async Task<ActionResult<ServiceResult>> GetMenuList()
        {
            var data = await _menuManageService.GetMenuList();
            return ServiceResult.SetData(data);
        }
        #endregion

        #region 新增
        /// <summary>
        /// 添加目录
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("addDirectory")]
        public async Task<ActionResult<ServiceResult>> AddDirectory([FromBody] AddDirectoryInput input)
        {
            var result = await _menuManageService.AddDirectory(input);
            return ServiceResult.SetData(result);
        }

        /// <summary>
        /// 添加菜单
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("addMenu")]
        public async Task<ActionResult<ServiceResult>> AddMenu([FromBody] AddMenuInput input)
        {
            var result = await _menuManageService.AddMenu(input);
            return ServiceResult.SetData(result);
        }

        /// <summary>
        /// 添加按钮
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("addMenuButton")]
        public async Task<ActionResult<ServiceResult>> AddMenuButton([FromBody] AddMenuButtonInput input)
        {
            var result = await _menuManageService.AddMenuButton(input);
            return ServiceResult.SetData(result);
        }
        #endregion

        #region 更新
        /// <summary>
        /// 更新目录
        /// </summary>  
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("updateDirectory")]
        public async Task<ActionResult<ServiceResult>> UpdateDirectory([FromBody] UpdateDirectoryInput input)
        {
            var result = await _menuManageService.UpdateDirectory(input);
            return ServiceResult.SetData(result);
        }

        /// <summary>
        /// 更新菜单
        /// </summary>  
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("updateMenu")]
        public async Task<ActionResult<ServiceResult>> UpdateMenu([FromBody] UpdateMeunInput input)
        {
            var result = await _menuManageService.UpdateMenu(input);
            return ServiceResult.SetData(result);
        }

        /// <summary>
        /// 更新按钮
        /// </summary>  
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("updateMenuButton")]
        public async Task<ActionResult<ServiceResult>> UpdateMenuButton([FromBody] UpdateMenuButtonInput input)
        {
            var result = await _menuManageService.UpdateMenuButton(input);
            return ServiceResult.SetData(result);
        }
        #endregion

        #region 删除
        /// <summary>
        /// 删除目录
        /// </summary>  
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("deleteDirectory")]
        public async Task<ActionResult<ServiceResult>> DeleteDirectory([FromBody] IdInput input)
        {
            var result = await _menuManageService.DeleteDirectory(input.Id);
            return ServiceResult.SetData(result);
        }

        /// <summary>
        /// 删除菜单
        /// </summary>  
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("deleteMenu")]
        public async Task<ActionResult<ServiceResult>> DeleteMenu([FromBody] IdInput input)
        {
            var result = await _menuManageService.DeleteMenu(input.Id);
            return ServiceResult.SetData(result);
        }

        /// <summary>
        /// 删除按钮
        /// </summary>  
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("deleteMenuButton")]
        public async Task<ActionResult<ServiceResult>> DeleteMenuButton([FromBody] IdInput input)
        {
            var result = await _menuManageService.DeleteMenuButton(input.Id);
            return ServiceResult.SetData(result);
        }
        #endregion

    }
}
