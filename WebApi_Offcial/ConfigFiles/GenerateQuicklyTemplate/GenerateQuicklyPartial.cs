
namespace WebApi_Offcial.ConfigFiles.GenerateQuicklyTemplate
{
    public partial class ControllerTemplate : ControllerTemplateBase
    {
        private GenerateQuicklyInput data;
        public ControllerTemplate(GenerateQuicklyInput generateQuicklyInput)
        {
            this.data = generateQuicklyInput;
        }
    }

    public partial class ServiceImplTemplate : ServiceImplTemplateBase
    {
        private GenerateQuicklyInput data;
        public ServiceImplTemplate(GenerateQuicklyInput generateQuicklyInput)
        {
            this.data = generateQuicklyInput;
        }
    }

    public partial class InputTemplate : InputTemplateBase
    {
        private GenerateQuicklyInput data;
        public InputTemplate(GenerateQuicklyInput generateQuicklyInput)
        {
            this.data = generateQuicklyInput;
        }
    }

    public partial class IServiceTemplate : IServiceTemplateBase
    {
        private GenerateQuicklyInput data;
        public IServiceTemplate(GenerateQuicklyInput generateQuicklyInput)
        {
            this.data = generateQuicklyInput;
        }
    }

    public partial class DataSphereTemplate : DataSphereTemplateBase
    {
        private GenerateQuicklyInput data;
        public DataSphereTemplate(GenerateQuicklyInput generateQuicklyInput)
        {
            this.data = generateQuicklyInput;
        }
    }

    public partial class IDataSphereTemplate : IDataSphereTemplateBase
    {
        private GenerateQuicklyInput data;
        public IDataSphereTemplate(GenerateQuicklyInput generateQuicklyInput)
        {
            this.data = generateQuicklyInput;
        }
    }

    public partial class ActionFilterTemplate : ActionFilterTemplateBase
    {
        private GenerateQuicklyInput data;
        public ActionFilterTemplate(GenerateQuicklyInput generateQuicklyInput)
        {
            this.data = generateQuicklyInput;
        }
    }
}
