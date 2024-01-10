using SharedLibrary.Enums;

namespace Model.Commons.Domain
{
    /// <summary>
    /// RESTFul返回格式
    /// </summary>
    public class ServiceResult
    {
        /// <summary>
        /// 是否成功
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// 状态码
        /// </summary>
        public int ResultCode { get; set; }

        /// <summary>
        /// 携带信息
        /// </summary>
        public string Message { get; set; } = string.Empty;

        /// <summary>
        /// 数据体
        /// </summary>
        public object Data { get; set; }

        /// <summary>
        /// 构造函数初始化
        /// </summary>
        private ServiceResult()
        {
            Success = true;
            ResultCode = (int)ServiceResultTypeEnum.Succeed;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="resultCode"></param>
        public ServiceResult(string msg, int resultCode)
        {
            Message = msg;
            ResultCode = resultCode;
            Success = ResultCode == (int)ServiceResultTypeEnum.Succeed;
        }

        /// <summary>
        /// 标记失败
        /// </summary>
        public static ServiceResult IsFailure(string msg = null, string data = null)
        {
            ServiceResult serviceResult = new();
            serviceResult.Success = false;
            if (data != null)
            {
                serviceResult.Data = data;
            }
            if (msg != null)
            {
                serviceResult.Message = msg;
            }
            return serviceResult;
        }

        /// <summary>
        /// 放置数据
        /// </summary>
        public static ServiceResult SetData(object data)
        {
            ServiceResult serviceResult = new();
            if (data != null)
            {
                serviceResult.Data = data;
            }
            return serviceResult;
        }

        /// <summary>
        /// 标记成功
        /// </summary>
        public static ServiceResult Successed()
        {
            ServiceResult serviceResult = new();
            return serviceResult;
        }

    }

}
