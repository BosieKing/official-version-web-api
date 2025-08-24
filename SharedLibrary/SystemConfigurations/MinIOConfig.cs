using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLibrary.SystemConfigurations
{
    /// <summary>
    /// MinIO文件服务
    /// </summary>
    public class MinIOConfig
    {
        /// <summary>
        /// 默认Key
        /// </summary>
        public string DefaultKey { get; set; } = string.Empty;

        /// <summary>
        /// Minio服务启动后地址
        /// </summary>
        public string Endpoint { get; set; } = string.Empty;

        /// <summary>
        /// 用户名
        /// </summary>
        public string AccessKey { get; set; } = string.Empty;

        /// <summary>
        /// 密码
        /// </summary>
        public string SecretKey { get; set; } = string.Empty;
    }

}
