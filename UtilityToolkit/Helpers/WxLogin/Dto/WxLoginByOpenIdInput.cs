using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UtilityToolkit.Helpers.WxLogin.Dto
{
    /// <summary>
    /// 微信登录输入类
    /// </summary>
    public class WxLoginByOpenIdInput
    {
        /// <summary>
        /// 电话号码
        /// </summary>
        public string Phone {  get; set; }   
        
        /// <summary>
        /// code
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 偏移码
        /// </summary>
        public string Iv { get; set; }
    }
}
