using SharedLibrary.Enums;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using WebApi_Offcial.Controllers;

namespace WebApi_Offcial.ConfigFiles.GenerateQuicklyTemplate
{
    /// <summary>
    /// 模板初始化输入类
    /// </summary>
    public class GenerateQuicklyInput
    {
        /// <summary>
        /// 类名前缀
        /// </summary>
        public string ClassNamePrefix { get; set; }

        /// <summary>
        /// 中文注释
        /// </summary>
        public string ChinesesName { get; set; }

        /// <summary>
        /// 主表名称，用于生成Add、Update等输入模型文件内容
        /// </summary>
        public string TableName { get; set; }

        /// <summary>
        /// 位置：1业务后台 2前台 3中间服务 4系统管理
        /// </summary>
        public SwaggerGroupEnum Address { get; set; }

        /// <summary>
        /// SwaggerGroup注释
        /// </summary>
        internal string SwaggerGroupEnumName { get; set; }

        /// <summary>
        /// 构造函数参数前缀
        /// </summary>
        internal string ParameterName { get; set; }

        /// <summary>
        /// 构造函数初始化文件
        /// </summary>
        /// <param name="classNamePrefix"></param>
        /// <param name="chinesesName"></param>
        /// <param name="tableName"></param>
        /// <param name="address"></param>
        public GenerateQuicklyInput(string classNamePrefix, string chinesesName, string tableName, SwaggerGroupEnum address)
        {
            this.ClassNamePrefix = classNamePrefix;
            this.ChinesesName = chinesesName;
            this.SwaggerGroupEnumName = typeof(SwaggerGroupEnum).GetEnumName(address);
            this.ParameterName = classNamePrefix.Substring(0, 1).ToLower() + classNamePrefix.Substring(1);
            this.TableName = tableName;
        }

    }

    /// <summary>
    /// 方法输入参数
    /// </summary>
    public class ActionGenerateQuicklyInput
    {
        /// <summary>
        /// 描述
        /// </summary>
        public string Summary { get; set; }

        /// <summary>
        /// 方法名称
        /// </summary>
        public string ActionName { get; set; }

        /// <summary>
        /// 是否是Get请求
        /// </summary>
        public bool IsHttpGet { get; set; }

        /// <summary>
        /// 输入模型名称
        /// </summary>
        public string InputName { get; set; }

        /// <summary>
        /// 是否需要生成对应的切面验证方法
        /// </summary>
        public bool IsNeedFilter { get; set; }

    }


    /// <summary>
    /// 控制器输入模板
    /// </summary>
    public class ControllerGenerateQuicklyInput : GenerateQuicklyInput
    {
        /// <summary>
        /// 是否需要菜单权限验证
        /// </summary>
        public bool IsNeedMenuControl { get; set; }

        /// <summary>
        /// 是否需要HttpContext上下文
        /// </summary>
        public bool IsNeedHttpContext { get; set; }

        /// <summary>
        /// 方法集合
        /// </summary>
        public List<ActionGenerateQuicklyInput> ActionDic { get; set; }

        /// <summary>
        /// 控制器所需要继承的父类
        /// </summary>
        internal string ControllerBaseName { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        public ControllerGenerateQuicklyInput(
            string classNamePrefix,
            string chinesesName,
            string tableName,
            SwaggerGroupEnum address,
            bool isNeedMenuControl,
            bool isNeedHttpContext) :
            base(classNamePrefix, chinesesName, tableName, address)
        {
            this.ControllerBaseName = isNeedMenuControl ? nameof(BaseController) : "ControllerBase";
            this.IsNeedHttpContext = isNeedHttpContext;
        }
    }

}
