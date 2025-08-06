using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLibrary.Enums
{
    /// <summary>
    /// 用户身份类型
    /// </summary>
    public enum UserIdentityTypeEnum
    {
        /// <summary>
        /// 学生
        /// </summary>
        [Description("学生")]
        Student = 1,

        /// <summary>
        /// 老师
        /// </summary>
        [Description("老师")]
        Teacher = 2,
    }
}
