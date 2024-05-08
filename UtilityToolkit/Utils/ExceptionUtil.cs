namespace UtilityToolkit.Utils
{
    public static class ExceptionUtil
    {
        /// <summary>
        /// 获取报错类型的中文描述
        /// </summary>
        /// <param name="typeName"></param>
        /// <returns></returns>
        public static string GetDescription(string typeName)
        {
            string description;
            switch (typeName)
            {
                case "AccessViolationException":
                    description = "当代码试图访问受保护的内存区域时引发的异常。";
                    break;
                case "AggregateException":
                    description = "当一个或多个任务发生异常时聚合所有异常的异常类型。";
                    break;
                case "AppDomainUnloadedException":
                    description = "当已卸载的应用程序域上发生错误时引发的异常。";
                    break;
                case "ApplicationException":
                    description = "由应用程序引发的异常的基类。";
                    break;
                case "ArithmeticException":
                    description = "在数学运算期间发生错误时引发的异常的基类。";
                    break;
                case "ArrayTypeMismatchException":
                    description = "当一个数组中的元素不是预期的类型时引发的异常。";
                    break;
                case "BadImageFormatException":
                    description = "当尝试加载格式不正确的程序集时引发的异常。";
                    break;
                case "CannotUnloadAppDomainException":
                    description = "当无法卸载应用程序域时引发的异常。";
                    break;
                case "ContextMarshalException":
                    description = "在尝试在不同的应用程序域之间传输上下文时引发的异常。";
                    break;
                case "DataMisalignedException":
                    description = "当数据类型的大小不正确以及数据对齐时引发的异常。";
                    break;
                case "DllNotFoundException":
                    description = "当程序尝试加载动态链接库（DLL）但找不到时引发的异常。";
                    break;
                case "EntryPointNotFoundException":
                    description = "当程序尝试调用本机方法入口点但找不到时引发的异常。";
                    break;
                case "ExecutionEngineException":
                    description = "当 CLR 执行引擎检测到无法恢复的错误时引发的异常。";
                    break;
                case "FieldAccessException":
                    description = "当代码尝试访问私有字段、方法或属性，但没有权限时引发的异常。";
                    break;
                case "FormatException":
                    description = "当一个字符串无法转换为所需的格式时引发的异常。";
                    break;
                case "IndexOutOfRangeException":
                    description = "当尝试访问数组或集合中不存在的索引时引发的异常。";
                    break;
                case "InvalidCastException":
                    description = "当进行类型转换但目标类型不兼容时引发的异常。";
                    break;
                case "InvalidOperationException":
                    description = "当对象处于无效状态并且方法调用不合法时引发的异常。";
                    break;
                case "InvalidProgramException":
                    description = "当程序包含无效的 Microsoft 中间语言（MSIL）或元数据时引发的异常。";
                    break;
                case "MemberAccessException":
                    description = "当代码尝试访问类的成员但没有权限时引发的异常。";
                    break;
                case "NotImplementedException":
                    description = "当方法或功能尚未实现时引发的异常。";
                    break;
                case "NotSupportedException":
                    description = "当方法不支持当前操作时引发的异常。";
                    break;
                case "NullReferenceException":
                    description = "当尝试访问引用对象的成员但对象为 null 时引发的异常。";
                    break;
                case "OperationCanceledException":
                    description = "当操作取消时引发的异常。";
                    break;
                case "OutOfMemoryException":
                    description = "当系统无法分配足够的内存时引发的异常。";
                    break;
                case "OverflowException":
                    description = "当算术操作超出类型的范围时引发的异常。";
                    break;
                case "PlatformNotSupportedException":
                    description = "当在不支持的操作系统或平台上调用方法时引发的异常。";
                    break;
                case "RankException":
                    description = "当在多维数组中使用错误数量的索引时引发的异常。";
                    break;
                case "StackOverflowException":
                    description = "当递归调用或方法调用的深度超过堆栈大小时引发的异常。";
                    break;
                case "TimeoutException":
                    description = "当操作超时时引发的异常。";
                    break;
                case "TypeInitializationException":
                    description = "当类型初始化器引发异常并导致类无法加载时引发的异常。";
                    break;
                case "TypeLoadException":
                    description = "当无法加载类型时引发的异常。";
                    break;
                case "TypeUnloadedException":
                    description = "当尝试访问已卸载的类型时引发的异常。";
                    break;
                case "UnauthorizedAccessException":
                    description = "当没有权限访问文件或目录时引发的异常。";
                    break;
                case "UriFormatException":
                    description = "当 Uri 实例的格式无效时引发的异常。";
                    break;
                case "XmlException":
                    description = "当 XML 文档无效时引发的异常。";
                    break;
                // 数据库异常
                case "DbUpdateException":
                    description = "在尝试更新数据库时发生错误的异常。";
                    break;
                case "DbUpdateConcurrencyException":
                    description = "并发更新数据库时发生的异常。";
                    break;
                case "DbUpdateCommandException":
                    description = "执行数据库更新命令时发生错误的异常。";
                    break;
                default:
                    description = "未知异常类型";
                    break;
            }
            return description;

        }
    }
}
