using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model.Commons.Domain;
using SharedLibrary.Enums;
using UtilityToolkit.Helpers;

namespace WebApi_Offcial.Controllers.BackEnd
{
    /// <summary>
    /// 系统管理
    /// </summary>
    [ApiController]
    [Route("System")]
    [ApiDescription(SwaggerGroupEnum.BackEnd)]
    public class SystemController : BaseController
    {

        #region 获取基础信息
        /// <summary>
        /// 上传轮播图片
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="file"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<ServiceResult> UpdateCarouselPic(string fileName, IFormFile file)
        {
            // 参数检查
            if (string.IsNullOrEmpty(fileName) || file == null || file.Length == 0)
            {
                return ServiceResult.Fail("文件名或文件不能为空");
            }

            // 获取系统文件夹
            var webRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
            var carouselFolder = Path.Combine(webRootPath, "CarouselPics");

            try
            {
                // 确保文件夹存在
                if (!Directory.Exists(carouselFolder))
                {
                    Directory.CreateDirectory(carouselFolder);
                }

                // 生成唯一文件名（避免冲突）
                var uniqueFileName = $"{Guid.NewGuid()}{Path.GetExtension(fileName)}";
                var filePath = Path.Combine(carouselFolder, uniqueFileName);

                // 保存新文件
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
                // 更新轮播图版本标记
                RedisMulititionHelper.CarouselPicChange();

                // 返回成功结果和文件名
                return ServiceResult.Successed("文件上传成功");
            }
            catch (Exception ex)
            {
                return ServiceResult.Fail($"文件上传失败: {ex.Message}");
            }
        }
        #endregion
    }
}
