using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model.Commons.Domain;
using Model.DTOs.FronDesk.PostHomePage;
using Service.FrontDesk.FrontDeskOAuth;
using SharedLibrary.Enums;
using UtilityToolkit.Helpers;
using UtilityToolkit.Tools;
using WebApi_Offcial.ActionFilters.FrontDesk;

namespace WebApi_Offcial.Controllers.FrontDesk
{
    /// <summary>
    /// 首页
    /// </summary>
    [ApiController]
    [Route("Home")]
    [ApiDescription(SwaggerGroupEnum.FrontDesk)]
    public class HomeController : ControllerBase
    {

        #region 获取基础信息
        /// <summary>
        /// 获取轮播图列表
        /// </summary>
        [HttpGet("getCarouselPicList")]
        [AllowAnonymous]
        public async Task<ServiceResult> GetCarouselPicList(string version)
        {
            bool isCarouselPicChange = RedisMulititionHelper.IsCarouselPicChange(version,out string cacheVersion);
            // 如果没有修改
            if (isCarouselPicChange)
            {
                // 返回结果
                return ServiceResult.SetData(new
                {
                    version = cacheVersion
                });
            }
            try
            {
                // 获取 wwwroot/CarouselPics 文件夹路径
                var webRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
                var carouselFolder = Path.Combine(webRootPath, "CarouselPics");
                // 检查文件夹是否存在
                if (!Directory.Exists(carouselFolder))
                {
                    return ServiceResult.SetData(new List<string>()); 
                }
                // 获取所有图片文件
                var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".webp", ".gif" };
                var filePaths = Directory.GetFiles(carouselFolder)
                    .Where(file => allowedExtensions.Contains(Path.GetExtension(file).ToLower()))
                    .ToList();
                // 拼接完整URL
                var baseUrl = ConfigSettingTool.SystemConfig.DomainAddress.TrimEnd('/');
                var imageUrls = filePaths.Select(filePath =>
                {
                    var relativePath = filePath
                        .Replace(webRootPath, "")
                        .Replace("\\", "/"); // 确保路径斜杠统一
                    return $"{baseUrl}{relativePath}";
                }).ToList();
                // 返回结果
                return ServiceResult.SetData(new
                {
                    ImageUrls = imageUrls,
                    version = cacheVersion
                });
            }
            catch (Exception ex)
            {
                return ServiceResult.Fail($"获取轮播图列表失败: {ex.Message}");
            }
        }


        /// <summary>
        /// 获取导师列表
        /// </summary>
        [HttpGet("getMentorList")]
        [AllowAnonymous]
        public async Task<ServiceResult> GetMentorList()
        {
       
        }
        #endregion
    }
}
