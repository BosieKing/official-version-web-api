using SharedLibrary.Enums;

namespace SharedLibrary.Models.DomainModels
{
    /// <summary>
    /// RESTFul返回格式
    /// </summary>
    public class DataResponseModel
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
        private DataResponseModel()
        {
            Success = true;
            ResultCode = (int)ServiceResultTypeEnum.Succeed;
        }

        public DataResponseModel(string msg, int resultCode)
        {
            Message = msg;
            ResultCode = resultCode;
            Success = ResultCode == (int)ServiceResultTypeEnum.Succeed;
        }

        /// <summary>
        /// 标记失败
        /// </summary>
        public static DataResponseModel IsFailure(string msg = null, string data = null)
        {
            DataResponseModel serviceResult = new DataResponseModel();
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
        public static DataResponseModel SetData(object data)
        {
            DataResponseModel serviceResult = new DataResponseModel();
            if (data != null)
            {
                serviceResult.Data = data;
            }
            return serviceResult;
        }

        /// <summary>
        /// 标记成功
        /// </summary>
        public static DataResponseModel Successed()
        {
            DataResponseModel serviceResult = new DataResponseModel();
            return serviceResult;
        }

    }

}
