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
    using System.Reflection;
    using System.ComponentModel.DataAnnotations;
    using System;
    
    /// <summary>
    /// Class to produce the template output
    /// </summary>
    
    #line 1 "D:\Student\Net项目\github项目\webapi项目\WebApi_Offcial\ConfigFiles\GenerateQuicklyTemplate\InputTemplate.tt"
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.TextTemplating", "17.0.0.0")]
    public partial class InputTemplate : InputTemplateBase
    {
#line hidden
        /// <summary>
        /// Create the template output
        /// </summary>
        public virtual string TransformText()
        {
            this.Write("using System.ComponentModel.DataAnnotations;\r\n");
            
            #line 11 "D:\Student\Net项目\github项目\webapi项目\WebApi_Offcial\ConfigFiles\GenerateQuicklyTemplate\InputTemplate.tt"
if(this.data.InputType == "GetPage"){
            
            #line default
            #line hidden
            this.Write("using Model.Commons.SharedData;\r\n");
            
            #line 12 "D:\Student\Net项目\github项目\webapi项目\WebApi_Offcial\ConfigFiles\GenerateQuicklyTemplate\InputTemplate.tt"
}
            
            #line default
            #line hidden
            
            #line 13 "D:\Student\Net项目\github项目\webapi项目\WebApi_Offcial\ConfigFiles\GenerateQuicklyTemplate\InputTemplate.tt"
if(this.data.InputType == "GetPage"){
            
            #line default
            #line hidden
            this.Write("namespace Model.DTOs.");
            
            #line 13 "D:\Student\Net项目\github项目\webapi项目\WebApi_Offcial\ConfigFiles\GenerateQuicklyTemplate\InputTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(this.data.SwaggerGroupEnumName));
            
            #line default
            #line hidden
            this.Write(".");
            
            #line 13 "D:\Student\Net项目\github项目\webapi项目\WebApi_Offcial\ConfigFiles\GenerateQuicklyTemplate\InputTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(this.data.ClassNamePrefix));
            
            #line default
            #line hidden
            
            #line 13 "D:\Student\Net项目\github项目\webapi项目\WebApi_Offcial\ConfigFiles\GenerateQuicklyTemplate\InputTemplate.tt"
}else{
            
            #line default
            #line hidden
            this.Write("namespace Model.DTOs.");
            
            #line 14 "D:\Student\Net项目\github项目\webapi项目\WebApi_Offcial\ConfigFiles\GenerateQuicklyTemplate\InputTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(this.data.SwaggerGroupEnumName));
            
            #line default
            #line hidden
            this.Write(".");
            
            #line 14 "D:\Student\Net项目\github项目\webapi项目\WebApi_Offcial\ConfigFiles\GenerateQuicklyTemplate\InputTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(this.data.ClassNamePrefix));
            
            #line default
            #line hidden
            this.Write(" ");
            
            #line 14 "D:\Student\Net项目\github项目\webapi项目\WebApi_Offcial\ConfigFiles\GenerateQuicklyTemplate\InputTemplate.tt"
} 
            
            #line default
            #line hidden
            this.Write("\r\n{");
            
            #line 16 "D:\Student\Net项目\github项目\webapi项目\WebApi_Offcial\ConfigFiles\GenerateQuicklyTemplate\InputTemplate.tt"
  
     switch (this.data.InputType)
          {             
             // 如果是新增类则输出
              case "Add": 
           
    
            
            #line default
            #line hidden
            this.Write("\r\n    public class ");
            
            #line 24 "D:\Student\Net项目\github项目\webapi项目\WebApi_Offcial\ConfigFiles\GenerateQuicklyTemplate\InputTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(this.data.InputType));
            
            #line default
            #line hidden
            
            #line 24 "D:\Student\Net项目\github项目\webapi项目\WebApi_Offcial\ConfigFiles\GenerateQuicklyTemplate\InputTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(this.data.TableName.Split("_")[1]));
            
            #line default
            #line hidden
            this.Write("Input\r\n    {");
            
            #line 25 "D:\Student\Net项目\github项目\webapi项目\WebApi_Offcial\ConfigFiles\GenerateQuicklyTemplate\InputTemplate.tt"
    
           
        foreach (var item in this.data.PropertyInfos)
        {
                string propertyName = item.Name;
                string typeName = string.Empty;
                switch (item.PropertyType.Name)
                {
                    case "Int16":
                        typeName = "short";
                        break;
                    case "Int32":
                        typeName = "int";
                        break;
                    case "Int64":
                        typeName = "long";
                        break;
                    case "Double":
                        typeName = "double";
                        break;
                    case "Decimal":
                        typeName = "decimal";
                        break;
                    case "Boolean":
                        typeName = "bool";
                        break;
                    case "String":
                        typeName = "string";
                        break;
                    case "Char":
                        typeName = "char";
                        break;
                    case "DateTime":
                        typeName = "DateTime";
                        break;
                }
            
            #line default
            #line hidden
            this.Write("               \r\n        /// <summary>\r\n        /// ");
            
            #line 62 "D:\Student\Net项目\github项目\webapi项目\WebApi_Offcial\ConfigFiles\GenerateQuicklyTemplate\InputTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(propertyName));
            
            #line default
            #line hidden
            this.Write("\r\n        /// </summary>\r\n        [Required(ErrorMessage = \"");
            
            #line 64 "D:\Student\Net项目\github项目\webapi项目\WebApi_Offcial\ConfigFiles\GenerateQuicklyTemplate\InputTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(propertyName));
            
            #line default
            #line hidden
            this.Write("Required\")]");
            
            #line 64 "D:\Student\Net项目\github项目\webapi项目\WebApi_Offcial\ConfigFiles\GenerateQuicklyTemplate\InputTemplate.tt"
 if(typeName == "string" || typeName == "char") { 
            
            #line default
            #line hidden
            
            #line 64 "D:\Student\Net项目\github项目\webapi项目\WebApi_Offcial\ConfigFiles\GenerateQuicklyTemplate\InputTemplate.tt"

            var customAttributes = item.GetCustomAttribute(typeof(MaxLengthAttribute)); 
            if(customAttributes != null){
            var maxLength = ((System.ComponentModel.DataAnnotations.MaxLengthAttribute)customAttributes).Length; 
         
            
            #line default
            #line hidden
            this.Write("        \r\n        [MaxLength(");
            
            #line 69 "D:\Student\Net项目\github项目\webapi项目\WebApi_Offcial\ConfigFiles\GenerateQuicklyTemplate\InputTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(maxLength));
            
            #line default
            #line hidden
            this.Write(",ErrorMessage = \"");
            
            #line 69 "D:\Student\Net项目\github项目\webapi项目\WebApi_Offcial\ConfigFiles\GenerateQuicklyTemplate\InputTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(propertyName));
            
            #line default
            #line hidden
            this.Write("TooLong");
            
            #line 69 "D:\Student\Net项目\github项目\webapi项目\WebApi_Offcial\ConfigFiles\GenerateQuicklyTemplate\InputTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(maxLength));
            
            #line default
            #line hidden
            this.Write("\")]");
            
            #line 69 "D:\Student\Net项目\github项目\webapi项目\WebApi_Offcial\ConfigFiles\GenerateQuicklyTemplate\InputTemplate.tt"
 }}
            
            #line default
            #line hidden
            this.Write("   \r\n        public ");
            
            #line 70 "D:\Student\Net项目\github项目\webapi项目\WebApi_Offcial\ConfigFiles\GenerateQuicklyTemplate\InputTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(typeName));
            
            #line default
            #line hidden
            this.Write(" ");
            
            #line 70 "D:\Student\Net项目\github项目\webapi项目\WebApi_Offcial\ConfigFiles\GenerateQuicklyTemplate\InputTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(propertyName));
            
            #line default
            #line hidden
            this.Write(" { get; set; }");
            
            #line 70 "D:\Student\Net项目\github项目\webapi项目\WebApi_Offcial\ConfigFiles\GenerateQuicklyTemplate\InputTemplate.tt"
 if(typeName == "bool"){ 
            
            #line default
            #line hidden
            this.Write(" = false;");
            
            #line 70 "D:\Student\Net项目\github项目\webapi项目\WebApi_Offcial\ConfigFiles\GenerateQuicklyTemplate\InputTemplate.tt"
 } 
            
            #line default
            #line hidden
            this.Write("        \r\n        ");
            
            #line 72 "D:\Student\Net项目\github项目\webapi项目\WebApi_Offcial\ConfigFiles\GenerateQuicklyTemplate\InputTemplate.tt"
       
     
     }            
 
            
            #line default
            #line hidden
            this.Write("} \r\n\r\n                \r\n                  ");
            
            #line 78 "D:\Student\Net项目\github项目\webapi项目\WebApi_Offcial\ConfigFiles\GenerateQuicklyTemplate\InputTemplate.tt"
  break;
                case "GetPage": 
            
            #line default
            #line hidden
            this.Write("\r\n    public class Get");
            
            #line 81 "D:\Student\Net项目\github项目\webapi项目\WebApi_Offcial\ConfigFiles\GenerateQuicklyTemplate\InputTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(this.data.TableName.Split("_")[1]));
            
            #line default
            #line hidden
            this.Write("PageInput : PageInput\r\n    {");
            
            #line 82 "D:\Student\Net项目\github项目\webapi项目\WebApi_Offcial\ConfigFiles\GenerateQuicklyTemplate\InputTemplate.tt"
       
        foreach (var item in this.data.PropertyInfos)
        {
                string propertyName = item.Name;
                string typeName = string.Empty;
                switch (item.PropertyType.Name)
                {
                    case "Int16":
                        typeName = "short";
                        break;
                    case "Int32":
                        typeName = "int";
                        break;
                    case "Int64":
                        typeName = "long";
                        break;
                    case "Double":
                        typeName = "double";
                        break;
                    case "Decimal":
                        typeName = "decimal";
                        break;
                    case "Boolean":
                        typeName = "bool";
                        break;
                    case "String":
                        typeName = "string";
                        break;
                    case "Char":
                        typeName = "char";
                        break;
                    case "DateTime":
                        typeName = "DateTime";
                        break;
                }
                
            
            #line default
            #line hidden
            this.Write("\r\n        /// <summary>\r\n        /// ");
            
            #line 120 "D:\Student\Net项目\github项目\webapi项目\WebApi_Offcial\ConfigFiles\GenerateQuicklyTemplate\InputTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(propertyName));
            
            #line default
            #line hidden
            this.Write("\r\n        /// </summary>");
            
            #line 121 "D:\Student\Net项目\github项目\webapi项目\WebApi_Offcial\ConfigFiles\GenerateQuicklyTemplate\InputTemplate.tt"
 
        if(typeName == "string" || typeName == "char"){
            var customAttributes = item.GetCustomAttribute(typeof(MaxLengthAttribute));           
               if(customAttributes != null){
            var maxLength = ((System.ComponentModel.DataAnnotations.MaxLengthAttribute)customAttributes).Length;    
        
            
            #line default
            #line hidden
            this.Write("\r\n        [MaxLength(");
            
            #line 128 "D:\Student\Net项目\github项目\webapi项目\WebApi_Offcial\ConfigFiles\GenerateQuicklyTemplate\InputTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(maxLength));
            
            #line default
            #line hidden
            this.Write(",ErrorMessage = \"");
            
            #line 128 "D:\Student\Net项目\github项目\webapi项目\WebApi_Offcial\ConfigFiles\GenerateQuicklyTemplate\InputTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(propertyName));
            
            #line default
            #line hidden
            this.Write("TooLong");
            
            #line 128 "D:\Student\Net项目\github项目\webapi项目\WebApi_Offcial\ConfigFiles\GenerateQuicklyTemplate\InputTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(maxLength));
            
            #line default
            #line hidden
            this.Write("\")]");
            
            #line 128 "D:\Student\Net项目\github项目\webapi项目\WebApi_Offcial\ConfigFiles\GenerateQuicklyTemplate\InputTemplate.tt"
}}
            
            
            #line default
            #line hidden
            this.Write("\r\n        public ");
            
            #line 131 "D:\Student\Net项目\github项目\webapi项目\WebApi_Offcial\ConfigFiles\GenerateQuicklyTemplate\InputTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(typeName));
            
            #line default
            #line hidden
            this.Write(" ");
            
            #line 131 "D:\Student\Net项目\github项目\webapi项目\WebApi_Offcial\ConfigFiles\GenerateQuicklyTemplate\InputTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(propertyName));
            
            #line default
            #line hidden
            this.Write(" { get; set; }");
            
            #line 131 "D:\Student\Net项目\github项目\webapi项目\WebApi_Offcial\ConfigFiles\GenerateQuicklyTemplate\InputTemplate.tt"
 
        if(typeName == "bool"){ 
            
            #line default
            #line hidden
            this.Write(" = false;");
            
            #line 132 "D:\Student\Net项目\github项目\webapi项目\WebApi_Offcial\ConfigFiles\GenerateQuicklyTemplate\InputTemplate.tt"
 }
        else if(typeName == "string" || typeName == "char"){ 
            
            #line default
            #line hidden
            this.Write(" = String.Empty;");
            
            #line 133 "D:\Student\Net项目\github项目\webapi项目\WebApi_Offcial\ConfigFiles\GenerateQuicklyTemplate\InputTemplate.tt"
 }
        if(typeName == "short" || typeName == "int" || typeName == "long"){ 
            
            #line default
            #line hidden
            this.Write(" = 0;");
            
            #line 134 "D:\Student\Net项目\github项目\webapi项目\WebApi_Offcial\ConfigFiles\GenerateQuicklyTemplate\InputTemplate.tt"
 }
            
            #line default
            #line hidden
            this.Write("\r\n        ");
            
            #line 136 "D:\Student\Net项目\github项目\webapi项目\WebApi_Offcial\ConfigFiles\GenerateQuicklyTemplate\InputTemplate.tt"

            
            
            
            #line default
            #line hidden
            
            #line 138 "D:\Student\Net项目\github项目\webapi项目\WebApi_Offcial\ConfigFiles\GenerateQuicklyTemplate\InputTemplate.tt"


}
    
            
            #line default
            #line hidden
            this.Write("}\r\n                  ");
            
            #line 143 "D:\Student\Net项目\github项目\webapi项目\WebApi_Offcial\ConfigFiles\GenerateQuicklyTemplate\InputTemplate.tt"
  break; 
                case "Update":
            
            #line default
            #line hidden
            this.Write("\r\n    public class ");
            
            #line 146 "D:\Student\Net项目\github项目\webapi项目\WebApi_Offcial\ConfigFiles\GenerateQuicklyTemplate\InputTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(this.data.InputType));
            
            #line default
            #line hidden
            
            #line 146 "D:\Student\Net项目\github项目\webapi项目\WebApi_Offcial\ConfigFiles\GenerateQuicklyTemplate\InputTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(this.data.TableName.Split("_")[1]));
            
            #line default
            #line hidden
            this.Write("Input :  Add");
            
            #line 146 "D:\Student\Net项目\github项目\webapi项目\WebApi_Offcial\ConfigFiles\GenerateQuicklyTemplate\InputTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(this.data.TableName.Split("_")[1]));
            
            #line default
            #line hidden
            this.Write("Input\r\n    {\r\n        /// <summary>\r\n        /// Id\r\n        /// </summary>\r\n    " +
                    "    [Required(ErrorMessage = \"IdRequried\")]\r\n        public long Id { get; set; " +
                    "}\r\n    }\r\n                  ");
            
            #line 154 "D:\Student\Net项目\github项目\webapi项目\WebApi_Offcial\ConfigFiles\GenerateQuicklyTemplate\InputTemplate.tt"
  break; 
                default:
                   break; 
            
            #line default
            #line hidden
            this.Write("          \r\n       ");
            
            #line 157 "D:\Student\Net项目\github项目\webapi项目\WebApi_Offcial\ConfigFiles\GenerateQuicklyTemplate\InputTemplate.tt"
       
     } 
            
            #line default
            #line hidden
            this.Write(" \r\n}");
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
    public class InputTemplateBase
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
