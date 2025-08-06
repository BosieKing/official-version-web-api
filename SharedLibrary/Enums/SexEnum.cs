using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLibrary.Enums
{
    /// <summary>
    /// 性别枚举
    /// </summary>
    public enum SexEnum
    {
        /// <summary>
        /// 未选择
        /// </summary>
        [Description("未选择")]
        UNKnown = 0,

        /// <summary>
        /// 男
        /// </summary>
        [Description("男")]
        Boy = 1,

        /// <summary>
        /// 女
        /// </summary>
        [Description("女")]
        Girl = 2,

      
    }
}
