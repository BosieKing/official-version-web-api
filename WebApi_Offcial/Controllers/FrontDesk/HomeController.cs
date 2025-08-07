using IDataSphere.Interfaces.FronDesk;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model.Commons.Domain;
using Model.Commons.SharedData;
using Model.DTOs.FronDesk.Home;
using SharedLibrary.Enums;
using UtilityToolkit.Helpers;
using UtilityToolkit.Helpers.WxLogin.Dto;
using UtilityToolkit.Tools;

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

        #region 构造函数
        private readonly IHomeDao _homeDao;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="homeDao"></param>
        public HomeController(IHomeDao homeDao)
        {
            _homeDao = homeDao;
        }
        #endregion

        #region 获取基础信息
        /// <summary>
        /// 获取Index界面需要的一些基础配置列表
        /// </summary>
        [HttpGet("getIndexDataList")]
        [AllowAnonymous]
        public async Task<ServiceResult> GetIndexDataList(string version)
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
                    Version = cacheVersion,
                    LogoIconUrl = $"{baseUrl}/{"/System/logo.png".TrimStart('/')}",
                    DefaultBgUrl = $"{baseUrl}/{"/System/teacher-detail-bg.png".TrimStart('/')}",
                    NoCourseTipUrl = $"{baseUrl}/{"/System/no-course-tip.png".TrimStart('/')}",
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
        [HttpGet("getTeacherList")]
        [AllowAnonymous]
        public async Task<ServiceResult> GetTeacherList([FromQuery]  GetTeacherListInput input)
        {
            return ServiceResult.SetData( await _homeDao.GetTeacherList(input));
        }

        /// <summary>
        /// 获取私教列表
        /// </summary>
        [HttpGet("GetPersonalCourseList")]
        [AllowAnonymous]
        public async Task<ServiceResult> GetPersonalCourseList([FromQuery] GetPersonalCourseListInput input)
        {
            return ServiceResult.SetData(await _homeDao.GetPersonalCourseList(input));
        }

        /// <summary>
        /// 获取私教当天的时间列表
        /// </summary>
        [HttpGet("GetPersonalCoureseTimeList")]
        [AllowAnonymous]
        public async Task<ServiceResult> GetPersonalCoureseTimeList([FromQuery] GetPersonalCoureseTimeListInput input)
        {
            return ServiceResult.SetData(await _homeDao.GetPersonalCourseTimeList(input));
        }

        /// <summary>
        /// 获取课程列表
        /// </summary>
        [HttpGet("getCourseList")]
        [AllowAnonymous]
        public async Task<ServiceResult> GetCourseList([FromQuery] GetCourseListInput input)
        {
            return ServiceResult.SetData(await _homeDao.GetCourseList(input));
        }


        /// <summary>
        /// 获取课程报名用户头像列表
        /// </summary>
        [HttpGet("GetCourseSignUpAvaratUrlList")]
        [AllowAnonymous]
        public async Task<ServiceResult> GetCourseSignUpAvaratUrlList([FromQuery] GetCourseSignUpAvaratUrlListInput input)
        {
            return ServiceResult.SetData(await _homeDao.GetCourseSignUpAvaratUrlList(input));
        }


        /// <summary>
        /// 获取我的卡
        /// </summary>
        [HttpGet("getMyCardList")]
        [AllowAnonymous]
        public async Task<ServiceResult> GetMyCardList([FromQuery] GetMyCardListInput input)
        {
            return ServiceResult.SetData(await _homeDao.GetMyCardList(input));
        }

        /// <summary>
        /// 获取卡片使用情况
        /// </summary>
        [HttpGet("GetUseCardRecodeList")]
        [AllowAnonymous]
        public async Task<ServiceResult> GetUseCardRecodeList([FromQuery] GetUseCardRecodeListInput input)
        {
            return ServiceResult.SetData(await _homeDao.GetUseCardRecodeList(input));
        }


        /// <summary>
        /// 获取我的课程预约集合
        /// </summary>
        [HttpGet("GetMyCourseSignUpList")]
        [AllowAnonymous]
        public async Task<ServiceResult> GetMyCourseSignUpList([FromQuery] GetMyCourseSignUpListInput input)
        {
            return ServiceResult.SetData(await _homeDao.GetMyCourseSignUpList(input));
        }

        /// <summary>
        /// 获取我的私教预约集合
        /// </summary>
        [HttpGet("GetMyPersonalCourseSignUpList")]
        [AllowAnonymous]
        public async Task<ServiceResult> GetMyPersonalCourseSignUpList([FromQuery] GetMyCourseSignUpListInput input)
        {
            return ServiceResult.SetData(await _homeDao.GetMyPersonalCourseSignUpList(input));
        }
        #endregion

        #region 新增
        /// <summary>
        /// 新增私教预约
        /// </summary>
        [HttpPost("AddPersonalCourseBooking")]
        [AllowAnonymous]
        public async Task<ServiceResult> AddPersonalCourseBooking([FromBody] AddPersonalCourseBookingInput input)
        {
            return await _homeDao.AddPersonalCourseBooking(input);
        }

        /// <summary>
        /// 新增课程预约
        /// </summary>
        [HttpPost("AddCourseBooking")]
        [AllowAnonymous]
        public async Task<ServiceResult> AddCourseBooking([FromBody] AddCourseBookingInput input)
        {
            return await _homeDao.AddCourseBooking(input);
        }
        #endregion

        #region 登录
        /// <summary>
        /// 微信登录
        /// </summary>
        [HttpPost("wxLogin")]
        [AllowAnonymous]
        public async Task<ServiceResult> WxLogin([FromBody] WxLoginInput input)
        {
            return ServiceResult.SetData(await _homeDao.WxLogin(input));
        }
        #endregion

        #region 取消
        /// <summary>
        /// 取消课程预约
        /// </summary>
        [HttpPost("CancelCourseSignUp")]
        [AllowAnonymous]
        public async Task<ServiceResult> CancelCourseSignUp([FromBody] IdInput input)
        {
            return await _homeDao.CancelCourseSignUp(input);
        }

        /// <summary>
        /// 取消私教预约
        /// </summary>
        [HttpPost("CancelPersonalCourseSignUp")]
        [AllowAnonymous]
        public async Task<ServiceResult> CancelPersonalCourseSignUp([FromBody] IdInput input)
        {
            return await _homeDao.CancelPersonalCourseSignUp(input);
        }
        #endregion
    }
}
