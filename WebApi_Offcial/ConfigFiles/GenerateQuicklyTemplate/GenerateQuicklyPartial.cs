namespace WebApi_Offcial.ConfigFiles.GenerateQuicklyTemplate
{
    /// <summary>
    /// 控制器模板
    /// </summary>
    public partial class ControllerTemplate : ControllerTemplateBase
    {
        private ControllerGenerateQuicklyInput input;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="_input"></param>
        public ControllerTemplate(ControllerGenerateQuicklyInput _input)
        {
            this.input = _input;
        }

    }
}
