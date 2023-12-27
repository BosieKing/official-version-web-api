using SharedLibrary.Enums;
using System.Reflection;
using WebApi_Offcial.ConfigFiles.GenerateQuicklyTemplate;
using WebApi_Offcial.Controllers;

namespace WebApi_Offcial.ConfigureServices
{
    /// <summary>
    /// 生成代码
    /// </summary>
    public class GenerateQuicklyTool
    {
        /// <summary>
        /// 参数
        /// </summary>
        private GenerateQuicklyInput input;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="generateQuicklyInput"></param>
        public GenerateQuicklyTool(GenerateQuicklyInput generateQuicklyInput)
        {
            input = generateQuicklyInput;
        }

        /// <summary>
        /// 生成代码文件
        /// </summary>
        public void Generate()
        {

            GenerateActionFilter();
            GenerateController();
            GenerateService();
            GenerateDao();
        }

        public void GenerateByTable(string inputType)
        {
            switch (inputType)
            {
                case "Add":
                case "Update":
                    // 根目录
                    string rootPath = Path.Combine(Environment.CurrentDirectory.ToString().Replace("WebAPI", "Service"), input.SwaggerGroupEnumName);
                    // 先在根目录创建类文件夹
                    string classPath = Path.Combine(rootPath, input.ClassNamePrefix);
                    string dtoPath = Path.Combine(classPath, "Dto");
                    GenerateInput(dtoPath, inputType);
                    break;
                case "GetPage":
                    string iDaoFath = Path.Combine(Environment.CurrentDirectory.ToString().Replace("WebAPI", "IDao"), "Interface", input.SwaggerGroupEnumName);
                    // 在类文件夹下创建dto文件夹
                    string managePath = Path.Combine(iDaoFath, input.ClassNamePrefix);
                    GenerateInput(managePath, "GetPage");
                    break;
                default:
                    break;
            }

        }


        /// <summary>
        /// 生成input
        /// </summary>
        private void GenerateInput(string path, string inputType)
        {
            Directory.CreateDirectory(path);
            Assembly assembly = Assembly.Load("IDao");
            Type tableType = assembly.GetTypes().Where(p => p.IsClass && !p.IsInterface && !p.IsSealed && p.Namespace.EndsWith("Repositoty")).Where(p => p.Name == input.TableName).First();
            input.PropertyInfos = tableType.GetProperties(BindingFlags.Public | BindingFlags.DeclaredOnly | BindingFlags.Instance).ToList();
            string inputNmae = input.TableName.Split("_")[1];
            input.InputType = inputType;
            InputTemplate inputTemplate = new InputTemplate(input);
            string text = inputTemplate.TransformText();
            string fileNmae = inputType == "GetPage" ? $"Get{inputNmae}PageInput.cs" : $"{input.InputType}{inputNmae}Input.cs";
            File.WriteAllText(Path.Combine(path, fileNmae), text);

        }

        /// <summary>
        /// 生成过滤器
        /// </summary>
        private void GenerateActionFilter()
        {
            ActionFilterTemplate actionFilter = new ActionFilterTemplate(input);
            string text = actionFilter.TransformText();
            File.WriteAllText(Path.Combine(Environment.CurrentDirectory.ToString(), "ActionFilters", input.SwaggerGroupEnumName, $"{input.ClassNamePrefix}ActionFilter.cs"), text);
        }

        /// <summary>
        /// 生成控制器
        /// </summary>
        private void GenerateController()
        {
            ControllerTemplate controllerTemplate = new ControllerTemplate(input);
            string text = controllerTemplate.TransformText();
            File.WriteAllText(Path.Combine(Environment.CurrentDirectory.ToString(), "Controllers", input.SwaggerGroupEnumName, $"{input.ClassNamePrefix}Controller.cs"), text);
        }

        /// <summary>
        /// 生成业务和业务接口
        /// </summary>
        private void GenerateService()
        {
            BusinesLogicImplTemplate serviceTemplate = new BusinesLogicImplTemplate(input);
            IBusinesLogicTemplate iServiceTemplate = new IBusinesLogicTemplate(input);
            string iserviceText = iServiceTemplate.TransformText();
            string serviceText = serviceTemplate.TransformText();
            // 根目录
            string rootPath = Path.Combine(Environment.CurrentDirectory.ToString().Replace("WebAPI", "Service"), input.SwaggerGroupEnumName);
            // 先在根目录创建类文件夹
            string classPath = Path.Combine(rootPath, input.ClassNamePrefix);
            Directory.CreateDirectory(classPath);
            // 在类文件夹下创建dto文件夹
            string dtoPath = Path.Combine(classPath, "Dto");
            GenerateInput(dtoPath, "Add");
            GenerateInput(dtoPath, "Update");
            File.WriteAllText(Path.Combine(classPath, $"I{input.ClassNamePrefix}Service.cs"), iserviceText);
            File.WriteAllText(Path.Combine(classPath, $"{input.ClassNamePrefix}BusinesLogic.cs"), serviceText);

        }

        /// <summary>
        /// 生成数据库访问层
        /// </summary>
        private void GenerateDao()
        {
            DataSphereTemplate daoTemplate = new DataSphereTemplate(input);
            string daoText = daoTemplate.TransformText();
            string daoPath = Path.Combine(Environment.CurrentDirectory.ToString().Replace("WebAPI", "Dao"), input.SwaggerGroupEnumName);
            File.WriteAllText(Path.Combine(daoPath, $"{input.ClassNamePrefix}Dao.cs"), daoText);

            IDataSphereTemplate iDaoTemplate = new IDataSphereTemplate(input);
            string idaoText = iDaoTemplate.TransformText();
            string iDaoFath = Path.Combine(Environment.CurrentDirectory.ToString().Replace("WebAPI", "IDao"), "Interface", input.SwaggerGroupEnumName);
            // 在类文件夹下创建dto文件夹
            string managePath = Path.Combine(iDaoFath, input.ClassNamePrefix);
            Directory.CreateDirectory(managePath);
            GenerateInput(managePath, "GetPage");
            File.WriteAllText(Path.Combine(managePath, $"I{input.ClassNamePrefix}Dao.cs"), idaoText);
        }
    }
}

/// <summary>
/// 参数类
/// </summary>
public class GenerateQuicklyInput
{
    /// <summary>
    /// 类名前缀
    /// </summary>
    public string ClassNamePrefix { get; set; }

    /// <summary>
    /// 参数前缀，ClassNamePrefix的值首字母小写
    /// </summary>
    public string ParameterName { get; set; }

    /// <summary>
    /// 中文注释
    /// </summary>
    public string ChinesesName { get; set; }

    /// <summary>
    /// 不用填
    /// </summary>
    public string SwaggerGroupEnumName { get; set; } = "";

    /// <summary>
    /// 不用填
    /// </summary>
    public string ControllerBase { get; set; } = "ControllerBase";

    /// <summary>
    /// 主表名称
    /// </summary>
    public string TableName { get; set; }

    /// <summary>
    /// 放在前台还是后台
    /// </summary>
    public SwaggerGroupEnum enums { get; set; }


    public List<PropertyInfo> PropertyInfos { get; set; }


    public string InputType { get; set; }

    public GenerateQuicklyInput(string classNamePrefix, string chinesesName, string tableName, SwaggerGroupEnum enums)
    {
        ClassNamePrefix = classNamePrefix;
        ChinesesName = chinesesName;
        switch (enums)
        {
            case SwaggerGroupEnum.BackEnd:
                SwaggerGroupEnumName = "BackEnd";
                ControllerBase = nameof(BaseController);
                break;
            case SwaggerGroupEnum.FrontDesk:
                SwaggerGroupEnumName = "FrontDesk";
                break;
            case SwaggerGroupEnum.Center:
                SwaggerGroupEnumName = "CenterService";
                break;
            case SwaggerGroupEnum.System:
                SwaggerGroupEnumName = "SystemManage";
                ControllerBase = nameof(BaseController);
                break;
            default:
                break;
        }
        ParameterName = classNamePrefix.Substring(0, 1).ToLower() + classNamePrefix.Substring(1);
        TableName = tableName;
    }
}

