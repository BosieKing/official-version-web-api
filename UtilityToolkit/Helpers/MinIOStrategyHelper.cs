using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using OnceMi.AspNetCore.OSS;
using UtilityToolkit.Tools;

namespace UtilityToolkit.Helpers
{
    /// <summary>
    /// minio服务类
    /// </summary>
    public class MinIOStrategyHelper
    {
        #region 构造函数和参数
        private readonly IStringLocalizer<UserTips> _stringLocalizer;
        private readonly IOSSServiceFactory _fileService;
        private readonly string DefaultKey = ConfigSettingTool.MinIOConfig.DefaultKey;
        private readonly IConfiguration _configuration;

        /// <summary>
        /// 构造函数
        /// </summary>
        public MinIOStrategyHelper(IConfiguration configuration, IStringLocalizer<UserTips> stringLocalizer, IOSSServiceFactory oSSServiceFactory)
        {
            _stringLocalizer = stringLocalizer;
            _fileService = oSSServiceFactory;
            _configuration = configuration;
        }
        #endregion

        #region 上传
        
        #endregion
    }
}
