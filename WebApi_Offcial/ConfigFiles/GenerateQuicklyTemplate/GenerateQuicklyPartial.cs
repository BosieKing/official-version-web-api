namespace WebApi_Offcial.ConfigFiles.GenerateQuicklyTemplate
{
    public partial class ControllerTemplate : ControllerTemplateBase
    {
        private GenerateQuicklyInput data;
        public ControllerTemplate(GenerateQuicklyInput generateQuicklyInput)
        {
            data = generateQuicklyInput;
        }
    }

    public partial class BusinesLogicImplTemplate : BusinesLogicImplTemplateBase
    {
        private GenerateQuicklyInput data;
        public BusinesLogicImplTemplate(GenerateQuicklyInput generateQuicklyInput)
        {
            data = generateQuicklyInput;
        }
    }

    public partial class InputTemplate : InputTemplateBase
    {
        private GenerateQuicklyInput data;
        public InputTemplate(GenerateQuicklyInput generateQuicklyInput)
        {
            data = generateQuicklyInput;
        }
    }

    public partial class IBusinesLogicTemplate : IBusinesLogicTemplateBase
    {
        private GenerateQuicklyInput data;
        public IBusinesLogicTemplate(GenerateQuicklyInput generateQuicklyInput)
        {
            data = generateQuicklyInput;
        }
    }

    public partial class DataSphereTemplate : DataSphereTemplateBase
    {
        private GenerateQuicklyInput data;
        public DataSphereTemplate(GenerateQuicklyInput generateQuicklyInput)
        {
            data = generateQuicklyInput;
        }
    }

    public partial class IDataSphereTemplate : IDataSphereTemplateBase
    {
        private GenerateQuicklyInput data;
        public IDataSphereTemplate(GenerateQuicklyInput generateQuicklyInput)
        {
            data = generateQuicklyInput;
        }
    }

    public partial class ActionFilterTemplate : ActionFilterTemplateBase
    {
        private GenerateQuicklyInput data;
        public ActionFilterTemplate(GenerateQuicklyInput generateQuicklyInput)
        {
            data = generateQuicklyInput;
        }
    }
}
