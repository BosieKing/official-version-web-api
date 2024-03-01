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
        /// 生成控制器
        /// </summary>
        /// <param name="input"></param>
        public void CreateController(ControllerGenerateQuicklyInput input) 
        {            
            ControllerTemplate controllerTemplate = new ControllerTemplate(input);
            string controllerText = controllerTemplate.TransformText();
            string controllerPath = GetPath("Controllers", input.SwaggerGroupEnumName);
            System.IO.File.WriteAllText(Path.Combine(controllerPath, $"{input.ClassNamePrefix}Controller.cs"), controllerText);
        }


        private string GetPath(string dirName, string swaggerGroupEnumName) 
        {
            string projectPath = Environment.CurrentDirectory.ToString();
            return (Path.Combine(projectPath, dirName, swaggerGroupEnumName));
        }
    }

   
}

