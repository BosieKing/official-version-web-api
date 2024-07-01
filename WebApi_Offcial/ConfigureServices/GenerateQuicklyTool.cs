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
            this.input = generateQuicklyInput;
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
            GenerateByTable();
        }

        /// <summary>
        /// 创建输入和输出类
        /// </summary>
        public void GenerateByTable()
        {
            // 根目录
            string rootPath = Path.Combine(Environment.CurrentDirectory.ToString().Replace("WebApi_Offcial", "Model"), "DTOs", input.SwaggerGroupEnumName, input.ClassNamePrefix);
            if (!Directory.Exists(rootPath))
            {
                Directory.CreateDirectory(rootPath);
            }
            Assembly assembly = Assembly.Load("Model");
            Type tableType = assembly.GetTypes().Where(p => p.IsClass && !p.IsInterface && !p.IsSealed ).Where(p => p.Name == input.TableName).First();

            this.input.PropertyInfos = tableType.GetProperties(BindingFlags.Public | BindingFlags.DeclaredOnly | BindingFlags.Instance).ToList();
            string inputNmae = this.input.TableName.Split("_")[1];
            string[] inputTypes = new string[] { "Add", "GetPage", "Update" };
            foreach (var inputType in inputTypes)
            {
                input.InputType = inputType;
                InputTemplate inputTemplate = new InputTemplate(input);
                string text = inputTemplate.TransformText();
                string fileNmae = inputType == "GetPage" ? $"Get{inputNmae}PageInput.cs" : $"{input.InputType}{inputNmae}Input.cs";
                System.IO.File.WriteAllText(Path.Combine(rootPath, fileNmae), text);
            }
        }

        /// <summary>
        /// 生成过滤器
        /// </summary>
        private void GenerateActionFilter()
        {
            ActionFilterTemplate actionFilter = new ActionFilterTemplate(input);
            string text = actionFilter.TransformText();
            System.IO.File.WriteAllText(Path.Combine(Environment.CurrentDirectory.ToString(), "ActionFilters", input.SwaggerGroupEnumName, $"{input.ClassNamePrefix}ActionFilter.cs"), text);
        }

        /// <summary>
        /// 生成控制器
        /// </summary>
        private void GenerateController()
        {
            ControllerTemplate controllerTemplate = new ControllerTemplate(input);
            string text = controllerTemplate.TransformText();
            System.IO.File.WriteAllText(Path.Combine(Environment.CurrentDirectory.ToString(), "Controllers", input.SwaggerGroupEnumName, $"{input.ClassNamePrefix}Controller.cs"), text);
        }

        /// <summary>
        /// 生成业务和业务接口
        /// </summary>
        private void GenerateService()
        {
            ServiceImplTemplate serviceTemplate = new ServiceImplTemplate(input);
            IServiceTemplate iServiceTemplate = new IServiceTemplate(input);
            string iserviceText = iServiceTemplate.TransformText();
            string serviceText = serviceTemplate.TransformText();
            // 根目录
            string rootPath = Path.Combine(Environment.CurrentDirectory.ToString().Replace("WebApi_Offcial", "Service"), input.SwaggerGroupEnumName, input.ClassNamePrefix);
            // 先在根目录创建类文件夹
            Directory.CreateDirectory(rootPath);
            // 创建接口和类
            System.IO.File.WriteAllText(Path.Combine(rootPath, $"I{input.ClassNamePrefix}Service.cs"), iserviceText);
            System.IO.File.WriteAllText(Path.Combine(rootPath, $"{input.ClassNamePrefix}ServiceImpl.cs"), serviceText);

        }

        /// <summary>
        /// 生成数据库访问层
        /// </summary>
        private void GenerateDao()
        {
            // 创建dao层代码
            DataSphereTemplate daoTemplate = new DataSphereTemplate(input);
            string daoText = daoTemplate.TransformText();
            string daoPath = (Path.Combine(Environment.CurrentDirectory.ToString().Replace("WebApi_Offcial", "DataSphere"), input.SwaggerGroupEnumName));
            System.IO.File.WriteAllText(Path.Combine(daoPath, $"{input.ClassNamePrefix}Dao.cs"), daoText);

            // 创建IDataSphereTemplate层代码
            IDataSphereTemplate iDaoTemplate = new IDataSphereTemplate(input);
            string idaoText = iDaoTemplate.TransformText();
            string iDaoFath = (Path.Combine(Environment.CurrentDirectory.ToString().Replace("WebApi_Offcial", "IDataSphere"), "Interfaces", input.SwaggerGroupEnumName, input.ClassNamePrefix));
            // 创建文件夹
            Directory.CreateDirectory(iDaoFath);
            // 创建接口
            System.IO.File.WriteAllText(Path.Combine(iDaoFath, $"I{input.ClassNamePrefix}Dao.cs"), idaoText);
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

    public string TableGroupName { get; set; }


    public string InputType { get; set; }

    public GenerateQuicklyInput(string classNamePrefix, string chinesesName, string tableName, SwaggerGroupEnum enums, TableGroupEnum tableGroup)
    {
        this.ClassNamePrefix = classNamePrefix;
        this.ChinesesName = chinesesName;
        this.TableGroupName = Enum.GetName(typeof(TableGroupEnum), tableGroup);
        switch (enums)
        {
            case SwaggerGroupEnum.BackEnd:
                this.SwaggerGroupEnumName = "BackEnd";
                this.ControllerBase = nameof(BaseController);
                break;
            case SwaggerGroupEnum.FrontDesk:
                this.SwaggerGroupEnumName = "FrontDesk";
                break;
            case SwaggerGroupEnum.Center:
                this.SwaggerGroupEnumName = "Center";
                break;
            case SwaggerGroupEnum.System:
                this.SwaggerGroupEnumName = "System";
                this.ControllerBase = nameof(BaseController);
                break;
            default:
                break;
        }
        ParameterName = classNamePrefix.Substring(0, 1).ToLower() + classNamePrefix.Substring(1);
        this.TableName = tableName;
    }
}

