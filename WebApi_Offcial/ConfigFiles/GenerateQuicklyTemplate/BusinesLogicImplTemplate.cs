﻿// ------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本: 17.0.0.0
//  
//     对此文件的更改可能导致不正确的行为，如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
// ------------------------------------------------------------------------------
namespace WebApi_Offcial.ConfigFiles.GenerateQuicklyTemplate
{
    using System.Linq;
    using System.Text;
    using System.Collections.Generic;
    using System;
    
    /// <summary>
    /// Class to produce the template output
    /// </summary>
    
    #line 1 "D:\Student\Net项目\webapi\正式版本\official-version-web-api\WebApi_Offcial\ConfigFiles\GenerateQuicklyTemplate\BusinesLogicImplTemplate.tt"
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.TextTemplating", "17.0.0.0")]
    public partial class BusinesLogicImplTemplate : BusinesLogicImplTemplateBase
    {
#line hidden
        /// <summary>
        /// Create the template output
        /// </summary>
        public virtual string TransformText()
        {
            this.Write("using IDataSphere.Interface.");
            
            #line 7 "D:\Student\Net项目\webapi\正式版本\official-version-web-api\WebApi_Offcial\ConfigFiles\GenerateQuicklyTemplate\BusinesLogicImplTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(this.data.SwaggerGroupEnumName));
            
            #line default
            #line hidden
            this.Write(";\r\nusing IDataSphere.Repositoty;\r\nusing Mapster;\r\nusing Microsoft.EntityFramework" +
                    "Core;\r\nusing SharedLibrary.NormalizeModel;\r\nusing BusinesLogic.");
            
            #line 12 "D:\Student\Net项目\webapi\正式版本\official-version-web-api\WebApi_Offcial\ConfigFiles\GenerateQuicklyTemplate\BusinesLogicImplTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(this.data.SwaggerGroupEnumName));
            
            #line default
            #line hidden
            this.Write(".");
            
            #line 12 "D:\Student\Net项目\webapi\正式版本\official-version-web-api\WebApi_Offcial\ConfigFiles\GenerateQuicklyTemplate\BusinesLogicImplTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(this.data.ClassNamePrefix));
            
            #line default
            #line hidden
            this.Write(".Dto;\r\nusing IDataSphere.Interface.");
            
            #line 13 "D:\Student\Net项目\webapi\正式版本\official-version-web-api\WebApi_Offcial\ConfigFiles\GenerateQuicklyTemplate\BusinesLogicImplTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(this.data.SwaggerGroupEnumName));
            
            #line default
            #line hidden
            this.Write(".");
            
            #line 13 "D:\Student\Net项目\webapi\正式版本\official-version-web-api\WebApi_Offcial\ConfigFiles\GenerateQuicklyTemplate\BusinesLogicImplTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(this.data.ClassNamePrefix));
            
            #line default
            #line hidden
            this.Write(";\r\nusing System.Text;\r\nusing UtilityToolkit.Utils;\r\n\r\nnamespace Service.");
            
            #line 17 "D:\Student\Net项目\webapi\正式版本\official-version-web-api\WebApi_Offcial\ConfigFiles\GenerateQuicklyTemplate\BusinesLogicImplTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(this.data.SwaggerGroupEnumName));
            
            #line default
            #line hidden
            this.Write(".");
            
            #line 17 "D:\Student\Net项目\webapi\正式版本\official-version-web-api\WebApi_Offcial\ConfigFiles\GenerateQuicklyTemplate\BusinesLogicImplTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(this.data.ClassNamePrefix));
            
            #line default
            #line hidden
            this.Write("\r\n{\r\n    /// <summary>\r\n    /// ");
            
            #line 20 "D:\Student\Net项目\webapi\正式版本\official-version-web-api\WebApi_Offcial\ConfigFiles\GenerateQuicklyTemplate\BusinesLogicImplTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(this.data.ChinesesName));
            
            #line default
            #line hidden
            this.Write("业务类\r\n    /// </summary>\r\n    public class ");
            
            #line 22 "D:\Student\Net项目\webapi\正式版本\official-version-web-api\WebApi_Offcial\ConfigFiles\GenerateQuicklyTemplate\BusinesLogicImplTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(this.data.ClassNamePrefix));
            
            #line default
            #line hidden
            this.Write("ServiceImpl : I");
            
            #line 22 "D:\Student\Net项目\webapi\正式版本\official-version-web-api\WebApi_Offcial\ConfigFiles\GenerateQuicklyTemplate\BusinesLogicImplTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(this.data.ClassNamePrefix));
            
            #line default
            #line hidden
            this.Write("Service\r\n    {\r\n        #region 构造函数\r\n        private readonly I");
            
            #line 25 "D:\Student\Net项目\webapi\正式版本\official-version-web-api\WebApi_Offcial\ConfigFiles\GenerateQuicklyTemplate\BusinesLogicImplTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(this.data.ClassNamePrefix));
            
            #line default
            #line hidden
            this.Write("Dao _");
            
            #line 25 "D:\Student\Net项目\webapi\正式版本\official-version-web-api\WebApi_Offcial\ConfigFiles\GenerateQuicklyTemplate\BusinesLogicImplTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(this.data.ParameterName));
            
            #line default
            #line hidden
            this.Write("Dao;\r\n        public ");
            
            #line 26 "D:\Student\Net项目\webapi\正式版本\official-version-web-api\WebApi_Offcial\ConfigFiles\GenerateQuicklyTemplate\BusinesLogicImplTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(this.data.ClassNamePrefix));
            
            #line default
            #line hidden
            this.Write("ServiceImpl(I");
            
            #line 26 "D:\Student\Net项目\webapi\正式版本\official-version-web-api\WebApi_Offcial\ConfigFiles\GenerateQuicklyTemplate\BusinesLogicImplTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(this.data.ClassNamePrefix));
            
            #line default
            #line hidden
            this.Write("Dao ");
            
            #line 26 "D:\Student\Net项目\webapi\正式版本\official-version-web-api\WebApi_Offcial\ConfigFiles\GenerateQuicklyTemplate\BusinesLogicImplTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(this.data.ParameterName));
            
            #line default
            #line hidden
            this.Write("Dao)\r\n        {\r\n            this._");
            
            #line 28 "D:\Student\Net项目\webapi\正式版本\official-version-web-api\WebApi_Offcial\ConfigFiles\GenerateQuicklyTemplate\BusinesLogicImplTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(this.data.ParameterName));
            
            #line default
            #line hidden
            this.Write("Dao = ");
            
            #line 28 "D:\Student\Net项目\webapi\正式版本\official-version-web-api\WebApi_Offcial\ConfigFiles\GenerateQuicklyTemplate\BusinesLogicImplTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(this.data.ParameterName));
            
            #line default
            #line hidden
            this.Write("Dao;\r\n        }\r\n        #endregion\r\n\r\n        #region 查询\r\n        /// <summary>\r" +
                    "\n        /// 分页查询\r\n        /// </summary>\r\n        /// <param name=\"input\"></par" +
                    "am>\r\n        /// <returns></returns>\r\n        public Task<PageResult> Get");
            
            #line 38 "D:\Student\Net项目\webapi\正式版本\official-version-web-api\WebApi_Offcial\ConfigFiles\GenerateQuicklyTemplate\BusinesLogicImplTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(this.data.TableName.Split("_")[1]));
            
            #line default
            #line hidden
            this.Write("Page(Get");
            
            #line 38 "D:\Student\Net项目\webapi\正式版本\official-version-web-api\WebApi_Offcial\ConfigFiles\GenerateQuicklyTemplate\BusinesLogicImplTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(this.data.TableName.Split("_")[1]));
            
            #line default
            #line hidden
            this.Write("PageInput input);\r\n        {\r\n            return await _");
            
            #line 40 "D:\Student\Net项目\webapi\正式版本\official-version-web-api\WebApi_Offcial\ConfigFiles\GenerateQuicklyTemplate\BusinesLogicImplTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(this.data.ParameterName));
            
            #line default
            #line hidden
            this.Write("Dao.Get");
            
            #line 40 "D:\Student\Net项目\webapi\正式版本\official-version-web-api\WebApi_Offcial\ConfigFiles\GenerateQuicklyTemplate\BusinesLogicImplTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(this.data.TableName.Split("_")[1]));
            
            #line default
            #line hidden
            this.Write("Page(input);\r\n        }\r\n        #endregion\r\n\r\n        #region 新增\r\n        /// <s" +
                    "ummary>\r\n        /// 新增\r\n        /// </summary>\r\n        /// <param name=\"input\"" +
                    "></param>\r\n        /// <returns></returns>\r\n        public async Task<bool> Add");
            
            #line 50 "D:\Student\Net项目\webapi\正式版本\official-version-web-api\WebApi_Offcial\ConfigFiles\GenerateQuicklyTemplate\BusinesLogicImplTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(this.data.TableName.Split("_")[1]));
            
            #line default
            #line hidden
            this.Write("(Add");
            
            #line 50 "D:\Student\Net项目\webapi\正式版本\official-version-web-api\WebApi_Offcial\ConfigFiles\GenerateQuicklyTemplate\BusinesLogicImplTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(this.data.TableName.Split("_")[1]));
            
            #line default
            #line hidden
            this.Write("Input input)\r\n        {          \r\n            ");
            
            #line 52 "D:\Student\Net项目\webapi\正式版本\official-version-web-api\WebApi_Offcial\ConfigFiles\GenerateQuicklyTemplate\BusinesLogicImplTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(this.data.TableName));
            
            #line default
            #line hidden
            this.Write(" data = input.Adapt<");
            
            #line 52 "D:\Student\Net项目\webapi\正式版本\official-version-web-api\WebApi_Offcial\ConfigFiles\GenerateQuicklyTemplate\BusinesLogicImplTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(this.data.TableName));
            
            #line default
            #line hidden
            this.Write(">();\r\n            return await _");
            
            #line 53 "D:\Student\Net项目\webapi\正式版本\official-version-web-api\WebApi_Offcial\ConfigFiles\GenerateQuicklyTemplate\BusinesLogicImplTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(this.data.ParameterName));
            
            #line default
            #line hidden
            this.Write("Dao.AddAsync(data);\r\n        }\r\n        #endregion\r\n\r\n        #region 更新\r\n       " +
                    " /// <summary>\r\n        /// 更新\r\n        /// </summary>\r\n        /// <param name=" +
                    "\"input\"></param>\r\n        /// <returns></returns>\r\n        public async Task<boo" +
                    "l> Update");
            
            #line 63 "D:\Student\Net项目\webapi\正式版本\official-version-web-api\WebApi_Offcial\ConfigFiles\GenerateQuicklyTemplate\BusinesLogicImplTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(this.data.TableName.Split("_")[1]));
            
            #line default
            #line hidden
            this.Write("(Update");
            
            #line 63 "D:\Student\Net项目\webapi\正式版本\official-version-web-api\WebApi_Offcial\ConfigFiles\GenerateQuicklyTemplate\BusinesLogicImplTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(this.data.TableName.Split("_")[1]));
            
            #line default
            #line hidden
            this.Write("Input input) \r\n        {\r\n            ");
            
            #line 65 "D:\Student\Net项目\webapi\正式版本\official-version-web-api\WebApi_Offcial\ConfigFiles\GenerateQuicklyTemplate\BusinesLogicImplTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(this.data.TableName));
            
            #line default
            #line hidden
            this.Write(" data = input.Adapt<");
            
            #line 65 "D:\Student\Net项目\webapi\正式版本\official-version-web-api\WebApi_Offcial\ConfigFiles\GenerateQuicklyTemplate\BusinesLogicImplTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(this.data.TableName));
            
            #line default
            #line hidden
            this.Write(">();\r\n            data.Id = input.Id;\r\n            return await _");
            
            #line 67 "D:\Student\Net项目\webapi\正式版本\official-version-web-api\WebApi_Offcial\ConfigFiles\GenerateQuicklyTemplate\BusinesLogicImplTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(this.data.ParameterName));
            
            #line default
            #line hidden
            this.Write("Dao.UpdateAsync(data);\r\n        }\r\n        #endregion\r\n\r\n        #region 删除\r\n    " +
                    "    /// <summary>\r\n        /// 删除\r\n        /// </summary>\r\n        /// <param na" +
                    "me=\"id\"></param>\r\n        /// <returns></returns>\r\n        public async Task<boo" +
                    "l> Delete");
            
            #line 77 "D:\Student\Net项目\webapi\正式版本\official-version-web-api\WebApi_Offcial\ConfigFiles\GenerateQuicklyTemplate\BusinesLogicImplTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(this.data.TableName.Split("_")[1]));
            
            #line default
            #line hidden
            this.Write("(long id)\r\n        {        \r\n            return await _");
            
            #line 79 "D:\Student\Net项目\webapi\正式版本\official-version-web-api\WebApi_Offcial\ConfigFiles\GenerateQuicklyTemplate\BusinesLogicImplTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(this.data.ParameterName));
            
            #line default
            #line hidden
            this.Write("Dao.DeleteAsync(id);\r\n        }\r\n        #endregion\r\n    }\r\n}\r\n");
            return this.GenerationEnvironment.ToString();
        }
    }
    
    #line default
    #line hidden
    #region Base class
    /// <summary>
    /// Base class for this transformation
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.TextTemplating", "17.0.0.0")]
    public class BusinesLogicImplTemplateBase
    {
        #region Fields
        private global::System.Text.StringBuilder generationEnvironmentField;
        private global::System.CodeDom.Compiler.CompilerErrorCollection errorsField;
        private global::System.Collections.Generic.List<int> indentLengthsField;
        private string currentIndentField = "";
        private bool endsWithNewline;
        private global::System.Collections.Generic.IDictionary<string, object> sessionField;
        #endregion
        #region Properties
        /// <summary>
        /// The string builder that generation-time code is using to assemble generated output
        /// </summary>
        protected System.Text.StringBuilder GenerationEnvironment
        {
            get
            {
                if ((this.generationEnvironmentField == null))
                {
                    this.generationEnvironmentField = new global::System.Text.StringBuilder();
                }
                return this.generationEnvironmentField;
            }
            set
            {
                this.generationEnvironmentField = value;
            }
        }
        /// <summary>
        /// The error collection for the generation process
        /// </summary>
        public System.CodeDom.Compiler.CompilerErrorCollection Errors
        {
            get
            {
                if ((this.errorsField == null))
                {
                    this.errorsField = new global::System.CodeDom.Compiler.CompilerErrorCollection();
                }
                return this.errorsField;
            }
        }
        /// <summary>
        /// A list of the lengths of each indent that was added with PushIndent
        /// </summary>
        private System.Collections.Generic.List<int> indentLengths
        {
            get
            {
                if ((this.indentLengthsField == null))
                {
                    this.indentLengthsField = new global::System.Collections.Generic.List<int>();
                }
                return this.indentLengthsField;
            }
        }
        /// <summary>
        /// Gets the current indent we use when adding lines to the output
        /// </summary>
        public string CurrentIndent
        {
            get
            {
                return this.currentIndentField;
            }
        }
        /// <summary>
        /// Current transformation session
        /// </summary>
        public virtual global::System.Collections.Generic.IDictionary<string, object> Session
        {
            get
            {
                return this.sessionField;
            }
            set
            {
                this.sessionField = value;
            }
        }
        #endregion
        #region Transform-time helpers
        /// <summary>
        /// Write text directly into the generated output
        /// </summary>
        public void Write(string textToAppend)
        {
            if (string.IsNullOrEmpty(textToAppend))
            {
                return;
            }
            // If we're starting off, or if the previous text ended with a newline,
            // we have to append the current indent first.
            if (((this.GenerationEnvironment.Length == 0) 
                        || this.endsWithNewline))
            {
                this.GenerationEnvironment.Append(this.currentIndentField);
                this.endsWithNewline = false;
            }
            // Check if the current text ends with a newline
            if (textToAppend.EndsWith(global::System.Environment.NewLine, global::System.StringComparison.CurrentCulture))
            {
                this.endsWithNewline = true;
            }
            // This is an optimization. If the current indent is "", then we don't have to do any
            // of the more complex stuff further down.
            if ((this.currentIndentField.Length == 0))
            {
                this.GenerationEnvironment.Append(textToAppend);
                return;
            }
            // Everywhere there is a newline in the text, add an indent after it
            textToAppend = textToAppend.Replace(global::System.Environment.NewLine, (global::System.Environment.NewLine + this.currentIndentField));
            // If the text ends with a newline, then we should strip off the indent added at the very end
            // because the appropriate indent will be added when the next time Write() is called
            if (this.endsWithNewline)
            {
                this.GenerationEnvironment.Append(textToAppend, 0, (textToAppend.Length - this.currentIndentField.Length));
            }
            else
            {
                this.GenerationEnvironment.Append(textToAppend);
            }
        }
        /// <summary>
        /// Write text directly into the generated output
        /// </summary>
        public void WriteLine(string textToAppend)
        {
            this.Write(textToAppend);
            this.GenerationEnvironment.AppendLine();
            this.endsWithNewline = true;
        }
        /// <summary>
        /// Write formatted text directly into the generated output
        /// </summary>
        public void Write(string format, params object[] args)
        {
            this.Write(string.Format(global::System.Globalization.CultureInfo.CurrentCulture, format, args));
        }
        /// <summary>
        /// Write formatted text directly into the generated output
        /// </summary>
        public void WriteLine(string format, params object[] args)
        {
            this.WriteLine(string.Format(global::System.Globalization.CultureInfo.CurrentCulture, format, args));
        }
        /// <summary>
        /// Raise an error
        /// </summary>
        public void Error(string message)
        {
            System.CodeDom.Compiler.CompilerError error = new global::System.CodeDom.Compiler.CompilerError();
            error.ErrorText = message;
            this.Errors.Add(error);
        }
        /// <summary>
        /// Raise a warning
        /// </summary>
        public void Warning(string message)
        {
            System.CodeDom.Compiler.CompilerError error = new global::System.CodeDom.Compiler.CompilerError();
            error.ErrorText = message;
            error.IsWarning = true;
            this.Errors.Add(error);
        }
        /// <summary>
        /// Increase the indent
        /// </summary>
        public void PushIndent(string indent)
        {
            if ((indent == null))
            {
                throw new global::System.ArgumentNullException("indent");
            }
            this.currentIndentField = (this.currentIndentField + indent);
            this.indentLengths.Add(indent.Length);
        }
        /// <summary>
        /// Remove the last indent that was added with PushIndent
        /// </summary>
        public string PopIndent()
        {
            string returnValue = "";
            if ((this.indentLengths.Count > 0))
            {
                int indentLength = this.indentLengths[(this.indentLengths.Count - 1)];
                this.indentLengths.RemoveAt((this.indentLengths.Count - 1));
                if ((indentLength > 0))
                {
                    returnValue = this.currentIndentField.Substring((this.currentIndentField.Length - indentLength));
                    this.currentIndentField = this.currentIndentField.Remove((this.currentIndentField.Length - indentLength));
                }
            }
            return returnValue;
        }
        /// <summary>
        /// Remove any indentation
        /// </summary>
        public void ClearIndent()
        {
            this.indentLengths.Clear();
            this.currentIndentField = "";
        }
        #endregion
        #region ToString Helpers
        /// <summary>
        /// Utility class to produce culture-oriented representation of an object as a string.
        /// </summary>
        public class ToStringInstanceHelper
        {
            private System.IFormatProvider formatProviderField  = global::System.Globalization.CultureInfo.InvariantCulture;
            /// <summary>
            /// Gets or sets format provider to be used by ToStringWithCulture method.
            /// </summary>
            public System.IFormatProvider FormatProvider
            {
                get
                {
                    return this.formatProviderField ;
                }
                set
                {
                    if ((value != null))
                    {
                        this.formatProviderField  = value;
                    }
                }
            }
            /// <summary>
            /// This is called from the compile/run appdomain to convert objects within an expression block to a string
            /// </summary>
            public string ToStringWithCulture(object objectToConvert)
            {
                if ((objectToConvert == null))
                {
                    throw new global::System.ArgumentNullException("objectToConvert");
                }
                System.Type t = objectToConvert.GetType();
                System.Reflection.MethodInfo method = t.GetMethod("ToString", new System.Type[] {
                            typeof(System.IFormatProvider)});
                if ((method == null))
                {
                    return objectToConvert.ToString();
                }
                else
                {
                    return ((string)(method.Invoke(objectToConvert, new object[] {
                                this.formatProviderField })));
                }
            }
        }
        private ToStringInstanceHelper toStringHelperField = new ToStringInstanceHelper();
        /// <summary>
        /// Helper to produce culture-oriented representation of an object as a string
        /// </summary>
        public ToStringInstanceHelper ToStringHelper
        {
            get
            {
                return this.toStringHelperField;
            }
        }
        #endregion
    }
    #endregion
}
